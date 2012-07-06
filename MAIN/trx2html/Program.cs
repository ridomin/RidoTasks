using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Reflection;
using trx2html.Parser;
namespace trx2html
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("trx2html.exe \n  Create HTML reports of VSTS TestRuns. (c)rido'11");
            Console.WriteLine("version:" + Assembly.GetExecutingAssembly().GetName().Version.ToString()+ "\n");
            if (args.Length != 1)
            {
                Console.WriteLine("Usage: trx2html <TestResult>.trx");
                return;
            }

            string fileName = args[0];
            ReportGenerator.GenerateReport(fileName);
        }

    }
}
