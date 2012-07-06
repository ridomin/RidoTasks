using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace trx2html.Test
{
    /// <summary>
    /// Summary description for SomeIgnored
    /// </summary>
    [TestClass]
    public class SomeIgnored
    {
        public SomeIgnored()
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

        [TestMethod, Description("TestMethodThatPassDescription")]
        public void TestMethod1()
        {
            Console.WriteLine("sample tracelog from ConsoleWriteLine");
            Assert.AreEqual(1, 1);
        }

        [TestMethod, Description("TestMethodThatPassDescription")]
        public void TestMethod2()
        {
            System.Diagnostics.Debug.WriteLine("sample tracelog from Debug.Writeline");
            Assert.AreEqual(1, 1);
        }

        [TestMethod, Description("TestMethodThatIsIgnored")]
        [Ignore]
        public void TestMethod3()
        {
            Console.Error.WriteLine("error tracelog from console.error");
            Assert.AreEqual(1, 1);
        }

        [TestMethod, Description("TestMethodThatIsIconclusive")]
        public void TestMethod4()
        {
            Assert.AreEqual(1, 1);
            Assert.Inconclusive("Inconclusive Message");
        }
    }
}
