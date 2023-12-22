using System.IO;
using System.Text;

namespace ML.Utils.Excel.CsvNew
{
    public class NullRemovalStreamReader : StreamReader
    {
        public NullRemovalStreamReader(Stream stream, bool addMark, int threshold, Encoding encoding, int bufferSize = 4096, bool detectEncoding = false)
            : base(new NullRemovalStream(stream, addMark, threshold, bufferSize), encoding, detectEncoding, bufferSize)
        {
        }
    }
}
