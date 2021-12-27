using System;
using System.Collections.Generic;
using System.Text;

namespace SvcLogAnalyzerBackEnd
{
    public class SvcLogAnalyzerBEMain
    {
        private SvcLogAnalyzerBEDataConfig _svcLogAnalyzerBEDataConfig;
        private List<string> _svcFileNames;
        ISystemConfiguration _systemConfiguration;
        public SvcLogAnalyzerBEMain(ISystemConfiguration systemConfiguration)
        {
            _systemConfiguration = systemConfiguration;
        }

        public void Run()
        {
            // TODO: Get configuration data from XML file
            //SvcLogFilesSearcher searchInSvcLogFiles = new SvcLogFilesSearcher();
            //searchInSvcLogFiles.Run();

            SetUp();
            //Process();
        }

        private void SetUp()
        {
            GetSystemConfiguration();
            GetFileNamesToSearchOn();
        }

        private void GetSystemConfiguration()
        {
            _svcLogAnalyzerBEDataConfig = _systemConfiguration.GetSystemConfiguration();
        }

        private void GetFileNamesToSearchOn()
        {

        }
    }
}
