using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace Img2B64
{
    class Program
    {
        public static void Main(string[] args)
        {
            new Program().Run();
        }

        void Run()
        {
            var f = "input.png";
            byte[] buff = null;
            using (FileStream fs = new FileStream(f, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    long numBytes = new FileInfo(f).Length;
                    buff = br.ReadBytes((int)numBytes);            
                }
            }
            
            string res = Convert.ToBase64String(buff);
            var fout =  File.CreateText(@".\output.png.b64.txt");
            fout.Write(res);
            fout.Close();
        }

        
    }
}
