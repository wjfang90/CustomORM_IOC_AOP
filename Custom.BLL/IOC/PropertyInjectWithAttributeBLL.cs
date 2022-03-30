using Custom.Framework.CustomContainer;
using Custom.IBLL.IOC;
using Custom.IDAL;
using Custom.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Custom.BLL.IOC
{
    public class PropertyInjectWithAttributeBLL : IPropertyInjectWithAttributeBLL
    {
        [CustomPropertyInject]
        public IConstructorInjectNoParameterBLL ConstructorNoParameter { get; set; }

        [CustomPropertyInject]
        public IUserDAL UserDAL { get; set; }

        public void Show()
        {
            Console.WriteLine($"{nameof(PropertyInjectWithAttributeBLL)}.{nameof(PropertyInjectWithAttributeBLL.Show)}");
            Console.WriteLine($"{nameof(IConstructorInjectNoParameterBLL)} = {ConstructorNoParameter != null} 属性注入");
            Console.WriteLine($"{nameof(IUserDAL)} = {UserDAL != null} 属性注入");
        }
    }
}
