using System;
using System.Runtime.Serialization;

namespace FileTransferService
{
    [DataContract]
    public class FileDataUpload
    {
        [DataMember]
        public string FileId { get; set; }

        [DataMember]
        public string FileName { get; set; }

        [DataMember]
        public string Author { get; set; }

        [DataMember]
        public DateTime DateUpload { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public byte[] Data { get; set; }

        [DataMember]
        public bool IsLastPart { get; set; }
    }
}