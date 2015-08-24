using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GTR24HXmlHandler
{
    public static class HelperMethods
    {
        public static bool WaitReady(string fileName)
        {
            var times = 0;
            while (times < 10) 
            {
                try
                {
                    using (Stream stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                    {
                        if (stream != null)
                        {
                            Logging.AddLogEntry(string.Format("Output file {0} ready.", fileName));
                            return true;
                        }
                    }
                }
                catch (FileNotFoundException ex)
                {
                   Logging.AddLogEntry(string.Format("Output file {0} not yet ready ({1})", fileName, ex.Message));
                }
                catch (IOException ex)
                {
                    Logging.AddLogEntry(string.Format("Output file {0} not yet ready ({1})", fileName, ex.Message));
                }
                catch (UnauthorizedAccessException ex)
                {
                    Logging.AddLogEntry(string.Format("Output file {0} not yet ready ({1})", fileName, ex.Message));
                }
                Thread.Sleep(500);
            }
            return false;
        }
    }
}
