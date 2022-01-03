using SvcLogAnalyzerBackEnd;
using System;
using System.IO;
using Xunit;

namespace SvcLogAnalyzerBackEndTest
{
    public class PatternSearcherTest
    {
        private PatternSearcher _patternSearcher;
        private StreamReader _streamReader;
        private string _fileNamePath;
        private string _fileContent;
        private string _pattern;
        public PatternSearcherTest()
        {
            _fileNamePath = "TestFile.svclog";
            _pattern = "";
            _patternSearcher = new PatternSearcher();
            CreateFile();
            _streamReader = new StreamReader(_fileNamePath);
        }
        
        private void CreateFile()
        {
            _fileContent = "New text for testing\na method with pattern = 12rt\nin unit testing";
            File.WriteAllText(_fileNamePath, _fileContent);
        }
        
        private void TearDown()
        {
            DeleteFileIfItExist(_fileNamePath);
        }

        private void DeleteFileIfItExist(string fileNamePath)
        {
            FileWrapper.DeleteFileIfItExist(_fileNamePath);
        }

        // TODO : Make all test cases with correct naming convention
        [Fact]
        public void ItContainsPattern_FindPattern_ReturnTrue()
        {            
            _pattern = "12rt";
            bool patternFound = _patternSearcher.ItContainsPattern(_streamReader, _pattern);
            TearDown();
            Assert.True(patternFound);
        }

        [Fact]
        public void ItContainsPattern_DoNotFindPattern_ReturnFalse()
        {
            _pattern = "sA34";
            bool patternFound = _patternSearcher.ItContainsPattern(_streamReader, _pattern);
            TearDown();
            Assert.False(patternFound);
        }
    }
}
