using System;
using System.Collections.Generic;
using System.Text;

namespace SvcLogAnalyzerBackEnd
{
    public interface ILog
    {
        public void WriteLogDebug(string message);
        public void WriteLogInfo(string message);
        public void WriteLogError(string message);
    }
}
