using log4net;
using ML.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ML.Utils.ShareFile
{
    public class ShareFileManager
    {
        internal static readonly ILog _log = LogManager.GetLogger(typeof(ShareFileManager));

        private WindowsImpersonationHelper _logon;

        public ShareSetting Setting { get; private set; }

        public bool LogDebug { get; set; } = true;

        public ShareFileManager(bool loadFromAppSeting = true, string appSettingKeyPrefix = "ShareFile_")
        {
            if (loadFromAppSeting && !string.IsNullOrEmpty(appSettingKeyPrefix))
            {
                SetSetting(new ShareSetting
                {
                    TempPath = ConfigurationManager.AppSettings[appSettingKeyPrefix + "TempPath"],
                    ShareRootPath = ConfigurationManager.AppSettings[appSettingKeyPrefix + "RootPath"],
                    ShareUserName = ConfigurationManager.AppSettings[appSettingKeyPrefix + "UserName"],
                    SharePassword = ConfigurationManager.AppSettings[appSettingKeyPrefix + "Password"],
                    ShareDomain = ConfigurationManager.AppSettings[appSettingKeyPrefix + "Domain"],
                    IgnoreImpersonate = ConfigurationManager.AppSettings[appSettingKeyPrefix + "IgnoreImpersonate"].ToBool()
                });
            }
        }

        public ShareFileManager(ShareSetting setting)
            : this(false)
        {
            SetSetting(setting);
        }

        public void SetSetting(ShareSetting setting)
        {
            Setting = setting;

            if (!Setting.IgnoreImpersonate)
            {
                _logon = new WindowsImpersonationHelper(Setting.ShareUserName, Setting.SharePassword, Setting.ShareDomain);
            }
        }

        public bool CreateFolder(string folderName)
        {
            return Processing(() =>
            {
                var path = FullPath(folderName);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    return true;
                }
                return false;
            });
        }

        /// <summary>
        /// Delete empty folder
        /// </summary>
        public bool DeleteFolder(string folderName, bool recursive = false)
        {
            return Processing(() =>
            {
                var path = FullPath(folderName);
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, recursive);
                    return true;
                }

                return false;
            });
        }

        public bool RenameFolder(string oldFolderName, string newFolderName)
        {
            return Processing(() =>
            {
                var oldPath = FullPath(oldFolderName);
                var newPath = FullPath(newFolderName);

                if (Directory.Exists(oldPath) && !Directory.Exists(newFolderName))
                {
                    Directory.Move(oldPath, newPath);
                    return true;
                }
                return false;
            });
        }

        public bool IsFolderExists(string folderName)
        {
            return Processing(() => Directory.Exists(FullPath(folderName)));
        }

        public FileHandle CreateFile(string ext = null)
        {
            var fileName = Guid.NewGuid().ToString().Replace("-", "");
            if (!string.IsNullOrWhiteSpace(ext))
            {
                fileName = Path.ChangeExtension(fileName, ext);
            }

            return new FileHandle(Path.Combine(Setting.TempPath, fileName), LogDebug);
        }

        public bool SaveFile(string folderName, string fileName, FileHandle fileHandle, bool overwrite = true, bool createFolderIfNotFound = true)
        {
            return Processing(() =>
            {
                var filePath = FullPath(folderName, fileName);

                var start = DateTime.Now;
                if (LogDebug) _log.Debug("Start put file processing: " + filePath);

                if (createFolderIfNotFound)
                {
                    var folderPath = Path.GetDirectoryName(filePath);
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                }

                File.Copy(fileHandle.LocalFilePath, filePath, overwrite);

                var end = DateTime.Now - start;
                if (LogDebug) _log.DebugFormat("Total put file ({0}): {1} ", filePath, end);

                return true;
            });
        }

        public bool SaveFile(string folderName, string fileName, MemoryStream stream, bool createFolderIfNotFound = true)
        {
            return Processing(() =>
            {
                var filePath = FullPath(folderName, fileName);

                var start = DateTime.Now;
                if (LogDebug) _log.Debug("Start put file processing: " + filePath);

                if (createFolderIfNotFound)
                {
                    var folderPath = Path.GetDirectoryName(filePath);
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                }

                using (var writer = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    var data = stream.ToArray();
                    writer.Write(data, 0, data.Length);
                }

                var end = DateTime.Now - start;
                if (LogDebug) _log.DebugFormat("Total put file ({0}): {1} ", filePath, end);

                return true;
            });
        }

        public bool DeleteFile(string folderName, string fileName)
        {
            return Processing(() =>
            {
                var filePath = FullPath(folderName, fileName);
                if (File.Exists(filePath))
                {
                    if (LogDebug) _log.Debug("Delete File: " + filePath);
                    File.Delete(filePath);
                    return true;
                }
                return false;
            });
        }

        public MemoryStream GetFileStream(string folderName, string fileName)
        {
            return Processing(() =>
            {
                var filePath = FullPath(folderName, fileName);
                if (File.Exists(filePath))
                {
                    if (LogDebug) _log.Debug("Get File: " + filePath);
                    return new MemoryStream(File.ReadAllBytes(filePath));
                }

                return new MemoryStream();
            });
        }

        public FileHandle GetFile(string folderName, string fileName, bool keepExtension = false)
        {
            return Processing(() =>
            {
                FileHandle handle = null;
                var filePath = FullPath(folderName, fileName);

                if (File.Exists(filePath))
                {
                    handle = keepExtension ? CreateFile(ext: Path.GetExtension(filePath)) : CreateFile();

                    if (LogDebug) _log.Debug("Get File: " + filePath);
                    File.Copy(filePath, handle.LocalFilePath);
                }

                return handle;
            });
        }

        public bool IsFileExists(string folderName, string fileName)
        {
            return Processing(() =>
            {
                var filePath = FullPath(folderName, fileName);
                return File.Exists(filePath);
            });
        }

        public Dictionary<string, FileHandle> GetFiles(string folderName, params string[] fileNames)
        {
            return Processing(() =>
            {
                var handles = new Dictionary<string, FileHandle>();
                var sharePath = Path.Combine(Setting.ShareRootPath, folderName);

                foreach (var fileName in fileNames)
                {
                    var filePath = Path.Combine(sharePath, fileName);

                    if (!handles.ContainsKey(filePath) && File.Exists(filePath))
                    {
                        var handle = CreateFile();

                        if (LogDebug) _log.Debug("Get File: " + filePath);
                        File.Copy(filePath, handle.LocalFilePath);

                        handles.Add(fileName, handle);
                    }
                }

                return handles;
            });
        }

        public IEnumerable<FileInfoHandle> GetFiles(string folderName, string searchPattern, SearchOption searchOption, bool ignoreHandle = false)
        {
            return Processing(() =>
            {
                var fileInfos = new List<FileInfoHandle>();

                var sharePath = Path.Combine(Setting.ShareRootPath, folderName);
                var filePaths = !string.IsNullOrWhiteSpace(searchPattern) ? Directory.GetFiles(sharePath, searchPattern, searchOption) : Directory.GetFiles(sharePath);

                return filePaths.Select(getFile);
            });

            FileInfoHandle getFile(string filePath)
            {
                FileHandle handle = null;

                if (!ignoreHandle)
                {
                    handle = CreateFile();

                    if (LogDebug) _log.Debug("Get File: " + filePath);
                    File.Copy(filePath, handle.LocalFilePath);
                }

                return new FileInfoHandle { FileName = Path.GetFileName(filePath), FilePath = filePath, Handle = handle };
            }
        }

        public bool DeleteFiles(string folderName, params string[] fileNames)
        {
            return Processing(() =>
            {
                var sharePath = Path.Combine(Setting.ShareRootPath, folderName);

                foreach (var fileName in fileNames)
                {
                    var filePath = Path.Combine(sharePath, fileName);
                    if (File.Exists(filePath))
                    {
                        if (LogDebug) _log.Debug("Delete File: " + filePath);
                        File.Delete(filePath);
                    }
                }

                return true;
            });
        }

        public bool CopyFile(string folderName, string sourceFileName, string destFileName, bool overwrite = true)
            => CopyFile(folderName, sourceFileName, folderName, destFileName, overwrite);

        public bool CopyFile(string sourceFolder, string sourceFileName, string destFolder, string destFileName, bool overwrite = true)
        {
            return Processing(() =>
            {
                var sourceFilePath = FullPath(sourceFolder, sourceFileName);
                if (!File.Exists(sourceFilePath))
                {
                    return false;
                }

                var destFilePath = FullPath(destFolder, destFileName);
                var destFolderPath = Path.GetDirectoryName(destFilePath);

                if (!Directory.Exists(destFolderPath))
                {
                    Directory.CreateDirectory(destFolderPath);
                }

                File.Copy(sourceFilePath, destFilePath, overwrite);

                return true;
            });
        }

        public bool MoveFile(string folderName, string sourceFileName, string destFileName)
            => MoveFile(folderName, sourceFileName, folderName, destFileName);

        public bool MoveFile(string sourceFolder, string sourceFileName, string destFolder, string destFileName)
        {
            return Processing(() =>
            {
                var sourceFilePath = FullPath(sourceFolder, sourceFileName);
                if (!File.Exists(sourceFilePath))
                {
                    return false;
                }

                var destFilePath = FullPath(destFolder, destFileName);
                var destFolderPath = Path.GetDirectoryName(destFilePath);

                if (!Directory.Exists(destFolderPath))
                {
                    Directory.CreateDirectory(destFolderPath);
                }

                File.Move(sourceFilePath, destFilePath);
                return true;
            });
        }

        public void Impersonate(Action process) => _ = Processing(() =>
        {
            process();
            return true;
        });

        #region Processing funcs

        private TResult Processing<TResult>(Func<TResult> action, [CallerMemberName] string callerMemberName = "")
        {
            if (Setting == null || (!Setting.IgnoreImpersonate && _logon == null))
            {
                throw new Exception("ShareFile setting is not yet configured. Please call SetSetting function");
            }

            try
            {
                if (!Setting.IgnoreImpersonate)
                {
                    _logon.Impersonate();
                }

                return action();
            }
            catch (Exception ex)
            {
                _log.Error($"{callerMemberName}: {ex.Message}", ex);
            }
            finally
            {
                if (!Setting.IgnoreImpersonate)
                {
                    _logon.Exit();
                }
            }

            return default;
        }

        public string FullPath(params string[] paths)
        {
            var temp = new List<string>(paths);
            temp.Insert(0, Setting.ShareRootPath);

            return Path.Combine(temp.ToArray());
        }

        #endregion
    }
}