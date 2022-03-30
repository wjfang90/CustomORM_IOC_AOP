using Custom.Framework.CustomContainer;
using Custom.IBLL.IOC;
using Custom.IDAL;
using Custom.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Custom.BLL.IOC
{
    public class AilasInjectWithSqlServerBLL : IAilasInjectWithNameBLL
    {

        public AilasInjectWithSqlServerBLL(IConstructorInjectNoParameterBLL noParameterBLL)
        {
            Console.WriteLine($"{nameof(AilasInjectWithSqlServerBLL)} ({nameof(IAilasInjectWithNameBLL)}) 构造函数调用,别名注入");
        }

        public void Show()
        {
            Console.WriteLine($"{nameof(AilasInjectWithSqlServerBLL)}.{nameof(AilasInjectWithSqlServerBLL.Show)}");
        }
    }
}
