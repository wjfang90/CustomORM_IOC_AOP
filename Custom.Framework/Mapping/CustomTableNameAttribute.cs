using System;
using System.Collections.Generic;
using System.Text;

namespace Custom.Framework.Mapping
{
    /// <summary>
    /// 类名别名
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class CustomTableNameAttribute : CustomBaseMappingAttribute
    {
        #region 优化前代码
        //private string mappingName;

        //public CustomTableNameAttribute(string name)
        //{
        //    this.mappingName = name;
        //}

        //public string GetMappingName()
        //{
        //    return this.mappingName;
        //}

        #endregion

        public CustomTableNameAttribute(string name) : base(name)
        {
        }
    }
}
