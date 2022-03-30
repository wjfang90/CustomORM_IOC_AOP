using System;
using System.Collections.Generic;
using System.Text;

namespace Custom.Framework.CustomAOP.Attributes
{
    public class CustomExceptionAttribute : CustomBaseAttribute
    {
        private int sort;
        public CustomExceptionAttribute(int sort = 0)
        {
            base.Sort = sort;
        }
        public override Action Handle(Action action)
        {
            return () =>
            {
                Console.WriteLine($"This is {nameof(CustomExceptionAttribute)}.{nameof(Handle)} start");
                action.Invoke();//执行真实的方法            
                Console.WriteLine($"This is {nameof(CustomExceptionAttribute)}.{nameof(Handle)} end");
            };
        }
    }
}
