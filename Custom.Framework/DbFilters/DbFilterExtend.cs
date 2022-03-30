using Custom.Framework.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Custom.Framework.DbFilters
{
    public static class DbFilterExtend
    {

        public static IEnumerable<PropertyInfo> GetPropertiesNoKey(this Type type)
        {
            return type.GetProperties().Where(t => !t.IsDefined(typeof(CustomKeyAttribute), true));
        }
        
        public static string GetKeyName(this Type type)
        {
            return type.GetProperties().Where(t => t.IsDefined(typeof(CustomKeyAttribute), true)).Select(t=>t.GetMappingName()).FirstOrDefault();
        }
    }
}
