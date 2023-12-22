using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.Utils.FreeSwitch
{
    #region User

    public class UserAddNewRequest
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    public class UserChangePasswordRequest
    {
        public string userid { get; set; }
        public string password { get; set; }
    }

    public class UserChangeStatusRequest
    {
        public string userid { get; set; }
        public string user_enabled { get; set; }
    }

    public class UserGetListRequest
    {
        public string username { get; set; }
    }

    #endregion

    #region Recording

    public class RecordingGetRequest
    {
        public string recording_uuid { get; set; }
    }

    public class RecordingDeleteRequest
    {
        public string recording_uuid { get; set; }
    }

    public class RecordingUploadFileRequest
    {
        public string filename { get; set; }
        public string filetype{ get; set; }
        public Stream filestream { get; set; }
    }

    #endregion

    #region DID

    public class DIDGetRequest
    {
        public string did_uuid { get; set; }
    }

    public class DIDDeleteRequest
    {
        public string did_uuid { get; set; }
    }

    public class DIDUpdateRequest
    {
        public string did_uuid { get; set; }

        public string action { get; set; }

        public string transfer_number { get; set; }
    }

    public class DIDGetListRequest 
    {
        public string did { get; set; }
        public string did__startswith { get; set; }
    }

    public class DIDSignupForwardRequest : DIDSignupRequest
    {
        public string transfer_number { get; set; }
        public string recording { get; set; }
    }

    public class DIDSignupRequest
    {
        public string did { get; set; }
        public string enabled { get; set; }
        public string action { get; set; }
    }

    #endregion
}

