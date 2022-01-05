using SvcLogAnalyzerBackEnd;
using Xunit;

namespace SvcLogAnalyzerBackEndTest
{
    public class SvcLogFilesSearcherTest
    {
        SvcLogAnalyzerBEDataConfig _configuration;
        LogFake _logFake;
        SvcLogFiles _svcLogFiles;

        public SvcLogFilesSearcherTest()
        {
            _configuration = new SvcLogAnalyzerBEDataConfig();
            _logFake = new LogFake();
            _svcLogFiles = new SvcLogFiles();
        }

        [Fact]
        public void GetFileNamesContainingPattern_FindSvclogFiles_ReturnRightAmountOfFiles()
        {
            SetUp();            

            SvcLogFilesSearcher svcLogFilesSearcher = new SvcLogFilesSearcher(
                _configuration, _svcLogFiles.GetSvcLogFileNames(), _logFake);
            
            var listOfFileNames = svcLogFilesSearcher.GetFileNamesContainingPattern();

            TearDown();
        }

        private void SetUp()
        {
            CreateSvcLogFiles();
            SetUpBEDataConfig();
        }

        private void CreateSvcLogFiles()
        {
            _svcLogFiles.CreateSvcLogFiles();
        }

        private void SetUpBEDataConfig()
        {
            _configuration.LogFilesPath = "";
            _configuration.PatternToSearch = "12rt";
        }

        private void TearDown()
        {
            DeleteSvcLogFiles();
        }

        private void DeleteSvcLogFiles()
        {
            _svcLogFiles.DeleteSvcLogFiles();
        }
    }
}
