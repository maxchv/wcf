using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RentAutoWcf
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IRentService" в коде и файле конфигурации.
    [ServiceContract]
    public interface IRentService
    {
        [OperationContract]
        List<string> GetAllCars();
        [OperationContract]
        void AddCar(string brand,decimal rent);
        [OperationContract]
        void RemoveCar(string brand);
        [OperationContract]
        decimal GetRent(DateTime start, DateTime end, string brand);
    }
}
