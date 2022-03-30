using System;
using System.Collections.Generic;
using System.Text;

namespace Custom.Framework.CustomContainer
{
    [AttributeUsage(AttributeTargets.Constructor)]
    public class CustomConstructorInjectAttribute : Attribute
    {
    }
}
