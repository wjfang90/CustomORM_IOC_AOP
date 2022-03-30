using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Custom.Framework.CustomAOP.CastleTest.InterfaceInject
{
    public abstract class AOPBaseInterfaceInjectWithMultitudeAttributeByAction : Attribute
    {
        public int Sort { get; set; }
        public abstract Action Handle(Action action);
    }

    /// <summary>
    /// 目的是把 拦截器 Interceptor 中的业务逻辑转移到特性 LoggerAttribure 中
    /// </summary>
    public class AOPLoggerInterfaceInjectWithMultitudeAttribute : AOPBaseInterfaceInjectWithMultitudeAttributeByAction
    {
        private int sort;
        public AOPLoggerInterfaceInjectWithMultitudeAttribute(int sort = 0)
        {
            base.Sort = sort;
        }
        public override Action Handle(Action action)
        {
            return () =>
            {
                Console.WriteLine($"This is {nameof(AOPLoggerInterfaceInjectWithMultitudeAttribute)}.{nameof(Handle)} start");
                action.Invoke();//执行真实的方法            
                Log();
                Console.WriteLine($"This is {nameof(AOPLoggerInterfaceInjectWithMultitudeAttribute)}.{nameof(Handle)} end");
            };
        }
        public void Log()
        {
            Console.WriteLine($"This is {nameof(AOPLoggerInterfaceInjectWithMultitudeAttribute)}.{nameof(Log)}");
        }
    }

    public class AOPExceptionHandleInterfaceInjectWithMultitudeAttribute : AOPBaseInterfaceInjectWithMultitudeAttributeByAction
    {
        private int sort;
        public AOPExceptionHandleInterfaceInjectWithMultitudeAttribute(int sort = 0)
        {
            base.Sort = sort;
        }
        public override Action Handle(Action action)
        {
            return () =>
            {
                Console.WriteLine($"This is {nameof(AOPExceptionHandleInterfaceInjectWithMultitudeAttribute)}.{nameof(Handle)} start");
                action.Invoke();
                Console.WriteLine($"This is {nameof(AOPExceptionHandleInterfaceInjectWithMultitudeAttribute)}.{nameof(Handle)} end");

            };
        }
    }

    public class AOPMonitorInterfaceInjectWithMultitudeAttribute : AOPBaseInterfaceInjectWithMultitudeAttributeByAction
    {
        private int sort;
        public AOPMonitorInterfaceInjectWithMultitudeAttribute(int sort = 0)
        {
            base.Sort = sort;
        }
        public override Action Handle(Action action)
        {
            return () =>
            {
                Console.WriteLine($"This is {nameof(AOPMonitorInterfaceInjectWithMultitudeAttribute)}.{nameof(Handle)} start");
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                action.Invoke();

                stopwatch.Stop();
                Console.WriteLine($"This is total coast {stopwatch.ElapsedMilliseconds} ms");
                Console.WriteLine($"This is {nameof(AOPMonitorInterfaceInjectWithMultitudeAttribute)}.{nameof(Handle)} end");
            };

        }
    }
}
