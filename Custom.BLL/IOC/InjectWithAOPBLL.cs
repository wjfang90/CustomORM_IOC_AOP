using Custom.IBLL.IOC;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custom.BLL.IOC
{
    public class InjectWithAOPBLL : IInjectWithAOPBLL
    {
        public void Show()
        {
            Console.WriteLine($"This is {nameof(InjectWithAOPBLL)}.{nameof(Show)}");
        }

        public void Test()
        {
            Console.WriteLine($"This is {nameof(InjectWithAOPBLL)}.{nameof(Test)}");
        }
    }
}
