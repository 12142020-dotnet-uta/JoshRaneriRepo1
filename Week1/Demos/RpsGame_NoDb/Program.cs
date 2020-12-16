using System;

namespace RpsGame_NoDb
{
    class Program
    {
        static void Main(string[] args)
        {
            bool userResponseParsed;
            int number;
            do{
                Console.WriteLine("Please make a selection");
                Console.WriteLine("  1. Rock");
                Console.WriteLine("  2. Paper");
                Console.WriteLine("  3. Scissors");
                string userResponse = Console.ReadLine();
                userResponseParsed = int.TryParse(userResponse, out number);
                if (userResponseParsed == false || (number < 1 || number > 3)){
                    Console.WriteLine("Invalid input.");
                }
            } while(userResponseParsed == false || (number < 1 || number > 3));
            switch (number)
            {

            }

        }
    }
}
