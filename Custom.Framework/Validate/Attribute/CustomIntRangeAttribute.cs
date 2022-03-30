using System;
using System.Collections.Generic;
using System.Text;

namespace Custom.Framework.Validate
{
    /// <summary>
    /// 数据验证--数据范围（左闭右开，有效值包含最小值，不包含最大值）
    /// </summary>
    public class CustomIntRangeAttribute : CustomBaseValidateAttribute
    {

        /// <summary>
        /// 待优化
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        //public bool Validate(object value)
        //{
            //return value!=null && Convert.ToInt32(value) >= minValue && Convert.ToInt32(value) < maxValue;
        //}

        private int minValue;
        private int maxValue;
        public CustomIntRangeAttribute(int minValue, int maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
        }
        public override bool Validate(object value)
        {
            return value!=null && Convert.ToInt32(value) >= minValue && Convert.ToInt32(value) < maxValue;
        }
    }
}
