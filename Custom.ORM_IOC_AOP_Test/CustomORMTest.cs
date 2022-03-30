using Custom.DAL;
using Custom.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Custom.ORM_IOC_AOP_Test
{
    public static class CustomORMTest
    {
        public static void queryOne()
        {
            //query one
            var company = SqlHelper.Find<CompanyModel>(1);//model与数据表不同名
            var user = SqlHelper.Find<UserModel>(1);//model中属性与数据字段不同名

            var company1 = SqlHelper.Find<CompanyModel>(5);//数据某字段空值优化
            var company6 = SqlHelper.Find<CompanyModel>(6);//数据空值优化
        }

        public static void Insert()
        {
            //insert
            var companyInsert = SqlHelper.Find<CompanyModel>(1);//model与数据表不同名
            companyInsert.Name = "fang company";
            companyInsert.CreateTime = DateTime.Now;
            companyInsert.LastModifierId = null;//数据空值优化

            var resInert = SqlHelper.Insert(companyInsert);
            Console.WriteLine($"insert {resInert}");
        }

        public static void Update()
        {
            //update
            var companyUpdate = SqlHelper.Find<CompanyModel>(4);//model与数据表不同名
            companyUpdate.Name += "fang company";
            companyUpdate.LastModifyTime = DateTime.Now;
            companyUpdate.LastModifierId = 0;

            var resutUpdate = SqlHelper.Update(companyUpdate);
            Console.WriteLine($"update {resutUpdate}");
        }

        public static void Delete()
        {
            //delete 
            var companyDelete = SqlHelper.Find<CompanyModel>(8);//model与数据表不同名
            var resutDelete = SqlHelper.Delete(companyDelete);
            Console.WriteLine($"delete {resutDelete}");
        }

        public static void ValidateData()
        {
            //validate
            var userInsert = SqlHelper.Find<UserModel>(1);//model与数据表不同名
            //userInsert.Name = null;// 必填验证 失败
            userInsert.Name = "fang";
            //userInsert.Account = "fang";// 长度验证5-20 失败
            userInsert.Account = "fangrain";
            //userInsert.Status = 0;//数值包含特定值验证 失败
            userInsert.Status = 1;
            //userInsert.UserType = 7;//数据范围验证 >=0 && <6 失败
            userInsert.UserType = 1;
            //userInsert.Email = "fang@.";//email格式验证 失败
            userInsert.Email = "fang@qq.com";

            //userInsert.Mobile = "12012345678";  //手机号验证 失败
            userInsert.Mobile = "13712345678";

            var resInert = SqlHelper.Insert(userInsert);
            Console.WriteLine($"insert validate {resInert}");


            userInsert = SqlHelper.Find<UserModel>(2);//model与数据表不同名
            userInsert.Name = "毛毛雨";
            userInsert.Account = "fangrain";
            userInsert.Status = 1;
            userInsert.Email = null;
            userInsert.CreateTime = DateTime.Now;
            userInsert.LastModifyTime = DateTime.Now;
            var resutUpdate = SqlHelper.Update(userInsert);
            Console.WriteLine($"update validate {resutUpdate}");

        }

        public static void QueryListWhere()
        {
            //同属性多条件，支持自定义别名 ok
            //Expression<Func<UserModel, bool>> where = t => (t.Account.StartsWith("fang") || t.Account.EndsWith("n")) 
            //                                                && (t.Status == 1 || t.Status == 0)
            //                                                && t.CreateTime > DateTime.Parse("2022.1.1")
            //                                                && t.Account.Length >= 6;

            //字符串类型长度 string.Length  ok
            //Expression<Func<UserModel, bool>> where = t => t.Account.Length >= 6;            

            //日期类型值 转换 DateTime.Parse  ok
            //Expression<Func<UserModel, bool>> where = t => t.CreateTime > DateTime.Parse("2022.1.1");

            //日期类型值 构造函数  ok
            //Expression<Func<UserModel, bool>> where = t => t.CreateTime < new DateTime(2022,3,1);

            // 日期类型值 静态属性  ok
            //Expression<Func<UserModel, bool>> where = t => t.CreateTime < DateTime.Now;

            //error  not support
            //Expression<Func<UserModel, bool>> where = t => t.Account.Substring(0, 4) == "fang";

            Expression<Func<UserModel, bool>> where = t => t.Account.Contains("fang");


            //new List<UserModel>().Where(where.Compile());

            //ExpressionToSqlVisitor visitor = new ExpressionToSqlVisitor();
            //visitor.Visit(where);
            //visitor.GetWhere();
            //visitor.GetParameters();

            var result = SqlHelper.Find<UserModel>(where);

        }
    }
}
