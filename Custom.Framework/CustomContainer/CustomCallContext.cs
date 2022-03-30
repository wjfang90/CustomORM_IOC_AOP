using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Custom.Framework.CustomContainer
{
    /// <summary>
    /// 自定义CallContext,只能保证自身是线程安全，
    /// 不同线程 还得用Key来区分
    /// </summary>
    public class CustomCallContext<T>
    {
        /// <summary>
        /// AsyncLocal await外的AsyncLocal值可以传递到await内, await内的AsyncLocal值无法传递到await外(只能读取不能修改).
        /// </summary>
        private static ConcurrentDictionary<string, AsyncLocal<T>> CallContextData = new ConcurrentDictionary<string, AsyncLocal<T>>();

        /// <summary>
        /// 添加线程数据
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        public static void SetData(string name, T data)
        {
            CallContextData.GetOrAdd(name, o => new AsyncLocal<T>()).Value = data;
        }
        /// <summary>
        /// 获取线程数据
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T GetData(string name)
        {
            return CallContextData.TryGetValue(name, out AsyncLocal<T> data) ? data.Value : default(T);
        }

    }
}
