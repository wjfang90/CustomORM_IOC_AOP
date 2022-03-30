using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Custom.Framework.CustomAOP.CastleTest.InterfaceInject
{


    public class AOPInterfaceInjectWithMultitudeAttribureByAction
    {
        public static void AOPTest()
        {
            //1.引用 Castle Core
            //2.创建动态代理
            ProxyGenerator proxyGenerator = new ProxyGenerator();
            //3.创建拦截器
            TestMultitudeAttribureInterceptorByAction testAttribureInterceptor = new TestMultitudeAttribureInterceptorByAction();

            //4.接口注入,CommonClassInterfaceInjectWithAttribure 类中 实现接口的并标记特性的方法都实现了拦截
            var obj = new CommonClassInterfaceInjectWithMultitudeAttribureByAction();
            var commonClassWithAttribure = proxyGenerator.CreateInterfaceProxyWithTarget(typeof(IAOPInterfaceInjectWithMultitudeAttribureByAction), obj, testAttribureInterceptor) as IAOPInterfaceInjectWithMultitudeAttribureByAction;
            commonClassWithAttribure.Test();//未标记特性，没有拦截方法
            commonClassWithAttribure.Show();//标记了特性，拦截了
        }
    }

    public class CommonClassInterfaceInjectWithMultitudeAttribureByAction : IAOPInterfaceInjectWithMultitudeAttribureByAction
    {
        public void Show()
        {
            Console.WriteLine($"这是实现接口的并且标记特性的 {nameof(Show)} 方法，接口注入，实现方法拦截");
        }

        public void Test()
        {
            Console.WriteLine($"这是实现接口的 {nameof(Test)} 方法，未标记特性，没有实现方法拦截");
        }
    }

    public class TestMultitudeAttribureInterceptorByAction : StandardInterceptor
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
            if (invocation.Method.IsDefined(typeof(AOPBaseInterfaceInjectWithMultitudeAttributeByAction), true))
            {
                var attributes = invocation.Method.GetCustomAttributes<AOPBaseInterfaceInjectWithMultitudeAttributeByAction>().OrderBy(t => t.Sort);
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

    public interface IAOPInterfaceInjectWithMultitudeAttribureByAction
    {
        [AOPLoggerInterfaceInjectWithMultitude(2)]//排序越小，离show方法越近（内层），排序越大，离show方法越远（外层）
        [AOPMonitorInterfaceInjectWithMultitude(1)]
        [AOPExceptionHandleInterfaceInjectWithMultitude(3)]
        void Show();
        void Test();
    }
}
