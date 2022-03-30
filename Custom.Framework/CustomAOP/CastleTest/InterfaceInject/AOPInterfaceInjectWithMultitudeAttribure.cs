using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Custom.Framework.CustomAOP.CastleTest.InterfaceInject
{


    public class AOPInterfaceInjectWithMultitudeAttribure
    {
        public static void AOPTest()
        {
            //1.引用 Castle Core
            //2.创建动态代理
            ProxyGenerator proxyGenerator = new ProxyGenerator();
            //3.创建拦截器
            TestMultitudeAttribureInterceptor testAttribureInterceptor = new TestMultitudeAttribureInterceptor();

            //4.接口注入,CommonClassInterfaceInjectWithAttribure 类中 实现接口的并标记特性的方法都实现了拦截
            var obj = new CommonClassInterfaceInjectWithMultitudeAttribure();
            var commonClassWithAttribure = proxyGenerator.CreateInterfaceProxyWithTarget(typeof(IAOPInterfaceInjectWithMultitudeAttribure), obj, testAttribureInterceptor) as IAOPInterfaceInjectWithMultitudeAttribure;
            commonClassWithAttribure.Test();//未标记特性，没有拦截方法
            commonClassWithAttribure.Show();//标记了特性，拦截了
        }
    }

    public class CommonClassInterfaceInjectWithMultitudeAttribure : IAOPInterfaceInjectWithMultitudeAttribure
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

    public class TestMultitudeAttribureInterceptor : StandardInterceptor
    {
        protected override void PreProceed(IInvocation invocation)
        {
            //base.PreProceed(invocation);
        }

        protected override void PerformProceed(IInvocation invocation)
        {
            // fang to do 真实的方法不能灵活的添加到特性中特定的位置
            base.PerformProceed(invocation);//这是拦截方法的执行

            //拦截器Interceptor 中的业务逻辑转移到特性LogAttribure中
            if (invocation.Method.IsDefined(typeof(BaseInterfaceInjectWithMultitudeAttribute), true))
            {
                var attributes = invocation.Method.GetCustomAttributes<BaseInterfaceInjectWithMultitudeAttribute>();
                foreach (var attribute in attributes)
                {
                    attribute.Handle();
                }
            }
        }

        protected override void PostProceed(IInvocation invocation)
        {
            //base.PostProceed(invocation);
        }
    }

    public interface IAOPInterfaceInjectWithMultitudeAttribure
    {
        [LoggerInterfaceInjectWithMultitude]
        [MonitorInterfaceInjectWithMultitude]
        [ExceptionHandleInterfaceInjectWithMultitude]
        void Show();
        void Test();
    }

    public abstract class BaseInterfaceInjectWithMultitudeAttribute: Attribute
    {
        public abstract void Handle();
    }

    /// <summary>
    /// 目的是把 拦截器 Interceptor 中的业务逻辑转移到特性 LoggerAttribure 中
    /// </summary>
    public class LoggerInterfaceInjectWithMultitudeAttribute : BaseInterfaceInjectWithMultitudeAttribute
    {
        public override void Handle()
        {
            Log();
        }
        public void Log()
        {
            Console.WriteLine($"This is {nameof(LoggerInterfaceInjectWithMultitudeAttribute)}.{nameof(Log)}");
        }
    }

    public class ExceptionHandleInterfaceInjectWithMultitudeAttribute : BaseInterfaceInjectWithMultitudeAttribute
    {
        public override void Handle()
        {
            Console.WriteLine($"This is {nameof(ExceptionHandleInterfaceInjectWithMultitudeAttribute)}.{nameof(Handle)}");
        }
    }

    public class MonitorInterfaceInjectWithMultitudeAttribute : BaseInterfaceInjectWithMultitudeAttribute
    {
        public override void Handle()
        {

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine($"This is {nameof(MonitorInterfaceInjectWithMultitudeAttribute)}.{nameof(Handle)}");
            stopwatch.Stop();

            Console.WriteLine($"This is total coast {stopwatch.ElapsedMilliseconds} ms");
        }
    }

}
