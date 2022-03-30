using System;
using System.Collections.Generic;
using System.Text;

namespace Custom.Framework.CustomContainer
{

    public class CustomContainerRegistModel
    {
        /// <summary>
        /// 注册的实例对象
        /// </summary>
        public Type TargetType { get; set; }

        /// <summary>
        /// 注册的实例对象的生命周期类型
        /// </summary>
        public ContainerLifeTimeType ContainerLifeTimeType { get; set; }

        /// <summary>
        /// 单例实例对象，ContainerLifeTimeType为 Singleton时有效
        /// </summary>
        public object SingletonInstance { get; set; }
    }

    public enum ContainerLifeTimeType
    {
        /// <summary>
        /// 瞬时，一个Container,每次Resolve都不同
        /// </summary>
        Transient,
        /// <summary>
        /// 单例，一个Container,每次Resolve都相同
        /// </summary>
        Singleton,
        /// <summary>
        /// 作用域，同一个请求的单例，一个Container,每个 ChildContainer的每次Resolve都相同
        /// </summary>
        Scope,
        /// <summary>
        /// 线程单例，同一个线程单例
        /// </summary>
        PerThread
        //外部可释放单例
    }
}
