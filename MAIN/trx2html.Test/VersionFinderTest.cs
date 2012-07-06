using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;
using System.Xml;

namespace trx2html.Test
{
    
    [TestClass]
    public class VersionFinderTest
    {
        [TestMethod]
        [DeploymentItem(@"trx2html.Test\TestFiles")]        
        public void GetVersions()
        {
            VersionFinder v = new VersionFinder();
            Assert.AreEqual(SupportedFormats.vs2005, v.GetFileVersion("VS2005.trx.xml"));
            Assert.AreEqual(SupportedFormats.vs2008, v.GetFileVersion("VS2008.trx.xml"));
            Assert.AreEqual(SupportedFormats.vs2010, v.GetFileVersion("VS2010.trx.xml"));
        }

    }
}
