using System;

namespace ML.Utils.ShareFile
{
    [Serializable]
    public class FileInfoHandle
    {
        public string FileName { get; set; }

        public string FilePath { get; set; }

        public FileHandle Handle { get; set; }
    }
}
