﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17626
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Wrox.ProCSharp.WCF.RoomReservationService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="RoomReservation", Namespace="http://schemas.datacontract.org/2004/07/Wrox.ProCSharp.WCF.Contracts")]
    [System.SerializableAttribute()]
    public partial class RoomReservation : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ContactField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime EndTimeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string RoomNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime StartTimeField;
        
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
        public string Contact {
            get {
                return this.ContactField;
            }
            set {
                if ((object.ReferenceEquals(this.ContactField, value) != true)) {
                    this.ContactField = value;
                    this.RaisePropertyChanged("Contact");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime EndTime {
            get {
                return this.EndTimeField;
            }
            set {
                if ((this.EndTimeField.Equals(value) != true)) {
                    this.EndTimeField = value;
                    this.RaisePropertyChanged("EndTime");
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
        public string RoomName {
            get {
                return this.RoomNameField;
            }
            set {
                if ((object.ReferenceEquals(this.RoomNameField, value) != true)) {
                    this.RoomNameField = value;
                    this.RaisePropertyChanged("RoomName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime StartTime {
            get {
                return this.StartTimeField;
            }
            set {
                if ((this.StartTimeField.Equals(value) != true)) {
                    this.StartTimeField = value;
                    this.RaisePropertyChanged("StartTime");
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
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.cninnovation.com/RoomReservation/2012", ConfigurationName="RoomReservationService.IRoomService")]
    public interface IRoomService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.cninnovation.com/RoomReservation/2012/IRoomService/ReserveRoom", ReplyAction="http://www.cninnovation.com/RoomReservation/2012/IRoomService/ReserveRoomResponse" +
            "")]
        bool ReserveRoom(Wrox.ProCSharp.WCF.RoomReservationService.RoomReservation roomReservation);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.cninnovation.com/RoomReservation/2012/IRoomService/ReserveRoom", ReplyAction="http://www.cninnovation.com/RoomReservation/2012/IRoomService/ReserveRoomResponse" +
            "")]
        System.Threading.Tasks.Task<bool> ReserveRoomAsync(Wrox.ProCSharp.WCF.RoomReservationService.RoomReservation roomReservation);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.cninnovation.com/RoomReservation/2012/IRoomService/GetRoomReservations" +
            "", ReplyAction="http://www.cninnovation.com/RoomReservation/2012/IRoomService/GetRoomReservations" +
            "Response")]
        Wrox.ProCSharp.WCF.RoomReservationService.RoomReservation[] GetRoomReservations(System.DateTime fromDate, System.DateTime toDate);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.cninnovation.com/RoomReservation/2012/IRoomService/GetRoomReservations" +
            "", ReplyAction="http://www.cninnovation.com/RoomReservation/2012/IRoomService/GetRoomReservations" +
            "Response")]
        System.Threading.Tasks.Task<Wrox.ProCSharp.WCF.RoomReservationService.RoomReservation[]> GetRoomReservationsAsync(System.DateTime fromDate, System.DateTime toDate);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IRoomServiceChannel : Wrox.ProCSharp.WCF.RoomReservationService.IRoomService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class RoomServiceClient : System.ServiceModel.ClientBase<Wrox.ProCSharp.WCF.RoomReservationService.IRoomService>, Wrox.ProCSharp.WCF.RoomReservationService.IRoomService {
        
        public RoomServiceClient() {
        }
        
        public RoomServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public RoomServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RoomServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RoomServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool ReserveRoom(Wrox.ProCSharp.WCF.RoomReservationService.RoomReservation roomReservation) {
            return base.Channel.ReserveRoom(roomReservation);
        }
        
        public System.Threading.Tasks.Task<bool> ReserveRoomAsync(Wrox.ProCSharp.WCF.RoomReservationService.RoomReservation roomReservation) {
            return base.Channel.ReserveRoomAsync(roomReservation);
        }
        
        public Wrox.ProCSharp.WCF.RoomReservationService.RoomReservation[] GetRoomReservations(System.DateTime fromDate, System.DateTime toDate) {
            return base.Channel.GetRoomReservations(fromDate, toDate);
        }
        
        public System.Threading.Tasks.Task<Wrox.ProCSharp.WCF.RoomReservationService.RoomReservation[]> GetRoomReservationsAsync(System.DateTime fromDate, System.DateTime toDate) {
            return base.Channel.GetRoomReservationsAsync(fromDate, toDate);
        }
    }
}
