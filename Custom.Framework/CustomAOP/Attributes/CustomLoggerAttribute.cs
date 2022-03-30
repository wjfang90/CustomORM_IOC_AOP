using System;
using System.Collections.Generic;
using System.Text;

namespace Custom.Framework.CustomAOP.Attributes
{
    public class CustomLoggerAttribute : CustomBaseAttribute
    {
        private int sort;
        public CustomLoggerAttribute(int sort = 0)
        {
            base.Sort = sort;
        }
        public override Action Handle(Action action)
        {
            return () =>
            {
                Console.WriteLine($"This is {nameof(CustomLoggerAttribute)}.{nameof(Handle)} start");
                action.Invoke();//执行真实的方法            
                Log();
                Console.WriteLine($"This is {nameof(CustomLoggerAttribute)}.{nameof(Handle)} end");
            };
        }
        public void Log()
        {
            Console.WriteLine($"This is {nameof(CustomLoggerAttribute)}.{nameof(Log)}");
        }
    }
}
