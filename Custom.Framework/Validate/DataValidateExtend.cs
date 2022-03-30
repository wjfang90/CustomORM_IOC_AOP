using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Custom.Framework.Validate
{
    public static class DataValidateExtend
    {
        public static bool ValidateData<T>(this T model)  //where T:BaseModel,new()  //引用Model项目会引发循环引用，这里没有添加约束；可把BaseModel类放在一个单独的项目
        {
            foreach (var prop in typeof(T).GetProperties().Where(t => t.IsDefined(typeof(CustomBaseValidateAttribute), true)))
            {
                var value = prop.GetValue(model) ?? null;
                var attributes = prop.GetCustomAttributes<CustomBaseValidateAttribute>();//一个属性可配置多个特性

                foreach (var attribute in attributes)
                {
                    if (!attribute.Validate(value))
                        return false;
                }
            }
            return true;
        }

        #region 待优化代码
        private static bool ValidateDataOld<T>(this T model)  //where T:BaseModel,new()  //引用Model项目会引发循环引用，这里没有添加约束；可把BaseModel类放在一个单独的项目
        {
            foreach (var prop in typeof(T).GetProperties())
            {
                //优化去掉多个if判断
                if (prop.IsDefined(typeof(CustomRequiredAttribute), true))
                {
                    object oValue = prop.GetValue(model);
                    var attribute = prop.GetCustomAttribute<CustomRequiredAttribute>();
                    if (!attribute.Validate(oValue))
                    {
                        return false;
                    }
                    //优化到自己的Attribute中的方法里
                    //if (oValue == null || string.IsNullOrWhiteSpace(oValue.ToString()))
                    //{
                    //    return false;
                    //}
                }


                if (prop.IsDefined(typeof(CustomLengthAttribute), true))
                {
                    object oValue = prop.GetValue(model);
                    var attribute = prop.GetCustomAttribute<CustomLengthAttribute>();
                    if (!attribute.Validate(oValue))
                    {
                        return false;
                    }

                    //优化到自己的Attribute中的方法里
                    //if (oValue == null || string.IsNullOrWhiteSpace(oValue.ToString())
                    //    || oValue.ToString().Length < attribute._iMin
                    //     || oValue.ToString().Length >= attribute._iMax)
                    //{
                    //    return false;
                    //}
                }
            }

            return true;
        }

        #endregion

    }
}
