using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Host
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IManagerService" in both code and config file together.
    [ServiceContract]
    public interface IManagerService
    {
        [OperationContract]
        Employee GetFullTimeEmployee(int id);

        [OperationContract]
        Employee GetPartTimeEmployee(int id);

        [OperationContract]
        List<Employee> GetAllEmployees();

        [OperationContract]
        int AddEmployee(Employee employee);

        [OperationContract]
        bool RemoveEmployee(Employee employee);

        [OperationContract]
        bool UpdateEmployee(Employee employee);
    }
}
