﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Client.ServiceReference1 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Commodity", Namespace="http://schemas.datacontract.org/2004/07/DataContractService")]
    [System.SerializableAttribute()]
    public partial class Commodity : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CategoryField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private decimal PriceField;
        
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
        public string Category {
            get {
                return this.CategoryField;
            }
            set {
                if ((object.ReferenceEquals(this.CategoryField, value) != true)) {
                    this.CategoryField = value;
                    this.RaisePropertyChanged("Category");
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
        public decimal Price {
            get {
                return this.PriceField;
            }
            set {
                if ((this.PriceField.Equals(value) != true)) {
                    this.PriceField = value;
                    this.RaisePropertyChanged("Price");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IServiceShop")]
    public interface IServiceShop {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceShop/AddCommoity", ReplyAction="http://tempuri.org/IServiceShop/AddCommoityResponse")]
        void AddCommoity(Client.ServiceReference1.Commodity com);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceShop/AddCommoity", ReplyAction="http://tempuri.org/IServiceShop/AddCommoityResponse")]
        System.Threading.Tasks.Task AddCommoityAsync(Client.ServiceReference1.Commodity com);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceShop/GetCommodity", ReplyAction="http://tempuri.org/IServiceShop/GetCommodityResponse")]
        Client.ServiceReference1.Commodity GetCommodity(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceShop/GetCommodity", ReplyAction="http://tempuri.org/IServiceShop/GetCommodityResponse")]
        System.Threading.Tasks.Task<Client.ServiceReference1.Commodity> GetCommodityAsync(int id);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceShopChannel : Client.ServiceReference1.IServiceShop, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceShopClient : System.ServiceModel.ClientBase<Client.ServiceReference1.IServiceShop>, Client.ServiceReference1.IServiceShop {
        
        public ServiceShopClient() {
        }
        
        public ServiceShopClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceShopClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceShopClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceShopClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void AddCommoity(Client.ServiceReference1.Commodity com) {
            base.Channel.AddCommoity(com);
        }
        
        public System.Threading.Tasks.Task AddCommoityAsync(Client.ServiceReference1.Commodity com) {
            return base.Channel.AddCommoityAsync(com);
        }
        
        public Client.ServiceReference1.Commodity GetCommodity(int id) {
            return base.Channel.GetCommodity(id);
        }
        
        public System.Threading.Tasks.Task<Client.ServiceReference1.Commodity> GetCommodityAsync(int id) {
            return base.Channel.GetCommodityAsync(id);
        }
    }
}