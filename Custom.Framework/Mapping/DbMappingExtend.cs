using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Custom.Framework.Mapping
{
    public static class DbMappingExtend
    {
        /// <summary>
        /// 已优化代码
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetMappingName<T>(this T t) where T : MemberInfo
        {
            if (t.IsDefined(typeof(CustomBaseMappingAttribute), true))
            {
                var attribute = t.GetCustomAttribute<CustomBaseMappingAttribute>(true);
                return attribute.GetMappingName();
            }

            return t.Name;
        }

        #region 可优化代码
        /// <summary>
        /// 可优化代码
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetMappingTableName(this Type type)
        {
            if (type.IsDefined(typeof(CustomTableNameAttribute), true))
            {
                var attribute = type.GetCustomAttribute<CustomTableNameAttribute>(true);
                return attribute.GetMappingName();
            }

            return type.Name;
        }

        /// <summary>
        /// 可优化代码
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetMappingColumnName(this PropertyInfo prop)
        {
            if (prop.IsDefined(typeof(CustomColumnNameAttribute), true))
            {
                var attribute = prop.GetCustomAttribute<CustomColumnNameAttribute>(true);
                return attribute.GetMappingName();
            }
            return prop.Name;
        }

        #endregion
    }
}
