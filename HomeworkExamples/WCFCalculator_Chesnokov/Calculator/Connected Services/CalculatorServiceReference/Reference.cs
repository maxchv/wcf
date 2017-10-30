﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Calculator.CalculatorServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="CalculatorServiceReference.ICalculatorService", CallbackContract=typeof(Calculator.CalculatorServiceReference.ICalculatorServiceCallback))]
    public interface ICalculatorService {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ICalculatorService/Sum")]
        void Sum(decimal a, decimal b);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ICalculatorService/Sum")]
        System.Threading.Tasks.Task SumAsync(decimal a, decimal b);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ICalculatorService/Sub")]
        void Sub(decimal a, decimal b);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ICalculatorService/Sub")]
        System.Threading.Tasks.Task SubAsync(decimal a, decimal b);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ICalculatorService/Mult")]
        void Mult(decimal a, decimal b);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ICalculatorService/Mult")]
        System.Threading.Tasks.Task MultAsync(decimal a, decimal b);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ICalculatorService/Div")]
        void Div(decimal a, decimal b);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ICalculatorService/Div")]
        System.Threading.Tasks.Task DivAsync(decimal a, decimal b);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICalculatorServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ICalculatorService/Calculation")]
        void Calculation(decimal resultCalculation);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICalculatorServiceChannel : Calculator.CalculatorServiceReference.ICalculatorService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CalculatorServiceClient : System.ServiceModel.DuplexClientBase<Calculator.CalculatorServiceReference.ICalculatorService>, Calculator.CalculatorServiceReference.ICalculatorService {
        
        public CalculatorServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public CalculatorServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public CalculatorServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public CalculatorServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public CalculatorServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void Sum(decimal a, decimal b) {
            base.Channel.Sum(a, b);
        }
        
        public System.Threading.Tasks.Task SumAsync(decimal a, decimal b) {
            return base.Channel.SumAsync(a, b);
        }
        
        public void Sub(decimal a, decimal b) {
            base.Channel.Sub(a, b);
        }
        
        public System.Threading.Tasks.Task SubAsync(decimal a, decimal b) {
            return base.Channel.SubAsync(a, b);
        }
        
        public void Mult(decimal a, decimal b) {
            base.Channel.Mult(a, b);
        }
        
        public System.Threading.Tasks.Task MultAsync(decimal a, decimal b) {
            return base.Channel.MultAsync(a, b);
        }
        
        public void Div(decimal a, decimal b) {
            base.Channel.Div(a, b);
        }
        
        public System.Threading.Tasks.Task DivAsync(decimal a, decimal b) {
            return base.Channel.DivAsync(a, b);
        }
    }
}
