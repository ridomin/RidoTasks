using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using trx2html.Parser;

namespace trx2html.Test.V2
{
    [TestClass]
    public class NotExecutedTrxFixture
    {
        [TestMethod]
        [DeploymentItem(@"trx2html.Test\TestFiles\NotRunnable.trx.xml")]
        public void CanParseNotRunnable()
        {
            TrxParser parser = new TrxParser();
            var result = parser.Parse("NotRunnable.trx.xml");
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.PercentOK);
            Assert.AreEqual(0, result.PercentKO);
            Assert.AreEqual(100, result.PercentIgnored);
            Assert.AreEqual(1, result.Inconclusive);
        }

        [TestMethod]
        [DeploymentItem(@"trx2html.Test\TestFiles\Aborted.trx.xml")]
        public void CanParseAborted()
        {
            TrxParser parser = new TrxParser();
            var result = parser.Parse("Aborted.trx.xml");
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.PercentOK);
            Assert.AreEqual(0, result.PercentKO);
            Assert.AreEqual(100, result.PercentIgnored);
            Assert.AreEqual(1, result.Inconclusive);
        }
    }
}
