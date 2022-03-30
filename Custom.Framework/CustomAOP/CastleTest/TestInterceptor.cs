using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custom.Framework.CustomAOP.CastleTest
{
    public class TestInterceptor : StandardInterceptor
    {
        protected override void PreProceed(IInvocation invocation)
        {
            Console.WriteLine($"This  execure before {invocation.Method.Name} in PreProceed start");
            base.PreProceed(invocation);
            Console.WriteLine($"This  execure before {invocation.Method.Name} in PreProceed end");
        }

        protected override void PerformProceed(IInvocation invocation)
        {
            Console.WriteLine($"This  execure  in {invocation.Method.Name} in PerformProceed start");
            base.PerformProceed(invocation);//这是拦截方法的执行
            Console.WriteLine($"This  execure  in {invocation.Method.Name} in PerformProceed end");
        }

        protected override void PostProceed(IInvocation invocation)
        {
            Console.WriteLine($"This  execure after {invocation.Method.Name} PostProceed start");
            base.PostProceed(invocation);
            Console.WriteLine($"This  execure after {invocation.Method.Name} PostProceed end");
        }
    }
}
