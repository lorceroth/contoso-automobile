using ContosoAutomobile.Advanced.Interfaces;
using ContosoAutomobile.Advanced.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace ContosoAutomobile.Advanced.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly string _filePath;

        public CarRepository(string filePath)
        {
            _filePath = filePath;
        }

        public IEnumerable<Car> GetAllCars()
        {
            var cars = new List<Car>();

            if (!File.Exists(_filePath))
            {
                return cars;
            }

            var serializedCars = File.ReadAllText(_filePath);
            cars = JsonSerializer.Deserialize<List<Car>>(serializedCars);

            return cars;
        }

        public Car GetCarByLicensePlate(string licensePlate)
        {
            return GetAllCars().FirstOrDefault(car => car.LicensePlate == licensePlate);
        }

        public void AddCar(Car car)
        {
            var cars = GetAllCars().ToList();

            car.Id = Guid.NewGuid();
            cars.Add(car);

            var serializedCars = JsonSerializer.Serialize(cars);
            File.WriteAllText(_filePath, serializedCars);
        }

        public void RemoveCar(Car car)
        {
            var cars = GetAllCars().ToList();

            var existingCar = cars.FirstOrDefault(c => c.LicensePlate == car.LicensePlate);
            if (existingCar != null)
            {
                cars.Remove(existingCar);

                var serializedCars = JsonSerializer.Serialize(cars);
                File.WriteAllText(_filePath, serializedCars);
            }
        }
    }
}
