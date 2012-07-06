using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace trx2html.Parser
{
    internal class TrxParser
    {
        XNamespace ns = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010";


        ErrorInfo ParseErrorInfo(XElement r)
        {
            ErrorInfo err = new ErrorInfo();
            if (r.Element(ns + "Output") != null && 
                r.Element(ns + "Output").Element(ns + "ErrorInfo") != null &&
                r.Element(ns + "Output").Element(ns + "ErrorInfo").Element(ns + "Message") != null )
            {
                err.Message = r.Element(ns + "Output").Element(ns + "ErrorInfo").Element(ns + "Message").Value;
                
            }

            if (r.Descendants(ns + "StackTrace").Count()> 0 )
            {
                err.StackTrace = r.Descendants(ns + "StackTrace").FirstOrDefault().Value;
            }

            if (r.Descendants(ns + "DebugTrace").Count() > 0)
            {
                err.StdOut = r.Descendants(ns + "DebugTrace").FirstOrDefault().Value.Replace("\r\n", "<br />");
            }

            return err;
        }

        string GetSafeValue(XElement el, XName name)
        {
            string result = string.Empty;
            if (null != el.Element(name))
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

        TimeSpan ParseDuration(XElement el, string attName)
        {
            TimeSpan result = new TimeSpan(0);
            if (el.Attribute(attName) != null)
            {
                result = TimeSpan.Parse(el.Attribute(attName).Value);
            }

            return result;
        }
        

        public TestRunResult Parse(string filePath)
        {
            XDocument doc = XDocument.Load(filePath);
            XNamespace ns = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010";

            string name = doc.Document.Root.Attribute("name").Value;
            string runUser = doc.Document.Root.Attribute("runUser").Value;
            var unitTests = doc.Descendants(ns + "UnitTest").ToList<XElement>();           
            var unitTestResults = doc.Descendants(ns + "UnitTestResult").ToList<XElement>();
            var result = from u in unitTests
                         let id = u.Element(ns + "Execution").Attribute("id").Value
                         let desc = GetSafeValue(u, ns + "Description")
                         let testClass = GetSafeAttrValue(u, ns + "TestMethod", "className")
                         join r in unitTestResults
                         on id equals r.Attribute("executionId").Value
                         orderby testClass
                         select new TestMethodRun
                         {
                             TestClass = testClass,
                             TestMethodName = u.Attribute("name").Value,
                             Description = desc,
                             Status = r.Attribute("outcome").Value,
                             Duration = ParseDuration(r,"duration"),
                             ErrorInfo = ParseErrorInfo(r),
                             ComputerName = r.Attribute("computerName").Value
                         };


            return new TestRunResult(name, runUser, result);
        }
    }
}
