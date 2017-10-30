using System.Runtime.Serialization;

namespace FileTransferService
{
    [DataContract]
    public class FileDataDownload
    {
        [DataMember]
        public bool IsLastPart { get; set; }

        [DataMember]
        public byte[] Data { get; set; }
    }
}
