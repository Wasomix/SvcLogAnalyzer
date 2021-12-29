using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SvcLogAnalyzerBackEnd
{    
    /// <summary>
    /// This class is responsible for searching all files from a determined 
    /// type in a folder.
    /// </summary>
    public class AutomaticalFileNameSearch : IFileNamesToSearchOn
    {
        private List<string> _fileNamesToSearchOn;
        private SvcLogAnalyzerBEDataConfig _svcLogAnalyzerBEDataConfig;
        private ILog _logger;
        public AutomaticalFileNameSearch(SvcLogAnalyzerBEDataConfig svcLogAnalyzerBEDataConfig,
                                         ILog logger)
        {
            _fileNamesToSearchOn = new List<string>();
            _svcLogAnalyzerBEDataConfig = svcLogAnalyzerBEDataConfig;
            _logger = logger;
        }
        public List<string> GetFileNamesToSearchInAFolder()
        {
            _logger.WriteLogInfo("Start of GetFileNamesToSearchInAFolder");
            FindFilesAutomatically();
            _logger.WriteLogInfo("End of GetFileNamesToSearchInAFolder");
            return _fileNamesToSearchOn;
        }

        private void FindFilesAutomatically()
        {
            try
            {
                DirectoryInfo d = new DirectoryInfo(_svcLogAnalyzerBEDataConfig.FilesPath);
                FileInfo[] Files = d.GetFiles(_svcLogAnalyzerBEDataConfig.TypeOfFile);

                foreach (var file in Files)
                {
                    _fileNamesToSearchOn.Add(file.Name);
                }
            }
            catch (Exception ex)
            {
                _logger.WriteLogError("Exception while FindFilesAutomatically");
                _logger.WriteLogError(ex.Message);
            }
        }
    }
}
