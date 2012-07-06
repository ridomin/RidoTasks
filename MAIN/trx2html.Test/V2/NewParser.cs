using System;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace trx2html.Test.V2
{
    [TestClass]
    public class NewParser
    {
        string GetSafeValue(XElement el, XName name)
        {
            string result = string.Empty;
            if (null!=el.Element(name))
            {
                result = el.Element(name).Value;
            }
            return result;
        }

        string GetSafeAttrValue(XElement el, XName name, XName atribName)
        {
            string result = string.Empty;
            if (null != el.Element(name) && null != el.Element(name).Attribute(atribName))
            {
                result = el.Element(name).Attribute(atribName).Value;
            }
            return result;
        }


        [TestMethod]
        [DeploymentItem(@"trx2html.Test\TestFiles\VS2010.trx.xml")]
        public void PlayWithLINQ()
        {

            XDocument doc = XDocument.Load("VS2010.trx.xml");
            XNamespace ns = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010";

            var unitTests = doc.Descendants(ns + "UnitTest").ToList<XElement>();
            Console.WriteLine(unitTests.Count);
            var unitTestResults = doc.Descendants(ns + "UnitTestResult").ToList<XElement>();
            Console.WriteLine(unitTestResults.Count);

            var result = from u in unitTests
                         let id = u.Element(ns + "Execution").Attribute("id").Value
                         let desc = GetSafeValue(u, ns + "Description")
                         let testClass = GetSafeAttrValue(u, ns + "TestMethod", "className")
                         join r in unitTestResults
                         on id equals r.Attribute("executionId").Value
                         orderby testClass
                         select new
                         {
                             Clase = testClass,
                             Test = u.Attribute("name").Value,
                             Description = desc,
                             Status = r.Attribute("outcome").Value
                         };


            var gresult = result.GroupBy(c => c.Clase);

            foreach (var test in gresult)
            {
                Console.WriteLine(test.Key);
                foreach (var method in test)
                {
                    Console.WriteLine("\t " + method.Test + " " + method.Description);
                }
            }
        }
    }
}
