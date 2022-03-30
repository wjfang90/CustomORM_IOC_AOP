using Custom.DAL.ExpressionToSql;
using Custom.Framework.Mapping;
using Custom.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Custom.DAL.ExpressionToSql
{
    /// <summary>
    /// 目标是解析表达式目录树 成sql
    /// </summary>
    public class ExpressionToSqlVisitor<T> : ExpressionVisitor where T : BaseModel, new()
    {
        // c => c.Id > 10 && c.Name.Contains("fang"); 转换成 Id>10 and Name like '%fang%'

        private Stack<string> conditionStack = new Stack<string>();

        private List<SqlParameter> parameters = new List<SqlParameter>();

        private int index;

        public string GetWhere()
        {
            var arrWhere = this.conditionStack.ToArray();
            //this.conditionStack.Clear();

            //数据库字段与类属性名不同时，映射数据库字段名
            var properties = typeof(T).GetProperties().Where(t => t.IsDefined(typeof(CustomBaseMappingAttribute), true));
            foreach (var prop in properties)
            {
                var index = Array.IndexOf(arrWhere, prop.Name);
                while (index != -1)
                {
                    arrWhere[index] = prop.GetMappingName();
                    index = Array.IndexOf(arrWhere, prop.Name);
                }
            }

            return string.Concat(arrWhere);
        }


        public IEnumerable<SqlParameter> GetParameters()
        {
            return this.parameters;
        }


        /// <summary>
        /// Visit入口--表达式目录树传递进去--解析判断类型然后调用对应的visit方法--方法里面继续visit--判断类型再去调用对应的visit方法---就是一个递归式的解析---二叉树深度无限必须得递归的
        /// 解析表达式目录树是从右到左解析的
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public override Expression Visit(Expression node)
        {
            Console.WriteLine($"Visit入口：{node.NodeType} {node.Type} {node.ToString()}");
            return base.Visit(node);
        }

        //protected override Expression VisitBinary(BinaryExpression node)
        //{
        //    Console.WriteLine($"VisitBinary：{node.NodeType} {node.Type} {node.ToString()}");
        //    // 解析表达式目录树是从右到左解析的
        //    this.conditionStack.Push(")");
        //    base.Visit(node.Right);
        //    this.conditionStack.Push(node.NodeType.ToSqlOperator());
        //    base.Visit(node.Left);
        //    this.conditionStack.Push("(");
        //    return node;
        //}

        protected override Expression VisitBinary(BinaryExpression node)
        {
            Console.WriteLine($"VisitBinary：{node.NodeType} {node.Type} {node.ToString()}");
            // 解析表达式目录树是从右到左解析的
            this.conditionStack.Push(")");

            SqlParameter parameter = null;

            #region  条件中的参数值

            if (node.Right is ConstantExpression)
            {
                parameter = new SqlParameter();
                parameter.Value = ((ConstantExpression)node.Right).Value?.ToString();
            }
            else if (node.Right is MethodCallExpression && (node.Right as MethodCallExpression).Object == null)
            {
                //通过函数调用初始化值 ，如CreateTime > DateTime.Parse("2022.1.1")
                var rightNode = node.Right as MethodCallExpression;
                var args = rightNode.Arguments.Select(t => ((ConstantExpression)t).Value).ToArray();
                var methodCallValue = rightNode.Method.Invoke(null, args);
                parameter = new SqlParameter();
                parameter.Value = methodCallValue;
            }
            else if (node.Right is NewExpression)
            {
                //通过构造函数初始化值，如 CreateTime< new DateTime(2022,1,1)
                var rightNode = node.Right as NewExpression;
                var args = rightNode.Arguments.Select(t => ((ConstantExpression)t).Value).ToArray();
                var contructorValue = rightNode.Constructor.Invoke(args);
                parameter = new SqlParameter();
                parameter.Value = contructorValue;
            }else if(node.Right is MemberExpression)
            {
                //通过属性类型的静态属性值初始化，如DateTime.Now
                var propName = (node.Right as MemberExpression).Member.Name;
                switch (propName)
                {
                    case "Empty":
                        parameter = new SqlParameter();
                        parameter.Value = string.Empty;
                        break;
                    case "Now":
                        parameter = new SqlParameter();
                        parameter.Value = DateTime.Now;
                        break;
                    case "Today":
                        parameter = new SqlParameter();
                        parameter.Value = DateTime.Today;
                        break;
                    case "UtcNow":
                        parameter = new SqlParameter();
                        parameter.Value = DateTime.UtcNow;
                        break;
                    default:
                            throw new NotSupportedException($"VisitMember node.Member.Name={propName} is not supported");
                }
            }
            else
            {
                base.Visit(node.Right);
            }

            #endregion



            #region 条件中的 参数名
            if (node.Left is MemberExpression)
            {
                var propNames = typeof(T).GetProperties().Select(t => t.Name);
                var leftNode = node.Left as MemberExpression;
                if (propNames.Contains(leftNode.Member.Name))
                {
                    var name = leftNode.Member.Name;
                    var paramName = $"@{name}{index++}";

                    this.conditionStack.Push(paramName);
                    this.conditionStack.Push(node.NodeType.ToSqlOperator());
                    this.conditionStack.Push(name);

                    if (parameter != null)
                        parameter.ParameterName = paramName;

                }
                else if (leftNode.Expression is MemberExpression && propNames.Contains((leftNode.Expression as MemberExpression).Member.Name))
                {
                    // Account.Length==6 
                    var name = (leftNode.Expression as MemberExpression).Member.Name;
                    var paramName = $"@{name}{index++}";

                    this.conditionStack.Push(paramName);
                    this.conditionStack.Push(node.NodeType.ToSqlOperator());

                    this.conditionStack.Push(")");
                    this.conditionStack.Push(name);
                    this.conditionStack.Push("(");

                    switch (leftNode.Member.Name)
                    {
                        case "Length"://类属性的Length属性转换为 SqlServer  内置函数len
                            this.conditionStack.Push("LEN");
                            break;
                        default:
                            throw new NotSupportedException($"VisitMember node.Member.Name={leftNode.Member.Name} is not supported");
                    }

                    if (parameter != null)
                        parameter.ParameterName = paramName;
                }
                else
                {
                    base.Visit(node.Left);
                }

                
            }
            else
            {
                this.conditionStack.Push(node.NodeType.ToSqlOperator());
                base.Visit(node.Left);
            }
            this.conditionStack.Push("(");

            #endregion 

            if (parameter != null)
                parameters.Add(parameter);

            return node;
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            Console.WriteLine($"VisitConstant：{node.NodeType} {node.Type} {node.ToString()}");
            //this.conditionStack.Push($"'{node.Value?.ToString()}'");
            this.conditionStack.Push(node.Value?.ToString());
            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            Console.WriteLine($"VisitMember：{node.NodeType} {node.Type} {node.ToString()}");
            this.conditionStack.Push(node.Member.Name);
            return node;
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node == null) throw new ArgumentNullException("MethodCallExpression");

            if (node.Object != null)
            {
                this.Visit(node.Object);
                foreach (var item in node.Arguments)
                {
                    this.Visit(item);
                }
            }

            string value = this.conditionStack.Pop();
            string name = this.conditionStack.Pop();
            var paramName = $"@{name}{index++}";

            this.conditionStack.Push(")");
            switch (node.Method.Name)
            {
                case "StartsWith":
                    value = $"{value}%";
                    break;
                case "Contains":
                    value = $"{value}%";
                    break;
                case "EndsWith":
                    value = $"{value}%";
                    break;
                default:
                    throw new NotSupportedException(node.NodeType + " is not supported!");
            }
            this.conditionStack.Push(paramName);
            this.conditionStack.Push(" like ");
            this.conditionStack.Push(name);
            this.conditionStack.Push("(");

            var parameter = new SqlParameter(paramName, value);
            parameters.Add(parameter);

            return node;
        }

        //直接拼sql,没有参数化
        //protected override Expression VisitMethodCall(MethodCallExpression node)
        //{
        //    if (node == null) throw new ArgumentNullException("MethodCallExpression");

        //    var format = string.Empty;
        //    switch (node.Method.Name)
        //    {
        //        case "StartsWith":
        //            format = "({0} like '{1}%')";
        //            break;
        //        case "Contains":
        //            format = "({0} like '%{1}%')";
        //            break;
        //        case "EndsWith":
        //            format = "({0} like '%{1}')";
        //            break;
        //        default:
        //            throw new NotSupportedException(node.NodeType + " is not supported!");
        //    }
        //    this.Visit(node.Object);
        //    this.Visit(node.Arguments.FirstOrDefault());

        //    string right = this.conditionStack.Pop();
        //    string left = this.conditionStack.Pop();
        //    this.conditionStack.Push(string.Format(format, left, right));

        //    return node;
        //}

        protected override Expression VisitNew(NewExpression node)
        {
            return base.VisitNew(node);
        }

    }
}
