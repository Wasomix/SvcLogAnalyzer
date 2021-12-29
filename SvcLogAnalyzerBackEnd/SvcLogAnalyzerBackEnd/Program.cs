using System;

namespace SvcLogAnalyzerBackEnd
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Start of test!");

            ILog logger = new Log4Wrapper();
            SvcLogAnalyzerBEDataConfig svcLogAnalyzerBEDataConfig = new SvcLogAnalyzerBEDataConfig();
            ISystemConfiguration systemConfiguration = new SvcLogAnalyzerBEJsonConfig();
            IFileNamesToSearchOn fileNamesToSearchOn = new AutomaticalFileNameToSearch(svcLogAnalyzerBEDataConfig, logger);

            SvcLogAnalyzerBEMain svcLogAnalyzerBEMain = new SvcLogAnalyzerBEMain(
                systemConfiguration, fileNamesToSearchOn, logger);
            svcLogAnalyzerBEMain.Run();

            Console.WriteLine("End of test!");
        }
    }
}
