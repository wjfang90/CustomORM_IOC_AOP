using Custom.Framework.CustomAOP.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custom.IBLL.IOC
{
    public interface IInjectWithAOPBLL
    {
        [CustomLogger(1)]
        [CustomException(2)]
        void Show();

        void Test();
    }
}
