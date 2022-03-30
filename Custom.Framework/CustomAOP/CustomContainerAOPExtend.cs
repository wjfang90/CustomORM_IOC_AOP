using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custom.Framework.CustomAOP
{
    public static class CustomContainerAOPExtend
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objTarget">实例对象</param>
        /// <param name="serviceType">接口类型</param>
        /// <returns>接口代理对象</returns>
        public static object AOP(this object objTarget, Type serviceType)
        {
            //1.引用 Castle Core
            //2.创建动态代理
            ProxyGenerator proxyGenerator = new ProxyGenerator();
            //3.创建拦截器
            CustomInterceptor customInterceptor = new CustomInterceptor();

            //4.接口注入,实现接口的并标记特性的方法都实现了拦截

            var proxyObj = proxyGenerator.CreateInterfaceProxyWithTarget(serviceType, objTarget, customInterceptor);
            return proxyObj;
        }
    }
}
