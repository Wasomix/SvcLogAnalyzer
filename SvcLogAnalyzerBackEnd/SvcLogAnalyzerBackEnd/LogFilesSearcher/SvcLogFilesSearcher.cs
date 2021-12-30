using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SvcLogAnalyzerBackEnd
{
    public class SvcLogFilesSearcher : ILogFilesSearcher
    {
        private List<string> _filesContainingPattern;
        private List<string> _svcFileNames;
        private SvcLogAnalyzerBEDataConfig _configuration;
        private ILog _logger;

        public SvcLogFilesSearcher(SvcLogAnalyzerBEDataConfig configuration,
                                   List<string> svcFileNames,
                                   ILog logger)
        {
            _filesContainingPattern = new List<string>();
            _svcFileNames = svcFileNames;
            _configuration = configuration;   
            _logger = logger;
        }

        public List<string> GetFileNamesContainingPattern()
        {
            try
            {
                _logger.WriteLogInfo($"[SvcLogFilesSearcher] Start from method GetFileNamesContainingPattern");
                SearchPatternInSvcLogFiles();
                _logger.WriteLogInfo($"[SvcLogFilesSearcher] End from method GetFileNamesContainingPattern");

                return _filesContainingPattern;
            }
            catch (Exception ex)
            {
                string messageError = "An exception occurs while searching the pattern in svclog files";
                _filesContainingPattern.Add(messageError);
                _logger.WriteLogError(ex.ToString());

                return _filesContainingPattern;
            }           
        }

        private void SearchPatternInSvcLogFiles()
        {            
            foreach (string fileName in _svcFileNames)
            {
                string fileNamePath = _configuration.LogFilesPath + @"\" + fileName;

                if (File.Exists(fileNamePath))
                {
                    CopyFileTemporally(fileNamePath, fileName);
                    SearchPattern(fileNamePath, fileName);
                    DeleteTemporalFile(fileName);
                }
            }
        }

        private void CopyFileTemporally(string fileNamePath, string fileName)
        {
            try
            {
                File.Copy(fileNamePath, fileName, true);
            }
            catch(Exception ex)
            {
                _logger.WriteLogError(ex.ToString());
            }
        }

        private void SearchPattern(string fileNamePath, string fileName)
        {           
            _logger.WriteLogInfo($"[SvcLogFilesSearcher] Start processing file {fileName} from method SearchPattern");

            PatternSearcher patternSearcher = new PatternSearcher();
            StreamReader reader = SetUpStreamReader(fileNamePath);
            
            if (patternSearcher.ItContainsPattern(reader, _configuration.PatternToSearch))
            {
                _filesContainingPattern.Add(fileName);
            }

            _logger.WriteLogInfo($"[SvcLogFilesSearcher] End processing file {fileName} from method SearchPattern");
        }

        private void DeleteTemporalFile(string fileName)
        {
            try
            {
                File.Delete(fileName);
            }
            catch (Exception ex)
            {
                _logger.WriteLogError(ex.ToString());
            }
        }

        private StreamReader SetUpStreamReader(string fileNamePath)
        {
            var filestream = new FileStream(fileNamePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete, 4096, FileOptions.RandomAccess);
            StreamReader reader = new StreamReader(filestream, Encoding.ASCII);
            long fromPosition = 0;
            reader.BaseStream.Seek(fromPosition, SeekOrigin.Begin);
            reader.DiscardBufferedData();
            return reader;
        }
    }
}
