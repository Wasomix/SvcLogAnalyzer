using Microsoft.Extensions.Configuration;
using System;

namespace SvcLogAnalyzerBackEnd
{
    public class SvcLogAnalyzerBEJsonConfig : ISystemConfiguration
    {
        public SvcLogAnalyzerBEDataConfig _configuration;

        public SvcLogAnalyzerBEDataConfig GetSystemConfiguration()
        {
            SetupSvcLogAnalyzerBE();
            return _configuration;
        }

        private void SetupSvcLogAnalyzerBE()
        {
            var config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
            var section = config.GetSection(nameof(SvcLogAnalyzerBEDataConfig));
            _configuration = section.Get<SvcLogAnalyzerBEDataConfig>();
        }
    }
}
