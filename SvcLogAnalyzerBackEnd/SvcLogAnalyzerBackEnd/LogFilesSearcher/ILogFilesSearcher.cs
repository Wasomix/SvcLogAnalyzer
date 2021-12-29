using System;
using System.Collections.Generic;
using System.Text;

namespace SvcLogAnalyzerBackEnd
{
    public interface ILogFilesSearcher
    {
        public List<string> GetFileNamesContainingPattern();
    }
}
