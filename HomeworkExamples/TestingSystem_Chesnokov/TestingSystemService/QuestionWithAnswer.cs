using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace TestingSystemService
{
    [DataContract]
    public class QuestionWithAnswer
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string IdentifierdInTest { get; set; }

        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public string Answer { get; set; }
    }
}