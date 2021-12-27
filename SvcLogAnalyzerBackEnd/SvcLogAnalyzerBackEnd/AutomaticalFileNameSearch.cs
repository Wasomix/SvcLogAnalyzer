using System;
using System.Collections.Generic;
using System.Text;

namespace SvcLogAnalyzerBackEnd
{    
    /// <summary>
    /// This class is responsible for searching all files from a determined 
    /// type in a folder.
    /// </summary>
    public class AutomaticalFileNameSearch : IFileNamesToSearchOn
    {
        private List<string> _fileNamesToSearchOn;
        public AutomaticalFileNameSearch()
        {
            _fileNamesToSearchOn = new List<string>();
        }
        public List<string> GetFileNamesToSearchInAFolder(SvcLogAnalyzerBEDataConfig svcLogAnalyzerBEDataConfig)
        {
            FindFiles();
            return _fileNamesToSearchOn;
        }

        private void FindFiles()
        {
            for (int i = INITIAL_VALUE; i < NUMBER_OF_FILES; i++)
            {
                string fileNameToAdd = _configuration.PrefixName + i.ToString() + _configuration.SufixName;
                _fileNamesToSearchOn.Add(fileNameToAdd);
            }
        }
    }
}
