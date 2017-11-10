using System.ServiceModel;

namespace RemoteEditingFilesService
{
    [ServiceContract(CallbackContract = typeof(IServiceCallback))]
    public interface IRemoteEditingFilesService
    {
        [OperationContract(IsOneWay = true)]
        void NewClientConnects();

        [OperationContract(IsOneWay = true)]
        void ClientDisconnected(string id);

        [OperationContract(IsOneWay = true)]
        void GetFilesList();

        [OperationContract(IsOneWay = true)]
        void CreateNewFile(string fileName, string id, string initData);

        [OperationContract(IsOneWay = true)]
        void RemoveFile(string fileName, string id);

        [OperationContract(IsOneWay = true)]
        void StartEditFile(string fileName, string id);

        [OperationContract(IsOneWay = true)]
        void EndEditFile(string fileName, string id);

        [OperationContract(IsOneWay = true)]
        void UpdateFileData(string fileName, string newData, string id);
    }

    [ServiceContract]
    public interface IServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void SendFilesList(string[] filesName);

        [OperationContract(IsOneWay = true)]
        void SendResultCreateNewFile(string message);

        [OperationContract(IsOneWay = true)]
        void SendResultRemoveFile(string message);

        [OperationContract(IsOneWay = true)]
        void SendDataFile(string text);

        [OperationContract(IsOneWay = true)]
        void SetUserId(string id);
    }
}