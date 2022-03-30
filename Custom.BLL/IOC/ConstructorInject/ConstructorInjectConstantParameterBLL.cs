using Custom.Framework.CustomContainer;
using Custom.IBLL.IOC;
using Custom.IDAL;
using Custom.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Custom.BLL.IOC
{
    public class ConstructorInjectConstantParameterBLL : IConstructorInjectConstantParameterBLL
    {
        public ConstructorInjectConstantParameterBLL(string test, int a)
        {
            Console.WriteLine($"{nameof(ConstructorInjectMultitudeParameterBLL)}({typeof(string).FullName},{typeof(int).FullName}) 值类型构造函数调用");
        }

        public ConstructorInjectConstantParameterBLL(IConstructorInjectNoParameterBLL noParameterBLL, IUserDAL userDAL)
        {
            Console.WriteLine($"{nameof(ConstructorInjectMultitudeParameterBLL)}({nameof(IConstructorInjectNoParameterBLL)}, {nameof(IUserDAL)})构造函数调用");
        }

        public ConstructorInjectConstantParameterBLL(IUserDAL userDAL, [CustomConstructorConstantPapameterInject] string username, [CustomConstructorConstantPapameterInject] int age, IConstructorInjectNoParameterBLL noParameterBLL)
        {
            Console.WriteLine($"{nameof(ConstructorInjectConstantParameterBLL)} ({nameof(IConstructorInjectNoParameterBLL)},{typeof(string)}={username},{typeof(int)}={age},{nameof(IConstructorInjectNoParameterBLL)}) 构造函数调用");
        }
        public void Show()
        {
            Console.WriteLine($"{nameof(ConstructorInjectMultitudeParameterBLL)}.{nameof(ConstructorInjectMultitudeParameterBLL.Show)}");
        }
    }
}
