using System;
using System.Collections.Generic;
using System.Text;

namespace Custom.Framework.CustomContainer
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
    public class CustomAilasInjectAttribute : Attribute
    {
        private string ailasName;

        public CustomAilasInjectAttribute(string ailasName)
        {
            this.ailasName = ailasName;
        }

        public string GetAilasName()
        {
            return ailasName;
        }
    }
}
