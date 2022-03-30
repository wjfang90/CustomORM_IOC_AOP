using Custom.IBLL.IOC;
using Custom.IDAL;
using Custom.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Custom.BLL.IOC
{
    public class ConstructorInjectMultitudeParameterBLL : IConstructorInjectMultitudeParameterBLL
    {
        public ConstructorInjectMultitudeParameterBLL()
        {
            Console.WriteLine($"{nameof(ConstructorInjectMultitudeParameterBLL)} 无参数构造函数调用");
        }

        public ConstructorInjectMultitudeParameterBLL(string test, int a)
        {
            Console.WriteLine($"{nameof(ConstructorInjectMultitudeParameterBLL)}({typeof(string).FullName},{typeof(int).FullName}) 值类型构造函数调用");
        }

        public ConstructorInjectMultitudeParameterBLL(IConstructorInjectNoParameterBLL noParameterBLL)
        {
            Console.WriteLine($"{nameof(ConstructorInjectMultitudeParameterBLL)} ({nameof(IConstructorInjectNoParameterBLL)}) 构造函数调用");
        }

        public ConstructorInjectMultitudeParameterBLL(IConstructorInjectNoParameterBLL noParameterBLL, IUserDAL userDAL)
        {
            Console.WriteLine($"{nameof(ConstructorInjectMultitudeParameterBLL)}({nameof(IConstructorInjectNoParameterBLL)}, {nameof(IUserDAL)})构造函数调用");
        }
        public ConstructorInjectMultitudeParameterBLL(ICompanyDAL companyDAL, IUserDAL userDAL)
        {
            Console.WriteLine($"{nameof(ConstructorInjectMultitudeParameterBLL)}({nameof(ICompanyDAL)}, {nameof(IUserDAL)})构造函数调用");
        }

        public ConstructorInjectMultitudeParameterBLL(IConstructorInjectNoParameterBLL noParameterBLL, ICompanyDAL companyDAL, IUserDAL userDAL)
        {
            Console.WriteLine($"{nameof(ConstructorInjectMultitudeParameterBLL)}({nameof(IConstructorInjectNoParameterBLL)},{nameof(ICompanyDAL)} , {nameof(IUserDAL)})构造函数调用");
        }
        public void Show()
        {
            Console.WriteLine($"{nameof(ConstructorInjectMultitudeParameterBLL)}.{nameof(ConstructorInjectMultitudeParameterBLL.Show)}");
        }
    }
}
