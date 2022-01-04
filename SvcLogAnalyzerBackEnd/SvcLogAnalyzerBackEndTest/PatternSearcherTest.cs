using SvcLogAnalyzerBackEnd;
using System;
using System.IO;
using Xunit;

namespace SvcLogAnalyzerBackEndTest
{
    public class PatternSearcherTest
    {
        private PatternSearcher _patternSearcher;
        private string _fileNamePath;
        private string _fileContent;
        private string _pattern;
        public PatternSearcherTest()
        {
            _fileNamePath = "TestFile.svclog";
            _pattern = "";
            _patternSearcher = new PatternSearcher();
            CreateFile();
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
            bool patternFound = false;
            _pattern = "12rt";
            
            using (var streamReader = new StreamReader(_fileNamePath))
            {
                patternFound = _patternSearcher.ItContainsPattern(streamReader, _pattern);
            }
            
            TearDown();
            Assert.True(patternFound);
        }

        [Fact]
        public void ItContainsPattern_DoNotFindPattern_ReturnFalse()
        {
            bool patternFound = true;
            _pattern = "a32G";

            using (var streamReader = new StreamReader(_fileNamePath))
            {
                patternFound = _patternSearcher.ItContainsPattern(streamReader, _pattern);
            }

            TearDown();
            Assert.False(patternFound);
        }
    }
}
