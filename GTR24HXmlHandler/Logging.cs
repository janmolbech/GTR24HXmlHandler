using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace GTR24HXmlHandler
{
    public static class Logging
    {
        public static void AddLogEntry(string logMessage)
        {
            string path = @"C:\GTR24HLogs\errorlog.txt";
            bool dirExists = Directory.Exists(@"C:\GTR24HLogs");

            if (!dirExists) {
                Directory.CreateDirectory(@"C:\GTR24HLogs");
            }
            using (var tw = new StreamWriter(path,true))
            {
                if (!File.Exists(path))
                {
                    File.Create(path);

                    Log(logMessage, tw);
                    
                }
                else if (File.Exists(path))
                {

                    Log(logMessage, tw);
                   
                }
            }
        }
        
        public static void Log(string logMessage, TextWriter w)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToLongDateString());
            w.WriteLine("  :");
            w.WriteLine("  :{0}", logMessage);
            w.WriteLine("-------------------------------");
        }
    }
}
