using System;
using System.Collections.Generic;
using System.Text;

namespace SvcLogAnalyzerBackEnd
{
    public class SvcLogAnalyzerBEDataConfig
    {
        public string LogFilesPath { get; set; }
        public string PatternToSearch { get; set; }
        public string TypeOfFile { get; set; }
        public string NameOfFileContainingPattern { get; set; }
    }
}
