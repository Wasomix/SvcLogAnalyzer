using System;

namespace SvcLogAnalyzerBackEnd
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start of test!");

            SvcLogFilesSearcher searchInSvcLogFiles = new SvcLogFilesSearcher();
            searchInSvcLogFiles.Run();

            Console.WriteLine("End of test!");
        }
    }
}
