using System;
using System.Collections.Generic;
using System.IO;
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
        private SvcLogAnalyzerBEDataConfig _svcLogAnalyzerBEDataConfig;
        public AutomaticalFileNameSearch(SvcLogAnalyzerBEDataConfig svcLogAnalyzerBEDataConfig)
        {
            _fileNamesToSearchOn = new List<string>();
            _svcLogAnalyzerBEDataConfig = svcLogAnalyzerBEDataConfig;
        }
        public List<string> GetFileNamesToSearchInAFolder()
        {
            FindFilesAutomatically();
            return _fileNamesToSearchOn;
        }

        private void FindFilesAutomatically()
        {
            DirectoryInfo d = new DirectoryInfo(_svcLogAnalyzerBEDataConfig.FilesPath);
            FileInfo[] Files = d.GetFiles(_svcLogAnalyzerBEDataConfig.TypeOfFile); 

            foreach (var file in Files)
            {
                _fileNamesToSearchOn.Add(file.Name);
            }
        }
    }
}
