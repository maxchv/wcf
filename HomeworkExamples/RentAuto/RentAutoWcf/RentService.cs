using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RentAutoWcf
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "RentService" в коде и файле конфигурации.
    public class RentService : IRentService
    {
        static Dictionary<string, decimal> cars = new Dictionary<string, decimal>();
        

        public void AddCar(string brand, decimal rent)
        {
            if (!cars.ContainsKey(brand))
                cars.Add(brand, rent);
        }

        public List<string> GetAllCars()
        {
            return new List<string>(cars.Keys);
        }

        public decimal GetRent(DateTime start, DateTime end, string brand)
        {
            if (cars.ContainsKey(brand))
            {
                var rentalTime = end - start;
                return (decimal) rentalTime.Days * cars[brand];
            }
            else
                throw new FaultException("Car is not found");
        }
        

        public void RemoveCar(string brand)
        {
            if (cars.ContainsKey(brand))
                cars.Remove(brand);
        }
    }
}
