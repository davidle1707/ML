using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace ML.Utils.FreeSwitch
{
    public class FwProcessor
    {
        private const string UserApi = "api/v1/user/";
        private const string RecordingApi = "api/v1/recording/";
        private const string UploadRecordingApi = "api/v1/upload-recording/";
        private const string DidApi = "api/v1/did/";

        private readonly FwSetting _setting;

        public FwProcessor(FwSetting setting)
        {
            _setting = setting;
        }

        #region User
        public UserAddNewResponse UserAddNew(UserAddNewRequest request)
        {
            var resReq = new RestRequest(UserApi, Method.POST);
            resReq.AddJsonBody(request);
            var resRes = Execute<UserAddNewResponse>(resReq);
            if (resRes.ErrorException != null)
            {
                return new UserAddNewResponse
                {
                    success = false,
                    reason = resRes.ErrorMessage
                };
            }
            return resRes.Data;
        }

        public UserGetResponse UserChangePassword(UserChangePasswordRequest request)
        {
            var resReq = new RestRequest(string.Format(UserApi + "/{0}/", request.userid), Method.PATCH);
            resReq.AddJsonBody(request);
            var resRes = Execute<UserGetResponse>(resReq);
            if (resRes.ErrorException != null)
            {
                return new UserGetResponse
                {
                    success = false,
                    reason = resRes.ErrorMessage
                };
            }
            return resRes.Data;
        }

        public UserGetResponse UserChangeStatus(UserChangeStatusRequest request)
        {
            var resReq = new RestRequest(string.Format(UserApi + "/{0}/", request.userid), Method.PATCH);
            resReq.AddJsonBody(request);
            var resRes = Execute<UserGetResponse>(resReq);

            if (resRes.ErrorException != null)
            {
                return new UserGetResponse
                {
                    success = false,
                    reason = resRes.ErrorMessage
                };
            }

            return resRes.Data;
        }

        public BaseListResponse<UserGetResponse> UserGetList(UserGetListRequest request)
        {
            var resReq = new RestRequest(UserApi, Method.GET);
            resReq.AddJsonBody(request);
            var resRes = Execute<BaseListResponse<UserGetResponse>>(resReq);

            if (resRes.ErrorException != null)
            {
                return new BaseListResponse<UserGetResponse>
                {
                    success = false,
                    reason = resRes.ErrorMessage
                };
            }

            return resRes.Data;
        }

        public async Task<UserAddNewResponse> UserAddNewAsync(UserAddNewRequest request)
        {
            var resReq = new RestRequest(UserApi, Method.POST);
            resReq.AddJsonBody(request);
            var resRes = await ExecuteAsync<UserAddNewResponse>(resReq);
            if (resRes.ErrorException != null)
            {
                return new UserAddNewResponse
                {
                    success = false,
                    reason = resRes.ErrorMessage
                };
            }
            return resRes.Data;
        }

        public async Task<UserGetResponse> UserChangePasswordAsync(UserChangePasswordRequest request)
        {
            var resReq = new RestRequest(string.Format(UserApi + "/{0}/", request.userid), Method.PATCH);
            resReq.AddJsonBody(request);
            var resRes = await ExecuteAsync<UserGetResponse>(resReq);
            if (resRes.ErrorException != null)
            {
                return new UserGetResponse
                {
                    success = false,
                    reason = resRes.ErrorMessage
                };
            }
            return resRes.Data;
        }

        public async Task<UserGetResponse> UserChangeStatusAsync(UserChangeStatusRequest request)
        {
            var resReq = new RestRequest(string.Format(UserApi + "/{0}/", request.userid), Method.PATCH);
            resReq.AddJsonBody(request);
            var resRes = await ExecuteAsync<UserGetResponse>(resReq);

            if (resRes.ErrorException != null)
            {
                return new UserGetResponse
                {
                    success = false,
                    reason = resRes.ErrorMessage
                };
            }

            return resRes.Data;
        }

        public async Task<BaseListResponse<UserGetResponse>> UserGetListAsync(UserGetListRequest request)
        {
            var resReq = new RestRequest(UserApi, Method.GET);
            resReq.AddJsonBody(request);
            var resRes = await ExecuteAsync<BaseListResponse<UserGetResponse>>(resReq);

            if (resRes.ErrorException != null)
            {
                return new BaseListResponse<UserGetResponse>
                {
                    success = false,
                    reason = resRes.ErrorMessage
                };
            }

            return resRes.Data;
        }

        #endregion

        #region Recording

        public RecordingUploadFileResponse RecordingUploadFile(RecordingUploadFileRequest request)
        {
            var resReq = new RestRequest(UploadRecordingApi, Method.POST);

            resReq.AddParameter("name", request.filename);

            var data = new byte[request.filestream.Length];
            request.filestream.Read(data, 0, (int)request.filestream.Length);
            resReq.AddFile("recording", data, request.filename, request.filetype);

            var resRes = Execute<RecordingUploadFileResponse>(resReq);
            if (resRes.ErrorException != null)
            {
                return new RecordingUploadFileResponse
                {
                    success = false,
                    reason = resRes.ErrorMessage
                };
            }
            return resRes.Data;
        }

        public BaseListResponse<RecordingGetResponse> RecordingGetList()
        {
            var resReq = new RestRequest(RecordingApi, Method.GET);
            var resRes = Execute<BaseListResponse<RecordingGetResponse>>(resReq);
            if (resRes.ErrorException != null)
            {
                return new BaseListResponse<RecordingGetResponse>
                {
                    success = false,
                    reason = resRes.ErrorMessage
                };
            }
            return resRes.Data;
        }

        public RecordingGetResponse RecordingGet(RecordingGetRequest request)
        {
            var resReq = new RestRequest(string.Format(RecordingApi + "/{0}/", request.recording_uuid), Method.GET);
            var resRes = Execute<RecordingGetResponse>(resReq);
            if (resRes.ErrorException != null)
            {
                return new RecordingGetResponse
                {
                    success = false,
                    reason = resRes.ErrorMessage
                };
            }
            return resRes.Data;
        }

        public BaseResponse RecordingDelete(RecordingDeleteRequest request)
        {
            var resReq = new RestRequest(string.Format(RecordingApi + "/{0}/", request.recording_uuid), Method.DELETE);
            var resRes = Execute<RecordingGetResponse>(resReq);
            if (resRes.ErrorException != null)
            {
                return new BaseResponse
                {
                    success = false,
                    reason = resRes.ErrorMessage
                };
            }
            return resRes.Data;
        }

        public async Task<RecordingUploadFileResponse> RecordingUploadFileAsync(RecordingUploadFileRequest request)
        {
            var resReq = new RestRequest(UploadRecordingApi, Method.POST);

            resReq.AddParameter("name", request.filename);

            var data = new byte[request.filestream.Length];
            request.filestream.Read(data, 0, (int)request.filestream.Length);
            resReq.AddFile("recording", data, request.filename, request.filetype);

            var resRes = await ExecuteAsync<RecordingUploadFileResponse>(resReq);
            if (resRes.ErrorException != null)
            {
                return new RecordingUploadFileResponse
                {
                    success = false,
                    reason = resRes.ErrorMessage
                };
            }
            return resRes.Data;
        }

        public async Task<BaseListResponse<RecordingGetResponse>> RecordingGetListAsync()
        {
            var resReq = new RestRequest(RecordingApi, Method.GET);
            var resRes = await ExecuteAsync<BaseListResponse<RecordingGetResponse>>(resReq);
            if (resRes.ErrorException != null)
            {
                return new BaseListResponse<RecordingGetResponse>
                {
                    success = false,
                    reason = resRes.ErrorMessage
                };
            }
            return resRes.Data;
        }

        public async Task<RecordingGetResponse> RecordingGetAsync(RecordingGetRequest request)
        {
            var resReq = new RestRequest(string.Format(RecordingApi + "/{0}/", request.recording_uuid), Method.GET);
            var resRes = await ExecuteAsync<RecordingGetResponse>(resReq);
            if (resRes.ErrorException != null)
            {
                return new RecordingGetResponse
                {
                    success = false,
                    reason = resRes.ErrorMessage
                };
            }
            return resRes.Data;
        }

        public async Task<BaseResponse> RecordingDeleteAsync(RecordingDeleteRequest request)
        {
            var resReq = new RestRequest(string.Format(RecordingApi + "/{0}/", request.recording_uuid), Method.DELETE);
            var resRes = await ExecuteAsync<RecordingGetResponse>(resReq);
            if (resRes.ErrorException != null)
            {
                return new BaseResponse
                {
                    success = false,
                    reason = resRes.ErrorMessage
                };
            }
            return resRes.Data;
        }

        #endregion

        #region DID
        public DIDGetResponse DIDSignupForward(DIDSignupForwardRequest request)
        {
            var resReq = new RestRequest(DidApi, Method.POST);

            //set value forward
            request.action = "forward";

            resReq.AddJsonBody(request);
            var resRes = Execute<DIDGetResponse>(resReq);
            if (resRes.ErrorException != null)
            {
                return new DIDGetResponse
                {
                    success = false,
                    reason = resRes.ErrorMessage
                };
            }
            return resRes.Data;
        }

        public DIDGetResponse DIDGet(DIDGetRequest request)
        {
            var resReq = new RestRequest(string.Format(DidApi + "/{0}/", request.did_uuid), Method.GET);
            var resRes = Execute<DIDGetResponse>(resReq);
            if (resRes.ErrorException != null)
            {
                return new DIDGetResponse
                {
                    success = false,
                    reason = resRes.ErrorMessage
                };
            }
            return resRes.Data;
        }

        public BaseListResponse<DIDGetResponse> DIDGetList(DIDGetListRequest request)
        {
            var resReq = new RestRequest(DidApi, Method.GET);
            if (!string.IsNullOrEmpty(request.did))
            {
                resReq.AddParameter("did", request.did);
            }

            if (!string.IsNullOrEmpty(request.did__startswith))
            {
                resReq.AddParameter("did__startswith", request.did__startswith);
            }

            var resRes = Execute<BaseListResponse<DIDGetResponse>>(resReq);
            if (resRes.ErrorException != null)
            {
                return new BaseListResponse<DIDGetResponse>
                {
                    success = false,
                    reason = resRes.ErrorMessage
                };
            }
            return resRes.Data;
        }

        public DIDGetResponse DIDUpdate(DIDUpdateRequest request)
        {
            var resReq = new RestRequest(string.Format(DidApi + "/{0}/", request.did_uuid), Method.PATCH);
            resReq.AddJsonBody(request);
            var resRes = Execute<DIDGetResponse>(resReq);
            if (resRes.ErrorException != null)
            {
                return new DIDGetResponse
                {
                    success = false,
                    reason = resRes.ErrorMessage
                };
            }
            return resRes.Data;
        }

        public BaseResponse DIDDelete(DIDDeleteRequest request)
        {
            var resReq = new RestRequest(string.Format(DidApi + "/{0}/", request.did_uuid), Method.DELETE);
            var resRes = Execute<BaseResponse>(resReq);
            if (resRes.ErrorException != null)
            {
                return new BaseResponse
                {
                    success = false,
                    reason = resRes.ErrorMessage
                };
            }

            return resRes.Data;
        }

        public async Task<DIDGetResponse> DIDSignupForwardAsync(DIDSignupForwardRequest request)
        {
            var resReq = new RestRequest(DidApi, Method.POST);

            //set value forward
            request.action = "forward";

            resReq.AddJsonBody(request);
            var resRes = await ExecuteAsync<DIDGetResponse>(resReq);
            if (resRes.ErrorException != null)
            {
                return new DIDGetResponse
                {
                    success = false,
                    reason = resRes.ErrorMessage
                };
            }
            return resRes.Data;
        }

        public async Task<DIDGetResponse> DIDGetAsync(DIDGetRequest request)
        {
            var resReq = new RestRequest(string.Format(DidApi + "/{0}/", request.did_uuid), Method.GET);
            var resRes = await ExecuteAsync<DIDGetResponse>(resReq);
            if (resRes.ErrorException != null)
            {
                return new DIDGetResponse
                {
                    success = false,
                    reason = resRes.ErrorMessage
                };
            }
            return resRes.Data;
        }

        public async Task<BaseListResponse<DIDGetResponse>> DIDGetListAsync(DIDGetListRequest request)
        {
            var resReq = new RestRequest(DidApi, Method.GET);
            if (!string.IsNullOrEmpty(request.did))
            {
                resReq.AddParameter("did", request.did);
            }

            if (!string.IsNullOrEmpty(request.did__startswith))
            {
                resReq.AddParameter("did__startswith", request.did__startswith);
            }

            var resRes = await ExecuteAsync<BaseListResponse<DIDGetResponse>>(resReq);
            if (resRes.ErrorException != null)
            {
                return new BaseListResponse<DIDGetResponse>
                {
                    success = false,
                    reason = resRes.ErrorMessage
                };
            }
            return resRes.Data;
        }

        public async Task<DIDGetResponse> DIDUpdateAsync(DIDUpdateRequest request)
        {
            var resReq = new RestRequest(string.Format(DidApi + "/{0}/", request.did_uuid), Method.PATCH);
            resReq.AddJsonBody(request);
            var resRes = await ExecuteAsync<DIDGetResponse>(resReq);
            if (resRes.ErrorException != null)
            {
                return new DIDGetResponse
                {
                    success = false,
                    reason = resRes.ErrorMessage
                };
            }
            return resRes.Data;
        }

        public async Task<BaseResponse> DIDDeleteAsync(DIDDeleteRequest request)
        {
            var resReq = new RestRequest(string.Format(DidApi + "/{0}/", request.did_uuid), Method.DELETE);
            var resRes = await ExecuteAsync<BaseResponse>(resReq);
            if (resRes.ErrorException != null)
            {
                return new BaseResponse
                {
                    success = false,
                    reason = resRes.ErrorMessage
                };
            }

            return resRes.Data;
        }

        #endregion

        #region Private function
        private async Task<IRestResponse<T>> ExecuteAsync<T>(IRestRequest request) where T : class, new()
        {

            var client = new RestClient(_setting.BaseUrl)
            {
                Authenticator = new HttpBasicAuthenticator(_setting.UserName, _setting.Password)
            };

            var taskCompletionSource = new TaskCompletionSource<IRestResponse<T>>();
            client.ExecuteAsync<T>(request, restResponse =>
            {
                taskCompletionSource.SetResult(restResponse);
            });

            return await taskCompletionSource.Task;
        }

        private IRestResponse<T> Execute<T>(IRestRequest request) where T : class, new()
        {
            var client = new RestClient(_setting.BaseUrl)
            {
                Authenticator = new HttpBasicAuthenticator(_setting.UserName, _setting.Password)
            };

            return client.Execute<T>(request);
        }

        private CredentialCache GetCredential()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
            var credentialCache = new CredentialCache
            {
                {
                    new Uri(_setting.BaseUrl), "Basic",
                    new NetworkCredential(_setting.UserName,_setting.Password)
                }
            };
            return credentialCache;
        }
        #endregion
    }
}

