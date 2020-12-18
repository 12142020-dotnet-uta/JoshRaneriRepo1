using System;
using System.Collections.Generic;

namespace RpsGame_NoDb
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Player> players = new List<Player>();
            List<Round> rounds = new List<Round>();
            List<Match> matches = new List<Match>();

            //create Computer player
            Player p1 = new Player()
            {
                FName = "Max",
                LName = "Headroom"
            };
            players.Add(p1);

            //log in or create a new player - unique information will create new player
            Player p2 = userLogin();
            userVerify(players, p2);            

            Match match = new Match();
            match.Player1 = p1;
            match.Player2 = p2;

            Round round = new Round();

            int gameSelect = 0;

            Console.WriteLine("Rock - Paper - Scissors");

            while (gameSelect != 4)
            {
                // main menu
                Console.WriteLine($"\nWelcome, {p2.FName}.\n");
                Console.WriteLine("1) 1 Round");
                Console.WriteLine("2) Best of 3");
                Console.WriteLine("3) Best of 5");
                Console.WriteLine("4) Logout");
                Console.WriteLine("5. Exit");
                Console.Write("Please choose a game mode: ");
                try // error catching on gameSelect
                {
                    gameSelect = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception){
                    gameSelect = 6;
                }
                switch (gameSelect)
                {
                    case 1:
                    {
                        Console.WriteLine("Single Round");
                        Console.WriteLine($"Welcome, {p2.FName}.\n");                       
                        round = oneRound(p1, p2);
                        if (round.WinningPlayer == p2)
                        {
                            match.RoundWinner(p2);
                            Console.WriteLine("\n" + round.Player2Choice.ToString() + " beats " + round.Player1Choice.ToString() 
                                                + ". You win!\n");                            
                        }
                        else if (round.WinningPlayer == p1)
                        {
                            match.RoundWinner(p1);
                            Console.WriteLine("\n" + round.Player1Choice.ToString() + " beats " + round.Player2Choice.ToString() 
                                                + ". You lose.\n");                           
                        }
                        else if (round.WinningPlayer == null)
                        {
                            match.RoundWinner(new Player("Tie", "Game"));
                            Console.WriteLine("\nBoth players chose " + round.Player2Choice.ToString() + ". Game is a tie.\n");
                        }
                        break;
                    }
                    case 2:
                    {
                    /*     Choice playerChoice = new Choice();
                        int pWins = 0;
                        int cWins = 0;
                        while (pWins < 2 && cWins < 2 && (int)playerChoice != 4)
                        {
                            // Best of 3 menu
                            Console.WriteLine("\nBest of 3 - Current score: P " + pWins + " | C " + cWins);
                            drawGameMenu();
                            try
                            {
                                playerChoice = (Choice)Convert.ToInt32(Console.ReadLine());
                            }
                            catch (Exception){
                                gameSelect = 5;
                            }
                            // run oneRound then adjust new scores
                            int roundResult = oneRound((int)playerChoice);
                            if ((int)roundResult == 1)
                            {
                                pWins++;
                            }
                            else if ((int)roundResult == 2)
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
                        } */
                        break;
                    }
                    case 3:
                    {
                       /*  int inInt = 0;
                        int pWins = 0;
                        int cWins = 0;
                        while (pWins < 3 && cWins < 3 && inInt != 4)
                        {
                            // Best of 5 menu
                            Console.WriteLine("\nBest of 5 - Current score: P " + pWins + " | C " + cWins);
                            drawGameMenu();
                            try // error catching on gameSelect
                            {
                                inInt = Convert.ToInt32(Console.ReadLine());
                            }
                            catch (Exception){
                                gameSelect = 5;
                            }
                            if (inInt == 4)
                                break;
                            // run oneRound then adjust new scores
                            int newRound = oneRound((int)playerChoice);
                            if (newRound == 1)
                            {
                                pWins++;
                            }
                            else if (newRound == 2)
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
                        } */
                        break;
                    }
                    case 4:
                        p2 = new Player();
                        userLogin();
                        break;
                    case 5:
                        break;
                    default:
                    {
                        // catches any invalid gameSelect
                        Console.WriteLine("Invalid Game Selection. Please enter a valid choice.\n");
                        break;
                    }
                }
            }
        }

        static Player userLogin()
        {
            Player pTemp = new Player();
            string userName = "";
            string[] userNameArray;
            int m1 = 0;
            do
            {
                Console.WriteLine("1) Enter Player Name (Unique name will create new Player)");
                Console.WriteLine("2) Exit");
                Console.Write("Please make a selection:");
                try // error catching on gameSelect
                {
                    m1 = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception){
                    Console.WriteLine("Invalid Entry.");
                    m1 = 3;
                }
                switch (m1)
                {
                    case 1:
                    {
                        do
                        {
                            Console.Write("Please Enter First and Last Name Separated by ' ': ");
                            userName = Console.ReadLine();
                            userNameArray = userName.Split(' ');
                        } while(userName == "" || userName == null);
                        
                        if (userNameArray.Length >= 1)
                        {
                            pTemp.FName = userNameArray[0];
                        }
                        else if (userNameArray.Length > 1)
                        {
                            pTemp.LName = userNameArray[1];
                        }
                        return pTemp;
                    }
                    case 2:
                        Environment.Exit(0);
                        return pTemp;
                    default:
                        Console.WriteLine("Invalid Entry.");
                        return pTemp;
                }
            } while (userName == "" || userName == null);
        }

        static List<Player> userVerify(List<Player> players, Player p2)
        {
            bool found = false;
            foreach (Player checkPlayer in players)
            {
                if (checkPlayer == p2)
                {
                    found = true;
                }
            }
            if (!found) 
            { 
                players.Add(p2); 
                Console.WriteLine("Player added successfully.");
            }
            else if (found)
            {
                Console.WriteLine("Player already exists.");
            }
            return players;
        }

        // method to run one round of RPS
        static Round oneRound(Player p1, Player p2)
        {
            Choice playerChoice = new Choice();
            Round currentRound = new Round();
            Console.WriteLine("1) Rock");
            Console.WriteLine("2) Paper");
            Console.WriteLine("3) Scissors");
            Console.WriteLine("4) Exit / Previous Menu");
            Console.Write("Please make a selection: ");
            try // error catching
            {
                playerChoice = (Choice)Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception){
                Console.WriteLine("Invalid Entry.");
                return null;
            }
            var rand = new Random();
            Choice compChoice = (Choice)rand.Next(1, 4);
            // switch takes user choice, creates a random number, compares the two, 
            // then returns a value for win, loss, or tie
            currentRound.Player2Choice = playerChoice;
            currentRound.Player1Choice = compChoice;
            
            switch ((int)playerChoice) 
            {
                case 1:
                {
                    if ((int)compChoice == 1) { 
                        currentRound.WinningPlayer = null;
                    }
                    else if ((int)compChoice == 2) { 
                        currentRound.WinningPlayer = p1;
                    }
                    else if ((int)compChoice == 3) { 
                        currentRound.WinningPlayer = p2;
                    }
                    break;
                }
                case 2:
                {
                    if ((int)compChoice == 1) { 
                        currentRound.WinningPlayer = p2;
                    }
                    else if ((int)compChoice == 2) { 
                        currentRound.WinningPlayer = null;
                    }
                    else if ((int)compChoice == 3) { 
                        currentRound.WinningPlayer = p1;
                    }
                    break;
                }
                case 3:
                {
                    if ((int)compChoice == 1) { 
                        currentRound.WinningPlayer = p1;
                    }
                    else if ((int)compChoice == 2) { 
                        currentRound.WinningPlayer = p2;
                    }
                    else if ((int)compChoice == 3) { 
                        currentRound.WinningPlayer = null;
                    }
                    break;
                }
            }
            return currentRound;
        }
    }
}