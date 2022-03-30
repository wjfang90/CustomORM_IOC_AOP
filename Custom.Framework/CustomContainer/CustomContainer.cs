using Custom.Framework.CustomAOP;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Custom.Framework.CustomContainer
{
    public class CustomContainer : ICustomContainer
    {
        /// <summary>
        /// 注册的实例对象字典
        /// </summary>
        private ConcurrentDictionary<string, CustomContainerRegistModel> containerDict = new ConcurrentDictionary<string, CustomContainerRegistModel>();

        /// <summary>
        /// 常量参数字典
        /// </summary>
        private ConcurrentDictionary<string, object[]> constantParameterDict = new ConcurrentDictionary<string, object[]>();

        /// <summary>
        /// 作用域实例，子容器的注册作用域实例对象字典
        /// </summary>
        private ConcurrentDictionary<string, object> ContainerScopeDict = new ConcurrentDictionary<string, object>();

        public CustomContainer()
        {

        }

        public ICustomContainer CreateChildContainer()
        {
            return new CustomContainer(this.containerDict, this.constantParameterDict, new ConcurrentDictionary<string, object>());
        }

        private CustomContainer(ConcurrentDictionary<string, CustomContainerRegistModel> containerDict, ConcurrentDictionary<string, object[]> constantParameterDict, ConcurrentDictionary<string, object> containerScopeDict)
        {
            this.containerDict = containerDict;
            this.constantParameterDict = constantParameterDict;
            this.ContainerScopeDict = containerScopeDict;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="name">别名名称，一个接口多个实现时，区分实现类</param>
        /// <typeparam name="TService">服务接口</typeparam>
        /// <param name="constantParameters">常量参数</param>
        /// <param name="containLifeTime">生命周期类型</param>
        /// <typeparam name="TImplementation">接口实现类</typeparam>
        public void Register<TService, TImplementation>(string name = null, object[] constantParameters = null, ContainerLifeTimeType containLifeTimeType = ContainerLifeTimeType.Transient) where TImplementation : TService
        {
            var key = GetKey(typeof(TService).FullName, name);

            if (!containerDict.ContainsKey(key))
                containerDict.TryAdd(key, new CustomContainerRegistModel()
                {
                    TargetType = typeof(TImplementation),
                    ContainerLifeTimeType = containLifeTimeType
                });

            if (constantParameters != null && constantParameters.Any())
                constantParameterDict.TryAdd(key, constantParameters);
        }

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="name">别名名称，一个接口多个实现时，区分实现类</param>
        /// <typeparam name="TService">接口实现类</typeparam>
        /// <returns></returns>
        public TService Resolve<TService>(string name = null)
        {
            return (TService)ResolveObject(typeof(TService), name);
        }

        private object ResolveObject(Type serviceType, string name = null)
        {
            #region 1.构造函数注入

            var key = GetKey(serviceType.FullName, name);

            if (!containerDict.ContainsKey(key))
                throw new ArgumentNullException($"{nameof(Type)}类型未注入");

            var containerModel = containerDict[key];
            var type = containerModel.TargetType;
            //1.1选择构造函数
            //1.1.1 使用标记特性的构造函数
            var constructor = type.GetConstructors().FirstOrDefault(t => t.IsDefined(typeof(CustomConstructorInjectAttribute), true));

            //1.1.2 匹配构造函数参数是所有构造参数合集（不包含未标记常量特性的string和值类型的参数）的构造函数
            if (constructor == null)
            {
                Dictionary<string, Type> constructorParameterDict = new Dictionary<string, Type>();
                type.GetConstructors().ToList().ForEach(
                    t => t.GetParameters().ToList().ForEach(p =>
                    {
                        var constructorParameterKey = p.ParameterType.FullName;
                        var isConstantParameter = p.IsDefined(typeof(CustomConstructorConstantPapameterInjectAttribute), true);
                        if (isConstantParameter)
                            constructorParameterKey = $"{constructorParameterKey}_{p.Name}";

                        //构造函数参数不包含未标记常量特性的 string和ValueType类型 参数
                        if (!constructorParameterDict.ContainsKey(constructorParameterKey)
                            && ((!p.ParameterType.Equals(typeof(string)) && (p.ParameterType.BaseType == null || !p.ParameterType.BaseType.Equals(typeof(ValueType))))
                                || isConstantParameter))
                            constructorParameterDict.Add(constructorParameterKey, p.ParameterType);
                    }));

                constructor = constructorParameterDict.Any()
                    ? type.GetConstructors().FirstOrDefault(t => t.GetParameters().Length == constructorParameterDict.Count && t.GetParameters().Any(p => constructorParameterDict.ContainsKey(p.ParameterType.FullName)))
                    : type.GetConstructor(new Type[0]);
            }

            //1.1.3 使用参数个数最多的构造函数
            //if (constructor == null)
            //    constructor = type.GetConstructors().OrderByDescending(t => t.GetParameters().Length).FirstOrDefault();

            if (constructor == null)
                throw new ArgumentNullException($"{nameof(Type)}实现类型的构造函数不匹配");

            //1.2准备构造函数参数
            List<object> constructorParameterList = new List<object>();
            var constantParameterIndex = 0;
            var constantParameterValueList = constantParameterDict.ContainsKey(key) ? constantParameterDict[key] : null;
            foreach (var parameter in constructor.GetParameters())
            {
                if (parameter.IsDefined(typeof(CustomConstructorConstantPapameterInjectAttribute), true))
                {
                    constructorParameterList.Add(constantParameterValueList[constantParameterIndex++]);
                }
                else
                {
                    var ailasName = GetAilasName(parameter);
                    var papamObj = ResolveObject(parameter.ParameterType, ailasName);
                    constructorParameterList.Add(papamObj);
                }
            }

            #endregion

            #region 生命周期管理 - 创建实例前

            switch (containerModel.ContainerLifeTimeType)
            {
                case ContainerLifeTimeType.Transient:
                    break;
                case ContainerLifeTimeType.Singleton:
                    if (containerModel.SingletonInstance != null)
                        return containerModel.SingletonInstance;
                    break;
                case ContainerLifeTimeType.Scope:
                    if (ContainerScopeDict.ContainsKey(key))
                        return ContainerScopeDict[key];
                    break;
                case ContainerLifeTimeType.PerThread:
                    var preTheadInstance = CustomCallContext<object>.GetData($"{key}_{Thread.CurrentThread.ManagedThreadId}");
                    if (preTheadInstance != null)
                        return preTheadInstance;
                    break;
                default:
                    break;
            }

            #endregion

            var objInstance = Activator.CreateInstance(type, constructorParameterList.ToArray());

            #region 2.属性注入

            var propertyInfoList = type.GetProperties().Where(t => t.IsDefined(typeof(CustomPropertyInjectAttribute), true));
            foreach (var propertyInfo in propertyInfoList)
            {
                var ailasName = GetAilasName(propertyInfo);
                var propertyObj = ResolveObject(propertyInfo.PropertyType, ailasName);
                propertyInfo.SetValue(objInstance, propertyObj);
            }

            #endregion

            #region 3.方法注入

            var methodInfoList = type.GetMethods().Where(t => t.IsDefined(typeof(CustomMethodInjectAttribute), true));
            foreach (var methodInfo in methodInfoList)
            {
                var methodParameterList = new List<object>();
                foreach (var methodParamParameterInfo in methodInfo.GetParameters())
                {
                    var methodParamObj = ResolveObject(methodParamParameterInfo.ParameterType);
                    methodParameterList.Add(methodParamObj);
                }
                methodInfo.Invoke(objInstance, methodParameterList.ToArray());
            }

            #endregion


            #region 生命周期管理 - 创建实例后

            switch (containerModel.ContainerLifeTimeType)
            {
                case ContainerLifeTimeType.Transient:
                    break;
                case ContainerLifeTimeType.Singleton:
                    containerModel.SingletonInstance = objInstance;
                    break;
                case ContainerLifeTimeType.Scope:
                    ContainerScopeDict.TryAdd(key, objInstance);
                    break;
                case ContainerLifeTimeType.PerThread:
                    CustomCallContext<object>.SetData($"{key}_{Thread.CurrentThread.ManagedThreadId}", objInstance);
                    break;
                default:
                    break;
            }

            #endregion

            #region AOP支持

            objInstance = objInstance.AOP(serviceType);

            #endregion

            return objInstance;
        }

        private string GetAilasName(ICustomAttributeProvider attributeProvider)
        {
            if (attributeProvider.IsDefined(typeof(CustomAilasInjectAttribute), true))
            {
                return (attributeProvider.GetCustomAttributes(typeof(CustomAilasInjectAttribute), true).FirstOrDefault() as CustomAilasInjectAttribute)?.GetAilasName();
            }

            return null;
        }

        private string GetKey(string fullName, string ailasName)
        {
            if (!string.IsNullOrEmpty(ailasName))
                return $"{fullName}_{ailasName}";
            return fullName;
        }
    }
}
