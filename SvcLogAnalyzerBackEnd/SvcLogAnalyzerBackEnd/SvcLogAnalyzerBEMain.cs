using System;
using System.Collections.Generic;
using System.IO;
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
        private List<string> _fileNamesContainingPattern;
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

            GetConfigurations();
            SearchPatternInFiles();
            SavesFileNamesContainingPattern();

            _logger.WriteLogInfo("End of Main from class SvcLogAnalyzerBEMain");
        }

        private void GetConfigurations()
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

        // TODO: Think how to change this to try to do not create the object here
        private void SearchPatternInFiles()
        {
            ILogFilesSearcher logFilesSearcher = new SvcLogFilesSearcher(_svcLogAnalyzerBEDataConfig, _svcFileNames);
            _fileNamesContainingPattern = logFilesSearcher.GetFileNamesContainingPattern();
        }

        private void SavesFileNamesContainingPattern()
        {
            File.Create(_svcLogAnalyzerBEDataConfig.NameOfFileContainingPattern);

            string filesNameContainingPattern = "";
            foreach(var file in _fileNamesContainingPattern)
            {
                filesNameContainingPattern = filesNameContainingPattern + file + "\n";
            }

            File.WriteAllText(".", filesNameContainingPattern);
        }
    }
}
