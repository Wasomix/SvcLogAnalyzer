using SvcLogAnalyzerBackEnd;
using System.Collections.Generic;
using System.IO;
using Xunit;

using System.Linq;
using System;

namespace SvcLogAnalyzerBackEndTest
{
    public class AutomaticalFileNameSearchTest
    {
        private SvcLogAnalyzerBEDataConfig _svcLogAnalyzerBEDataConfig;
        private const int NUMBER_OF_SVCLOG_FILES = 4;
        private List<string> _fileNames;
        private string _logFilePath;
        private string _fileNamePreffix;
        private string _fileType;
        private ILog _logFake;

        public AutomaticalFileNameSearchTest()
        {
            _svcLogAnalyzerBEDataConfig = new SvcLogAnalyzerBEDataConfig();
            _fileNames = new List<string>();
            _logFilePath = @".\";
            _fileNamePreffix = "TestFile_";
            _fileType = ".svclog";
            _logFake = new LogFake();
    }

        [Fact]
        public void GetFileNamesToSearchInAFolder_FindSvclogFiles_ReturnRightAmountOfFiles()
        {
            try
            {
                SetUp();
                IFileNamesToSearchOn fileNamesToSearchOn = new AutomaticalFileNameToSearch(_svcLogAnalyzerBEDataConfig, _logFake);
                var fileNames = fileNamesToSearchOn.GetFileNamesToSearchInAFolder();
                var testPassed = AreBothListOfStringsTheSame(fileNames, _fileNames);

                Assert.True(testPassed);

                TearDown();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void SetUp()
        {
            ResetFileNamesList();
            CreateSvcLogFiles();
            SetConfiguration();            
        }

        private bool AreBothListOfStringsTheSame(List<string> fileNames1, List<string> fileNames2)
        {
            return fileNames1.Except(fileNames2).ToList().Count == 0;
        }

        private void TearDown()
        {
            DeleteSvcLogFiles();
        }

        private void ResetFileNamesList()
        {
            if(IsThereAnyFileNameInTheList())
            {
                EmptyFileNamesList();
            }

        }

        private bool IsThereAnyFileNameInTheList()
        {
            return _fileNames.Count > 0;
        }

        private void EmptyFileNamesList()
        {
            _fileNames.Clear();
        }

        private void CreateSvcLogFiles()
        {            
            for(int i=0; i < NUMBER_OF_SVCLOG_FILES; i++)
            {
                string fileName = _fileNamePreffix + i.ToString() + _fileType;
                using var file = File.Create(fileName);
                _fileNames.Add(fileName);
            }
        }

        private void SetConfiguration()
        {
            _svcLogAnalyzerBEDataConfig.LogFilesPath = _logFilePath;
            _svcLogAnalyzerBEDataConfig.TypeOfFile = "*.svclog";
        }

        private void DeleteSvcLogFiles()
        {
            for (int i = 0; i < NUMBER_OF_SVCLOG_FILES; i++)
            {
                string fileName = _fileNamePreffix + i.ToString() + _fileType;
                File.Delete(fileName);
            }
        }
    }
}
