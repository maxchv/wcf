using System.Runtime.Serialization;

namespace RemoteEditingFilesService
{
    [DataContract]
    public class User
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public IServiceCallback Callback { get; set; }
    }
}