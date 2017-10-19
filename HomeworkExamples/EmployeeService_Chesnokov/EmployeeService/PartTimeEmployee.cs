using System.Runtime.Serialization;

namespace EmployeeService
{
    [DataContract(Namespace = "http://itstep.dp.ua/employee")]
    public class PartTimeEmployee : Employee
    {
        [DataMember(Name = "HourlyPay")]
        public decimal HourlyPay { get; set; }

        [DataMember(Name = "HoursWorked")]
        public int HoursWorked { get; set; }
    }
}