using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Custom.Framework.Validate
{
    /// <summary>
    /// 数据验证Eamil格式
    /// </summary>
    public class CustomEmailAttribute : CustomBaseValidateAttribute
    {

        /// <summary>
        /// 待优化
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        //public bool Validate(object value)
        //{
        //    var emailPattern = @"\w[-\w.+]*@([A-Za-z0-9][-A-Za-z0-9]+\.)+[A-Za-z]{2,14}";
        //    return value != null && !string.IsNullOrEmpty(value.ToString()) && !string.IsNullOrWhiteSpace(value.ToString())
        //           && Regex.IsMatch(value.ToString(), emailPattern);
        //}

        public override bool Validate(object value)
        {
            var emailPattern = @"\w[-\w.+]*@([A-Za-z0-9][-A-Za-z0-9]+\.)+[A-Za-z]{2,14}";
            return value != null && !string.IsNullOrEmpty(value.ToString()) && !string.IsNullOrWhiteSpace(value.ToString())
                   && Regex.IsMatch(value.ToString(), emailPattern);
        }
    }
}
