using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using trx2html.Parser;
using System.IO;

namespace trx2html
{
    internal class ReportGenerator
    {
        internal static void GenerateReport(string fileName)
        {
            VersionFinder v = new VersionFinder();
            SupportedFormats f = v.GetFileVersion(fileName);
            if (f != SupportedFormats.vs2010)
            {
                Console.WriteLine("File {0} is not a recognized as a valid trx. Only VS2010 are supported", fileName);
            }
            else
            {
                Console.WriteLine("Processing {0} trx file", f.ToString());

                TrxParser parser = new TrxParser();
                TestRunResult r = parser.Parse(fileName);
                string html = new HtmlConverter(r).GetHtml();

                using (TextWriter file = File.CreateText(fileName + ".htm"))
                {
                    file.Write(html);
                }

                Console.WriteLine("Tranformation Succeed. OutputFile: " + fileName + ".htm\n");
            }
        }
    }
}
