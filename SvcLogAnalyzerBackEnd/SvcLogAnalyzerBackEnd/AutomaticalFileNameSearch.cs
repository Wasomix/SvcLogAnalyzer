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
        private List<string> _fileNamesToSearchOnAutomatically;
        private SvcLogAnalyzerBEDataConfig _svcLogAnalyzerBEDataConfig;
        public AutomaticalFileNameSearch(SvcLogAnalyzerBEDataConfig svcLogAnalyzerBEDataConfig)
        {
            _fileNamesToSearchOn = new List<string>();
            _fileNamesToSearchOnAutomatically = new List<string>();
            _svcLogAnalyzerBEDataConfig = svcLogAnalyzerBEDataConfig;
        }
        public List<string> GetFileNamesToSearchInAFolder()
        {
            FindFiles();
            FindFilesAutomatically();
            return _fileNamesToSearchOn;
        }

        private void FindFiles()
        {
            const int NUMBER_OF_FILES = 53;
            const int INITIAL_VALUE = 1;

            for (int i = INITIAL_VALUE; i < NUMBER_OF_FILES; i++)
            {
                string fileNameToAdd = _svcLogAnalyzerBEDataConfig.PrefixName + 
                    i.ToString() + _svcLogAnalyzerBEDataConfig.SufixName;
                _fileNamesToSearchOn.Add(fileNameToAdd);
            }
        }

        private void FindFilesAutomatically()
        {
            //DirectoryInfo d = new DirectoryInfo(@"D:\Test"); //Assuming Test is your Folder
            DirectoryInfo d = new DirectoryInfo(_svcLogAnalyzerBEDataConfig.FilesPath); //Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles(_svcLogAnalyzerBEDataConfig.TypeOfFile); 

            foreach (var file in Files)
            {
                _fileNamesToSearchOnAutomatically.Add(file.Name);
            }
        }
    }
}
