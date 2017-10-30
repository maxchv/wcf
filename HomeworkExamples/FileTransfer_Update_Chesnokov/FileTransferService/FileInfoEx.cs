using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace FileTransferService
{
    [DataContract]
    public class FileInfoEx
    {
        [Key]
        [DataMember]
        public string FileNameOnServer { get; set; }

        [DataMember]
        public string OriginalFileName { get; set; }

        [DataMember]
        public string Author { get; set; }

        [DataMember]
        public DateTime DateUpload { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
    
    public enum FileListType
    {
        AllFiles,
        ByAuthor,
        ByFileName,
        ByDateUpload
    }
}