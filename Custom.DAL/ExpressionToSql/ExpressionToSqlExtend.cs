using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Custom.Framework.Mapping;
using Custom.Model;

namespace Custom.DAL.ExpressionToSql
{
    public class ExpressionToSqlExtend<T> where T : BaseModel, new()
    {
        private ExpressionToSqlVisitor<T> visitor = new ExpressionToSqlVisitor<T>();

        public void Init(Expression<Func<T, bool>> expression)
        {
            visitor.Visit(expression);
        }
        public string GetWhere()
        {
            var condition = visitor.GetWhere();
            return string.IsNullOrEmpty(condition) ? null : $" where {condition}";
        }

        public IEnumerable<SqlParameter> GetDbParameter()
        {
            return visitor.GetParameters();
        }
    }
}
