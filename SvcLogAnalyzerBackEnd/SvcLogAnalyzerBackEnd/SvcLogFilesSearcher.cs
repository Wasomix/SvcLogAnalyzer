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
        private List<string> _svcFileNames;
        private SvcLogAnalyzerBEDataConfig _configuration;

        public SvcLogFilesSearcher(SvcLogAnalyzerBEDataConfig configuration)
        {
            //_patternToSearch = "200006199089";
            _filesContainingPattern = new List<string>();
            _svcFileNames = new List<string>();
            _configuration = configuration;
        }

        public void Run()
        {
            //Configuration
            //
            //
            const int NUMBER_OF_FILES = 53;
            const int INITIAL_VALUE = 1;            

            try
            {
                for (int i = INITIAL_VALUE; i < NUMBER_OF_FILES; i++)
                {
                    string fileNameToAdd = _configuration.PrefixName + i.ToString() + _configuration.SufixName;
                    _svcFileNames.Add(fileNameToAdd);
                }

                //GetSvclogFiles();
                SearchPatternInSvcLogFiles(_configuration.FilesPath);
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
                string fileNamePath = _configuration.FilesPath + @"\" + fileName;

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

            if (patternSearcher.ItContainsPattern(reader, _configuration.PatternToSearch))
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
