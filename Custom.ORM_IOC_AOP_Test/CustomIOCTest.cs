using Custom.BLL;
using Custom.BLL.IOC;
using Custom.DAL;
using Custom.Framework.CustomContainer;
using Custom.IBLL;
using Custom.IBLL.IOC;
using Custom.IDAL;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Custom.ORM_IOC_AOP_Test
{
    public static class CustomIOCTest
    {
        /// <summary>
        /// 构造函数-无参数
        /// </summary>
        public static void CustomConstructorInjectNoParameterTest()
        {
            CustomContainer container = new CustomContainer();

            //无构造函数注入
            container.Register<IConstructorInjectNoParameterBLL, ConstructorInjectNoParameterBLL>();

            var testConstructorNoParam = container.Resolve<IConstructorInjectNoParameterBLL>();
            testConstructorNoParam.Show();
        }

        /// <summary>
        /// 构造函数-匹配参数为所有参数合集
        /// </summary>
        public static void CustomConstructorInjectMultitudeParameterTest()
        {
            CustomContainer container = new CustomContainer();

            //多构造函数注入，匹配参数为所有参数合集的一个构造函数
            container.Register<ICompanyDAL, CompanyDAL>();
            container.Register<IUserDAL, UserDAL>();
            container.Register<IConstructorInjectNoParameterBLL, ConstructorInjectNoParameterBLL>();
            container.Register<IConstructorInjectMultitudeParameterBLL, ConstructorInjectMultitudeParameterBLL>();

            var testConstructorMultitudeParam = container.Resolve<IConstructorInjectMultitudeParameterBLL>();
            testConstructorMultitudeParam.Show();
        }

        /// <summary>
        /// 构造函数-特性注入
        /// </summary>
        public static void CustomConstructorInjectWithAttributeTest()
        {
            CustomContainer container = new CustomContainer();
            container.Register<IUserDAL, UserDAL>();
            container.Register<IConstructorInjectNoParameterBLL, ConstructorInjectNoParameterBLL>();
            container.Register<IConstructorInjectWithAttributeBLL, ConstructorInjectWithAttributeBLL>();

            var constructorWithAttribute = container.Resolve<IConstructorInjectWithAttributeBLL>();
            constructorWithAttribute.Show();
        }

        /// <summary>
        /// 属性注入
        /// </summary>
        public static void CustomPropertyInjectWithAttributeTest()
        {
            CustomContainer container = new CustomContainer();
            container.Register<IUserDAL, UserDAL>();
            container.Register<IConstructorInjectNoParameterBLL, ConstructorInjectNoParameterBLL>();
            container.Register<IPropertyInjectWithAttributeBLL, PropertyInjectWithAttributeBLL>();

            var propertyWithAttribute = container.Resolve<IPropertyInjectWithAttributeBLL>();
            propertyWithAttribute.Show();
        }

        /// <summary>
        /// 方法注入
        /// </summary>
        public static void CustomMethodInjectWithAttributeTest()
        {
            CustomContainer container = new CustomContainer();
            container.Register<IUserDAL, UserDAL>();
            container.Register<IConstructorInjectNoParameterBLL, ConstructorInjectNoParameterBLL>();
            container.Register<IMethodInjectWithAttributeBLL, MethodInjectWithAttributeBLL>();

            var methodWithAttribute = container.Resolve<IMethodInjectWithAttributeBLL>();
            methodWithAttribute.Show();
        }

        /// <summary>
        /// 别名注入
        /// </summary>
        public static void CustomAilasInjectWithNameTest()
        {
            CustomContainer container = new CustomContainer();
            container.Register<IConstructorInjectNoParameterBLL, ConstructorInjectNoParameterBLL>();
            container.Register<IAilasInjectWithNameBLL, AilasInjectWithMysqlBLL>();
            container.Register<IAilasInjectWithNameBLL, AilasInjectWithMysqlBLL>("mysql");
            container.Register<IAilasInjectWithNameBLL, AilasInjectWithSqlServerBLL>("sqlserver");

            var ailasInjectMysqlDefault = container.Resolve<IAilasInjectWithNameBLL>();
            var ailasInjectMysql = container.Resolve<IAilasInjectWithNameBLL>("mysql");
            var ailasInjectSqlServer = container.Resolve<IAilasInjectWithNameBLL>("sqlserver");

            ailasInjectMysqlDefault.Show();
            ailasInjectMysql.Show();
            ailasInjectSqlServer.Show();
        }

        /// <summary>
        /// 别名注入-多层级
        /// </summary>
        public static void CustomAilasInjectWithNameMultitudeLevelTest()
        {
            CustomContainer container = new CustomContainer();

            container.Register<IConstructorInjectNoParameterBLL, ConstructorInjectNoParameterBLL>();
            container.Register<IAilasInjectWithNameBLL, AilasInjectWithMysqlBLL>("mysql");
            container.Register<IAilasInjectWithMultitudeLevelBLL, AilasInjectWithMultitudeLevelBLL>();

            var ailasInjectMultitudeLevelMysql = container.Resolve<IAilasInjectWithMultitudeLevelBLL>();

            ailasInjectMultitudeLevelMysql.Show();
        }

        /// <summary>
        /// 构造函数-常量参数注入
        /// </summary>
        public static void CustomConstructorConstantParameterInjectTest()
        {
            CustomContainer container = new CustomContainer();

            //多构造函数注入，匹配参数为所有参数合集的一个构造函数
            container.Register<IUserDAL, UserDAL>();
            container.Register<IConstructorInjectNoParameterBLL, ConstructorInjectNoParameterBLL>();
            container.Register<IConstructorInjectConstantParameterBLL, ConstructorInjectConstantParameterBLL>(constantParameters: new object[] { "fang", 18 });

            var testConstructorMultitudeParam = container.Resolve<IConstructorInjectConstantParameterBLL>();
            testConstructorMultitudeParam.Show();
        }

        /// <summary>
        /// 生命周期-默认-瞬时周期
        /// </summary>
        public static void CustomContainerLifeTimeDefaultTest()
        {
            CustomContainer container = new CustomContainer();
            container.Register<IConstructorInjectNoParameterBLL, ConstructorInjectNoParameterBLL>();

            var constructorBll1 = container.Resolve<IConstructorInjectNoParameterBLL>();
            var constructorBll2 = container.Resolve<IConstructorInjectNoParameterBLL>();

            Console.WriteLine($"Default {nameof(constructorBll1)}=={nameof(constructorBll2)} {object.ReferenceEquals(constructorBll1, constructorBll2)}");
        }

        /// <summary>
        /// 生命周期-瞬时周期
        /// </summary>
        public static void CustomContainerLifeTimeTransientTest()
        {
            CustomContainer container = new CustomContainer();
            container.Register<IConstructorInjectNoParameterBLL, ConstructorInjectNoParameterBLL>(containLifeTimeType: ContainerLifeTimeType.Transient);

            var constructorBll1 = container.Resolve<IConstructorInjectNoParameterBLL>();
            var constructorBll2 = container.Resolve<IConstructorInjectNoParameterBLL>();

            Console.WriteLine($"Transient {nameof(constructorBll1)}=={nameof(constructorBll2)} {object.ReferenceEquals(constructorBll1, constructorBll2)}");
        }

        /// <summary>
        /// 生命周期-单例周期
        /// </summary>
        public static void CustomContainerLifeTimeSingletonTest()
        {
            CustomContainer container = new CustomContainer();
            container.Register<IConstructorInjectNoParameterBLL, ConstructorInjectNoParameterBLL>(containLifeTimeType: ContainerLifeTimeType.Singleton);

            var constructorBll1 = container.Resolve<IConstructorInjectNoParameterBLL>();
            var constructorBll2 = container.Resolve<IConstructorInjectNoParameterBLL>();

            Console.WriteLine($"Singleton {nameof(constructorBll1)}=={nameof(constructorBll2)} {object.ReferenceEquals(constructorBll1, constructorBll2)}");
        }

        /// <summary>
        /// 生命周期-作用域周期
        /// 就是Http请求时，一个请求处理过程中，创建都是同一个实例； 不同的请求处理过程中，就是不同的实例
        ///  得区分请求，Http请求---Asp.NetCore内置Kestrel，初始化一个容器实例；然后每次来一个Http请求，就clone一个，或者叫创建子容器(包含注册关系)，然后一个请求就一个子容器实例，那么就可以做到请求单例了(其实就是子容器单例)</summary>
        public static void CustomContainerLifeTimeScopeTest()
        {
            CustomContainer container = new CustomContainer();
            container.Register<IConstructorInjectNoParameterBLL, ConstructorInjectNoParameterBLL>(containLifeTimeType: ContainerLifeTimeType.Scope);

            var constructorBll1 = container.Resolve<IConstructorInjectNoParameterBLL>();
            var constructorBll2 = container.Resolve<IConstructorInjectNoParameterBLL>();

            var childContainer = container.CreateChildContainer();
            var childConstructorBll1 = childContainer.Resolve<IConstructorInjectNoParameterBLL>();
            var childConstructorBll2 = childContainer.Resolve<IConstructorInjectNoParameterBLL>();

            var childContainer2 = container.CreateChildContainer();
            var child2ConstructorBll1 = childContainer2.Resolve<IConstructorInjectNoParameterBLL>();
            var child2ConstructorBll2 = childContainer2.Resolve<IConstructorInjectNoParameterBLL>();

            Console.WriteLine($"Scope all container {nameof(constructorBll1)}=={nameof(constructorBll2)} {object.ReferenceEquals(constructorBll1, constructorBll2)}");
            Console.WriteLine($"Scope container and childContainer1 {nameof(constructorBll1)}=={nameof(childConstructorBll1)} {object.ReferenceEquals(constructorBll1, childConstructorBll1)}");
            Console.WriteLine($"Scope container and childContainer1 {nameof(constructorBll1)}=={nameof(childConstructorBll2)} {object.ReferenceEquals(constructorBll1, childConstructorBll2)}");

            Console.WriteLine($"Scope all childContainer1 {nameof(childConstructorBll1)}=={nameof(childConstructorBll2)} {object.ReferenceEquals(childConstructorBll1, childConstructorBll2)}");
            Console.WriteLine($"Scope all childContainer2 {nameof(child2ConstructorBll1)}=={nameof(child2ConstructorBll2)} {object.ReferenceEquals(child2ConstructorBll1, child2ConstructorBll2)}");

            Console.WriteLine($"Scope childContainer1 and childContainer2 {nameof(childConstructorBll1)}=={nameof(child2ConstructorBll1)} {object.ReferenceEquals(childConstructorBll1, child2ConstructorBll1)}");
            Console.WriteLine($"Scope childContainer1 and childContainer2 {nameof(childConstructorBll1)}=={nameof(child2ConstructorBll2)} {object.ReferenceEquals(childConstructorBll1, child2ConstructorBll2)}");
            Console.WriteLine($"Scope childContainer1 and childContainer2 {nameof(childConstructorBll2)}=={nameof(child2ConstructorBll1)} {object.ReferenceEquals(childConstructorBll2, child2ConstructorBll1)}");
            Console.WriteLine($"Scope childContainer1 and childContainer2 {nameof(childConstructorBll2)}=={nameof(child2ConstructorBll2)} {object.ReferenceEquals(childConstructorBll2, child2ConstructorBll2)}");
        }


        /// <summary>
        /// 线程单例，基于AsyncLocal 实现。
        /// 注意：基于AsyncLocal 在主线程中获取不到子线程中创建的实例，在子线程可以修改或获取主线程的实例
        /// </summary>
        public static void CustomContainerLifeTimePreThreadTest()
        {
            CustomContainer container = new CustomContainer();
            container.Register<IConstructorInjectNoParameterBLL, ConstructorInjectNoParameterBLL>(containLifeTimeType: ContainerLifeTimeType.PerThread);

            var constructorBll1 = container.Resolve<IConstructorInjectNoParameterBLL>();
            var constructorBll2 = container.Resolve<IConstructorInjectNoParameterBLL>();

            IConstructorInjectNoParameterBLL constructorBllThread1 = null;
            IConstructorInjectNoParameterBLL constructorBllThread2 = null;
            IConstructorInjectNoParameterBLL constructorBllThread3 = null;
            IConstructorInjectNoParameterBLL constructorBllThread4 = null;

            Console.WriteLine($"------This is {Thread.CurrentThread.ManagedThreadId} {nameof(constructorBll1)}");
            Console.WriteLine($"------This is {Thread.CurrentThread.ManagedThreadId} {nameof(constructorBll2)}");

            Task.Run(() =>
             {
                 Console.WriteLine($"------This is {Thread.CurrentThread.ManagedThreadId} {nameof(constructorBllThread1)}");
                 constructorBllThread1 = container.Resolve<IConstructorInjectNoParameterBLL>();

                 Task.Run(() =>
                 {
                     Console.WriteLine($"------This is {Thread.CurrentThread.ManagedThreadId} {nameof(constructorBllThread2)}");
                     constructorBllThread2 = container.Resolve<IConstructorInjectNoParameterBLL>();

                     Task.Run(() =>
                     {
                         Console.WriteLine($"------This is {Thread.CurrentThread.ManagedThreadId} {nameof(constructorBllThread3)}");
                         constructorBllThread3 = container.Resolve<IConstructorInjectNoParameterBLL>();

                         Task.Run(() =>
                         {
                             Console.WriteLine($"------This is {Thread.CurrentThread.ManagedThreadId} {nameof(constructorBllThread4)}");
                             constructorBllThread4 = container.Resolve<IConstructorInjectNoParameterBLL>();

                             Task.Delay(1000);

                             Console.WriteLine($"PreThread all main thread {nameof(constructorBll1)}=={nameof(constructorBll2)} {object.ReferenceEquals(constructorBll1, constructorBll2)}");
                             Console.WriteLine($"PreThread main and child thread {nameof(constructorBllThread1)}=={nameof(constructorBll1)} {object.ReferenceEquals(constructorBllThread1, constructorBll1)}");
                             Console.WriteLine($"PreThread all child thread {nameof(constructorBllThread1)}=={nameof(constructorBllThread2)} {object.ReferenceEquals(constructorBllThread1, constructorBllThread2)}");
                             Console.WriteLine($"PreThread all child thread {nameof(constructorBllThread1)}=={nameof(constructorBllThread3)} {object.ReferenceEquals(constructorBllThread1, constructorBllThread3)}");
                             Console.WriteLine($"PreThread all child thread {nameof(constructorBllThread1)}=={nameof(constructorBllThread4)} {object.ReferenceEquals(constructorBllThread1, constructorBllThread4)}");
                             Console.WriteLine($"PreThread all child thread {nameof(constructorBllThread2)}=={nameof(constructorBllThread3)} {object.ReferenceEquals(constructorBllThread2, constructorBllThread3)}");
                             Console.WriteLine($"PreThread all child thread {nameof(constructorBllThread2)}=={nameof(constructorBllThread4)} {object.ReferenceEquals(constructorBllThread2, constructorBllThread4)}");
                             Console.WriteLine($"PreThread all child thread {nameof(constructorBllThread3)}=={nameof(constructorBllThread4)} {object.ReferenceEquals(constructorBllThread3, constructorBllThread4)}");
                         });
                     });
                 });
             });
        }

        public static void CustomContainerAOPTest()
        {
            CustomContainer container = new CustomContainer();
            container.Register<IInjectWithAOPBLL, InjectWithAOPBLL>(containLifeTimeType: ContainerLifeTimeType.Singleton);

            var objInstance = container.Resolve<IInjectWithAOPBLL>();
            objInstance.Test();
            Console.WriteLine("==============AOP==================");
            objInstance.Show();

        }



        /// <summary>
        /// await外的AsyncLocal值可以传递到await内, await内的AsyncLocal值无法传递到await外(只能读取不能修改).
        /// </summary>
        public static void TestAsyncLocal()
        {
            AsyncLocal<int> asyncLocal = new AsyncLocal<int>(AsyncLocalChangeHandle) { Value = Thread.CurrentThread.ManagedThreadId };

            Console.WriteLine($"init { asyncLocal.Value} threadId = {Thread.CurrentThread.ManagedThreadId}");

            Task.Run(() =>
            {
                Console.WriteLine($"modify before { asyncLocal.Value} threadId = {Thread.CurrentThread.ManagedThreadId}");
                asyncLocal.Value = Thread.CurrentThread.ManagedThreadId;
                Console.WriteLine($"modify after { asyncLocal.Value} threadId = {Thread.CurrentThread.ManagedThreadId}");

                Task.Run(() =>
                {
                    Console.WriteLine($"modify before { asyncLocal.Value} threadId = {Thread.CurrentThread.ManagedThreadId}");
                    asyncLocal.Value = Thread.CurrentThread.ManagedThreadId;
                    Console.WriteLine($"modify after { asyncLocal.Value} threadId = {Thread.CurrentThread.ManagedThreadId}");
                });
            }).Wait();

            Console.WriteLine($"end { asyncLocal.Value} threadId = {Thread.CurrentThread.ManagedThreadId}");
        }

        /// <summary>
        /// AsyncLocal Value 改变会触发AsyncLocalChangeHandle 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="change"></param>
        private static void AsyncLocalChangeHandle<T>(AsyncLocalValueChangedArgs<T> change)
        {
            Console.WriteLine($"isChange = {change.ThreadContextChanged} , CurrentValue = {change.CurrentValue}, PreviousValue = {change.PreviousValue} , threadId = {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
