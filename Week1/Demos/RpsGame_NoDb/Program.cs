using System;

namespace RpsGame_NoDb
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Rock - Paper - Scissors");
            int input = 0;
            while (input != 4)
            {
                // main menu
                Console.WriteLine("1) 1 Round");
                Console.WriteLine("2) Best of 3");
                Console.WriteLine("3) Best of 5");
                Console.WriteLine("4) Exit");
                Console.Write("Please choose a game mode: ");
                try // error catching on input
                {
                    input = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception){
                    input = 5;
                }
                switch (input)
                {
                    case 1:
                    {
                        int inInt = 0;
                        // Single round submenu
                        Console.WriteLine("1) Rock");
                        Console.WriteLine("2) Paper");
                        Console.WriteLine("3) Scissors");
                        Console.WriteLine("4) Return to menu");
                        Console.Write("Please make a selection: ");
                        try // error catching on input
                        {
                            inInt = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception){
                            input = 5;
                        }
                        oneRound(inInt);
                        break;
                    }
                    case 2:
                    {
                        int inInt = 0;
                        int pWins = 0;
                        int cWins = 0;
                        while (pWins < 2 && cWins < 2 && inInt != 4)
                        {
                            // Best of 3 menu
                            Console.WriteLine("\nBest of 3 - Current score: P " + pWins + " | C " + cWins);
                            Console.WriteLine("1) Rock");
                            Console.WriteLine("2) Paper");
                            Console.WriteLine("3) Scissors");
                            Console.WriteLine("4) Return to menu");
                            Console.Write("Please make a selection: ");
                            try
                            {
                                inInt = Convert.ToInt32(Console.ReadLine());
                            }
                            catch (Exception){
                                input = 5;
                            }
                            // run oneRound then adjust new scores
                            int round = oneRound(inInt);
                            if (round == 1)
                            {
                                pWins++;
                            }
                            else if (round == 2)
                            {
                                cWins++;
                            }
                        }
                        // score checking
                        if (pWins > cWins && inInt != 4) 
                        {
                            Console.WriteLine("\nYou win Best of 3!\n");
                        }
                        else if (cWins > pWins || inInt == 4) 
                        {
                            Console.WriteLine("\nYou lose Best of 3.\n");
                        }
                        break;
                    }
                    case 3:
                    {
                        int inInt = 0;
                        int pWins = 0;
                        int cWins = 0;
                        while (pWins < 3 && cWins < 3 && inInt != 4)
                        {
                            // Best of 5 menu
                            Console.WriteLine("\nBest of 5 - Current score: P " + pWins + " | C " + cWins);
                            Console.WriteLine("1) Rock");
                            Console.WriteLine("2) Paper");
                            Console.WriteLine("3) Scissors");
                            Console.WriteLine("4) Return to menu");
                            Console.Write("Please make a selection: ");
                            try // error catching on input
                            {
                                inInt = Convert.ToInt32(Console.ReadLine());
                            }
                            catch (Exception){
                                input = 5;
                            }
                            if (inInt == 4)
                                break;
                            // run oneRound then adjust new scores
                            int round = oneRound(inInt);
                            if (round == 1)
                            {
                                pWins++;
                            }
                            else if (round == 2)
                            {
                                cWins++;
                            }
                        }
                        // score checking
                        if (pWins > cWins && inInt != 4) 
                        {
                            Console.WriteLine("\nYou win Best of 5!\n");
                        }
                        else if (cWins > pWins || inInt == 4) 
                        {
                            Console.WriteLine("\nYou lose Best of 5.\n");
                        }
                        break;
                    }
                    case 4:
                        break;
                    default:
                    {
                        // catches any invalid input
                        Console.WriteLine("Invalid input. Please enter a valid choice.\n");
                        continue;
                    }
                }
            }
        }

        // method to run one round of RPS
        static int oneRound(int choice)
        {
            var rand = new Random();
            int gameInt = rand.Next(1, 4);
            // switch takes user choice, creates a random number, compares the two, 
            // then returns a value for win, loss, or tie
            switch (choice) 
            {
                case 1:
                {
                    if (gameInt == 1) { 
                        Console.WriteLine("\nBoth chose Rock. Tie.\n"); return 3;
                    }
                    else if (gameInt == 2) { 
                        Console.WriteLine("\nPaper covers Rock. You Lose.\n"); return 2;
                    }
                    else if (gameInt == 3) { 
                        Console.WriteLine("\nRock crushes Scissors. You win!\n"); return 1;
                    }
                    break;
                }
                case 2:
                {
                    if (gameInt == 1) { 
                        Console.WriteLine("\nPaper covers Rock. You win!\n"); return 1;
                    }
                    else if (gameInt == 2) { 
                        Console.WriteLine("\nBoth chose Paper. Tie.\n"); return 3;
                    }
                    else if (gameInt == 3) { 
                        Console.WriteLine("\nScissors cut Paper. You lose.\n"); return 2;
                    }
                    break;
                }
                case 3:
                {
                    if (gameInt == 1) { 
                        Console.WriteLine("Rock crushes Scissors. You lose."); return 2;
                    }
                    else if (gameInt == 2) { 
                        Console.WriteLine("Scissors cut Paper. You win!"); return 1;
                    }
                    else if (gameInt == 3) { 
                        Console.WriteLine("Both chose Scissors. Tie."); return 3;
                    }
                    break;
                }
            }
            return 4;
        }
    }
}
