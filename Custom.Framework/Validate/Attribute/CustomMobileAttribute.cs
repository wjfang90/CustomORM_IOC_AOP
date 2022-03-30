using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Custom.Framework.Validate
{
    /// <summary>
    /// 数据验证国内手机号格式
    /// </summary>
    public class CustomMobileAttribute : CustomBaseValidateAttribute
    {

        /// <summary>
        /// 待优化
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        //public bool Validate(object value)
        //{
        //    var mobilePattern = @"^(13[0-9]|14[01456879]|15[0-35-9]|16[2567]|17[0-8]|18[0-9]|19[0-35-9])\d{8}$";//国内手机号，2022年
        //    return value != null && !string.IsNullOrEmpty(value.ToString()) && !string.IsNullOrWhiteSpace(value.ToString())
        //           && Regex.IsMatch(value.ToString(), emailPattern);
        //}

        public override bool Validate(object value)
        {
            var mobilePattern = @"^(13[0-9]|14[01456879]|15[0-35-9]|16[2567]|17[0-8]|18[0-9]|19[0-35-9])\d{8}$";//国内手机号，2022年
            return value != null && !string.IsNullOrEmpty(value.ToString()) && !string.IsNullOrWhiteSpace(value.ToString())
                   && Regex.IsMatch(value.ToString(), mobilePattern);
        }
    }
}
