using log4net;
using ML.Common;
using ML.Utils.OnlineFantasyData.Entities;
using ML.Utils.ShareFile;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace ML.Utils.OnlineFantasyData
{
	public abstract class OnlineDataProvider
	{
		internal readonly ILog Log;

		internal DateTime LocalNow
		{
			get { return DateTime.UtcNow.ConvertToLocal(std.AppSettings["TimeZone"]); }
		}

		internal bool StoreToFile { get; set; }

		private readonly ShareFileManager _fileManager;

		protected OnlineDataProvider(Type inherit)
		{
			Log = LogManager.GetLogger(inherit);

			_fileManager = new ShareFileManager(new ShareSetting
			                                    {
													IgnoreImpersonate = std.AppSettings["FILESHARE_IGNORE_IMPERSONATE"].ToBool(),
													TempPath = std.AppSettings["FILESHARE_TEMP_PATH"],
													ShareRootPath = std.AppSettings["FILESHARE_ROOT_FILEPATH"],
													ShareUserName = std.AppSettings["FILESHARE_USER"],
													SharePassword = std.AppSettings["FILESHARE_PASSWORD"]
			                                    });
		}

		internal abstract string ProviderName { get; }

		internal abstract List<Player> GetMostUsedPlayers(string sport, DateTime? estDate = null);

		internal abstract List<ProjectedPlayer> GetProjectedPlayers(string sport, DateTime? estDate = null);

		internal abstract void Download(string sport, DateTime? estDate = null);

		internal void WriteToFile(string fileKey, string content)
		{
			if (!StoreToFile)
			{
				return;
			}

			try
			{
				using (var fileHandle = _fileManager.CreateFile())
				{
					File.WriteAllText(fileHandle.LocalFilePath, content);

					_fileManager.SaveFile(Path.Combine("OnlineFantasyData", ProviderName ?? string.Empty), fileKey, fileHandle);
				}
			}
			catch (Exception ex)
			{
				Log.Error(ex);
			}
		}

		internal string ReadFromFile(string fileKey)
		{
			if (StoreToFile)
			{
				try
				{
					using (var fileHandle = _fileManager.GetFile(Path.Combine("OnlineFantasyData", ProviderName ?? string.Empty), fileKey))
					{
						if (fileHandle != null)
						{
							return File.ReadAllText(fileHandle.LocalFilePath);
						}
					}
				}
				catch (Exception ex)
				{
					Log.Error(ex);
				}
			}

			return string.Empty;
		}

		internal T DeserializeFromJson<T>(string json)
		{
			return JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings
			{
				Error = (sender, args) =>
				{
					args.ErrorContext.Handled = true;
					//Log.Error(args.ErrorContext.Error);
				}
			});
		}

		internal object DeserializeFromJson(string json)
		{
			return JsonConvert.DeserializeObject(json, new JsonSerializerSettings
			{
				Error = (sender, args) =>
				{
					args.ErrorContext.Handled = true;
					//Log.Error(args.ErrorContext.Error);
				}
			});
		}
	}
}
