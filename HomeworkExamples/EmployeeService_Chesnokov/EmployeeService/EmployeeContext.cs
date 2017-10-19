using System;
using System.Data.Entity;

namespace EmployeeService
{
    class EmployeeContext : DbContext
    {
        class InitializeContext : CreateDatabaseIfNotExists<EmployeeContext>
        {
            protected override void Seed(EmployeeContext context)
            {
                FullTimeEmployee employee = new FullTimeEmployee();
                employee.Name = "Вася";
                employee.DateOfBirth = new DateTime(1986, 04, 21);
                employee.Gender = Gender.Male;
                employee.MonthSallary = 4000;
                employee.EmployeeType = EmployeeType.FullTimeEmployee;

                PartTimeEmployee employee2 = new PartTimeEmployee();
                employee2.Name = "Соня";
                employee2.DateOfBirth = new DateTime(1989, 02, 10);
                employee2.Gender = Gender.Female;
                employee2.HourlyPay = 250;
                employee2.HoursWorked = 16;
                employee2.EmployeeType = EmployeeType.PartTimeEmployee;

                context.Employees.Add(employee);
                context.Employees.Add(employee2);
                context.SaveChanges();
            }
        }

        public EmployeeContext() : base("AgencyCS")
        {
            Database.SetInitializer(new InitializeContext());
        }

        public DbSet<Employee> Employees { get; set; }
    }
}