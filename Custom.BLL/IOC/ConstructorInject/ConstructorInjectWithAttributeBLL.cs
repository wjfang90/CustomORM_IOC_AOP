using Custom.Framework.CustomContainer;
using Custom.IBLL.IOC;
using Custom.IDAL;
using Custom.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Custom.BLL.IOC
{
    public class ConstructorInjectWithAttributeBLL : IConstructorInjectWithAttributeBLL
    {
        public ConstructorInjectWithAttributeBLL()
        {
            Console.WriteLine($"{nameof(ConstructorInjectWithAttributeBLL)} 无参数构造函数调用");
        }
        public ConstructorInjectWithAttributeBLL(string test, int a)
        {
            Console.WriteLine($"{nameof(ConstructorInjectWithAttributeBLL)}({typeof(string).FullName},{typeof(int).FullName}) 值类型构造函数调用");
        }

        public ConstructorInjectWithAttributeBLL(IConstructorInjectNoParameterBLL noParameterBLL)
        {
            Console.WriteLine($"{nameof(ConstructorInjectWithAttributeBLL)} ({nameof(IConstructorInjectNoParameterBLL)}) 构造函数调用");
        }

        [CustomConstructorInject]
        public ConstructorInjectWithAttributeBLL(IConstructorInjectNoParameterBLL noParameterBLL, IUserDAL userDAL)
        {
            Console.WriteLine($"{nameof(ConstructorInjectWithAttributeBLL)}({nameof(IConstructorInjectNoParameterBLL)}, {nameof(IUserDAL)})构造函数调用");
        }
        public ConstructorInjectWithAttributeBLL(ICompanyDAL companyDAL, IUserDAL userDAL)
        {
            Console.WriteLine($"{nameof(ConstructorInjectWithAttributeBLL)}({nameof(ICompanyDAL)}, {nameof(IUserDAL)})构造函数调用");
        }

        public void Show()
        {
            Console.WriteLine($"{nameof(ConstructorInjectWithAttributeBLL)}.{nameof(ConstructorInjectWithAttributeBLL.Show)}");
        }
    }
}
