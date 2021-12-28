using System;
using System.Collections.Generic;
using System.Text;

namespace SvcLogAnalyzerBackEnd
{
    public class SvcLogAnalyzerBEDataConfig
    {
        public string FilesPath { get; set; }
        public string PatternToSearch { get; set; }
        public string TypeOfFile { get; set; }
    }
}
