using System.ServiceModel;

namespace FileTransferService
{
    [ServiceContract]
    public interface IFileTransferService
    {
        [OperationContract]
        void Upload(FileDataUpload file);

        [OperationContract]
        FileDataDownload Download(FileInfoEx fileInfoEx);

        [OperationContract]
        FileInfoEx[] GetListFiles(FileListType type, string value);

        [OperationContract]
        void Remove(FileInfoEx fileInfoEx);
    }
}
