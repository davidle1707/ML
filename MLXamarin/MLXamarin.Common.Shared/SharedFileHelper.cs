using System;
using System.Collections.Generic;
using System.IO;

namespace MLXamarin.Common.Shared
{
    public class SharedFileHelper
    {
        public string RootFolder => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public string GetPath(params string[] paths)
        {
            var tmp = new List<string>(paths);
            tmp.Insert(0, RootFolder);

            return Path.Combine(tmp.ToArray());
        }

        public bool Exists(string fileName)
        {
            var filePath = GetPath(fileName);

            return File.Exists(filePath);
        }

        public void WriteAllText(string fileName, string contents)
        {
            var filePath = GetPath(fileName);

            File.WriteAllText(filePath, contents);
        }        public string ReadAllText(string fileName)
        {
            var filePath = GetPath(fileName);

            return File.ReadAllText(filePath);
        }        public void Delete(string fileName)
        {
            var filePath = GetPath(fileName);

            File.Delete(filePath);
        }
    }
}
