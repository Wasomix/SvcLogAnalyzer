using System;
using System.Collections.Generic;
using System.Text;

namespace SvcLogAnalyzerBackEnd
{
    /// <summary>
    /// This is the main class where the high level operations of the program are done
    /// </summary>
    public class SvcLogAnalyzerBEMain
    {
        private SvcLogAnalyzerBEDataConfig _svcLogAnalyzerBEDataConfig;
        private List<string> _svcFileNames;
        ISystemConfiguration _systemConfiguration;
        IFileNamesToSearchOn _fileNamesToSearchOn;
        ILog _logger;
        public SvcLogAnalyzerBEMain(ISystemConfiguration systemConfiguration,
                                    IFileNamesToSearchOn fileNamesToSearchOn,
                                    ILog logger)
        {
            _systemConfiguration = systemConfiguration;
            _fileNamesToSearchOn = fileNamesToSearchOn;
            _logger = logger;
        }

        public void Run()
        {
            _logger.WriteLogInfo("Start of Main from class SvcLogAnalyzerBEMain");
            // TODO: Get configuration data from XML file
            //SvcLogFilesSearcher searchInSvcLogFiles = new SvcLogFilesSearcher();
            //searchInSvcLogFiles.Run();

            SetUp();
            //Process();
            _logger.WriteLogInfo("End of Main from class SvcLogAnalyzerBEMain");
        }

        private void SetUp()
        {
            GetSystemConfiguration();
            GetFileNamesToSearchOn();
        }

        private void GetSystemConfiguration()
        {
            _svcLogAnalyzerBEDataConfig = _systemConfiguration.GetSystemConfiguration();
        }

        private void GetFileNamesToSearchOn()
        {
            _svcFileNames = _fileNamesToSearchOn.GetFileNamesToSearchInAFolder();
        }
    }
}
