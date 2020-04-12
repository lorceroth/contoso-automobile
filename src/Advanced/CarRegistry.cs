using ContosoAutomobile.Advanced.Interfaces;
using ContosoAutomobile.Advanced.Models;
using System;
using System.Linq;

namespace ContosoAutomobile.Advanced
{
    public class CarRegistry
    {
        private readonly ICarRepository _carRepository;

        public CarRegistry(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public void Run()
        {
            Console.WriteLine("Contoso Automobile - Bilregister");

            while (true)
            {
                var choice = GetChoice();
                if (choice == 4)
                {
                    break;
                }

                HandleChoice(choice);
            }

            Console.WriteLine("Välkommen åter!");
        }

        public int GetChoice()
        {
            Console.WriteLine("Vad vill du göra?");
            Console.WriteLine(" [1] Lista alla bilar");
            Console.WriteLine(" [2] Lägg till en ny bil");
            Console.WriteLine(" [3] Ta bort en bil");
            Console.WriteLine(" [4] Avsluta");

            Console.Write("Ditt val: ");
            var choice = Console.ReadLine();

            if ((int.TryParse(choice, out var value)) && Enumerable.Range(1, 4).Contains(value))
            {
                return value;
            }

            Console.WriteLine("Felaktigt val, var god försök igen.");

            return GetChoice();
        }

        public void HandleChoice(int choice)
        {
            switch (choice)
            {
                case 1:
                    ListCars();
                    break;
                case 2:
                    AddCar();
                    break;
                case 3:
                    DeleteCar();
                    break;
            }
        }

        public void ListCars()
        {
            Console.WriteLine("Lista alla bilar");

            var cars = _carRepository.GetAllCars();
            if (!cars.Any())
            {
                Console.WriteLine("Det finns inga bilar i registret ännu.");

                return;
            }

            foreach (var car in cars)
            {
                Console.WriteLine($"ID\n " +
                    $"{car.Id}");
                Console.WriteLine($"Märke\n " +
                    $"{car.Brand}");
                Console.WriteLine($"Modell\n " +
                    $"{car.Model}");
                Console.WriteLine($"Registeringsnummer\n " +
                    $"{car.LicensePlate}");
                Console.WriteLine("---");
            }
        }

        public void AddCar()
        {
            Console.WriteLine("Lägg till en ny bil (lämna tomt för att avbryta)");

            Console.Write("Märke: ");
            var brand = Console.ReadLine();

            if (string.IsNullOrEmpty(brand))
            {
                Console.WriteLine("Avbryter");

                return;
            }

            Console.Write("Modell: ");
            var model = Console.ReadLine();

            Console.Write("Registreringsnummer: ");
            var reg = Console.ReadLine();

            var car = new Car
            {
                Brand = brand,
                Model = model,
                LicensePlate = reg,
            };

            _carRepository.AddCar(car);

            Console.WriteLine("Bilen lades till i registret.");
        }

        public void DeleteCar()
        {
            Console.WriteLine("Ta bort en bil (lämna tomt för att avbryta)");

            Console.Write("Registreringsnummer: ");
            var reg = Console.ReadLine();

            if (string.IsNullOrEmpty(reg))
            {
                Console.WriteLine("Avrbyter");

                return;
            }

            var existingCar = _carRepository.GetCarByLicensePlate(reg);
            if (existingCar != null)
            {
                _carRepository.RemoveCar(existingCar);

                Console.WriteLine($"Tog bort bilen med registreringsnumret {reg} från registret.");
            }
            else
            {
                Console.WriteLine($"Bil med registreringsnumret {reg} finns inte i registret.");
            }
        }
    }
}
