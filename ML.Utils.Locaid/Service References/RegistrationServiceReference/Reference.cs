﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ML.Utils.Locaid.RegistrationServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://impl.webservice.asl.locaid.net/RegistrationService", ConfigurationName="RegistrationServiceReference.RegistrationServicePortType")]
    public interface RegistrationServicePortType {
        
        // CODEGEN: Generating message contract since the operation registerPhone is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        ML.Utils.Locaid.RegistrationServiceReference.registerPhoneResponse1 registerPhone(ML.Utils.Locaid.RegistrationServiceReference.registerPhoneRequest1 request);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        System.Threading.Tasks.Task<ML.Utils.Locaid.RegistrationServiceReference.registerPhoneResponse1> registerPhoneAsync(ML.Utils.Locaid.RegistrationServiceReference.registerPhoneRequest1 request);
        
        // CODEGEN: Generating message contract since the operation getPhoneStatus is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        ML.Utils.Locaid.RegistrationServiceReference.getPhoneStatusResponse1 getPhoneStatus(ML.Utils.Locaid.RegistrationServiceReference.getPhoneStatusRequest1 request);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        System.Threading.Tasks.Task<ML.Utils.Locaid.RegistrationServiceReference.getPhoneStatusResponse1> getPhoneStatusAsync(ML.Utils.Locaid.RegistrationServiceReference.getPhoneStatusRequest1 request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18408")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://webservice.asl.locaid.net/RegistrationService")]
    public partial class RegisterPhoneRequest : object, System.ComponentModel.INotifyPropertyChanged {
        
        private Credentials credentialsField;
        
        private RegistrationAppPhones appPhonesField;
        
        private string trackingIDField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public Credentials credentials {
            get {
                return this.credentialsField;
            }
            set {
                this.credentialsField = value;
                this.RaisePropertyChanged("credentials");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public RegistrationAppPhones appPhones {
            get {
                return this.appPhonesField;
            }
            set {
                this.appPhonesField = value;
                this.RaisePropertyChanged("appPhones");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string trackingID {
            get {
                return this.trackingIDField;
            }
            set {
                this.trackingIDField = value;
                this.RaisePropertyChanged("trackingID");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18408")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://webservice.common.asl.locaid.net/")]
    public partial class Credentials : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string loginField;
        
        private string passwordField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string login {
            get {
                return this.loginField;
            }
            set {
                this.loginField = value;
                this.RaisePropertyChanged("login");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string password {
            get {
                return this.passwordField;
            }
            set {
                this.passwordField = value;
                this.RaisePropertyChanged("password");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18408")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://webservice.common.asl.locaid.net/")]
    public partial class ClassIdPhoneStatus : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string classIdField;
        
        private string subscriptionField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string classId {
            get {
                return this.classIdField;
            }
            set {
                this.classIdField = value;
                this.RaisePropertyChanged("classId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string subscription {
            get {
                return this.subscriptionField;
            }
            set {
                this.subscriptionField = value;
                this.RaisePropertyChanged("subscription");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18408")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://webservice.common.asl.locaid.net/")]
    public partial class MsisdnPhoneStatus : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string msisdnField;
        
        private Response responseField;
        
        private ClassIdPhoneStatus[] classIdPhoneStatusField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string msisdn {
            get {
                return this.msisdnField;
            }
            set {
                this.msisdnField = value;
                this.RaisePropertyChanged("msisdn");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public Response response {
            get {
                return this.responseField;
            }
            set {
                this.responseField = value;
                this.RaisePropertyChanged("response");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("classIdPhoneStatus", Order=2)]
        public ClassIdPhoneStatus[] classIdPhoneStatus {
            get {
                return this.classIdPhoneStatusField;
            }
            set {
                this.classIdPhoneStatusField = value;
                this.RaisePropertyChanged("classIdPhoneStatus");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18408")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://webservice.common.asl.locaid.net/")]
    public partial class Response : object, System.ComponentModel.INotifyPropertyChanged {
        
        private StatusTypeEnum statusField;
        
        private bool statusFieldSpecified;
        
        private Error errorField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public StatusTypeEnum status {
            get {
                return this.statusField;
            }
            set {
                this.statusField = value;
                this.RaisePropertyChanged("status");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool statusSpecified {
            get {
                return this.statusFieldSpecified;
            }
            set {
                this.statusFieldSpecified = value;
                this.RaisePropertyChanged("statusSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public Error error {
            get {
                return this.errorField;
            }
            set {
                this.errorField = value;
                this.RaisePropertyChanged("error");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18408")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://webservice.common.asl.locaid.net/")]
    public enum StatusTypeEnum {
        
        /// <remarks/>
        OK,
        
        /// <remarks/>
        ERROR,
        
        /// <remarks/>
        NOT_FOUND,
        
        /// <remarks/>
        FOUND,
        
        /// <remarks/>
        IN_PROGRESS,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18408")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://webservice.common.asl.locaid.net/")]
    public partial class Error : object, System.ComponentModel.INotifyPropertyChanged {
        
        private int errorIdField;
        
        private bool errorIdFieldSpecified;
        
        private string errorDescField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public int errorId {
            get {
                return this.errorIdField;
            }
            set {
                this.errorIdField = value;
                this.RaisePropertyChanged("errorId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool errorIdSpecified {
            get {
                return this.errorIdFieldSpecified;
            }
            set {
                this.errorIdFieldSpecified = value;
                this.RaisePropertyChanged("errorIdSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string errorDesc {
            get {
                return this.errorDescField;
            }
            set {
                this.errorDescField = value;
                this.RaisePropertyChanged("errorDesc");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18408")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://webservice.common.asl.locaid.net/")]
    public partial class PhoneStatus : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string msisdnField;
        
        private Response responseField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string msisdn {
            get {
                return this.msisdnField;
            }
            set {
                this.msisdnField = value;
                this.RaisePropertyChanged("msisdn");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public Response response {
            get {
                return this.responseField;
            }
            set {
                this.responseField = value;
                this.RaisePropertyChanged("response");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18408")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://webservice.common.asl.locaid.net/")]
    public partial class TransactionResponse : object, System.ComponentModel.INotifyPropertyChanged {
        
        private long requestIdField;
        
        private bool requestIdFieldSpecified;
        
        private Response responseField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public long requestId {
            get {
                return this.requestIdField;
            }
            set {
                this.requestIdField = value;
                this.RaisePropertyChanged("requestId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool requestIdSpecified {
            get {
                return this.requestIdFieldSpecified;
            }
            set {
                this.requestIdFieldSpecified = value;
                this.RaisePropertyChanged("requestIdSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public Response response {
            get {
                return this.responseField;
            }
            set {
                this.responseField = value;
                this.RaisePropertyChanged("response");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18408")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://webservice.common.asl.locaid.net/")]
    public partial class RegistrationAppPhones : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string classIdField;
        
        private string commandField;
        
        private string[] msisdnField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string classId {
            get {
                return this.classIdField;
            }
            set {
                this.classIdField = value;
                this.RaisePropertyChanged("classId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string command {
            get {
                return this.commandField;
            }
            set {
                this.commandField = value;
                this.RaisePropertyChanged("command");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("msisdn", Order=2)]
        public string[] msisdn {
            get {
                return this.msisdnField;
            }
            set {
                this.msisdnField = value;
                this.RaisePropertyChanged("msisdn");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18408")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://webservice.asl.locaid.net/RegistrationService")]
    public partial class RegisterPhoneResponse : object, System.ComponentModel.INotifyPropertyChanged {
        
        private TransactionResponse transactionResponseField;
        
        private string classIdField;
        
        private PhoneStatus[] phoneStatusField;
        
        private string trackingIDField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public TransactionResponse transactionResponse {
            get {
                return this.transactionResponseField;
            }
            set {
                this.transactionResponseField = value;
                this.RaisePropertyChanged("transactionResponse");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string classId {
            get {
                return this.classIdField;
            }
            set {
                this.classIdField = value;
                this.RaisePropertyChanged("classId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("phoneStatus", Order=2)]
        public PhoneStatus[] phoneStatus {
            get {
                return this.phoneStatusField;
            }
            set {
                this.phoneStatusField = value;
                this.RaisePropertyChanged("phoneStatus");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string trackingID {
            get {
                return this.trackingIDField;
            }
            set {
                this.trackingIDField = value;
                this.RaisePropertyChanged("trackingID");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class registerPhoneRequest1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://webservice.asl.locaid.net/RegistrationService", Order=0)]
        public ML.Utils.Locaid.RegistrationServiceReference.RegisterPhoneRequest RegisterPhoneRequest;
        
        public registerPhoneRequest1() {
        }
        
        public registerPhoneRequest1(ML.Utils.Locaid.RegistrationServiceReference.RegisterPhoneRequest RegisterPhoneRequest) {
            this.RegisterPhoneRequest = RegisterPhoneRequest;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class registerPhoneResponse1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://webservice.asl.locaid.net/RegistrationService", Order=0)]
        public ML.Utils.Locaid.RegistrationServiceReference.RegisterPhoneResponse RegisterPhoneResponse;
        
        public registerPhoneResponse1() {
        }
        
        public registerPhoneResponse1(ML.Utils.Locaid.RegistrationServiceReference.RegisterPhoneResponse RegisterPhoneResponse) {
            this.RegisterPhoneResponse = RegisterPhoneResponse;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18408")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://webservice.asl.locaid.net/RegistrationService")]
    public partial class GetPhoneStatusRequest : object, System.ComponentModel.INotifyPropertyChanged {
        
        private Credentials credentialsField;
        
        private string[] msisdnField;
        
        private string trackingIDField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public Credentials credentials {
            get {
                return this.credentialsField;
            }
            set {
                this.credentialsField = value;
                this.RaisePropertyChanged("credentials");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("msisdn", Order=1)]
        public string[] msisdn {
            get {
                return this.msisdnField;
            }
            set {
                this.msisdnField = value;
                this.RaisePropertyChanged("msisdn");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string trackingID {
            get {
                return this.trackingIDField;
            }
            set {
                this.trackingIDField = value;
                this.RaisePropertyChanged("trackingID");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18408")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://webservice.asl.locaid.net/RegistrationService")]
    public partial class GetPhoneStatusResponse : object, System.ComponentModel.INotifyPropertyChanged {
        
        private TransactionResponse transactionResponseField;
        
        private MsisdnPhoneStatus[] msisdnPhoneStatusField;
        
        private string trackingIDField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public TransactionResponse transactionResponse {
            get {
                return this.transactionResponseField;
            }
            set {
                this.transactionResponseField = value;
                this.RaisePropertyChanged("transactionResponse");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("msisdnPhoneStatus", Order=1)]
        public MsisdnPhoneStatus[] msisdnPhoneStatus {
            get {
                return this.msisdnPhoneStatusField;
            }
            set {
                this.msisdnPhoneStatusField = value;
                this.RaisePropertyChanged("msisdnPhoneStatus");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string trackingID {
            get {
                return this.trackingIDField;
            }
            set {
                this.trackingIDField = value;
                this.RaisePropertyChanged("trackingID");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class getPhoneStatusRequest1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://webservice.asl.locaid.net/RegistrationService", Order=0)]
        public ML.Utils.Locaid.RegistrationServiceReference.GetPhoneStatusRequest GetPhoneStatusRequest;
        
        public getPhoneStatusRequest1() {
        }
        
        public getPhoneStatusRequest1(ML.Utils.Locaid.RegistrationServiceReference.GetPhoneStatusRequest GetPhoneStatusRequest) {
            this.GetPhoneStatusRequest = GetPhoneStatusRequest;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class getPhoneStatusResponse1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://webservice.asl.locaid.net/RegistrationService", Order=0)]
        public ML.Utils.Locaid.RegistrationServiceReference.GetPhoneStatusResponse GetPhoneStatusResponse;
        
        public getPhoneStatusResponse1() {
        }
        
        public getPhoneStatusResponse1(ML.Utils.Locaid.RegistrationServiceReference.GetPhoneStatusResponse GetPhoneStatusResponse) {
            this.GetPhoneStatusResponse = GetPhoneStatusResponse;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface RegistrationServicePortTypeChannel : ML.Utils.Locaid.RegistrationServiceReference.RegistrationServicePortType, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class RegistrationServicePortTypeClient : System.ServiceModel.ClientBase<ML.Utils.Locaid.RegistrationServiceReference.RegistrationServicePortType>, ML.Utils.Locaid.RegistrationServiceReference.RegistrationServicePortType {
        
        public RegistrationServicePortTypeClient() {
        }
        
        public RegistrationServicePortTypeClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public RegistrationServicePortTypeClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RegistrationServicePortTypeClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RegistrationServicePortTypeClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ML.Utils.Locaid.RegistrationServiceReference.registerPhoneResponse1 ML.Utils.Locaid.RegistrationServiceReference.RegistrationServicePortType.registerPhone(ML.Utils.Locaid.RegistrationServiceReference.registerPhoneRequest1 request) {
            return base.Channel.registerPhone(request);
        }
        
        public ML.Utils.Locaid.RegistrationServiceReference.RegisterPhoneResponse registerPhone(ML.Utils.Locaid.RegistrationServiceReference.RegisterPhoneRequest RegisterPhoneRequest) {
            ML.Utils.Locaid.RegistrationServiceReference.registerPhoneRequest1 inValue = new ML.Utils.Locaid.RegistrationServiceReference.registerPhoneRequest1();
            inValue.RegisterPhoneRequest = RegisterPhoneRequest;
            ML.Utils.Locaid.RegistrationServiceReference.registerPhoneResponse1 retVal = ((ML.Utils.Locaid.RegistrationServiceReference.RegistrationServicePortType)(this)).registerPhone(inValue);
            return retVal.RegisterPhoneResponse;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ML.Utils.Locaid.RegistrationServiceReference.registerPhoneResponse1> ML.Utils.Locaid.RegistrationServiceReference.RegistrationServicePortType.registerPhoneAsync(ML.Utils.Locaid.RegistrationServiceReference.registerPhoneRequest1 request) {
            return base.Channel.registerPhoneAsync(request);
        }
        
        public System.Threading.Tasks.Task<ML.Utils.Locaid.RegistrationServiceReference.registerPhoneResponse1> registerPhoneAsync(ML.Utils.Locaid.RegistrationServiceReference.RegisterPhoneRequest RegisterPhoneRequest) {
            ML.Utils.Locaid.RegistrationServiceReference.registerPhoneRequest1 inValue = new ML.Utils.Locaid.RegistrationServiceReference.registerPhoneRequest1();
            inValue.RegisterPhoneRequest = RegisterPhoneRequest;
            return ((ML.Utils.Locaid.RegistrationServiceReference.RegistrationServicePortType)(this)).registerPhoneAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ML.Utils.Locaid.RegistrationServiceReference.getPhoneStatusResponse1 ML.Utils.Locaid.RegistrationServiceReference.RegistrationServicePortType.getPhoneStatus(ML.Utils.Locaid.RegistrationServiceReference.getPhoneStatusRequest1 request) {
            return base.Channel.getPhoneStatus(request);
        }
        
        public ML.Utils.Locaid.RegistrationServiceReference.GetPhoneStatusResponse getPhoneStatus(ML.Utils.Locaid.RegistrationServiceReference.GetPhoneStatusRequest GetPhoneStatusRequest) {
            ML.Utils.Locaid.RegistrationServiceReference.getPhoneStatusRequest1 inValue = new ML.Utils.Locaid.RegistrationServiceReference.getPhoneStatusRequest1();
            inValue.GetPhoneStatusRequest = GetPhoneStatusRequest;
            ML.Utils.Locaid.RegistrationServiceReference.getPhoneStatusResponse1 retVal = ((ML.Utils.Locaid.RegistrationServiceReference.RegistrationServicePortType)(this)).getPhoneStatus(inValue);
            return retVal.GetPhoneStatusResponse;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ML.Utils.Locaid.RegistrationServiceReference.getPhoneStatusResponse1> ML.Utils.Locaid.RegistrationServiceReference.RegistrationServicePortType.getPhoneStatusAsync(ML.Utils.Locaid.RegistrationServiceReference.getPhoneStatusRequest1 request) {
            return base.Channel.getPhoneStatusAsync(request);
        }
        
        public System.Threading.Tasks.Task<ML.Utils.Locaid.RegistrationServiceReference.getPhoneStatusResponse1> getPhoneStatusAsync(ML.Utils.Locaid.RegistrationServiceReference.GetPhoneStatusRequest GetPhoneStatusRequest) {
            ML.Utils.Locaid.RegistrationServiceReference.getPhoneStatusRequest1 inValue = new ML.Utils.Locaid.RegistrationServiceReference.getPhoneStatusRequest1();
            inValue.GetPhoneStatusRequest = GetPhoneStatusRequest;
            return ((ML.Utils.Locaid.RegistrationServiceReference.RegistrationServicePortType)(this)).getPhoneStatusAsync(inValue);
        }
    }
}
