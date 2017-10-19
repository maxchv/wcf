using System.Collections.Generic;
using System.Linq;

namespace EmployeeService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class EmployeeService : IEmployeeService
    {
        public int AddEmployee(Employee employee)
        {
            try
            {
                using (EmployeeContext context = new EmployeeContext())
                {
                    context.Employees.Add(employee);
                    context.SaveChanges();
                }
            }
            catch
            {
                // ignored
            }

            return employee.Id;
        }

        public bool RemoveEmployee(int id)
        {
            bool result = false;
            try
            {
                using (EmployeeContext context = new EmployeeContext())
                {
                    Employee employee = context.Employees.FirstOrDefault(e => e.Id == id);

                    if (employee != null)
                    {
                        context.Employees.Remove(employee);
                        context.SaveChanges();

                        result = true;
                    }
                }
            }
            catch
            {
                result = false;
            }

            return result;
        }

        public bool EditEmployee(int id, Employee newEmployeeData)
        {
            bool result = false;

            try
            {
                using (EmployeeContext context = new EmployeeContext())
                {
                    Employee employee = context.Employees.FirstOrDefault(e => e.Id == id);

                    if (employee != null)
                    {
                        employee.Name = newEmployeeData.Name;
                        employee.DateOfBirth = newEmployeeData.DateOfBirth;
                        employee.Gender = newEmployeeData.Gender;
                        employee.EmployeeType = newEmployeeData.EmployeeType;

                        if (employee is FullTimeEmployee &&
                            newEmployeeData is FullTimeEmployee)
                        {
                            (employee as FullTimeEmployee).MonthSallary =
                                (newEmployeeData as FullTimeEmployee).MonthSallary;
                        }
                        else if (employee is PartTimeEmployee &&
                                 newEmployeeData is PartTimeEmployee)
                        {
                            (employee as PartTimeEmployee).HourlyPay =
                                (newEmployeeData as PartTimeEmployee).HourlyPay;
                            (employee as PartTimeEmployee).HoursWorked =
                                (newEmployeeData as PartTimeEmployee).HoursWorked;
                        }
                        else
                        {
                            if (newEmployeeData is FullTimeEmployee)
                            {
                                FullTimeEmployee newEmployee = new FullTimeEmployee();
                                newEmployee.Id = employee.Id;
                                newEmployee.Name = employee.Name;
                                newEmployee.DateOfBirth = employee.DateOfBirth;
                                newEmployee.Gender = employee.Gender;
                                newEmployee.EmployeeType = EmployeeType.FullTimeEmployee;
                                newEmployee.MonthSallary = (newEmployeeData as FullTimeEmployee).MonthSallary;

                                context.Employees.Remove(employee);
                                context.SaveChanges();
                                context.Employees.Add(newEmployee);
                            }
                            else if (newEmployeeData is PartTimeEmployee)
                            {
                                PartTimeEmployee newEmployee = new PartTimeEmployee();
                                newEmployee.Id = employee.Id;
                                newEmployee.Name = employee.Name;
                                newEmployee.DateOfBirth = employee.DateOfBirth;
                                newEmployee.Gender = employee.Gender;
                                newEmployee.EmployeeType = EmployeeType.PartTimeEmployee;
                                newEmployee.HourlyPay = (newEmployeeData as PartTimeEmployee).HourlyPay;
                                newEmployee.HoursWorked = (newEmployeeData as PartTimeEmployee).HoursWorked;
                                
                                context.Employees.Remove(employee);
                                context.SaveChanges();
                                context.Employees.Add(newEmployee);
                            }
                        }
                        
                        context.SaveChanges();

                        result = true;
                    }
                }
            }
            catch
            {
                result = false;
            }

            return result;
        }

        public List<Employee> GetEmployees(EmployeeType employeeType)
        {
            List<Employee> employees = new List<Employee>();

            try
            {
                using (EmployeeContext context = new EmployeeContext())
                {
                    employees = context.Employees.Where(e => e.EmployeeType == employeeType).ToList();
                }
            }
            catch
            {
                // ignored
            }

            return employees;
        }
    }
}