using System.Collections.Generic;

namespace SvcLogAnalyzerBackEnd
{
    public interface IFileNamesToSearchOn
    {
        public List<string> GetFileNamesToSearchInAFolder(string logFilesPath, 
                                                          string typeOfFile);
    }
}
