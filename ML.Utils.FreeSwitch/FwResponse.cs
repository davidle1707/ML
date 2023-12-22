using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ML.Utils.FreeSwitch
{
    #region User
    public class UserAddNewResponse : BaseResponse
    {
        public string domain_uuid { get; set; }
        public int id { get; set; }
        public string password { get; set; }
        public string resource_uri { get; set; }
        public string user_enabled { get; set; }
        public string username { get; set; }
    }

    public class UserGetResponse : BaseResponse
    {
        public string domain_uuid { get; set; }
        public int id { get; set; }
        public string resource_uri { get; set; }
        public string user_enabled { get; set; }
        public string username { get; set; }
    }

    #endregion

    #region Recording

    public class RecordingGetResponse : BaseResponse
    {
        public string domain_uuid { get; set; }
        public string recording_description { get; set; }
        public string recording_filename { get; set; }
        public string recording_name { get; set; }
        public string recording_uuid { get; set; }
        public string resource_uri { get; set; }
    }

    public class RecordingUploadFileResponse 
    {
        public bool success { get; set; }
        public string reason { get; set; }
        public string domain_uuid { get; set; }
        public string recording_uuid { get; set; }
        public string recording_filename { get; set; }
        public string recording_name { get; set; }
        public string resource_uri { get; set; }
    }

    #endregion

    #region DID

    public class DIDGetResponse : BaseResponse
    {
        public string action { get; set; }
        public string des_app { get; set; }
        public BaseDesData des_data { get; set; }
        public string destination_enabled { get; set; }
        public string did { get; set; }
        public string did_uuid { get; set; }
        public string enabled { get; set; }
        public string resource_uri { get; set; }
        public string transfer_number { get; set; }
    }




    #endregion

    #region BaseClass
    public class BaseDesData
    {
        public string recording { get; set; }
        public string transfer { get; set; }
    }

    public class BaseListResponse<T> : BaseResponse
    {
        public BaseMeta meta { get; set; }

        public List<T> objects { get; set; }
    }

    public class BaseMeta
    {
        public int limit { get; set; }
        public object next { get; set; }
        public int offset { get; set; }
        public object previous { get; set; }
        public int total_count { get; set; }
    }

    public class BaseResponse
    {
        public BaseResponse()
        {
            success = true;
        }

        public string reason { get; set; }
        public bool success { get; set; }
        public string error { get; set; }

    }
    #endregion
}
