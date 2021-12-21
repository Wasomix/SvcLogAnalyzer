using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SvcLogAnalyzerBackEnd
{
    public class SvcLogFilesSearcher
    {
        private string _patterToSearch = "200006199089";
        public void Run()
        {
            string prefixName = "TransferWorker_Messages__";
            string suffixName = "transferworker_in_0.svclog";
            const int NUMBER_OF_FILES = 53;
            const int INITIAL_VALUE = 1;
            
            string filePath = @"C:\Users\sferrand\Sergio\00_ACTUAL\Capgemini\Projects\PostNL\IncidentsWorkitem\Active\INCIDENT 26789\20211213";

            List<string> filesContainingPattern = new List<string>();
            List<string> fileNames = new List<string>();

            try
            {
                for (int i = INITIAL_VALUE; i < NUMBER_OF_FILES; i++)
                {
                    string fileNameToAdd = prefixName + i.ToString() + suffixName;
                    fileNames.Add(fileNameToAdd);
                }

                foreach (string fileName in fileNames)
                {
                    string fileNamePath = filePath + @"\" + fileName;

                    if (File.Exists(fileNamePath))
                    {
                        System.Console.WriteLine($"Start processing file {fileName}");

                        File.Copy(fileNamePath, fileName, true);

                        var filestream = new FileStream(fileNamePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete, 4096, FileOptions.RandomAccess);
                        StreamReader reader = new StreamReader(filestream, Encoding.ASCII);

                        long fromPosition = 0;
                        reader.BaseStream.Seek(fromPosition, SeekOrigin.Begin);
                        reader.DiscardBufferedData();
                        char[] buffer = new char[4096];
                        int charsRead;
                        //int count = 0;
                        //long result = -1;
                        bool patternNotFound = true;
                        while (((charsRead = reader.Read(buffer, 0, buffer.Length)) > 0) && (patternNotFound))
                        {
                            if(ItContainsPattern(buffer))
                            {
                                patternNotFound = false;
                                filesContainingPattern.Add(fileName);
                            }
                        }

                        File.Delete(fileName);
                        System.Console.WriteLine($"End processing file {fileName}");
                        System.Console.WriteLine("");
                    }
                }

                System.Console.WriteLine("");
                System.Console.WriteLine("");
                System.Console.WriteLine("");

                foreach (var fileName in filesContainingPattern)
                {
                    System.Console.WriteLine(fileName);
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
            }
        }

        private bool ItContainsPattern(char[] buffer)
        {
            var bufferRead = string.Join("", buffer);
            return bufferRead.Contains(_patterToSearch);
        }
    }
}
