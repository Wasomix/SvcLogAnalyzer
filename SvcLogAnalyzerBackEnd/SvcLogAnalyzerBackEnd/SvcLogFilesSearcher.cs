using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SvcLogAnalyzerBackEnd
{
    public class SvcLogFilesSearcher
    {
        //private string _patternToSearch;
        private List<string> _filesContainingPattern;
        private IList<string> _svcFileNames;
        private SvcLogAnalyzerBEDataConfig _configuration;

        public SvcLogFilesSearcher(SvcLogAnalyzerBEDataConfig configuration,
                                   IList<string> svcFileNames)
        {
            //_patternToSearch = "200006199089";
            _filesContainingPattern = new List<string>();
            _svcFileNames = /*new List<string>()*/svcFileNames;
            _configuration = configuration;            
        }

        public IList<string> GetFileNamesContainingPattern()
        {
            try
            {
                //GetSvclogFiles();
                SearchPatternInSvcLogFiles(_configuration.FilesPath);

                PrintNumberOfNewLines(3);
                PrintFileNamesContainingPattern();
                return _filesContainingPattern;
            }
            catch (Exception ex)
            {
                string messageError = "An exception occurs while searching the pattern in svclog files";
                _filesContainingPattern.Add(messageError);
                Console.WriteLine(ex.ToString());
                return _filesContainingPattern;
            }           
        }

        private void SearchPatternInSvcLogFiles(string filePath)
        {            
            foreach (string fileName in _svcFileNames)
            {
                string fileNamePath = _configuration.FilesPath + @"\" + fileName;

                if (File.Exists(fileNamePath))
                {
                    CopyFileTemporally(fileNamePath, fileName);
                    SearchPattern(fileNamePath, fileName);
                    DeleteTemporalFile();
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

            }
        }

        private void ProcessSvcLogFile(string fileNamePath, string fileName)
        {
            PatternSearcher patternSearcher = new PatternSearcher();

            Console.WriteLine($"Start processing file {fileName}");

            //File.Copy(fileNamePath, fileName, true);

            SearchPatternInSvcLogFiles()
            StreamReader reader = SetUpStreamReader(fileNamePath);
            if (patternSearcher.ItContainsPattern(reader, _configuration.PatternToSearch))
            {
                _filesContainingPattern.Add(fileName);
            }


            File.Delete(fileName);
            Console.WriteLine($"End processing file {fileName}");
            PrintNumberOfNewLines(1);
        }

        private void DeleteTemporalFile()
        {

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

        private void PrintFileNamesContainingPattern()
        {
            foreach (var fileName in _filesContainingPattern)
            {
                Console.WriteLine(fileName);
            }
        }

        private void PrintNumberOfNewLines(int numberOfNewLines)
        {
            for(int i=0; i<numberOfNewLines; i++)
            {
                Console.WriteLine("");
            }            
        }
    }
}
