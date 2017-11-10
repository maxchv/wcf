using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace TestingSystemService
{
    [DataContract]
    public class Test
    {
        [Key]
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int NumberOfQuestions { get; set; }

        [DataMember]
        public int NumberMinutesToPassTest { get; set; }

        [DataMember]
        public List<Question> Questions { get; set; } = new List<Question>();
    }
}