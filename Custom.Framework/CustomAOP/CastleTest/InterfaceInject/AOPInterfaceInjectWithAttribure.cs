using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Custom.Framework.CustomAOP.CastleTest.InterfaceInject
{


    public class AOPInterfaceInjectWithAttribure
    {
        public static void AOPTest()
        {
            //1.引用 Castle Core
            //2.创建动态代理
            ProxyGenerator proxyGenerator = new ProxyGenerator();
            //3.创建拦截器
            TestAttribureInterceptor testAttribureInterceptor = new TestAttribureInterceptor();

            //4.接口注入,CommonClassInterfaceInjectWithAttribure 类中 实现接口的并标记特性的方法都实现了拦截
            var obj = new CommonClassInterfaceInjectWithAttribure();
            var commonClassWithAttribure = proxyGenerator.CreateInterfaceProxyWithTarget(typeof(IAOPInterfaceInjectWithAttribure), obj, testAttribureInterceptor) as IAOPInterfaceInjectWithAttribure;
            commonClassWithAttribure.Test();//未标记特性，没有拦截方法
            commonClassWithAttribure.Show();//标记了特性，拦截了
        }
    }

    public class CommonClassInterfaceInjectWithAttribure : IAOPInterfaceInjectWithAttribure
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

    public class TestAttribureInterceptor : StandardInterceptor
    {
        protected override void PreProceed(IInvocation invocation)
        {
            //base.PreProceed(invocation);
        }

        protected override void PerformProceed(IInvocation invocation)
        {

            base.PerformProceed(invocation);//这是拦截方法的执行

            //拦截器Interceptor 中的业务逻辑转移到特性LogAttribure中
            if (invocation.Method.IsDefined(typeof(LogAttribure), true))
            {
                var attribute = invocation.Method.GetCustomAttribute<LogAttribure>();
                attribute.Log();
            }
        }

        protected override void PostProceed(IInvocation invocation)
        {
            //base.PostProceed(invocation);
        }
    }

    public interface IAOPInterfaceInjectWithAttribure
    {
        [LogAttribure]
        void Show();
        void Test();
    }



    /// <summary>
    /// 目的是把 拦截器Interceptor 中的业务逻辑转移到特性LogAttribure中
    /// </summary>
    public class LogAttribure : Attribute
    {
        public void Log()
        {
            Console.WriteLine($"This is {nameof(LogAttribure)}.{nameof(Log)}");
        }
    }


}
