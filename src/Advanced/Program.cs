using ContosoAutomobile.Advanced.Repositories;
using System.IO;
using System.Reflection;

namespace ContosoAutomobile.Advanced
{
    class Program
    {
        public static void Main(string[] args)
        {
            var filePath = Path.Join(GetCurrentDirectory(), "CarRegistry.txt");
            var carRepository = new CarRepository(filePath);
            var carRegistry = new CarRegistry(carRepository);

            carRegistry.Run();
        }

        public static string GetCurrentDirectory()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }
    }
}
