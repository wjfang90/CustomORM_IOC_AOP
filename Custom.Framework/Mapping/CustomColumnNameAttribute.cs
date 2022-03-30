using System;
using System.Collections.Generic;
using System.Text;

namespace Custom.Framework.Mapping
{
    /// <summary>
    /// 属性别名
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class CustomColumnNameAttribute : CustomBaseMappingAttribute
    {
        #region 优化前代码
        //private string mappingName;

        //public CustomColumnNameAttribute(string name)
        //{
        //    this.mappingName = name;
        //}

        //public string GetMappingName()
        //{
        //    return this.mappingName;
        //}
        #endregion

        public CustomColumnNameAttribute(string name) : base(name)
        {

        }
    }
}
