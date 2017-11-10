using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace TestingSystemService
{
    [DataContract]
    public class TestResult
    {
        [Key]
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public Test Test { get; set; }

        [DataMember]
        public User User { get; set; }

        [DataMember]
        public int Rating { get; set; }

        [DataMember]
        public List<QuestionWithAnswer> Questions { get; set; }
    }
}