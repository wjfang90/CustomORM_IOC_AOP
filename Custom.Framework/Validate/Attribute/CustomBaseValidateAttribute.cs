using System;
using System.Collections.Generic;
using System.Text;

namespace Custom.Framework.Validate
{
    /// <summary>
    /// 数据验证基类
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public abstract class CustomBaseValidateAttribute : Attribute
    {
        public abstract bool Validate(object value);
    }
}
