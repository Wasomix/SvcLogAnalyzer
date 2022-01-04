using System;
using System.IO;

namespace SvcLogAnalyzerBackEnd
{
    public class Log4Wrapper : ILog
    {
        private string _fileName = "SvcLogAnalyzerBELog.log";

        public Log4Wrapper(/*SvcLogAnalyzerBEDataConfig configuration*/)
        {
            FileWrapper.DeleteFileIfItExist( _fileName );
        }

        public void WriteLogDebug(string message)
        {
            string debugMessage = "[DEBUG] " + message; 
            WriteLog4Wrapper(debugMessage);
        }

        public void WriteLogInfo(string message)
        {
            string debugMessage = "[DEBUG] " + message;
            WriteLog4Wrapper(debugMessage);
        }
        public void WriteLogError(string message)
        {
            string debugMessage = "[DEBUG] " + message;
            WriteLog4Wrapper(debugMessage);
        }

        private void WriteLog4Wrapper(string message)
        {
            string logMessage = message + "\n";
            File.AppendAllText(_fileName, logMessage);
        }
    }
}
