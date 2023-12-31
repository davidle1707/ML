﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ML.Utils.DocuSign.Credential {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="LoginResult", Namespace="http://www.docusign.net/API/Credential")]
    [System.SerializableAttribute()]
    public partial class LoginResult : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private bool SuccessField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private ML.Utils.DocuSign.Credential.ErrorCode ErrorCodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AuthenticationMessageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private ML.Utils.DocuSign.Credential.Account[] AccountsField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public bool Success {
            get {
                return this.SuccessField;
            }
            set {
                if ((this.SuccessField.Equals(value) != true)) {
                    this.SuccessField = value;
                    this.RaisePropertyChanged("Success");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=1)]
        public ML.Utils.DocuSign.Credential.ErrorCode ErrorCode {
            get {
                return this.ErrorCodeField;
            }
            set {
                if ((this.ErrorCodeField.Equals(value) != true)) {
                    this.ErrorCodeField = value;
                    this.RaisePropertyChanged("ErrorCode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string AuthenticationMessage {
            get {
                return this.AuthenticationMessageField;
            }
            set {
                if ((object.ReferenceEquals(this.AuthenticationMessageField, value) != true)) {
                    this.AuthenticationMessageField = value;
                    this.RaisePropertyChanged("AuthenticationMessage");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public ML.Utils.DocuSign.Credential.Account[] Accounts {
            get {
                return this.AccountsField;
            }
            set {
                if ((object.ReferenceEquals(this.AccountsField, value) != true)) {
                    this.AccountsField = value;
                    this.RaisePropertyChanged("Accounts");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ErrorCode", Namespace="http://www.docusign.net/API/Credential")]
    public enum ErrorCode : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        User_Does_Not_Exist_In_System = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Account_Lacks_Permissions = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        User_Lacks_Permissions = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        User_Authentication_Failed = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Unspecified_Error = 4,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Success = 5,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Account", Namespace="http://www.docusign.net/API/Credential")]
    [System.SerializableAttribute()]
    public partial class Account : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AccountIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AccountNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EmailField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string BaseUrlField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string AccountID {
            get {
                return this.AccountIDField;
            }
            set {
                if ((object.ReferenceEquals(this.AccountIDField, value) != true)) {
                    this.AccountIDField = value;
                    this.RaisePropertyChanged("AccountID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string AccountName {
            get {
                return this.AccountNameField;
            }
            set {
                if ((object.ReferenceEquals(this.AccountNameField, value) != true)) {
                    this.AccountNameField = value;
                    this.RaisePropertyChanged("AccountName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string UserID {
            get {
                return this.UserIDField;
            }
            set {
                if ((object.ReferenceEquals(this.UserIDField, value) != true)) {
                    this.UserIDField = value;
                    this.RaisePropertyChanged("UserID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string UserName {
            get {
                return this.UserNameField;
            }
            set {
                if ((object.ReferenceEquals(this.UserNameField, value) != true)) {
                    this.UserNameField = value;
                    this.RaisePropertyChanged("UserName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string Email {
            get {
                return this.EmailField;
            }
            set {
                if ((object.ReferenceEquals(this.EmailField, value) != true)) {
                    this.EmailField = value;
                    this.RaisePropertyChanged("Email");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string BaseUrl {
            get {
                return this.BaseUrlField;
            }
            set {
                if ((object.ReferenceEquals(this.BaseUrlField, value) != true)) {
                    this.BaseUrlField = value;
                    this.RaisePropertyChanged("BaseUrl");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.docusign.net/API/Credential", ConfigurationName="Credential.CredentialSoap")]
    public interface CredentialSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.docusign.net/API/Credential/Ping", ReplyAction="*")]
        bool Ping();
        
        // CODEGEN: Generating message contract since element name Email from namespace http://www.docusign.net/API/Credential is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://www.docusign.net/API/Credential/Login", ReplyAction="*")]
        ML.Utils.DocuSign.Credential.LoginResponse Login(ML.Utils.DocuSign.Credential.LoginRequest request);
        
        // CODEGEN: Generating message contract since element name Email from namespace http://www.docusign.net/API/Credential is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://www.docusign.net/API/Credential/GetAuthenticationToken", ReplyAction="*")]
        ML.Utils.DocuSign.Credential.GetAuthenticationTokenResponse GetAuthenticationToken(ML.Utils.DocuSign.Credential.GetAuthenticationTokenRequest request);
        
        // CODEGEN: Generating message contract since element name Email from namespace http://www.docusign.net/API/Credential is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://www.docusign.net/API/Credential/RequestSenderToken", ReplyAction="*")]
        ML.Utils.DocuSign.Credential.RequestSenderTokenResponse RequestSenderToken(ML.Utils.DocuSign.Credential.RequestSenderTokenRequest request);
        
        // CODEGEN: Generating message contract since element name Email from namespace http://www.docusign.net/API/Credential is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://www.docusign.net/API/Credential/RequestCorrectToken", ReplyAction="*")]
        ML.Utils.DocuSign.Credential.RequestCorrectTokenResponse RequestCorrectToken(ML.Utils.DocuSign.Credential.RequestCorrectTokenRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class LoginRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="Login", Namespace="http://www.docusign.net/API/Credential", Order=0)]
        public ML.Utils.DocuSign.Credential.LoginRequestBody Body;
        
        public LoginRequest() {
        }
        
        public LoginRequest(ML.Utils.DocuSign.Credential.LoginRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.docusign.net/API/Credential")]
    public partial class LoginRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string Email;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string Password;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public bool ReturnBaseUrl;
        
        public LoginRequestBody() {
        }
        
        public LoginRequestBody(string Email, string Password, bool ReturnBaseUrl) {
            this.Email = Email;
            this.Password = Password;
            this.ReturnBaseUrl = ReturnBaseUrl;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class LoginResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="LoginResponse", Namespace="http://www.docusign.net/API/Credential", Order=0)]
        public ML.Utils.DocuSign.Credential.LoginResponseBody Body;
        
        public LoginResponse() {
        }
        
        public LoginResponse(ML.Utils.DocuSign.Credential.LoginResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.docusign.net/API/Credential")]
    public partial class LoginResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ML.Utils.DocuSign.Credential.LoginResult LoginResult;
        
        public LoginResponseBody() {
        }
        
        public LoginResponseBody(ML.Utils.DocuSign.Credential.LoginResult LoginResult) {
            this.LoginResult = LoginResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetAuthenticationTokenRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetAuthenticationToken", Namespace="http://www.docusign.net/API/Credential", Order=0)]
        public ML.Utils.DocuSign.Credential.GetAuthenticationTokenRequestBody Body;
        
        public GetAuthenticationTokenRequest() {
        }
        
        public GetAuthenticationTokenRequest(ML.Utils.DocuSign.Credential.GetAuthenticationTokenRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.docusign.net/API/Credential")]
    public partial class GetAuthenticationTokenRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string Email;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string Password;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string AccountID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string GoToEnvelopeID;
        
        public GetAuthenticationTokenRequestBody() {
        }
        
        public GetAuthenticationTokenRequestBody(string Email, string Password, string AccountID, string GoToEnvelopeID) {
            this.Email = Email;
            this.Password = Password;
            this.AccountID = AccountID;
            this.GoToEnvelopeID = GoToEnvelopeID;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetAuthenticationTokenResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetAuthenticationTokenResponse", Namespace="http://www.docusign.net/API/Credential", Order=0)]
        public ML.Utils.DocuSign.Credential.GetAuthenticationTokenResponseBody Body;
        
        public GetAuthenticationTokenResponse() {
        }
        
        public GetAuthenticationTokenResponse(ML.Utils.DocuSign.Credential.GetAuthenticationTokenResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.docusign.net/API/Credential")]
    public partial class GetAuthenticationTokenResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetAuthenticationTokenResult;
        
        public GetAuthenticationTokenResponseBody() {
        }
        
        public GetAuthenticationTokenResponseBody(string GetAuthenticationTokenResult) {
            this.GetAuthenticationTokenResult = GetAuthenticationTokenResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class RequestSenderTokenRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="RequestSenderToken", Namespace="http://www.docusign.net/API/Credential", Order=0)]
        public ML.Utils.DocuSign.Credential.RequestSenderTokenRequestBody Body;
        
        public RequestSenderTokenRequest() {
        }
        
        public RequestSenderTokenRequest(ML.Utils.DocuSign.Credential.RequestSenderTokenRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.docusign.net/API/Credential")]
    public partial class RequestSenderTokenRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string Email;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string Password;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string AccountID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string EnvelopeID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string ReturnURL;
        
        public RequestSenderTokenRequestBody() {
        }
        
        public RequestSenderTokenRequestBody(string Email, string Password, string AccountID, string EnvelopeID, string ReturnURL) {
            this.Email = Email;
            this.Password = Password;
            this.AccountID = AccountID;
            this.EnvelopeID = EnvelopeID;
            this.ReturnURL = ReturnURL;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class RequestSenderTokenResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="RequestSenderTokenResponse", Namespace="http://www.docusign.net/API/Credential", Order=0)]
        public ML.Utils.DocuSign.Credential.RequestSenderTokenResponseBody Body;
        
        public RequestSenderTokenResponse() {
        }
        
        public RequestSenderTokenResponse(ML.Utils.DocuSign.Credential.RequestSenderTokenResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.docusign.net/API/Credential")]
    public partial class RequestSenderTokenResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string RequestSenderTokenResult;
        
        public RequestSenderTokenResponseBody() {
        }
        
        public RequestSenderTokenResponseBody(string RequestSenderTokenResult) {
            this.RequestSenderTokenResult = RequestSenderTokenResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class RequestCorrectTokenRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="RequestCorrectToken", Namespace="http://www.docusign.net/API/Credential", Order=0)]
        public ML.Utils.DocuSign.Credential.RequestCorrectTokenRequestBody Body;
        
        public RequestCorrectTokenRequest() {
        }
        
        public RequestCorrectTokenRequest(ML.Utils.DocuSign.Credential.RequestCorrectTokenRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.docusign.net/API/Credential")]
    public partial class RequestCorrectTokenRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string Email;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string Password;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string EnvelopeID;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public bool SuppressNavigation;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string ReturnURL;
        
        public RequestCorrectTokenRequestBody() {
        }
        
        public RequestCorrectTokenRequestBody(string Email, string Password, string EnvelopeID, bool SuppressNavigation, string ReturnURL) {
            this.Email = Email;
            this.Password = Password;
            this.EnvelopeID = EnvelopeID;
            this.SuppressNavigation = SuppressNavigation;
            this.ReturnURL = ReturnURL;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class RequestCorrectTokenResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="RequestCorrectTokenResponse", Namespace="http://www.docusign.net/API/Credential", Order=0)]
        public ML.Utils.DocuSign.Credential.RequestCorrectTokenResponseBody Body;
        
        public RequestCorrectTokenResponse() {
        }
        
        public RequestCorrectTokenResponse(ML.Utils.DocuSign.Credential.RequestCorrectTokenResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.docusign.net/API/Credential")]
    public partial class RequestCorrectTokenResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string RequestCorrectTokenResult;
        
        public RequestCorrectTokenResponseBody() {
        }
        
        public RequestCorrectTokenResponseBody(string RequestCorrectTokenResult) {
            this.RequestCorrectTokenResult = RequestCorrectTokenResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface CredentialSoapChannel : ML.Utils.DocuSign.Credential.CredentialSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CredentialSoapClient : System.ServiceModel.ClientBase<ML.Utils.DocuSign.Credential.CredentialSoap>, ML.Utils.DocuSign.Credential.CredentialSoap {
        
        public CredentialSoapClient() {
        }
        
        public CredentialSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CredentialSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CredentialSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CredentialSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool Ping() {
            return base.Channel.Ping();
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ML.Utils.DocuSign.Credential.LoginResponse ML.Utils.DocuSign.Credential.CredentialSoap.Login(ML.Utils.DocuSign.Credential.LoginRequest request) {
            return base.Channel.Login(request);
        }
        
        public ML.Utils.DocuSign.Credential.LoginResult Login(string Email, string Password, bool ReturnBaseUrl) {
            ML.Utils.DocuSign.Credential.LoginRequest inValue = new ML.Utils.DocuSign.Credential.LoginRequest();
            inValue.Body = new ML.Utils.DocuSign.Credential.LoginRequestBody();
            inValue.Body.Email = Email;
            inValue.Body.Password = Password;
            inValue.Body.ReturnBaseUrl = ReturnBaseUrl;
            ML.Utils.DocuSign.Credential.LoginResponse retVal = ((ML.Utils.DocuSign.Credential.CredentialSoap)(this)).Login(inValue);
            return retVal.Body.LoginResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ML.Utils.DocuSign.Credential.GetAuthenticationTokenResponse ML.Utils.DocuSign.Credential.CredentialSoap.GetAuthenticationToken(ML.Utils.DocuSign.Credential.GetAuthenticationTokenRequest request) {
            return base.Channel.GetAuthenticationToken(request);
        }
        
        public string GetAuthenticationToken(string Email, string Password, string AccountID, string GoToEnvelopeID) {
            ML.Utils.DocuSign.Credential.GetAuthenticationTokenRequest inValue = new ML.Utils.DocuSign.Credential.GetAuthenticationTokenRequest();
            inValue.Body = new ML.Utils.DocuSign.Credential.GetAuthenticationTokenRequestBody();
            inValue.Body.Email = Email;
            inValue.Body.Password = Password;
            inValue.Body.AccountID = AccountID;
            inValue.Body.GoToEnvelopeID = GoToEnvelopeID;
            ML.Utils.DocuSign.Credential.GetAuthenticationTokenResponse retVal = ((ML.Utils.DocuSign.Credential.CredentialSoap)(this)).GetAuthenticationToken(inValue);
            return retVal.Body.GetAuthenticationTokenResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ML.Utils.DocuSign.Credential.RequestSenderTokenResponse ML.Utils.DocuSign.Credential.CredentialSoap.RequestSenderToken(ML.Utils.DocuSign.Credential.RequestSenderTokenRequest request) {
            return base.Channel.RequestSenderToken(request);
        }
        
        public string RequestSenderToken(string Email, string Password, string AccountID, string EnvelopeID, string ReturnURL) {
            ML.Utils.DocuSign.Credential.RequestSenderTokenRequest inValue = new ML.Utils.DocuSign.Credential.RequestSenderTokenRequest();
            inValue.Body = new ML.Utils.DocuSign.Credential.RequestSenderTokenRequestBody();
            inValue.Body.Email = Email;
            inValue.Body.Password = Password;
            inValue.Body.AccountID = AccountID;
            inValue.Body.EnvelopeID = EnvelopeID;
            inValue.Body.ReturnURL = ReturnURL;
            ML.Utils.DocuSign.Credential.RequestSenderTokenResponse retVal = ((ML.Utils.DocuSign.Credential.CredentialSoap)(this)).RequestSenderToken(inValue);
            return retVal.Body.RequestSenderTokenResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ML.Utils.DocuSign.Credential.RequestCorrectTokenResponse ML.Utils.DocuSign.Credential.CredentialSoap.RequestCorrectToken(ML.Utils.DocuSign.Credential.RequestCorrectTokenRequest request) {
            return base.Channel.RequestCorrectToken(request);
        }
        
        public string RequestCorrectToken(string Email, string Password, string EnvelopeID, bool SuppressNavigation, string ReturnURL) {
            ML.Utils.DocuSign.Credential.RequestCorrectTokenRequest inValue = new ML.Utils.DocuSign.Credential.RequestCorrectTokenRequest();
            inValue.Body = new ML.Utils.DocuSign.Credential.RequestCorrectTokenRequestBody();
            inValue.Body.Email = Email;
            inValue.Body.Password = Password;
            inValue.Body.EnvelopeID = EnvelopeID;
            inValue.Body.SuppressNavigation = SuppressNavigation;
            inValue.Body.ReturnURL = ReturnURL;
            ML.Utils.DocuSign.Credential.RequestCorrectTokenResponse retVal = ((ML.Utils.DocuSign.Credential.CredentialSoap)(this)).RequestCorrectToken(inValue);
            return retVal.Body.RequestCorrectTokenResult;
        }
    }
}
