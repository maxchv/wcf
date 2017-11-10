using System.Runtime.Serialization;

namespace RemoteEditingFilesService
{
    [DataContract]
    public class Files
    {
        [DataMember]
        public string FileName { get; set; }

        [DataMember]
        public string Text { get; set; }
    }
}