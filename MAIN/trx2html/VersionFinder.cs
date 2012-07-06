using System;

using System.Xml;


namespace trx2html
{
    internal enum SupportedFormats
    {
        unknown, vs2005, vs2008, vs2010
    }

    internal sealed class VersionFinder
    {


        internal SupportedFormats GetFileVersion(string file)
        {
            SupportedFormats result = SupportedFormats.unknown;

            using (XmlReader r = XmlReader.Create(file))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(r);

                if (CheckVersion(doc.DocumentElement))
                {
                    result = SupportedFormats.vs2005;
                }
                if (IsVS2008(doc.DocumentElement))
                {
                    result = SupportedFormats.vs2008;
                }
                if (IsVS2010(doc.DocumentElement))
                {
                    result = SupportedFormats.vs2010;
                }
            }
            return result;
        }


        bool CheckVersion(XmlElement e)
        {
            try
            {
                if (e.Name == "Tests" &&
                    e.ChildNodes[0].Name == "edtdocversion" &&
                    Convert.ToInt32(e.ChildNodes[0].Attributes["build"].Value) > 50726)
                {
                    return true;
                }
            }
            catch { }
            return false;
        }

        bool IsVS2008(XmlElement e)
        {
            try
            {
                if (e.Name == "TestRun" &&
                    e.NamespaceURI == "http://microsoft.com/schemas/VisualStudio/TeamTest/2006")
                    return true;
            }
            catch { }
            return false;
        }

        bool IsVS2010(XmlElement e)
        {
            try
            {
                if (e.Name == "TestRun" &&
                    e.NamespaceURI == "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")
                    return true;
            }
            catch { }
            return false;
        }
    }
}