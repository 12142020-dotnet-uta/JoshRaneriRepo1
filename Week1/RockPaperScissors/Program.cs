using System;

namespace RockPaperScissors
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Rock - Paper - Scissors");
            int input = 0;
            while (input != 4)
            {
                Console.WriteLine("1) 1 Round");
                Console.WriteLine("2) Best of 3");
                Console.WriteLine("3) Best of 5");
                Console.WriteLine("4) Exit");
                Console.Write("Please choose a game mode: ");
                input = Convert.ToInt32(Console.ReadLine());

                switch (input)
                {
                    case 1:
                    {
                        int inInt = 0;                        
                        Console.WriteLine("1) Rock");
                        Console.WriteLine("2) Paper");
                        Console.WriteLine("3) Scissors");
                        Console.WriteLine("4) Return to menu");
                        Console.Write("Please make a selection: ");
                        inInt = Convert.ToInt32(Console.ReadLine());
                        oneRound(inInt);
                        break;
                    }
                    case 2:
                    {
                        int inInt = 0;
                        int pWins = 0;
                        int cWins = 0;
                        while (pWins < 2 && cWins < 2)
                        {
                            Console.WriteLine("1) Rock");
                            Console.WriteLine("2) Paper");
                            Console.WriteLine("3) Scissors");
                            Console.WriteLine("4) Return to menu");
                            Console.Write("Please make a selection: ");
                            inInt = Convert.ToInt32(Console.ReadLine());
                            if (oneRound(inInt) == 1)
                            {
                                pWins++;
                            }
                            else if (oneRound(inInt) == 2)
                            {
                                cWins++;
                            }
                        }
                        if (pWins > cWins) 
                        {
                            Console.WriteLine("You win Best of 3!");
                        }
                        else if (cWins > pWins) 
                        {
                            Console.WriteLine("You lose Best of 3.");
                        }
                        break;
                    }
                    case 3:
                    {
                        int inInt = 0;
                        int pWins = 0;
                        int cWins = 0;
                        while (pWins < 3 && cWins < 3)
                        {
                            Console.WriteLine("1) Rock");
                            Console.WriteLine("2) Paper");
                            Console.WriteLine("3) Scissors");
                            Console.WriteLine("4) Return to menu");
                            Console.Write("Please make a selection: ");
                            inInt = Convert.ToInt32(Console.ReadLine());
                            if (oneRound(inInt) == 1)
                            {
                                pWins++;
                            }
                            else if (oneRound(inInt) == 2)
                            {
                                cWins++;
                            }
                        }
                        if (pWins > cWins) 
                        {
                            Console.WriteLine("You win Best of 5!");
                        }
                        else if (cWins > pWins) 
                        {
                            Console.WriteLine("You lose Best of 5.");
                        }
                        break;
                    }
                    case 4:
                        break;
                }
            }

        }

        static int oneRound(int choice)
        {
            var rand = new Random();
            int gameInt = rand.Next(1, 3);
            switch (choice) 
            {
                case 1:
                {
                    if (gameInt == 1) { Console.WriteLine("Both chose Rock. Tie."); return 3;}
                    else if (gameInt == 2) { Console.WriteLine("Paper covers Rock. You Lose."); return 2;}
                    else if (gameInt == 3) { Console.WriteLine("Rock crushes Scissors. You win!"); return 1;}
                    break;
                }
                case 2:
                {
                    if (gameInt == 1) { Console.WriteLine("Paper covers Rock. You win!"); return 1;}
                    else if (gameInt == 2) { Console.WriteLine("Both chose Paper. Tie."); return 3;}
                    else if (gameInt == 3) { Console.WriteLine("Scissors cut Paper. You lose."); return 2;}
                    break;
                }
                case 3:
                {
                    if (gameInt == 1) { Console.WriteLine("Rock crushes Scissors. You lose."); return 2;}
                    else if (gameInt == 2) { Console.WriteLine("Scissors cut Paper. You win!"); return 1;}
                    else if (gameInt == 3) { Console.WriteLine("Both chose Scissors. Tie."); return 3;}
                    break;
                }
            }
            return 4;
        }
    }
}
