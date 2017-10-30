﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FileTransferClient.FileTransferServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FileDataUpload", Namespace="http://schemas.datacontract.org/2004/07/FileTransferService")]
    [System.SerializableAttribute()]
    public partial class FileDataUpload : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AuthorField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private byte[] DataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime DateUploadField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FileIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FileNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool IsLastPartField;
        
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
        public string Author {
            get {
                return this.AuthorField;
            }
            set {
                if ((object.ReferenceEquals(this.AuthorField, value) != true)) {
                    this.AuthorField = value;
                    this.RaisePropertyChanged("Author");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public byte[] Data {
            get {
                return this.DataField;
            }
            set {
                if ((object.ReferenceEquals(this.DataField, value) != true)) {
                    this.DataField = value;
                    this.RaisePropertyChanged("Data");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime DateUpload {
            get {
                return this.DateUploadField;
            }
            set {
                if ((this.DateUploadField.Equals(value) != true)) {
                    this.DateUploadField = value;
                    this.RaisePropertyChanged("DateUpload");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FileId {
            get {
                return this.FileIdField;
            }
            set {
                if ((object.ReferenceEquals(this.FileIdField, value) != true)) {
                    this.FileIdField = value;
                    this.RaisePropertyChanged("FileId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FileName {
            get {
                return this.FileNameField;
            }
            set {
                if ((object.ReferenceEquals(this.FileNameField, value) != true)) {
                    this.FileNameField = value;
                    this.RaisePropertyChanged("FileName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsLastPart {
            get {
                return this.IsLastPartField;
            }
            set {
                if ((this.IsLastPartField.Equals(value) != true)) {
                    this.IsLastPartField = value;
                    this.RaisePropertyChanged("IsLastPart");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="FileInfoEx", Namespace="http://schemas.datacontract.org/2004/07/FileTransferService")]
    [System.SerializableAttribute()]
    public partial class FileInfoEx : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AuthorField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime DateUploadField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FileNameOnServerField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string OriginalFileNameField;
        
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
        public string Author {
            get {
                return this.AuthorField;
            }
            set {
                if ((object.ReferenceEquals(this.AuthorField, value) != true)) {
                    this.AuthorField = value;
                    this.RaisePropertyChanged("Author");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime DateUpload {
            get {
                return this.DateUploadField;
            }
            set {
                if ((this.DateUploadField.Equals(value) != true)) {
                    this.DateUploadField = value;
                    this.RaisePropertyChanged("DateUpload");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FileNameOnServer {
            get {
                return this.FileNameOnServerField;
            }
            set {
                if ((object.ReferenceEquals(this.FileNameOnServerField, value) != true)) {
                    this.FileNameOnServerField = value;
                    this.RaisePropertyChanged("FileNameOnServer");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string OriginalFileName {
            get {
                return this.OriginalFileNameField;
            }
            set {
                if ((object.ReferenceEquals(this.OriginalFileNameField, value) != true)) {
                    this.OriginalFileNameField = value;
                    this.RaisePropertyChanged("OriginalFileName");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="FileDataDownload", Namespace="http://schemas.datacontract.org/2004/07/FileTransferService")]
    [System.SerializableAttribute()]
    public partial class FileDataDownload : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private byte[] DataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool IsLastPartField;
        
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
        public byte[] Data {
            get {
                return this.DataField;
            }
            set {
                if ((object.ReferenceEquals(this.DataField, value) != true)) {
                    this.DataField = value;
                    this.RaisePropertyChanged("Data");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsLastPart {
            get {
                return this.IsLastPartField;
            }
            set {
                if ((this.IsLastPartField.Equals(value) != true)) {
                    this.IsLastPartField = value;
                    this.RaisePropertyChanged("IsLastPart");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="FileListType", Namespace="http://schemas.datacontract.org/2004/07/FileTransferService")]
    public enum FileListType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        AllFiles = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        ByAuthor = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        ByFileName = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        ByDateUpload = 3,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="FileTransferServiceReference.IFileTransferService")]
    public interface IFileTransferService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFileTransferService/Upload", ReplyAction="http://tempuri.org/IFileTransferService/UploadResponse")]
        void Upload(FileTransferClient.FileTransferServiceReference.FileDataUpload file);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFileTransferService/Upload", ReplyAction="http://tempuri.org/IFileTransferService/UploadResponse")]
        System.Threading.Tasks.Task UploadAsync(FileTransferClient.FileTransferServiceReference.FileDataUpload file);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFileTransferService/Download", ReplyAction="http://tempuri.org/IFileTransferService/DownloadResponse")]
        FileTransferClient.FileTransferServiceReference.FileDataDownload Download(FileTransferClient.FileTransferServiceReference.FileInfoEx fileInfoEx);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFileTransferService/Download", ReplyAction="http://tempuri.org/IFileTransferService/DownloadResponse")]
        System.Threading.Tasks.Task<FileTransferClient.FileTransferServiceReference.FileDataDownload> DownloadAsync(FileTransferClient.FileTransferServiceReference.FileInfoEx fileInfoEx);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFileTransferService/GetListFiles", ReplyAction="http://tempuri.org/IFileTransferService/GetListFilesResponse")]
        FileTransferClient.FileTransferServiceReference.FileInfoEx[] GetListFiles(FileTransferClient.FileTransferServiceReference.FileListType type, string value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFileTransferService/GetListFiles", ReplyAction="http://tempuri.org/IFileTransferService/GetListFilesResponse")]
        System.Threading.Tasks.Task<FileTransferClient.FileTransferServiceReference.FileInfoEx[]> GetListFilesAsync(FileTransferClient.FileTransferServiceReference.FileListType type, string value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFileTransferService/Remove", ReplyAction="http://tempuri.org/IFileTransferService/RemoveResponse")]
        void Remove(FileTransferClient.FileTransferServiceReference.FileInfoEx fileInfoEx);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFileTransferService/Remove", ReplyAction="http://tempuri.org/IFileTransferService/RemoveResponse")]
        System.Threading.Tasks.Task RemoveAsync(FileTransferClient.FileTransferServiceReference.FileInfoEx fileInfoEx);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IFileTransferServiceChannel : FileTransferClient.FileTransferServiceReference.IFileTransferService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class FileTransferServiceClient : System.ServiceModel.ClientBase<FileTransferClient.FileTransferServiceReference.IFileTransferService>, FileTransferClient.FileTransferServiceReference.IFileTransferService {
        
        public FileTransferServiceClient() {
        }
        
        public FileTransferServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public FileTransferServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FileTransferServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FileTransferServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void Upload(FileTransferClient.FileTransferServiceReference.FileDataUpload file) {
            base.Channel.Upload(file);
        }
        
        public System.Threading.Tasks.Task UploadAsync(FileTransferClient.FileTransferServiceReference.FileDataUpload file) {
            return base.Channel.UploadAsync(file);
        }
        
        public FileTransferClient.FileTransferServiceReference.FileDataDownload Download(FileTransferClient.FileTransferServiceReference.FileInfoEx fileInfoEx) {
            return base.Channel.Download(fileInfoEx);
        }
        
        public System.Threading.Tasks.Task<FileTransferClient.FileTransferServiceReference.FileDataDownload> DownloadAsync(FileTransferClient.FileTransferServiceReference.FileInfoEx fileInfoEx) {
            return base.Channel.DownloadAsync(fileInfoEx);
        }
        
        public FileTransferClient.FileTransferServiceReference.FileInfoEx[] GetListFiles(FileTransferClient.FileTransferServiceReference.FileListType type, string value) {
            return base.Channel.GetListFiles(type, value);
        }
        
        public System.Threading.Tasks.Task<FileTransferClient.FileTransferServiceReference.FileInfoEx[]> GetListFilesAsync(FileTransferClient.FileTransferServiceReference.FileListType type, string value) {
            return base.Channel.GetListFilesAsync(type, value);
        }
        
        public void Remove(FileTransferClient.FileTransferServiceReference.FileInfoEx fileInfoEx) {
            base.Channel.Remove(fileInfoEx);
        }
        
        public System.Threading.Tasks.Task RemoveAsync(FileTransferClient.FileTransferServiceReference.FileInfoEx fileInfoEx) {
            return base.Channel.RemoveAsync(fileInfoEx);
        }
    }
}