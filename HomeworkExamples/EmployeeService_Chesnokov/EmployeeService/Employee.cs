using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace EmployeeService
{
    [DataContract(Namespace = "http://itstep.dp.ua/employee")]
    [KnownType(typeof(FullTimeEmployee))]
    [KnownType(typeof(PartTimeEmployee))]
    public class Employee
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember(Name = "Id", Order = 1)]
        public int Id { get; internal set; }

        [DataMember(Name = "Name", Order = 2)]
        public string Name { get; set; }

        [DataMember(Name = "Gender", Order = 3)]
        public Gender Gender { get; set; }

        [DataMember(Name = "DateOfBirth", Order = 4)]
        public DateTime DateOfBirth { get; set; }

        [DataMember(Name = "EmployeeType", Order = 5)]
        public EmployeeType EmployeeType { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }

    public enum EmployeeType
    {
        FullTimeEmployee,
        PartTimeEmployee
    }
}