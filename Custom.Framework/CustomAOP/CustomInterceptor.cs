using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;

namespace Custom.Framework.CustomAOP
{
    public class CustomInterceptor:StandardInterceptor
    {
        protected override void PreProceed(IInvocation invocation)
        {
            //base.PreProceed(invocation);
        }

        protected override void PerformProceed(IInvocation invocation)
        {
            //真实的方法灵活的添加到特性中特定的位置，组合方法或配置管道(用委托)
            Action action = () => base.PerformProceed(invocation);

            //拦截器Interceptor 中的业务逻辑转移到特性LogAttribure中
            if (invocation.Method.IsDefined(typeof(CustomBaseAttribute), true))
            {
                var attributes = invocation.Method.GetCustomAttributes<CustomBaseAttribute>().OrderBy(t => t.Sort);
                foreach (var attribute in attributes)
                {
                    action = attribute.Handle(action);
                }
            }

            action.Invoke();//这是拦截方法的执行
        }

        protected override void PostProceed(IInvocation invocation)
        {
            //base.PostProceed(invocation);
        }
    }
}
