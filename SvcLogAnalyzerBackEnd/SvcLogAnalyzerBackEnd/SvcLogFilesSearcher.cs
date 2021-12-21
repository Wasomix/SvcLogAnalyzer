using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SvcLogAnalyzerBackEnd
{
    public class SvcLogFilesSearcher
    {
        private string _patternToSearch;
        private List<string> _filesContainingPattern;
        List<string> _svcFileNames;

        public SvcLogFilesSearcher()
        {
            _patternToSearch = "200006199089";
            _filesContainingPattern = new List<string>();
            _svcFileNames = new List<string>();
        }

        public void Run()
        {
            string prefixName = "TransferWorker_Messages__";
            string suffixName = "transferworker_in_0.svclog";
            const int NUMBER_OF_FILES = 53;
            const int INITIAL_VALUE = 1;            
            string filePath = @"C:\Users\sferrand\Sergio\00_ACTUAL\Capgemini\Projects\PostNL\IncidentsWorkitem\Active\INCIDENT 26789\20211213";           

            try
            {
                for (int i = INITIAL_VALUE; i < NUMBER_OF_FILES; i++)
                {
                    string fileNameToAdd = prefixName + i.ToString() + suffixName;
                    _svcFileNames.Add(fileNameToAdd);
                }

                SearchPatternInSvcLogFiles(filePath);
                PrintNumberOfNewLines(3);
                PrintFileNamesContainingPattern();
                
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
            }
        }

        private void SearchPatternInSvcLogFiles(string filePath)
        {            
            foreach (string fileName in _svcFileNames)
            {
                string fileNamePath = filePath + @"\" + fileName;

                if (File.Exists(fileNamePath))
                {
                    ProcessSvcLogFile(fileNamePath, fileName);
                }
            }
        }

        private void ProcessSvcLogFile(string fileNamePath, string fileName)
        {
            PatternSearcher patternSearcher = new PatternSearcher();

            System.Console.WriteLine($"Start processing file {fileName}");

            File.Copy(fileNamePath, fileName, true);
            StreamReader reader = SetUpStreamReader(fileNamePath);

            if (patternSearcher.ItContainsPattern(reader, _patternToSearch))
            {
                _filesContainingPattern.Add(fileName);
            }

            File.Delete(fileName);
            System.Console.WriteLine($"End processing file {fileName}");
            PrintNumberOfNewLines(1);
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
                System.Console.WriteLine(fileName);
            }
        }

        private void PrintNumberOfNewLines(int numberOfNewLines)
        {
            for(int i=0; i<numberOfNewLines; i++)
            {
                System.Console.WriteLine("");
            }            
        }
    }
}
