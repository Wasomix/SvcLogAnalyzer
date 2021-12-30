using System.IO;

namespace SvcLogAnalyzerBackEnd
{
    public static class FileWrapper
    {
        public static void DeleteFileIfItExist(string fileName)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }
    }
}
