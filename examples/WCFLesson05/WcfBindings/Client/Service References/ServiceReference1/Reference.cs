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
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IPublicService")]
    public interface IPublicService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPublicService/GetPublicData", ReplyAction="http://tempuri.org/IPublicService/GetPublicDataResponse")]
        string GetPublicData(string msg);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPublicService/GetPublicData", ReplyAction="http://tempuri.org/IPublicService/GetPublicDataResponse")]
        System.Threading.Tasks.Task<string> GetPublicDataAsync(string msg);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IPublicServiceChannel : Client.ServiceReference1.IPublicService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class PublicServiceClient : System.ServiceModel.ClientBase<Client.ServiceReference1.IPublicService>, Client.ServiceReference1.IPublicService {
        
        public PublicServiceClient() {
        }
        
        public PublicServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public PublicServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PublicServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PublicServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetPublicData(string msg) {
            return base.Channel.GetPublicData(msg);
        }
        
        public System.Threading.Tasks.Task<string> GetPublicDataAsync(string msg) {
            return base.Channel.GetPublicDataAsync(msg);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IPrivateService")]
    public interface IPrivateService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPrivateService/GetPrivateData", ReplyAction="http://tempuri.org/IPrivateService/GetPrivateDataResponse")]
        string GetPrivateData(string msg);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPrivateService/GetPrivateData", ReplyAction="http://tempuri.org/IPrivateService/GetPrivateDataResponse")]
        System.Threading.Tasks.Task<string> GetPrivateDataAsync(string msg);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IPrivateServiceChannel : Client.ServiceReference1.IPrivateService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class PrivateServiceClient : System.ServiceModel.ClientBase<Client.ServiceReference1.IPrivateService>, Client.ServiceReference1.IPrivateService {
        
        public PrivateServiceClient() {
        }
        
        public PrivateServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public PrivateServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PrivateServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PrivateServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetPrivateData(string msg) {
            return base.Channel.GetPrivateData(msg);
        }
        
        public System.Threading.Tasks.Task<string> GetPrivateDataAsync(string msg) {
            return base.Channel.GetPrivateDataAsync(msg);
        }
    }
}