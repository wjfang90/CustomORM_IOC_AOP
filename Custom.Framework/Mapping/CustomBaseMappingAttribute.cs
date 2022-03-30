using System;
using System.Collections.Generic;
using System.Text;

namespace Custom.Framework.Mapping
{
    /// <summary>
    /// 映射基类
    /// </summary>
    public class CustomBaseMappingAttribute : Attribute
    {
        private string mappingName;
        public CustomBaseMappingAttribute(string name)
        {
            this.mappingName = name;
        }

        public string GetMappingName()
        {
            return this.mappingName;
        }
    }
}
