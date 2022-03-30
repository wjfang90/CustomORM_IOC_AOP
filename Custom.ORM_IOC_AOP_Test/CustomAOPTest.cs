using Custom.Framework.CustomAOP.CastleTest;
using Custom.Framework.CustomAOP.CastleTest.InterfaceInject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custom.ORM_IOC_AOP_Test
{

    /*
         AOP 的功能是 在不修改原来代码的前提下，增加业务功能
    */

    public class CustomAOPTest
    {
        public static void AOPClassVisualMethodInjectTest()
        {
            AOPTypeClassVisualMethodInject.AOPTest();
        }
        public static void AOPInterfaceInjectAllMethodsTest()
        {
            AOPInterfaceInjectAllMethods.AOPTest();
        }

        public static void AOPInterfaceInjectWithAttribureTest()
        {
            AOPInterfaceInjectWithAttribure.AOPTest();
        }

        public static void AOPInterfaceInjectWithMultitudeAttribureTest()
        {
            AOPInterfaceInjectWithMultitudeAttribure.AOPTest();
        }

        public static void AOPInterfaceInjectWithMultitudeAttribureByActionTest()
        {
            AOPInterfaceInjectWithMultitudeAttribureByAction.AOPTest();
        }
    }
}
