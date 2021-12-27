using System;
using System.Collections.Generic;
using System.Text;

namespace SvcLogAnalyzerBackEnd
{
    public class SvcLogAnalyzerBEDataConfig
    {
        public string PrefixName { get; set; }
        public string SufixName { get; set; } 
        public string FilePath { get; set; }
        public string PatternToSearch { get; set; }
        public string TypeOfFile { get; set; }
    }
}
