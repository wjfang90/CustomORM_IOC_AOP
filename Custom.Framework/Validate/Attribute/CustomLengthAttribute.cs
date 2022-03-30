using System;
using System.Collections.Generic;
using System.Text;

namespace Custom.Framework.Validate
{
    /// <summary>
    /// 数据验证字符长度
    /// </summary>
    public class CustomLengthAttribute : CustomBaseValidateAttribute
    {

        /// <summary>
        /// 待优化
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        //public bool Validate(object value)
        //{
        //    return value != null && !string.IsNullOrEmpty(value.ToString()) && !string.IsNullOrWhiteSpace(value.ToString()) 
                   //&& value.ToString().Length >= minValue && value.ToString().Length<maxValue;
        //}

        private int minValue;
        private int maxValue;
        public CustomLengthAttribute(int minValue, int maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
        }
        public override bool Validate(object value)
        {
            return value != null && !string.IsNullOrEmpty(value.ToString()) && !string.IsNullOrWhiteSpace(value.ToString()) 
                   && value.ToString().Length >= minValue && value.ToString().Length < maxValue;
        }
    }
}
