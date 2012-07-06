using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace trx2html.Test
{
    /// <summary>
    /// Summary description for FailAndIgnored
    /// </summary>
    [TestClass]
    public class FailAndIgnored
    {
        public FailAndIgnored()
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

        [TestMethod, Description("TestMethodThatFailsDescription")]
        public void TestMethodFail2()
        {
            Assert.Fail("Test fail");
        }

        [TestMethod, Description("TestMethodThatIsIconclusive")]
        public void TestMethod4()
        {
            Assert.AreEqual(1, 1);
            Assert.Inconclusive("Inconclusive");
        }
        [TestMethod, Description("TestMethodThatIsIconclusive")]
        public void TestMethod5()
        {
            Assert.AreEqual(1, 1);
            Assert.Inconclusive("Inconclusive");
        }
        [TestMethod, Description("TestMethodThatIsIconclusive")]
        public void TestMethod6()
        {
            Assert.AreEqual(1, 1);
            Assert.Inconclusive("Inconclusive");
        }


    }
}
