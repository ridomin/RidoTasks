using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using trx2html.Parser;
using System.IO;
using System.Diagnostics;

namespace trx2html.Test.V2
{
    [TestClass]      
    public class TrxParserTest
    {
        [TestMethod]
        [DeploymentItem(@"trx2html.Test\TestFiles\VS2010.trx.xml")]
        public void ParseVS10SampleFile()
        {
            TrxParser parser = new TrxParser();
            var result = parser.Parse("VS2010.trx.xml");
            Assert.AreEqual("Sample", result.Name);
            Assert.AreEqual(1, result.Computers.Count());
            Assert.AreEqual("RIDOHP", result.Computers.First());
            Assert.AreEqual(@"RIDOHP\rido", result.UserName);
            Assert.AreEqual(18, result.TestMethodRunList.Count(), "No se ha calculado el número total de TestMethods");
            Assert.AreEqual(18, result.TotalMethods, "No se ha calculado el número total de TestMethods");
            Assert.AreEqual(6,result.Passed, "No se ha calculado el número total de TestMethods OK");
            Assert.AreEqual(7, result.Failed, "No se ha calculado el número total de TestMethods Failed");
            Assert.AreEqual(5, result.Inconclusive, "No se ha calculado el número total de TestMethods Ignored");
            Assert.AreEqual(1, result.Assemblies.Count());
            Assert.AreEqual("SampleReport, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", result.Assemblies.First().FullName);
            Assert.AreEqual(TimeSpan.Parse("00:00:00.0537262"), result.TimeTaken);

            Assert.AreEqual(33, result.PercentOK, "no se ha calculado el PercentOK");
            Assert.AreEqual(39, result.PercentKO, "no se ha calculado el PercentKO");
            Assert.AreEqual(28, result.PercentIgnored, "no se ha calculado el PercentIgnored");

            Assert.AreEqual(56, result.TotalPercent);

            Assert.AreEqual(6, result.TestClassList.Count);            
            AssertAllFailed(result);
            AssertSomeFailed(result);
            AssertAllPassed(result);
            AssertFailAndIgnored(result);

        }

        private static void AssertAllFailed(TestRunResult result)
        {
            TestClassRun tcr = result.TestClassList.First(t => t.Name == "trx2html.Test.AllFailed");
            Assert.AreEqual("trx2html.Test.AllFailed", tcr.Name, "No coincide el nombre del TestClass");
            //Assert.AreEqual("trx2html.Test.AllFailed, trx2html.Test, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null",
            //                    tcr.FullName, "No coincide el nombre del TestClass");
            Assert.AreEqual(TimeSpan.Parse("00:00:00.1596216"), tcr.Duration, "No se ha calculado la duración");
            Assert.AreEqual(3, tcr.Failed, "No se ha calculado los fallos");
            Assert.AreEqual(0, tcr.Ignored, "No se ha calculado los ignorados");
            Assert.AreEqual(0, tcr.Percent, "No se ha calculado El %");
            Assert.AreEqual("Failed", tcr.Status, "No se ha calculado el status");
            Assert.AreEqual(0, tcr.Success, "No se ha calculado el exito");
            Assert.AreEqual("trx2html.Test, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null", tcr.AssemblyName.FullName);
        }

        private static void AssertSomeFailed(TestRunResult result)
        {
            TestClassRun tcr = result.TestClassList.First(t => t.Name == "trx2html.Test.SomeFailed");
            Assert.AreEqual("trx2html.Test.SomeFailed", tcr.Name, "No coincide el nombre del TestClass");
            Assert.AreEqual("trx2html.Test.SomeFailed, trx2html.Test, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null",
                                tcr.FullName, "No coincide el nombre del TestClass");
            Assert.AreEqual(TimeSpan.Parse("00:00:00.0031813"), tcr.Duration, "No se ha calculado la duración");
            Assert.AreEqual(3, tcr.TestMethods.Count(), "No se ha calculado el total");
            Assert.AreEqual(3, tcr.Total, "No se ha calculado el total");
            Assert.AreEqual(2, tcr.Failed, "No se ha calculado los fallos");
            Assert.AreEqual(0, tcr.Ignored, "No se ha calculado los ignorados");
            Assert.AreEqual(33.33 , tcr.Percent, "No se ha calculado El %");
            Assert.AreEqual("Failed", tcr.Status, "No se ha calculado el status");
            Assert.AreEqual(1, tcr.Success, "No se ha calculado el exito");
            Assert.AreEqual("trx2html.Test, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null", tcr.AssemblyName.FullName);
        }

