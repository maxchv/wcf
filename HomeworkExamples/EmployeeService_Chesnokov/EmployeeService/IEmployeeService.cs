using System.Collections.Generic;
using System.ServiceModel;

namespace EmployeeService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IEmployeeService
    {
        [OperationContract]
        int AddEmployee(Employee employee);

        [OperationContract]
        bool RemoveEmployee(int id);

        [OperationContract]
        bool EditEmployee(int id, Employee newEmployeeData);

        [OperationContract]
        List<Employee> GetEmployees(EmployeeType employeeType);
    }
}