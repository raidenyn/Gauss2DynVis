using System.IO;

namespace Gauss2DynVis
{
    public static class TextReaderExtensions
    {
        public static void SkipLines(this TextReader reader, int lineCount)
        {
            for (var i = 0; i < lineCount; i++)
            {
                reader.ReadLine();
            }
        }
    }
}
