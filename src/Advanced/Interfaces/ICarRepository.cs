using ContosoAutomobile.Advanced.Models;
using System.Collections.Generic;

namespace ContosoAutomobile.Advanced.Interfaces
{
    public interface ICarRepository
    {
        IEnumerable<Car> GetAllCars();

        Car GetCarByLicensePlate(string licensePlate);

        void AddCar(Car car);

        void RemoveCar(Car car);
    }
}
