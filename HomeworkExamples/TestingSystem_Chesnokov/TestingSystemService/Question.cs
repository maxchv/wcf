using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace TestingSystemService
{
    [DataContract]
    public class Question
    {
        [Key]
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string Text { get; set; }
    }
}