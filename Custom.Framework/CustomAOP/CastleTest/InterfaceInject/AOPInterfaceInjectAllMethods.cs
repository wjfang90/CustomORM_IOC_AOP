using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custom.Framework.CustomAOP.CastleTest
{
    public class AOPInterfaceInjectAllMethods
    {
        public static void AOPTest()
        {
            //1.引用 Castle Core
            //2.创建动态代理
            IProxyGenerator proxyGenerator = new ProxyGenerator();

            //3.创建拦截器
            IInterceptor testInvocation = new TestInterceptor();

            //4.接口注入,CommonClassInterfaceInject 类中 实现接口的所有方法都实现了拦截
            object commonClassObj = new CommonClassInterfaceInject();
            var commonClass = proxyGenerator.CreateInterfaceProxyWithTarget(typeof(IAOPInterfaceInjectAllMethods), commonClassObj, testInvocation) as IAOPInterfaceInjectAllMethods;

            commonClass.Show();
            commonClass.Test();
        }
    }

    public class CommonClassInterfaceInject: IAOPInterfaceInjectAllMethods
    {
        public void Show()
        {
            Console.WriteLine($"这是实现接口的 {nameof(Show)} 方法，接口注入，实现方法拦截");
        }

        public void Test()
        {
            Console.WriteLine($"这是实现接口的 {nameof(Test)} 方法，接口注入，实现方法拦截");
        }
    }

    public interface IAOPInterfaceInjectAllMethods
    {
        void Show();
        void Test();
    }
}
