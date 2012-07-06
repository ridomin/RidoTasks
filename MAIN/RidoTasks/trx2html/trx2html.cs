using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Xsl;
using System.Collections.Generic;
using System.Text;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using trx2html;

namespace RidoTasks
{
    public class trx2html : Task
    {
        private string fileName;

        [Required]
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        public override bool Execute()
        {
            LogHeaderMessage();

            if (!File.Exists(fileName))
            {
                Log.LogError("TRX File not found {0}", fileName);
                return false;
            }

            try
            {
                Log.LogMessage("Creating HTML Report from TRX file: {0}", fileName);
                ReportGenerator.GenerateReport(fileName);
                
                return true;
            }
            catch (Exception ex)
            {               
                Log.LogErrorFromException(ex);
                throw;
            }
        }

        private void LogHeaderMessage()
        {
            Log.LogMessage("trx2html  Creates HTML reports of VSTS TRX TestResults . (c)rido'11");
            Log.LogMessage("version: {0} \n", Assembly.GetExecutingAssembly().GetName().Version.ToString());            
        }     
    }
}
