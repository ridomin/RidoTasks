using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Build.Utilities;
using Microsoft.Build.Framework;
using System.IO;
using System.Net;
using System.Reflection;

namespace RidoTasks
{
    public class file2wss : Task
    {

        private string fileName;

        [Required]
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        [Required]
        public string TargetUrl { get; set; }

        public override bool Execute()
        {
            return UploadDocument(fileName, TargetUrl);
        }

        private  bool UploadDocument(string source, string remoteFile)
        {
            Log.LogMessage("Start uploading {0} to {1} \r\n", source, remoteFile);
            bool result = false;
            try
            {
                using (FileStream stream = File.OpenRead(source))
                {
                    byte[] buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);
                    WebRequest request = WebRequest.Create(remoteFile);
                    request.Credentials = CredentialCache.DefaultCredentials;
                    request.Method = "PUT";
                    request.ContentLength = buffer.Length;
                    using (BinaryWriter writer = new BinaryWriter(request.GetRequestStream()))
                    {
                        writer.Write(buffer, 0, buffer.Length);
                        writer.Close();
                    }
                    ((HttpWebResponse)request.GetResponse()).Close();
                }

                Log.LogMessage("OK \r\n");
                result = true;
            }
            catch (Exception ex)
            {
                Log.LogErrorFromException(ex);
                throw;
              
            }
            
            return result;
            
        }

        private void LogHeaderMessage()
        {
            Log.LogMessage("file2wss  Upload Files to Windows Sharepoint Lists . (c)rido'11");
            Log.LogMessage("version: {0} \n", Assembly.GetExecutingAssembly().GetName().Version.ToString());
        }     
    }
}
