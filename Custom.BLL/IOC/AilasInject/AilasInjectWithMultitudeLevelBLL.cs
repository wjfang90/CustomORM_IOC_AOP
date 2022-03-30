using Custom.Framework.CustomContainer;
using Custom.IBLL.IOC;
using Custom.IDAL;
using Custom.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Custom.BLL.IOC
{
    public class AilasInjectWithMultitudeLevelBLL : IAilasInjectWithMultitudeLevelBLL
    {
        private IAilasInjectWithNameBLL ailasInjectWithNameMySqlBLL { get; set; }

        [CustomPropertyInject]
        [CustomAilasInject("mysql")]
        public IAilasInjectWithNameBLL ailasInjectWithNameMySqlPropertyBLL { get; set; }

        public AilasInjectWithMultitudeLevelBLL([CustomAilasInject("mysql")]IAilasInjectWithNameBLL  ailasInjectWithMysql)
        {
            ailasInjectWithNameMySqlBLL = ailasInjectWithMysql;

            Console.WriteLine($"{nameof(AilasInjectWithMultitudeLevelBLL)} ({nameof(IAilasInjectWithMultitudeLevelBLL)}) 构造函数调用,别名注入");
        }

        public void Show()
        {
            Console.WriteLine($"{nameof(AilasInjectWithMultitudeLevelBLL)}.{nameof(AilasInjectWithMultitudeLevelBLL.Show)}");
            Console.WriteLine($"{nameof(AilasInjectWithMultitudeLevelBLL)}.{nameof(AilasInjectWithMultitudeLevelBLL.ailasInjectWithNameMySqlBLL)} = {ailasInjectWithNameMySqlBLL != null} 别名注入-构造函数参数别名");
            Console.WriteLine($"{nameof(AilasInjectWithMultitudeLevelBLL)}.{nameof(AilasInjectWithMultitudeLevelBLL.ailasInjectWithNameMySqlPropertyBLL)} = {ailasInjectWithNameMySqlPropertyBLL != null} 别名注入-属性别名");
        }
    }
}
