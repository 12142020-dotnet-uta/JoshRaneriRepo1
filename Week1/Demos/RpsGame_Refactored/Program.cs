using System;
using System.Collections.Generic;

namespace RpsGame_NoDb
{
    class Program
    {
        static bool loggedIn = false;
        static bool gameSelected = false;
        static List<Player> players = new List<Player>();
        static List<Round> rounds = new List<Round>();
        static List<Match> matches = new List<Match>();

        static void Main(string[] args)
        {
            Console.WriteLine("Rock - Paper - Scissors");

            Player tiePlayer = new Player("Tie", "Game");
            Player p2 = new Player();
            //create Computer player
            Player p1 = new Player("Max", "Headroom");
            players.Add(p1);

            do
            {
                Console.WriteLine("1) Enter Player Name (Unique name will create new Player)");
                Console.WriteLine("2) Exit");
                Console.Write("Please make a selection:");
                Player pTemp = UserLogin();
                p2 = userVerify(pTemp);
                if (p2.PlayerId == pTemp.PlayerId)
                {
                    players.Add(p2);
                    Console.WriteLine("\nNew player successfully added. Enjoy.");
                    loggedIn = true;
                }
                else
                {
                    Console.WriteLine("\nUser logged in. Welcome back.");
                    loggedIn = true;
                }
                while (loggedIn)
                {
                    Match match = new Match();
                    match.Player1 = p1;
                    match.Player2 = p2;
                    match.TiePlayer = tiePlayer;
                    Console.WriteLine($"\nWelcome, {match.Player2.FName}.\n");
                    Console.WriteLine("1) 1 Round");
                    Console.WriteLine("2) Best of 3");
                    Console.WriteLine("3) Best of 5");
                    Console.WriteLine("4) Logout");
                    Console.WriteLine("5. Exit");
                    Console.Write("Please choose a game mode: ");
                    int roundsToWin = selectGame(match);

                    while (gameSelected && !match.MatchWon && roundsToWin < 4 && roundsToWin > 0)
                    {
                        Round round = new Round();
                        Console.WriteLine($"\nWelcome, {match.Player2.FName}.\n");
                        if (roundsToWin > 1)
                        {
                            Console.WriteLine($"The current score is CPU: {match.GetPlayerWins(match.Player1)} - Player: {match.GetPlayerWins(match.Player2)} - Ties - {match.GetPlayerWins(new Player())}\n");
                        }
                        Console.WriteLine("1) Rock");
                        Console.WriteLine("2) Paper");
                        Console.WriteLine("3) Scissors");
                        Console.WriteLine("4) Exit / Previous Menu");
                        Console.Write("Please make a selection: ");
                        round = OneRound(match);
                        DeclareAWinner(match, round);
                        if (match.GetPlayerWins(match.Player1) >= roundsToWin || match.GetPlayerWins(match.Player2) >= roundsToWin && roundsToWin > 1)
                        {
                            Console.WriteLine($"{match.MatchWinner(roundsToWin).FName} has won the match.");
                            gameSelected = false;
                        }
                    }
                }
            } while (!loggedIn);
        }

        static int selectGame(Match match)
        {
            int selectedGame = CheckInt();

            switch (selectedGame)
            {
                case 1:
                    {
                        gameSelected = true;
                        return 1;
                    }
                case 2:
                    {
                        gameSelected = true;
                        return 2;
                    }
                case 3:
                    {
                        gameSelected = true;
                        return 3;
                    }
                case 4:
                    {
                        gameSelected = false;
                        loggedIn = false;
                        return 4;
                    }
                case 5:
                    Environment.Exit(0);
                    return 5;
                default:
                    {
                        // catches any invalid selectedGame
                        Console.WriteLine("Invalid Game Selection. Please enter a valid choice.\n");
                        return -1;
                    }
            }

        }

        static Player userVerify(Player pT)
        {
            Player p = new Player();
            foreach (Player checkPlayer in players)
            {
                if (checkPlayer.FName == pT.FName && checkPlayer.LName == pT.LName)
                {
                    p = checkPlayer;
                    break;
                }
                else
                {
                    p = pT;
                }
            }
            return p;
        }

        // method to run one round of RPS
        static Round OneRound(Match match)
        {
            Round currentRound = new Round();
            var rand = new Random((int)DateTime.Now.Millisecond);
            Choice playerChoice = (Choice)CheckInt();
            Choice compChoice = (Choice)rand.Next(1, 4);
            currentRound.Player2Choice = playerChoice;
            currentRound.Player1Choice = compChoice;

            switch ((int)playerChoice)
            {
                case 1:
                    {
                        if ((int)compChoice == 1)
                        {
                            currentRound.WinningPlayer = match.TiePlayer;
                        }
                        else if ((int)compChoice == 2)
                        {
                            currentRound.WinningPlayer = match.Player1;
                        }
                        else if ((int)compChoice == 3)
                        {
                            currentRound.WinningPlayer = match.Player2;
                        }
                        return currentRound;
                    }
                case 2:
                    {
                        if ((int)compChoice == 1)
                        {
                            currentRound.WinningPlayer = match.Player2;
                        }
                        else if ((int)compChoice == 2)
                        {
                            currentRound.WinningPlayer = match.TiePlayer;
                        }
                        else if ((int)compChoice == 3)
                        {
                            currentRound.WinningPlayer = match.Player1;
                        }
                        return currentRound;
                    }
                case 3:
                    {
                        if ((int)compChoice == 1)
                        {
                            currentRound.WinningPlayer = match.Player1;
                        }
                        else if ((int)compChoice == 2)
                        {
                            currentRound.WinningPlayer = match.Player2;
                        }
                        else if ((int)compChoice == 3)
                        {
                            currentRound.WinningPlayer = match.TiePlayer;
                        }
                        return currentRound;
                    }
                default:
                    {
                        currentRound.WinningPlayer = match.TiePlayer;
                        gameSelected = false;
                        return currentRound;
                    }
            }
        }

        static Player UserLogin()
        {
            string userName = "";
            string[] userNameArray;
            int loginSelection = CheckInt();
            do
            {
                if (loginSelection == 1)
                {
                    Player pTemp = new Player();
                    do
                    {
                        Console.Write("\nPlease enter First and Last Name separated by ' ': ");
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
                    return pTemp;
                }
                else if (loginSelection == 2)
                {
                    Environment.Exit(0);
                    return null;
                }
            } while (loginSelection != 1);
            return null;
        }
        static int CheckInt()
        {
            int selection = 0;
            bool selected = int.TryParse(Console.ReadLine(), out selection);
            if (selected)
            {
                return selection;
            }
            else
            {
                return -1;
            }
        }

        static void DeclareAWinner(Match match, Round round)
        {
            match.RoundWinner(round.WinningPlayer);
            rounds.Add(round);
            matches.Add(match);
            if (round.WinningPlayer == match.Player2)
            {
                Console.WriteLine("\n" + round.Player2Choice.ToString() + " beats " + round.Player1Choice.ToString() + ". You win!");
            }
            else if (round.WinningPlayer == match.Player1)
            {
                Console.WriteLine("\n" + round.Player1Choice.ToString() + " beats " + round.Player2Choice.ToString() + ". You lose.");
            }
            else if (round.WinningPlayer == match.TiePlayer)
            {
                if (match.MatchWon)
                {
                    Console.WriteLine("\nBoth players chose " + round.Player2Choice.ToString() + ". Game is a tie.");
                }
            }
        }
    }
}