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
        private List<string> FileNamesContainingPattern { get; set; }
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
            InitialSetUp();

            _logger.WriteLogInfo("Start of Main from class SvcLogAnalyzerBEMain");

            DeleteFileContainingFileNamesWithPatternIfItExist();
            SearchPatternInFiles();
            SavesFileNamesContainingPattern();

            _logger.WriteLogInfo("End of Main from class SvcLogAnalyzerBEMain");
        }

        private void InitialSetUp()
        {
            GetSystemConfiguration();
            SetLogFileNameAndDeletePreviousLogFile();
            GetFileNamesToSearchOn();
        }

        private void GetSystemConfiguration()
        {
            _svcLogAnalyzerBEDataConfig = _systemConfiguration.GetSystemConfiguration();
        }

        private void GetFileNamesToSearchOn()
        {
            _svcFileNames = _fileNamesToSearchOn.GetFileNamesToSearchInAFolder(
                _svcLogAnalyzerBEDataConfig.LogFilesPath, _svcLogAnalyzerBEDataConfig.TypeOfFile);
        }

        private void SetLogFileNameAndDeletePreviousLogFile()
        {
            string logFileName = _svcLogAnalyzerBEDataConfig.ApplicationLogFileName;
            _logger.SetLogFileNameAndDeletePreviousLogFile(logFileName);
        }

        private void DeleteFileContainingFileNamesWithPatternIfItExist()
        {
            FileWrapper.DeleteFileIfItExist(_svcLogAnalyzerBEDataConfig.NameOfFileContainingPattern);
        }

        // TODO: Think how to change this to try to do not create the object here
        private void SearchPatternInFiles()
        {
            ILogFilesSearcher logFilesSearcher = new SvcLogFilesSearcher(_svcLogAnalyzerBEDataConfig, _svcFileNames, _logger);
            FileNamesContainingPattern = logFilesSearcher.GetFileNamesContainingPattern();
        }

        private void SavesFileNamesContainingPattern()
        {
            string filesNameContainingPattern = "";
            foreach(var file in FileNamesContainingPattern)
            {
                filesNameContainingPattern = filesNameContainingPattern + file + "\n";
            }

            File.WriteAllText(_svcLogAnalyzerBEDataConfig.NameOfFileContainingPattern, filesNameContainingPattern);
        }
    }
}
