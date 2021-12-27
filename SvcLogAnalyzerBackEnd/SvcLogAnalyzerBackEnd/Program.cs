using System;

namespace SvcLogAnalyzerBackEnd
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Start of test!");

            SvcLogAnalyzerBEMain svcLogAnalyzerBEMain = new SvcLogAnalyzerBEMain(new SvcLogAnalyzerBEJsonConfig());
            svcLogAnalyzerBEMain.Run();

            Console.WriteLine("End of test!");
        }
    }
}
