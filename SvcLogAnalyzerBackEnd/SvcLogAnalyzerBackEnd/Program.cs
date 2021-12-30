using System;

namespace SvcLogAnalyzerBackEnd
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ILog logger = new Log4Wrapper();

            logger.WriteLogInfo("Start of program!");

            SvcLogAnalyzerBEDataConfig svcLogAnalyzerBEDataConfig = new SvcLogAnalyzerBEDataConfig();
            ISystemConfiguration systemConfiguration = new SvcLogAnalyzerBEJsonConfig();
            IFileNamesToSearchOn fileNamesToSearchOn = new AutomaticalFileNameToSearch(svcLogAnalyzerBEDataConfig, logger);

            SvcLogAnalyzerBEMain svcLogAnalyzerBEMain = new SvcLogAnalyzerBEMain(
                systemConfiguration, fileNamesToSearchOn, logger);
            svcLogAnalyzerBEMain.Run();

            logger.WriteLogInfo("End of program!");
        }
    }
}
