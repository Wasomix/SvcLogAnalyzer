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
    public class AutomaticalFileNameToSearch : IFileNamesToSearchOn
    {
        private readonly List<string> _fileNamesToSearchOn;
        private readonly ILog _logger;
        public AutomaticalFileNameToSearch(ILog logger)
        {
            _fileNamesToSearchOn = new List<string>();
            _logger = logger;
        }
        public List<string> GetFileNamesToSearchInAFolder(string logFilesPath,
                                                          string typeOfFile)
        {
            _logger.WriteLogInfo("Start of GetFileNamesToSearchInAFolder");
            FindFilesAutomatically(logFilesPath, typeOfFile);
            _logger.WriteLogInfo("End of GetFileNamesToSearchInAFolder");
            return _fileNamesToSearchOn;
        }

        private void FindFilesAutomatically(string logFilesPath,
                                            string typeOfFile)
        {
            try
            {
                DirectoryInfo d = new DirectoryInfo(logFilesPath);
                FileInfo[] Files = d.GetFiles(typeOfFile);

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
