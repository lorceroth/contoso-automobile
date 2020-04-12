using System;
using System.Collections.Generic;
using System.Linq;

namespace ContosoAutomobile.Simple
{
    class Program
    {
        public static List<object> Cars = new List<object>();

        public static void Main(string[] args)
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

        public static int GetChoice()
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

        public static void HandleChoice(int choice)
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

        public static void ListCars()
        {
            Console.WriteLine("Lista alla bilar");

            if (!Cars.Any())
            {
                Console.WriteLine("Det finns inga bilar i registret ännu.");

                return;
            }

            foreach (var car in Cars)
            {
                Console.WriteLine(car.ToString());
            }
        }

        public static void AddCar()
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

            var car = new
            {
                Brand = brand,
                Model = model,
                Reg = reg,
            };

            Cars.Add(car);

            Console.WriteLine("Bilen lades till i registret.");
        }

        public static void DeleteCar()
        {
            Console.WriteLine("Ta bort en bil (lämna tomt för att avbryta)");

            Console.Write("Registreringsnummer: ");
            var reg = Console.ReadLine();

            if (string.IsNullOrEmpty(reg))
            {
                Console.WriteLine("Avrbyter");

                return;
            }

            var existingCar = Cars.FirstOrDefault(car => ((dynamic)car).Reg == reg);
            if (existingCar != null)
            {
                Cars.Remove(existingCar);

                Console.WriteLine($"Tog bort bilen med registreringsnumret {reg} från registret.");
            }
            else
            {
                Console.WriteLine($"Bil med registreringsnumret {reg} finns inte i registret.");
            }
        }
    }
}
