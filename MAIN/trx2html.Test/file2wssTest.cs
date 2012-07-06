using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using RidoTasks;

namespace trx2html.Test
{    
    [TestClass()]
    public class file2wssTest
    {

        

        
        [TestMethod()]
        public void file2wssConstructorTest()
        {
            file2wss target = new file2wss();
            Assert.IsNotNull(target);
        }

        [DeploymentItem(@"trx2html.Test\TestFiles\VS2010.trx.xml")]
        [TestMethod()]
        public void ExecuteTest()
        {
            file2wss target = new file2wss(); // TODO: Initialize to an appropriate value
            target.FileName = @"VS2010.trx.xml";

            target.TargetUrl = @"https://vstf-eu-dub-01.partners.extranet.microsoft.com/sites/EXT04_Consol_TPC/TM3/Bits/VS2010.trx.xml";
            bool actual = target.Execute();
            Assert.IsTrue(actual);
        }

        

    
    }
}
