using System;

namespace SvcLogAnalyzerBackEnd
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Start of test!");

            ILog logger = new Log4Wrapper();
            SvcLogAnalyzerBEMain svcLogAnalyzerBEMain = new SvcLogAnalyzerBEMain(
                new SvcLogAnalyzerBEJsonConfig(), 
                new AutomaticalFileNameSearch(new SvcLogAnalyzerBEDataConfig(), logger)
            );
            svcLogAnalyzerBEMain.Run();

            Console.WriteLine("End of test!");
        }
    }
}
