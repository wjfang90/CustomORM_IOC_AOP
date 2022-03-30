using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custom.Framework.CustomAOP.CastleTest
{
    public class AOPTypeClassVisualMethodInject
    {
        public static void AOPTest()
        {
            //1.引用 Castle Core
            //2.创建动态代理
            IProxyGenerator proxyGenerator = new ProxyGenerator();

            //3.创建拦截器
            IInterceptor testInvocation = new TestInterceptor();

            //4.类型注入
            var commonClassProxy = proxyGenerator.CreateClassProxy<CommonClassTypeInject>(testInvocation);
            commonClassProxy.Test();//普通方法不能实现拦截
            commonClassProxy.TestVisual();//Visual 方法可以实现拦截
        }
    }
    

    public class CommonClassTypeInject
    {
        public virtual void TestVisual()
        {
            Console.WriteLine("这是类型注入的 Visual 方法，可以类型注入，实现方法拦截");
        }

        public void Test()
        {
            Console.WriteLine("这是普通的方法，不能类型注入，方法拦截");
        }
    }
}
