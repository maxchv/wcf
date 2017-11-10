using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace TestingSystemService
{
    [DataContract]
    public class User
    {
        [Key]
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string Login { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Group { get; set; }
    }
}