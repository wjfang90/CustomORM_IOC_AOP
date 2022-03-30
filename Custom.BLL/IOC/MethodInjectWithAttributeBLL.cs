using Custom.Framework.CustomContainer;
using Custom.IBLL.IOC;
using Custom.IDAL;
using Custom.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Custom.BLL.IOC
{
    public class MethodInjectWithAttributeBLL : IMethodInjectWithAttributeBLL
    {
        private IConstructorInjectNoParameterBLL ConstructorNoParameter;

        private IUserDAL UserDAL;

        [CustomMethodInject]
        public void MethodInject(IConstructorInjectNoParameterBLL noParameterBLL, IUserDAL userDAL)
        {
            this.ConstructorNoParameter = noParameterBLL;
            this.UserDAL = userDAL;
        }

        public void Show()
        {
            Console.WriteLine($"{nameof(MethodInjectWithAttributeBLL)}.{nameof(MethodInjectWithAttributeBLL.Show)}");
            Console.WriteLine($"{nameof(IConstructorInjectNoParameterBLL)} = {ConstructorNoParameter != null} 方法注入");
            Console.WriteLine($"{nameof(IUserDAL)} = {UserDAL != null} 方法注入");
        }
    }
}
