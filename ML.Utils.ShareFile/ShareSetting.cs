
using ML.Common;

namespace ML.Utils.ShareFile
{
    public class ShareSetting
    {
        public string ShareDomain { get; set; }

        public string ShareUserName { get; set; }

        public string SharePassword { get; set; }

        public string ShareRootPath { get; set; }

        public string TempPath { get; set; }

        public bool IgnoreImpersonate { get; set; }

        public static ShareSetting FromAppSetting(string prefix = "FILEDB") => new ShareSetting
        {
            TempPath = std.AppSettings[$"{prefix}_TEMP_PATH"],
            ShareRootPath = std.AppSettings[$"{prefix}_ROOT_FILEPATH"],
            ShareUserName = std.AppSettings[$"{prefix}_USER"],
            SharePassword = std.AppSettings[$"{prefix}_PASSWORD"],
            ShareDomain = std.AppSettings[$"{prefix}_DOMAIN"],
            IgnoreImpersonate = std.AppSettings[$"{prefix}_IGNORE_IMPERSONATE"].ToBool()
        };
    }
}
