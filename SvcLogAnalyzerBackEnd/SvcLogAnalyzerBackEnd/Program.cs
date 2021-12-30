using System;

namespace SvcLogAnalyzerBackEnd
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SvcLogAnalyzerBEDataConfig svcLogAnalyzerBEDataConfig = new SvcLogAnalyzerBEDataConfig();
            ISystemConfiguration systemConfiguration = new SvcLogAnalyzerBEJsonConfig();
            ILog logger = new Log4Wrapper(/*svcLogAnalyzerBEDataConfig*/);
            IFileNamesToSearchOn fileNamesToSearchOn = new AutomaticalFileNameToSearch(logger);
            
            logger.WriteLogInfo("Start of program!");
            
            SvcLogAnalyzerBEMain svcLogAnalyzerBEMain = new SvcLogAnalyzerBEMain(
                systemConfiguration, fileNamesToSearchOn, logger);
            svcLogAnalyzerBEMain.Run();

            logger.WriteLogInfo("End of program!");
        }
    }
}
