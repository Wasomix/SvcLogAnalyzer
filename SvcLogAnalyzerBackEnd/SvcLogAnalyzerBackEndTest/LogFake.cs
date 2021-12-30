using SvcLogAnalyzerBackEnd;
using System;

namespace SvcLogAnalyzerBackEndTest
{
    public class LogFake : ILog
    {
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
            Console.WriteLine(message);
        }
    }
}
