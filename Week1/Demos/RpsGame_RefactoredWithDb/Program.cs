using System;
using System.Collections.Generic;

namespace RpsGame_NoDb
{
    class Program
    {
        static RpsRepositoryLayer gameContext = new RpsRepositoryLayer();

        static void Main(string[] args)
        {
            Console.WriteLine("Rock - Paper - Scissors");

            Player tiePlayer = gameContext.UserVerify(new Player("Tie", "Game"));
            Player p2 = new Player();
            //create Computer player
            Player p1 = gameContext.UserVerify(new Player("Max", "Headroom"));
            gameContext.AddPlayer(p1);
            gameContext.AddPlayer(tiePlayer);

            do
            {
                Console.WriteLine("1) Enter Player Name (Unique name will create new Player)");
                Console.WriteLine("2) Exit");
                Console.Write("Please make a selection:");
                Player pTemp = gameContext.UserLogin();
                p2 = gameContext.UserVerify(pTemp);
                if (p2.PlayerId == pTemp.PlayerId)
                {
                    gameContext.AddPlayer(p2);
                    Console.WriteLine("\nNew player successfully added. Enjoy.");
                    gameContext.SetLogged(true);
                }
                else
                {
                    Console.WriteLine("\nUser logged in. Welcome back.");
                    gameContext.SetLogged(true);
                }
                while (gameContext.CheckLogged())
                {
                    Match match = gameContext.NewMatch(p1, p2, tiePlayer);
                    Console.WriteLine($"\nWelcome, {match.Player2.FName}.\n");
                    Console.WriteLine("  1) 1 Round");
                    Console.WriteLine("  2) Best of 3");
                    Console.WriteLine("  3) Best of 5");
                    Console.WriteLine("  4) Logout");
                    Console.WriteLine("  5. Exit\n");
                    Console.Write("Please choose a game mode: ");
                    int roundsToWin = 0;
                    roundsToWin = gameContext.SelectGame(match);

                    while (gameContext.CheckGameSelected() && !match.MatchWon && (roundsToWin < 4 && roundsToWin > 0))
                    {
                        Round round = new Round();
                        Console.WriteLine($"\nWelcome, {match.Player2.FName}.\n");
                        if (roundsToWin > 1)
                        {
                            Console.WriteLine($"The current score is CPU: {match.GetPlayerWins(match.Player1)} - Player: {match.GetPlayerWins(match.Player2)} - Ties - {match.GetPlayerWins(match.TiePlayer)}\n");
                        }
                        Console.WriteLine("  1) Rock");
                        Console.WriteLine("  2) Paper");
                        Console.WriteLine("  3) Scissors");
                        Console.WriteLine("  4) Exit / Previous Menu\n");
                        Console.Write("Please make a selection: ");
                        round = gameContext.OneRound(match);
                        gameContext.DeclareAWinner(match, round);
                        if ((match.GetPlayerWins(match.Player1) >= roundsToWin || match.GetPlayerWins(match.Player2) >= roundsToWin) && roundsToWin >= 1)
                        {
                            Console.WriteLine($"{match.MatchWinner(roundsToWin).FName} has won the match.");
                            gameContext.SetGameSelected(false);
                        }
                        else if ((match.GetPlayerWins(match.Player1) >= roundsToWin || match.GetPlayerWins(match.Player2) >= roundsToWin) && roundsToWin == 1)
                        {
                            Console.WriteLine($"{match.MatchWinner(roundsToWin).FName} has won the match.");
                            gameContext.SetGameSelected(false);
                        }
                        else if (roundsToWin > 3 || roundsToWin < 0)
                        {
                            gameContext.SetGameSelected(false);
                        }                
                    }
                }
            } while (!gameContext.CheckLogged());
        }
    }
}