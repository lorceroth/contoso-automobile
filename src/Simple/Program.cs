// Using-uttryck används för att importera/referera klasser från andra namnrymder.
using System;
using System.Collections.Generic;
using System.Linq;

// En namnrymd kan jämföras med en mapp i filsystemet. Man kan inte ha två filer med
// samma namn i en och samma mapp.
//
// Detsamma gäller med namnrymder i C#, man kan inte ha två klasser med samma namn i
// samma namnrymd.
//
// Oftast går namnrymder ett i ett med projektets mappstruktur. Skulle vi exempelvis
// skapa en mapp "Models" i det här projektet, så hade alla klasser i mappen fått
// namnrymden "ContosoAutomobile.Simple.Models".
namespace ContosoAutomobile.Simple
{
    class Program
    {
        /// <summary>
        /// Initiera en instansvariabel/klassvariabel som kan innehålla en lista av vilka
        /// C# typer som helst (strängar, heltal, m.m.). Instansvariabler är åtkomliga
        /// från alla metoder i klassen.
        /// </summary>
        public static List<object> Cars = new List<object>();

        /// <summary>
        /// Main-metoden är alltid den första metoden som körs i alla C#-program.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            // Skriv ut text i utdataströmmen (a.k.a. terminalen)
            Console.WriteLine("Contoso Automobile - Bilregister");

            // En while-loop (typ av kontrollstruktur) med ett logiskt uttryck som alltid kommer vara sant.
            // Detta kallas även för en evighetsloop. För att avbryta en evighetsloop så behöver
            // man kalla på nyckelordet "break" någonstans i loopen.
            while (true)
            {
                // Kalla på metoden GetChoice() (se längre ned) och tilldela returvärdet till variabeln choice.
                var choice = GetChoice();

                // Om värdet vi fick tillbaka är exakt 4
                if (choice == 4)
                {
                    // Hoppa ur while-loopen
                    break;
                }

                // Kalla på metoden HandleChoice() och skicka med värdet i variabeln choice
                // som ett argument.
                HandleChoice(choice);
            }

            Console.WriteLine("Välkommen åter!");
        }

        /// <summary>
        /// Den här metoden skriver ut ett godtyckligt antal val och returnerar valet som
        /// användaren skriver in. Valet måste vara ett heltal (int).
        /// 
        /// Struktur:
        ///  * Synlighet (public) - Metodens åtkomlighet
        ///  * Statisk - Behövs just nu för att Main-metoden är statisk
        ///  * Returtyp (int) - Metoden måste returnera ett heltal
        ///  * Namn (GetChoice)
        ///  * Parametrar (inga)
        /// </summary>
        /// <returns></returns>
        public static int GetChoice()
        {
            Console.WriteLine("Vad vill du göra?");
            Console.WriteLine(" [1] Lista alla bilar");
            Console.WriteLine(" [2] Lägg till en ny bil");
            Console.WriteLine(" [3] Ta bort en bil");
            Console.WriteLine(" [4] Avsluta");

            // Console.Write() gör ingen radbrytning efter utskrift
            Console.Write("Ditt val: ");
            // Läs in användarens text från terminalen och tilldela värdet till variabeln choice
            var choice = Console.ReadLine();

            // int.TryParse returnerar true om värdet i variabeln choice kan konverteras till ett heltal.
            // Enumerable.Range() skapar en lista med heltal från 1 till 4
            // Contains() returnerar true om variabeln value finns i listan (med andra ord är mellan 1 och 4)
            if ((int.TryParse(choice, out var value)) && Enumerable.Range(1, 4).Contains(value))
            {
                // Returnera value och hoppa ur metoden
                return value;
            }

            // Om vi kom hit så matade användaren in ett felaktigt värde enligt reglerna vi bestämde ovanför
            Console.WriteLine("Felaktigt val, var god försök igen.");

            // Kalla på den här metoden igen. Detta gör att den här metoden blir rekursiv, vilket innebär att
            // den kan kalla på sig själv om och om igen.
            // Detta gör att användaren kan försöka på nytt igen.
            return GetChoice();
        }

        /// <summary>
        /// Den här metoden hanterar valet som användaren gjorde. Detta är ett elegant sätt att bryta upp logiken
        /// på så metoderna inte blir för stora.
        /// Den här metoden har returtypen void och behöver inte returnera något värde, men den har en
        /// parameter som man måste skicka in när man kallar på den.
        /// </summary>
        /// <param name="choice">Parameter av typen heltal som måste skickas med när man kallar på den här metoden.</param>
        public static void HandleChoice(int choice)
        {
            // En switch-case-sats (en typ av kontrollstruktur) är ett renare alternativ till if-satsen.
            // Den är användbar när man ska jämföra exakta värden i en variabel.
            switch (choice)
            {
                // När choice är lika med 1.
                // Varje case måste ha en break som bryter switch-case-satsen. Utan den fortsätter man
                // vidare till nästa case (i det här fallet case 2 och därefter case 3).
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

        /// <summary>
        /// Den här metoden hanterar logiken för att lägga till bilar i listan Cars.
        /// </summary>
        public static void ListCars()
        {
            Console.WriteLine("Lista alla bilar");

            // Any() metoden kommer från System.Linq och är ett elegant sätt att kolla ifall en
            // lista är tom.
            // I det här fallet har vi vänt på/negerat uttrycket med ett utropstecken (!) vilket
            // kan översättas till "om listan är tom".
            if (!Cars.Any())
            {
                Console.WriteLine("Det finns inga bilar i registret ännu.");

                // En tom return i en void metod avbryter resten av exekveringen och
                // hoppar ur metoden.
                return;
            }

            // En foreach-loop (typ av kontrollstruktur) är användbar för att loopa/iterera igenom en lista
            // med saker. I det här fallet går vi igenom varje objekt i Cars-listan.
            // Vid varje "snurr" kommer variabeln car att tilldelas ett objekt från listan, vilket vi
            // kan använda för att komma åt objektets egenskaper och metoder.
            foreach (var car in Cars)
            {
                // Skriv ut en sträng-representation av det aktuella värdet i car variabeln
                Console.WriteLine(car.ToString());
            }
        }

        /// <summary>
        /// Den här metoden hanterar logiken för att lägga till en ny bil i listan Cars.
        /// </summary>
        public static void AddCar()
        {
            Console.WriteLine("Lägg till en ny bil (lämna tomt för att avbryta)");

            Console.Write("Märke: ");
            var brand = Console.ReadLine();

            // Returnerar true om användaren inte skriver något
            if (string.IsNullOrEmpty(brand))
            {
                Console.WriteLine("Avbryter");

                return;
            }

            Console.Write("Modell: ");
            var model = Console.ReadLine();

            Console.Write("Registreringsnummer: ");
            var reg = Console.ReadLine();

            // Skapar ett nytt anonymt objekt med egenskaperna Brand, Model och Reg.
            var car = new
            {
                Brand = brand,
                Model = model,
                Reg = reg,
            };

            // Lägger till det anonyma objektet i listan Cars
            Cars.Add(car);

            Console.WriteLine("Bilen lades till i registret.");
        }

        /// <summary>
        /// Den här metoden hanterar logiken för att ta bort en bil från listan Cars.
        /// </summary>
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

            // LINQ är ett väldigt kraftfullt sätt att arbeta med listor i C# (https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/).
            // Här försöker vi hitta den första bilen i listan med det registreringsnumret som
            // användaren matade in ovanför.
            var existingCar = Cars.FirstOrDefault(car => ((dynamic)car).Reg == reg);
            if (existingCar != null)
            {
                // Ta bort bilen från listan.
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
