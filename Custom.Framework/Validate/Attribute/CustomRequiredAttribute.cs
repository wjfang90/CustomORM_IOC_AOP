using System;
using System.Collections.Generic;
using System.Text;

namespace Custom.Framework.Validate
{
    /// <summary>
    /// 数据验证必填字段
    /// </summary>
    public class CustomRequiredAttribute : CustomBaseValidateAttribute
    {

        /// <summary>
        /// 待优化
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        //public bool Validate(object value)
        //{
        //    return value != null && !string.IsNullOrWhiteSpace(value.ToString());
        //}

        public override bool Validate(object value)
        {
            return value != null && !string.IsNullOrWhiteSpace(value.ToString());
        }
    }
}
