using System;
using System.Collections.Generic;
using System.Text;

namespace Custom.Framework.CustomAOP
{
    public abstract class CustomBaseAttribute : Attribute
    {
        public int Sort { get; set; }
        public abstract Action Handle(Action action);
    }
}
