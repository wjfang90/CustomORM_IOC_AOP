using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Custom.Framework.Validate
{
    /// <summary>
    /// 数据验证--特定的几个值之一
    /// </summary>
    public class CustomIntContainsAttribute : CustomBaseValidateAttribute
    {

        /// <summary>
        /// 待优化
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        //public bool Validate(object value)
        //{
            //return value != null && values != null && values.Contains(Convert.ToInt32(value));
        //}

        private int[] values;
        public CustomIntContainsAttribute(params int[] values)
        {
            this.values = values;
        }
        public override bool Validate(object value)
        {
            return value != null && values != null && values.Contains(Convert.ToInt32(value));
        }
    }
}
