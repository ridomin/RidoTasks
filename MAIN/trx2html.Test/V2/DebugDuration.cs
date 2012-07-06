using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using trx2html.Parser;

namespace trx2html.Test.V2
{
    [TestClass]
    public class DebugDuration
    {
        [TestMethod]
        [DeploymentItem(@"trx2html.Test\TestFiles\Duration.trx.xml")]
        public void TestMethod1()
        {
            var res = new TrxParser().Parse("Duration.trx.xml");
            Assert.IsTrue(res.TimeTaken.TotalMilliseconds > 0);
        }
    }
}
