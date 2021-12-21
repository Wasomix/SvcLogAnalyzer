using System.IO;

namespace SvcLogAnalyzerBackEnd
{
    /// <summary>
    /// This class is responsible for searching the pattern
    /// </summary>
    public class PatternSearcher
    {
        private bool _patternFound;
        private const int BUFFER_SIZE = 4096;

        public PatternSearcher()
        {
            ResetPatternFound();
        }

        private void ResetPatternFound()
        {
            _patternFound = false;
        }

        public bool ItContainsPattern(StreamReader reader, string patternToSearch)
        {            
            char[] buffer = new char[BUFFER_SIZE];

            ResetPatternFound();

            while (((reader.Read(buffer, 0, buffer.Length)) > 0) && (IsPatternNotFound()))
            {
                if (ItContainsPattern(buffer, patternToSearch))
                {
                    _patternFound = true;                    
                }
            }

            return IsPatternFound();
        }

        private bool ItContainsPattern(char[] buffer, string patternToSearch)
        {
            var bufferRead = string.Join("", buffer);
            return bufferRead.Contains(patternToSearch);
        }

        private bool IsPatternNotFound()
        {
            return !IsPatternFound();
        }

        private bool IsPatternFound()
        {
            return _patternFound;
        }
    }
}
