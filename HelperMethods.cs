using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperProject
{
    public static class HelperMethods
    {
        
       // Placeholder
       public static void FeatureInMaking()
        {
            Console.Clear();
            Console.WriteLine("Funktion är under utveckling.");
        }

        //Method to get user back to menu
        public static void ReturnToMenu()
        {
            Console.Write("\nTryck på valfri tangent för att gå tillbaka.");
            Console.ReadKey();
        }

        //Inputvalidering för string
        public static string ReadString(string questionText)
        {
            while (true)
            {
                Console.Clear();
                Console.Write(questionText);

                string? userInput = Console.ReadLine();

                if (!String.IsNullOrEmpty(userInput))
                {
                    return userInput;
                }

                else
                {
                    Console.WriteLine("Ange ett giltigt svar!");
                    Thread.Sleep(2000);
                }
            }            
        }

        //Inputvalidering för string med begränsning för antal tecken.
    public static string ReadString(string questionText, int minAmountOfCharacters, int maxAmountOfCharacters)
    {
        while (true)
        {
            Console.Clear();
            Console.Write(questionText);

            string? userInput = Console.ReadLine();

            if (!String.IsNullOrEmpty(userInput))
            {
                if (userInput.Length>=minAmountOfCharacters && userInput.Length<=maxAmountOfCharacters)
                {
                    return userInput;
                }

                else
                {
                    Console.WriteLine($"Please enter an answer between {minAmountOfCharacters} and {maxAmountOfCharacters} characters.");
                    Thread.Sleep(2000);
                }                    
            }

            else
            {
                Console.WriteLine("Please enter a valid answer!");
                Thread.Sleep(2000);
            }
        }
    }

    public static DateOnly ReadDate(string questionText)
    {
        while (true)
        {
            Console.Write(questionText);
            string? userInput = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(userInput))
            {
                bool success = DateOnly.TryParseExact(
                    userInput,
                    "yyyy-MM-dd",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateOnly parsedDate
                );

                if (success)
                {
                    return parsedDate;
                }
                else
                {
                    Console.WriteLine("Fel format. Ange datum som ÅÅÅÅ-MM-DD (t.ex. 2025-12-27).");
                    Thread.Sleep(2000);
                }
            }
            else
            {
                Console.WriteLine("Ange ett datum.");
                Thread.Sleep(2000);
            }
        }
    }


    public static int ReadInt(string questionText)
        {          
            while (true)
            {                  
                  Console.Write(questionText);

                  string? userInput = Console.ReadLine();

                    if (!String.IsNullOrEmpty(userInput))
                    {
                        bool success = int.TryParse(userInput, out int parsedValue);
                        if (success)
                        {
                            return parsedValue;
                        }

                        else
                        {
                            Console.WriteLine("Please enter a valid number.");
                            Thread.Sleep(2000);
                        }
                    }

                    else
                    {
                        Console.WriteLine("Please enter a number.");
                        Thread.Sleep(2000);
                    }
            }
        }

        //Lägg till min och maxvärde för siffra
        public static int ReadInt(string questionText, int minValue, int maxValue)
        {
            while (true)
            {                
                Console.Write(questionText);

                string? userInput = Console.ReadLine();

                if (!String.IsNullOrEmpty(userInput))
                {
                    bool success = int.TryParse(userInput, out int parsedValue);
                    if (success && parsedValue >= minValue && parsedValue <= maxValue)
                    {
                        return parsedValue;
                    }

                    else
                    {
                        Console.WriteLine($"Please enter a valid number between {minValue} and {maxValue}.");
                        Thread.Sleep(2000);
                    }
                }

                else
                {
                    Console.WriteLine("Please enter a number.");
                    Thread.Sleep(2000);
                }
            }
        }

        //Hantera ja/nej-svar -anpassa att likna andra inputvalideringar
        public static string ReadYesNo(string questionText)
        {
            while (true)
            {                
                Console.WriteLine(questionText);

                Console.Write("Enter your choice (Y/N): ");
                string? userInput = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(userInput))
                {
                    Console.WriteLine("Please enter a valid answer!");
                    continue;
                }

                userInput = userInput.Trim().ToUpper();
                if (userInput == "Y" || userInput == "N")
                {
                    return userInput;
                }

                else
                {
                    Console.WriteLine($"\nPlease enter Y (yes) or N (no).");
                }   
            }
        }

    }
}
