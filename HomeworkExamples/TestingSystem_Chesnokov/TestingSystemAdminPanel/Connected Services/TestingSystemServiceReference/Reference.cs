﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestingSystemAdminPanel.TestingSystemServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="User", Namespace="http://schemas.datacontract.org/2004/07/TestingSystemService")]
    [System.SerializableAttribute()]
    public partial class User : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string GroupField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LoginField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PasswordField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserNameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Group {
            get {
                return this.GroupField;
            }
            set {
                if ((object.ReferenceEquals(this.GroupField, value) != true)) {
                    this.GroupField = value;
                    this.RaisePropertyChanged("Group");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Id {
            get {
                return this.IdField;
            }
            set {
                if ((object.ReferenceEquals(this.IdField, value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Login {
            get {
                return this.LoginField;
            }
            set {
                if ((object.ReferenceEquals(this.LoginField, value) != true)) {
                    this.LoginField = value;
                    this.RaisePropertyChanged("Login");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Password {
            get {
                return this.PasswordField;
            }
            set {
                if ((object.ReferenceEquals(this.PasswordField, value) != true)) {
                    this.PasswordField = value;
                    this.RaisePropertyChanged("Password");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
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
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Test", Namespace="http://schemas.datacontract.org/2004/07/TestingSystemService")]
    [System.SerializableAttribute()]
    public partial class Test : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int NumberMinutesToPassTestField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int NumberOfQuestionsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private TestingSystemAdminPanel.TestingSystemServiceReference.Question[] QuestionsField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Id {
            get {
                return this.IdField;
            }
            set {
                if ((object.ReferenceEquals(this.IdField, value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int NumberMinutesToPassTest {
            get {
                return this.NumberMinutesToPassTestField;
            }
            set {
                if ((this.NumberMinutesToPassTestField.Equals(value) != true)) {
                    this.NumberMinutesToPassTestField = value;
                    this.RaisePropertyChanged("NumberMinutesToPassTest");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int NumberOfQuestions {
            get {
                return this.NumberOfQuestionsField;
            }
            set {
                if ((this.NumberOfQuestionsField.Equals(value) != true)) {
                    this.NumberOfQuestionsField = value;
                    this.RaisePropertyChanged("NumberOfQuestions");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public TestingSystemAdminPanel.TestingSystemServiceReference.Question[] Questions {
            get {
                return this.QuestionsField;
            }
            set {
                if ((object.ReferenceEquals(this.QuestionsField, value) != true)) {
                    this.QuestionsField = value;
                    this.RaisePropertyChanged("Questions");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Question", Namespace="http://schemas.datacontract.org/2004/07/TestingSystemService")]
    [System.SerializableAttribute()]
    public partial class Question : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TextField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Id {
            get {
                return this.IdField;
            }
            set {
                if ((object.ReferenceEquals(this.IdField, value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Text {
            get {
                return this.TextField;
            }
            set {
                if ((object.ReferenceEquals(this.TextField, value) != true)) {
                    this.TextField = value;
                    this.RaisePropertyChanged("Text");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TestResult", Namespace="http://schemas.datacontract.org/2004/07/TestingSystemService")]
    [System.SerializableAttribute()]
    public partial class TestResult : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private TestingSystemAdminPanel.TestingSystemServiceReference.QuestionWithAnswer[] QuestionsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int RatingField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private TestingSystemAdminPanel.TestingSystemServiceReference.Test TestField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private TestingSystemAdminPanel.TestingSystemServiceReference.User UserField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Id {
            get {
                return this.IdField;
            }
            set {
                if ((object.ReferenceEquals(this.IdField, value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public TestingSystemAdminPanel.TestingSystemServiceReference.QuestionWithAnswer[] Questions {
            get {
                return this.QuestionsField;
            }
            set {
                if ((object.ReferenceEquals(this.QuestionsField, value) != true)) {
                    this.QuestionsField = value;
                    this.RaisePropertyChanged("Questions");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Rating {
            get {
                return this.RatingField;
            }
            set {
                if ((this.RatingField.Equals(value) != true)) {
                    this.RatingField = value;
                    this.RaisePropertyChanged("Rating");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public TestingSystemAdminPanel.TestingSystemServiceReference.Test Test {
            get {
                return this.TestField;
            }
            set {
                if ((object.ReferenceEquals(this.TestField, value) != true)) {
                    this.TestField = value;
                    this.RaisePropertyChanged("Test");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public TestingSystemAdminPanel.TestingSystemServiceReference.User User {
            get {
                return this.UserField;
            }
            set {
                if ((object.ReferenceEquals(this.UserField, value) != true)) {
                    this.UserField = value;
                    this.RaisePropertyChanged("User");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="QuestionWithAnswer", Namespace="http://schemas.datacontract.org/2004/07/TestingSystemService")]
    [System.SerializableAttribute()]
    public partial class QuestionWithAnswer : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AnswerField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IdentifierdInTestField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TextField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Answer {
            get {
                return this.AnswerField;
            }
            set {
                if ((object.ReferenceEquals(this.AnswerField, value) != true)) {
                    this.AnswerField = value;
                    this.RaisePropertyChanged("Answer");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string IdentifierdInTest {
            get {
                return this.IdentifierdInTestField;
            }
            set {
                if ((object.ReferenceEquals(this.IdentifierdInTestField, value) != true)) {
                    this.IdentifierdInTestField = value;
                    this.RaisePropertyChanged("IdentifierdInTest");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Text {
            get {
                return this.TextField;
            }
            set {
                if ((object.ReferenceEquals(this.TextField, value) != true)) {
                    this.TextField = value;
                    this.RaisePropertyChanged("Text");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="TestingSystemServiceReference.ITestingSystemService", CallbackContract=typeof(TestingSystemAdminPanel.TestingSystemServiceReference.ITestingSystemServiceCallback))]
    public interface ITestingSystemService {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ITestingSystemService/NewClientConnected")]
        void NewClientConnected();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ITestingSystemService/NewClientConnected")]
        System.Threading.Tasks.Task NewClientConnectedAsync();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ITestingSystemService/ClientDisconnected")]
        void ClientDisconnected();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ITestingSystemService/ClientDisconnected")]
        System.Threading.Tasks.Task ClientDisconnectedAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITestingSystemService/LoginIsFree", ReplyAction="http://tempuri.org/ITestingSystemService/LoginIsFreeResponse")]
        bool LoginIsFree(string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITestingSystemService/LoginIsFree", ReplyAction="http://tempuri.org/ITestingSystemService/LoginIsFreeResponse")]
        System.Threading.Tasks.Task<bool> LoginIsFreeAsync(string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITestingSystemService/RegisterNewUser", ReplyAction="http://tempuri.org/ITestingSystemService/RegisterNewUserResponse")]
        bool RegisterNewUser(TestingSystemAdminPanel.TestingSystemServiceReference.User user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITestingSystemService/RegisterNewUser", ReplyAction="http://tempuri.org/ITestingSystemService/RegisterNewUserResponse")]
        System.Threading.Tasks.Task<bool> RegisterNewUserAsync(TestingSystemAdminPanel.TestingSystemServiceReference.User user);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ITestingSystemService/SignIn")]
        void SignIn(string login, string password);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ITestingSystemService/SignIn")]
        System.Threading.Tasks.Task SignInAsync(string login, string password);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ITestingSystemService/CreateNewTest")]
        void CreateNewTest(TestingSystemAdminPanel.TestingSystemServiceReference.Test test);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ITestingSystemService/CreateNewTest")]
        System.Threading.Tasks.Task CreateNewTestAsync(TestingSystemAdminPanel.TestingSystemServiceReference.Test test);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ITestingSystemService/GetListOfTests")]
        void GetListOfTests();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ITestingSystemService/GetListOfTests")]
        System.Threading.Tasks.Task GetListOfTestsAsync();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ITestingSystemService/StartTesting")]
        void StartTesting(TestingSystemAdminPanel.TestingSystemServiceReference.User user, string testId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ITestingSystemService/StartTesting")]
        System.Threading.Tasks.Task StartTestingAsync(TestingSystemAdminPanel.TestingSystemServiceReference.User user, string testId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ITestingSystemService/EndTesting")]
        void EndTesting(TestingSystemAdminPanel.TestingSystemServiceReference.TestResult result, string groupId, string subgroupId, string testId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ITestingSystemService/EndTesting")]
        System.Threading.Tasks.Task EndTestingAsync(TestingSystemAdminPanel.TestingSystemServiceReference.TestResult result, string groupId, string subgroupId, string testId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ITestingSystemService/GetResultTest")]
        void GetResultTest(string testId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ITestingSystemService/GetResultTest")]
        System.Threading.Tasks.Task GetResultTestAsync(string testId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITestingSystemService/ChangeRating", ReplyAction="http://tempuri.org/ITestingSystemService/ChangeRatingResponse")]
        bool ChangeRating(string resultId, int newRating);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITestingSystemService/ChangeRating", ReplyAction="http://tempuri.org/ITestingSystemService/ChangeRatingResponse")]
        System.Threading.Tasks.Task<bool> ChangeRatingAsync(string resultId, int newRating);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ITestingSystemService/GetResultsTest")]
        void GetResultsTest(string testId, string userId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ITestingSystemService/GetResultsTest")]
        System.Threading.Tasks.Task GetResultsTestAsync(string testId, string userId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITestingSystemServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ITestingSystemService/SetTestList")]
        void SetTestList(System.Collections.Generic.Dictionary<string, string> testsList);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ITestingSystemService/SetQuestions")]
        void SetQuestions(TestingSystemAdminPanel.TestingSystemServiceReference.Question[] questions, string groupId, string subgroupId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ITestingSystemService/ForciblyReturnTestResult")]
        void ForciblyReturnTestResult();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ITestingSystemService/ResultOfUserSignIn")]
        void ResultOfUserSignIn(TestingSystemAdminPanel.TestingSystemServiceReference.User user, string message);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ITestingSystemService/ResultEndTesting")]
        void ResultEndTesting(bool result, string message);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ITestingSystemService/SetTestResults")]
        void SetTestResults(TestingSystemAdminPanel.TestingSystemServiceReference.TestResult[] results);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ITestingSystemService/SetRemainingTime")]
        void SetRemainingTime(long second);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITestingSystemServiceChannel : TestingSystemAdminPanel.TestingSystemServiceReference.ITestingSystemService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TestingSystemServiceClient : System.ServiceModel.DuplexClientBase<TestingSystemAdminPanel.TestingSystemServiceReference.ITestingSystemService>, TestingSystemAdminPanel.TestingSystemServiceReference.ITestingSystemService {
        
        public TestingSystemServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public TestingSystemServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public TestingSystemServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public TestingSystemServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public TestingSystemServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void NewClientConnected() {
            base.Channel.NewClientConnected();
        }
        
        public System.Threading.Tasks.Task NewClientConnectedAsync() {
            return base.Channel.NewClientConnectedAsync();
        }
        
        public void ClientDisconnected() {
            base.Channel.ClientDisconnected();
        }
        
        public System.Threading.Tasks.Task ClientDisconnectedAsync() {
            return base.Channel.ClientDisconnectedAsync();
        }
        
        public bool LoginIsFree(string login) {
            return base.Channel.LoginIsFree(login);
        }
        
        public System.Threading.Tasks.Task<bool> LoginIsFreeAsync(string login) {
            return base.Channel.LoginIsFreeAsync(login);
        }
        
        public bool RegisterNewUser(TestingSystemAdminPanel.TestingSystemServiceReference.User user) {
            return base.Channel.RegisterNewUser(user);
        }
        
        public System.Threading.Tasks.Task<bool> RegisterNewUserAsync(TestingSystemAdminPanel.TestingSystemServiceReference.User user) {
            return base.Channel.RegisterNewUserAsync(user);
        }
        
        public void SignIn(string login, string password) {
            base.Channel.SignIn(login, password);
        }
        
        public System.Threading.Tasks.Task SignInAsync(string login, string password) {
            return base.Channel.SignInAsync(login, password);
        }
        
        public void CreateNewTest(TestingSystemAdminPanel.TestingSystemServiceReference.Test test) {
            base.Channel.CreateNewTest(test);
        }
        
        public System.Threading.Tasks.Task CreateNewTestAsync(TestingSystemAdminPanel.TestingSystemServiceReference.Test test) {
            return base.Channel.CreateNewTestAsync(test);
        }
        
        public void GetListOfTests() {
            base.Channel.GetListOfTests();
        }
        
        public System.Threading.Tasks.Task GetListOfTestsAsync() {
            return base.Channel.GetListOfTestsAsync();
        }
        
        public void StartTesting(TestingSystemAdminPanel.TestingSystemServiceReference.User user, string testId) {
            base.Channel.StartTesting(user, testId);
        }
        
        public System.Threading.Tasks.Task StartTestingAsync(TestingSystemAdminPanel.TestingSystemServiceReference.User user, string testId) {
            return base.Channel.StartTestingAsync(user, testId);
        }
        
        public void EndTesting(TestingSystemAdminPanel.TestingSystemServiceReference.TestResult result, string groupId, string subgroupId, string testId) {
            base.Channel.EndTesting(result, groupId, subgroupId, testId);
        }
        
        public System.Threading.Tasks.Task EndTestingAsync(TestingSystemAdminPanel.TestingSystemServiceReference.TestResult result, string groupId, string subgroupId, string testId) {
            return base.Channel.EndTestingAsync(result, groupId, subgroupId, testId);
        }
        
        public void GetResultTest(string testId) {
            base.Channel.GetResultTest(testId);
        }
        
        public System.Threading.Tasks.Task GetResultTestAsync(string testId) {
            return base.Channel.GetResultTestAsync(testId);
        }
        
        public bool ChangeRating(string resultId, int newRating) {
            return base.Channel.ChangeRating(resultId, newRating);
        }
        
        public System.Threading.Tasks.Task<bool> ChangeRatingAsync(string resultId, int newRating) {
            return base.Channel.ChangeRatingAsync(resultId, newRating);
        }
        
        public void GetResultsTest(string testId, string userId) {
            base.Channel.GetResultsTest(testId, userId);
        }
        
        public System.Threading.Tasks.Task GetResultsTestAsync(string testId, string userId) {
            return base.Channel.GetResultsTestAsync(testId, userId);
        }
    }
}