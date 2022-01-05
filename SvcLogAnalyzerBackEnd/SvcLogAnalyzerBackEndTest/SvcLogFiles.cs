using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SvcLogAnalyzerBackEndTest
{
    public class SvcLogFiles
    {
        private string _fileNamePreffix;
        private string _fileType;
        private List<string> _fileNames;
        private const int NUMBER_OF_SVCLOG_FILES = 4;

        public SvcLogFiles()
        {
            _fileNamePreffix = "TestFile_";
            _fileType = ".svclog";
        }

        public void CreateSvcLogFiles()
        {
            for (int i = 0; i < NUMBER_OF_SVCLOG_FILES; i++)
            {
                string fileName = _fileNamePreffix + i.ToString() + _fileType;
                using var file = File.Create(fileName);
                _fileNames.Add(fileName);
            }
        }

        public List<string> GetSvcLogFileNames()
        {
            return _fileNames;
        }

        public void DeleteSvcLogFiles()
        {
            for (int i = 0; i < NUMBER_OF_SVCLOG_FILES; i++)
            {
                string fileName = _fileNamePreffix + i.ToString() + _fileType;
                File.Delete(fileName);
            }
        }
    }
}
