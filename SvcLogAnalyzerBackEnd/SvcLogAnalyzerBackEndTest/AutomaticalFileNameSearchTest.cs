using SvcLogAnalyzerBackEnd;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SvcLogAnalyzerBackEndTest
{
    public class AutomaticalFileNameSearchTest
    {
        private SvcLogAnalyzerBEDataConfig _svcLogAnalyzerBEDataConfig;
        private List<string> _fileNames;
        private string _logFilePath;
        SvcLogFiles _svcLogFiles;
        private ILog _logFake;

        public AutomaticalFileNameSearchTest()
        {
            _svcLogAnalyzerBEDataConfig = new SvcLogAnalyzerBEDataConfig();
            _fileNames = new List<string>();
            _logFilePath = @".\";
            _svcLogFiles = new SvcLogFiles();
            _logFake = new LogFake();
    }

        [Fact]
        public void GetFileNamesToSearchInAFolder_FindSvclogFiles_ReturnRightAmountOfFiles()
        {
            try
            {
                SetUp();
                IFileNamesToSearchOn fileNamesToSearchOn = new AutomaticalFileNameToSearch(_logFake);
                var fileNames = fileNamesToSearchOn.GetFileNamesToSearchInAFolder(_svcLogAnalyzerBEDataConfig.LogFilesPath, _svcLogAnalyzerBEDataConfig.TypeOfFile);
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
            _svcLogFiles.CreateSvcLogFiles();
        }

        private void SetConfiguration()
        {
            _svcLogAnalyzerBEDataConfig.LogFilesPath = _logFilePath;
            _svcLogAnalyzerBEDataConfig.TypeOfFile = "*.svclog";
        }

        private void DeleteSvcLogFiles()
        {
            _svcLogFiles.DeleteSvcLogFiles();
        }
    }
}
