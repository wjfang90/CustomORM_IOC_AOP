using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Custom.ORM_IOC_AOP_Test
{
    class Program
    {
        static void Main(string[] args)
        {

            #region ORM

            //CustomORMTest.queryOne();

            //CustomORMTest.Insert();

            //CustomORMTest.ValidateData();

            //CustomORMTest.Update();

            //CustomORMTest.Delete();

            //CustomORMTest.QueryListWhere();

            #endregion

            #region IOC

            //CustomIOCTest.CustomConstructorInjectNoParameterTest();

            //CustomIOCTest.CustomConstructorInjectMultitudeParameterTest();

            //CustomIOCTest.CustomConstructorInjectWithAttributeTest();

            //CustomIOCTest.CustomPropertyInjectWithAttributeTest();

            //CustomIOCTest.CustomMethodInjectWithAttributeTest();

            //CustomIOCTest.CustomAilasInjectWithNameTest();

            //CustomIOCTest.CustomAilasInjectWithNameMultitudeLevelTest();

            //CustomIOCTest.CustomConstructorConstantParameterInjectTest();


            //CustomIOCTest.CustomContainerLifeTimeDefaultTest();

            //CustomIOCTest.CustomContainerLifeTimeTransientTest();

            //CustomIOCTest.CustomContainerLifeTimeSingletonTest();

            //CustomIOCTest.CustomContainerLifeTimeScopeTest();

            //CustomIOCTest.CustomContainerLifeTimePreThreadTest();

            //CustomIOCTest.TestAsyncLocal();

            //CustomIOCTest.CustomContainerAOPTest();


            #endregion IOC


            #region AOP

            //CustomAOPTest.AOPClassVisualMethodInjectTest();

            //CustomAOPTest.AOPInterfaceInjectAllMethodsTest();

            //CustomAOPTest.AOPInterfaceInjectWithAttribureTest();

            //CustomAOPTest.AOPInterfaceInjectWithMultitudeAttribureTest();

            //CustomAOPTest.AOPInterfaceInjectWithMultitudeAttribureByActionTest();

            CustomIOCTest.CustomContainerAOPTest();

            #endregion



            Console.ReadKey();
        }
    }
}
