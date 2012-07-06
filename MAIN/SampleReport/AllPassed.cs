using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace trx2html.Test
{
    /// <summary>
    /// Summary description for AllPassed
    /// </summary>
    [TestClass]
    public class AllPassed
    {
        public AllPassed()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod, Description("TestMethod1Description")]
        public void TestMethod1()
        {
            Console.Out.WriteLine("tracelog from console.out");
            Console.Error.WriteLine("tracelog from error.out");
            Trace.WriteLine("tracelog from trace");
            Trace.TraceInformation("traceinfo from trace");
            System.Diagnostics.Debug.WriteLine("tracelog from debug");
        }
        [TestMethod,Description("TestMethod2Description")]
        public void TestMethod2()
        {
            //
            // TODO: Add test logic	here
            //
        }
        [TestMethod, Description("TestMethod3Description")]
        public void TestMethod3()
        {
            //
            // TODO: Add test logic	here
            //
        }
    }
}