        private static void AssertAllPassed(TestRunResult result)
        {
            TestClassRun tcr = result.TestClassList.First(t => t.Name == "trx2html.Test.AllPassed");
            Assert.AreEqual("trx2html.Test.AllPassed", tcr.Name, "No coincide el nombre del TestClass");
            Assert.AreEqual("trx2html.Test.AllPassed, trx2html.Test, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null",
                                tcr.FullName, "No coincide el nombre del TestClass");
            Assert.AreEqual(TimeSpan.Parse("00:00:00.0010958"), tcr.Duration, "No se ha calculado la duración");
            Assert.AreEqual(3, tcr.TestMethods.Count(), "No se ha calculado el total");
            Assert.AreEqual(3, tcr.Total, "No se ha calculado el total");
            Assert.AreEqual(0, tcr.Failed, "No se ha calculado los fallos");
            Assert.AreEqual(0, tcr.Ignored, "No se ha calculado los ignorados");
            Assert.AreEqual(100.00, tcr.Percent, "No se ha calculado El %");
            Assert.AreEqual("Succeed", tcr.Status, "No se ha calculado el status");
            Assert.AreEqual(3, tcr.Success, "No se ha calculado el exito");
            Assert.AreEqual("trx2html.Test, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null", tcr.AssemblyName.FullName);
        }

        private static void AssertFailAndIgnored(TestRunResult result)
        {
            TestClassRun tcr = result.TestClassList.First(t => t.Name == "trx2html.Test.FailAndIgnored");
            Assert.AreEqual("trx2html.Test.FailAndIgnored", tcr.Name, "No coincide el nombre del TestClass");
            Assert.AreEqual("trx2html.Test.FailAndIgnored, trx2html.Test, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null",
                                tcr.FullName, "No coincide el nombre del TestClass");
            Assert.AreEqual(TimeSpan.Parse("00:00:00.0061758"), tcr.Duration, "No se ha calculado la duración");
            Assert.AreEqual(5, tcr.TestMethods.Count(), "No se ha calculado el total");
            Assert.AreEqual(5, tcr.Total, "No se ha calculado el total");
            Assert.AreEqual(2, tcr.Failed, "No se ha calculado los fallos");
            Assert.AreEqual(3, tcr.Ignored, "No se ha calculado los ignorados");
            Assert.AreEqual(0.0, tcr.Percent, "No se ha calculado El %");
            Assert.AreEqual("Failed", tcr.Status, "No se ha calculado el status");
            Assert.AreEqual(0, tcr.Success, "No se ha calculado el exito");
            Assert.AreEqual("trx2html.Test, Version=0.0.4.0, Culture=neutral, PublicKeyToken=null", tcr.AssemblyName.FullName);
        }


        [TestMethod]
        [DeploymentItem(@"trx2html.Test\TestFiles\VS2010.trx.xml")]
        public void GroupedByClass()
        {
            TrxParser parser = new TrxParser();
            var result = parser.Parse("VS2010.trx.xml");
            var grouped = result.TestMethodRunList.GroupBy(c => c.TestClass);
            Assert.AreEqual(6, grouped.Count());
            var group = grouped.ElementAt(0);
            Assert.AreEqual(1, group.Count());
        }

        [TestMethod]
        [DeploymentItem(@"trx2html.Test\TestFiles\VS2010.trx.xml")]
        public void GroupedByStatus()
        {
            TrxParser parser = new TrxParser();
            var result = parser.Parse("VS2010.trx.xml");
            var grouped = result.TestMethodRunList.GroupBy(c => c.Status);
            Assert.AreEqual(4, grouped.Count());
            var group = grouped.ElementAt(0);
            Assert.AreEqual(1, group.Count());
        }

        [TestMethod]
        [DeploymentItem(@"trx2html.Test\TestFiles\VS2010.trx.xml")]
        public void CreateHtmlFile()
        {
            TrxParser parser = new TrxParser();
            var result = parser.Parse("VS2010.trx.xml");
            HtmlConverter html = new HtmlConverter(result);
            string fileName = Path.Combine(System.Environment.CurrentDirectory, @"newTrx2Html.html");
            using (TextWriter file = File.CreateText(fileName))
            {
                file.Write(html.GetHtml());
            }
            Process.Start("IExplore.exe", fileName);
        }

    }
}
