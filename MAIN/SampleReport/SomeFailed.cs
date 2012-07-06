using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace trx2html.Test
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class SomeFailed
    {
        public SomeFailed()
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

        [TestMethod, Description("TestMethodThatFailsDescription")]
        public void TestMethodFail()
        {
            Assert.Fail("Test fail");
        }
        [TestMethod, Description("TestMethodThatPassDescription")]
        public void TestMethod2()
        {
            Assert.AreEqual(1,1);
        }
        [TestMethod, Description("TestMethodThatFailDescription")]
        public void TestMethod3()
        {
            Assert.Fail("Test failed");
        }
    }
}
