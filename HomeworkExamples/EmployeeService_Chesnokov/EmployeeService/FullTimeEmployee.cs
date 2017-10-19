using System.Runtime.Serialization;

namespace EmployeeService
{
    [DataContract(Namespace = "http://itstep.dp.ua/employee")]
    public class FullTimeEmployee : Employee
    {
        [DataMember(Name = "MonthSallary")]
        public decimal MonthSallary { get; set; }
    }
}