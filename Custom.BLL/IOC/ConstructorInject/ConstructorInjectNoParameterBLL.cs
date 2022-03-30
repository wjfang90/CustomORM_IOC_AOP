using Custom.IBLL.IOC;
using Custom.IDAL;
using Custom.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Custom.BLL.IOC
{
    public class ConstructorInjectNoParameterBLL : IConstructorInjectNoParameterBLL
    {
        public ConstructorInjectNoParameterBLL()
        {
            //Console.WriteLine($"{nameof(ConstructorInjectNoParameterBLL)} 无参数构造函数调用");
        }
        public void Show()
        {
            Console.WriteLine($"{nameof(ConstructorInjectNoParameterBLL)}.{nameof(ConstructorInjectNoParameterBLL.Show)}");
        }
    }
}
