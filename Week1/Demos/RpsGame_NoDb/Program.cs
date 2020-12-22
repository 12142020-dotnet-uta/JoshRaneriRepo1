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

            Match match = new Match();
            Round round = new Round();            
            
            string userName = "";
            string[] userNameArray;
            int m1 = 0;
            int selectedGame = 0;
            bool loggedIn = false;

            Console.WriteLine("Rock - Paper - Scissors");
            
            //create Computer player
            Player p1 = new Player()
            {
                FName = "Max",
                LName = "Headroom"
            };
            players.Add(p1);
            Player p2 = new Player();
            match.Player1 = p1;
            match.Player2 = p2;
            

            do
            {
                userLogin(selectedGame, p1, p2, players, loggedIn, match, round);
            } while (m1 != 2 && loggedIn == false);
        }

        static void gameSelect(int selectedGame, Player p1, Player p2, List<Player> players, bool loggedIn, Match match, Round round)
        {
            do
            {
                // main menu
                Console.WriteLine($"\nWelcome, {p2.FName}.\n");
                Console.WriteLine("1) 1 Round");
                Console.WriteLine("2) Best of 3");
                Console.WriteLine("3) Best of 5");
                Console.WriteLine("4) Logout");
                Console.WriteLine("5. Exit");
                Console.Write("Please choose a game mode: ");
                try // error catching on selectedGame
                {
                    selectedGame = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    selectedGame = 6;
                }

                switch (selectedGame)
                {
                    case 1:
                        {
                            int playerChoiceInt = 0;
                            Console.WriteLine("Single Round\n");
                            Console.WriteLine($"Welcome, {p2.FName}.");
                            Console.WriteLine("1) Rock");
                            Console.WriteLine("2) Paper");
                            Console.WriteLine("3) Scissors");
                            Console.WriteLine("4) Exit / Previous Menu");
                            Console.Write("Please make a selection: ");
                            try // error catching
                            {
                                playerChoiceInt = Convert.ToInt32(Console.ReadLine());
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Invalid Entry.");
                                selectedGame = 5;
                            }
                            round = oneRound(p1, p2, playerChoiceInt);
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
                                match.RoundWinner(null);
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
                                        selectedGame = 5;
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
                                    try // error catching on selectedGame
                                    {
                                        inInt = Convert.ToInt32(Console.ReadLine());
                                    }
                                    catch (Exception){
                                        selectedGame = 5;
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
                        loggedIn = false;
                        userLogin(selectedGame, p1, p2, players, loggedIn, match, round);
                        break;
                    case 5:
                        Environment.Exit(0);
                        break;
                    default:
                        {
                            // catches any invalid selectedGame
                            Console.WriteLine("Invalid Game Selection. Please enter a valid choice.\n");
                            break;
                        }
                }
            } while (selectedGame != 5 && loggedIn);
        }

        static Player userVerify(List<Player> players, Player pT)
        {
            Player p = new Player();
            foreach (Player checkPlayer in players)
            {
                if (checkPlayer.FName == pT.FName && checkPlayer.LName == pT.LName)
                {
                    p = checkPlayer;
                }
                else
                {
                    p = pT;
                }
            }
            return p;
        }

        // method to run one round of RPS
        static Round oneRound(Player p1, Player p2, int playerChoiceInt)
        {
            Round currentRound = new Round();
            var rand = new Random((int)DateTime.Now.Millisecond);
            Choice playerChoice = (Choice)playerChoiceInt;
            Choice compChoice = (Choice)rand.Next(1, 4);
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
                    return currentRound;
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
                    return currentRound;
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
                    return currentRound;
                }
                default:
                    return currentRound = new Round();
            }            
        }

        static void userLogin(int selectedGame, Player p1, Player p2, List<Player> players, bool loggedIn, Match match, Round round)
        {
            foreach (Player p in players)
            {
                Console.WriteLine($"{p.FName} {p.LName} {p.PlayerId}");
            }

            string userName = "";
            string[] userNameArray;
            int m1 = 0;
            Console.WriteLine("1) Enter Player Name (Unique name will create new Player)");
            Console.WriteLine("2) Exit");
            Console.Write("Please make a selection:");
            //log in or create a new player - unique information will create new player
            try // error catching on selectedGame
            {
                m1 = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid Entry.");
                m1 = 3;
            }
            switch (m1)
            {
                case 1:
                    {
                        Player pTemp = new Player();
                        do
                        {
                            Console.Write("Please enter First and Last Name separated by ' ': ");
                            userName = Console.ReadLine();
                            userNameArray = userName.Split(' ');
                        } while (userName == "" || userName == null);

                        if (userNameArray.Length == 1)
                        {
                            pTemp.FName = userNameArray[0];
                        }
                        else if (userNameArray.Length > 1)
                        {
                            pTemp.FName = userNameArray[0];
                            pTemp.LName = userNameArray[1];
                        }

                        Console.WriteLine($"{pTemp.FName} {pTemp.LName} {pTemp.PlayerId}");
                        foreach (Player p in players)
                        {
                            Console.WriteLine($"{p.FName} {p.LName} {p.PlayerId}");
                        }

                        p2 = userVerify(players, pTemp);
                        if (p2.PlayerId == pTemp.PlayerId)
                        {
                            players.Add(p2);
                            Console.WriteLine("New player successfully added. Enjoy.");
                            loggedIn = true;
                        }
                        else
                        {
                            Console.WriteLine("User logged in. Welcome back.");
                            loggedIn = true;
                        }
                        gameSelect(selectedGame, p1, p2, players, loggedIn, match, round);
                        break;
                    }
                case 2:
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
        }
    }
    
}