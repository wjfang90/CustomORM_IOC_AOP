using System;
using System.Collections.Generic;
using System.Text;

namespace Custom.Framework.CustomContainer
{
    public interface ICustomContainer
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="name">别名名称，一个接口多个实现时，区分实现类</param>
        /// <param name="constantParameters">常量参数</param>
        /// <param name="containLifeTime">生命周期类型</param>
        /// <typeparam name="TService">服务接口</typeparam>
        /// <typeparam name="TImplementation">接口实现类</typeparam>
        void Register<TService, TImplementation>(string name = null, object[] constantParameters = null, ContainerLifeTimeType containLifeTime = ContainerLifeTimeType.Transient) where TImplementation : TService;
        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="name">别名名称，一个接口多个实现时，区分实现类</param>
        /// <param name="constantParameters">常量参数</param>
        /// <typeparam name="TService">接口</typeparam>
        /// <returns></returns>
        TService Resolve<TService>(string name = null);

        ICustomContainer CreateChildContainer();
    }
}
