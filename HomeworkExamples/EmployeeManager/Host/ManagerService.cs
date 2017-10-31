using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Host
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ManagerService" in both code and config file together.

    [DataContract(Name = "Gender")]
    public enum Gender
    {
        [EnumMember]
        Male,
        [EnumMember]
        Female
    }

    [DataContract(Name = "EmployeeType")]
    public enum EmployeeType
    {
        [EnumMember]
        FullTimeEmployee,
        [EnumMember]
        PartTimeEmployee
    }


    public class ManagerService : IManagerService
    {
        EmployeeContext ctx = new EmployeeContext();
        public int AddEmployee(Employee employee)
        {
            int result = 0;
            if (employee is FullTimeEmployee)
            {
                var v = ctx.FullEmployee.Add(employee as FullTimeEmployee);
                result = v.Id;
            }
            else if (employee is PartTimeEmployee)
            {
                var v = ctx.PartEmployee.Add(employee as PartTimeEmployee);
                result = v.Id;
            }
            else
                result = -1;
            ctx.SaveChanges();
            return result;
        }

        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>(ctx.FullEmployee.ToList());
            employees.AddRange(ctx.PartEmployee.ToList());
            return employees;
        }

        public Employee GetFullTimeEmployee(int id)
        {
            return ctx.FullEmployee.Where(e => e.Id == id).FirstOrDefault();
        }

        public Employee GetPartTimeEmployee(int id)
        {
            return ctx.PartEmployee.Where(e => e.Id == id).FirstOrDefault();
        }

        public bool RemoveEmployee(Employee employee)
        {
            bool result = false;
            if (employee is FullTimeEmployee)
            {
                var v = ctx.FullEmployee.Remove(ctx.FullEmployee.Where(e=>e.Id == employee.Id).FirstOrDefault());
                result = true;
            }
            else if (employee is PartTimeEmployee)
            {
                var v = ctx.PartEmployee.Remove(ctx.PartEmployee.Where(e => e.Id == employee.Id).FirstOrDefault());
                result = true;
            }
            ctx.SaveChanges();
            return result;
        }

        public bool UpdateEmployee(Employee employee)
        {
            bool result = false;
            if (employee is FullTimeEmployee)
            {
                var v = ctx.FullEmployee.Where(e => e.Id == employee.Id).FirstOrDefault();
                if (v != null)
                {
                    ctx.Entry(v).State = EntityState.Modified;
                    CopyProperties(v, employee);
                    result = true;
                }
                else
                    result = false;
            }
            else if (employee is PartTimeEmployee)
            {
                var v = ctx.PartEmployee.Where(e => e.Id == employee.Id).FirstOrDefault();
                if (v != null)
                {
                    ctx.Entry(v).State = EntityState.Modified;
                    CopyProperties(v, employee);
                    result = true;
                }
                else
                    result = false;
            }
            ctx.SaveChanges();
            return result;
        }

        private void CopyProperties(Employee to, Employee from)
        {
            if(to is PartTimeEmployee&&from is PartTimeEmployee)
            {
                (to as PartTimeEmployee).Name = (from as PartTimeEmployee).Name;
                (to as PartTimeEmployee).Gender = (from as PartTimeEmployee).Gender;
                (to as PartTimeEmployee).DateOfBirth = (from as PartTimeEmployee).DateOfBirth;
                (to as PartTimeEmployee).Type = (from as PartTimeEmployee).Type;
                (to as PartTimeEmployee).HourlyPay = (from as PartTimeEmployee).HourlyPay;
                (to as PartTimeEmployee).HoursWorked = (from as PartTimeEmployee).HoursWorked;
            }
            else if (to is FullTimeEmployee && from is FullTimeEmployee)
            {
                (to as FullTimeEmployee).Name = (from as FullTimeEmployee).Name;
                (to as FullTimeEmployee).Gender = (from as FullTimeEmployee).Gender;
                (to as FullTimeEmployee).DateOfBirth = (from as FullTimeEmployee).DateOfBirth;
                (to as FullTimeEmployee).Type = (from as FullTimeEmployee).Type;
                (to as FullTimeEmployee).MonthSallary = (from as FullTimeEmployee).MonthSallary;
            }
        }
    }

    public class EmployeeContext : DbContext
    {
        public EmployeeContext() : base() =>
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<EmployeeContext>());
        public DbSet<FullTimeEmployee> FullEmployee { get; set; }
        public DbSet<PartTimeEmployee> PartEmployee { get; set; }
    }



    [KnownType(typeof(FullTimeEmployee))]
    [KnownType(typeof(PartTimeEmployee))]
    [DataContract]
    public class Employee
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public Gender Gender { get; set; }

        [DataMember]
        public DateTime DateOfBirth { get; set; }

        [DataMember]
        public EmployeeType Type { get; set; }
    }


    [DataContract]
    public class FullTimeEmployee : Employee
    {
        [DataMember]
        public decimal MonthSallary { get; set; }
    }

    [DataContract]
    public class PartTimeEmployee : Employee
    {
        [DataMember]
        public decimal HourlyPay { get; set; }
        [DataMember]
        public int HoursWorked { get; set; }
    }
}
