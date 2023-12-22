namespace MLXamarin.Common
{
    public interface IFileHelper
    {
        string RootFolder { get; }

        string GetPath(params string[] paths);

        bool Exists(string fileName);

        void WriteAllText(string fileName, string contents);

        string ReadAllText(string fileName);

        void Delete(string fileName);
    }
}
