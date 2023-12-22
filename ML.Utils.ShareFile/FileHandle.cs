using System;
using System.IO;
using log4net;

namespace ML.Utils.ShareFile
{
    public class FileHandle : IDisposable
    {
        public string LocalFilePath { get; private set; }

        public bool LogDebug { get; set; }

        public FileHandle(string localFilePath, bool logDebug = false)
        {
            LocalFilePath = localFilePath;
            LogDebug = LogDebug;
        }

        #region Disposable

        private bool _disposed;

        ~FileHandle()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    try
                    {
                        if (LogDebug) ShareFileManager._log.Debug($"Remove FileDB temp file: {LocalFilePath}");

                        if (File.Exists(LocalFilePath))
                        {
                            File.Delete(LocalFilePath);
                        }
                    }
                    catch (Exception ex)
                    {
                        ShareFileManager._log.Error($"Error removing FileDB temp file '{LocalFilePath}': {ex.Message}", ex);
                    }
                }

                _disposed = true;
            }
        }

        #endregion
    }
}
