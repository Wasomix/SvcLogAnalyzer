using System;

namespace SvcLogAnalyzerBackEnd
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ISystemConfiguration systemConfiguration = new SvcLogAnalyzerBEJsonConfig();
            ILog logger = new Log4Wrapper();
            IFileNamesToSearchOn fileNamesToSearchOn = new AutomaticalFileNameToSearch(logger);
            
            SvcLogAnalyzerBEMain svcLogAnalyzerBEMain = new SvcLogAnalyzerBEMain(
                systemConfiguration, fileNamesToSearchOn, logger);
            svcLogAnalyzerBEMain.Run();
        }
    }
}
