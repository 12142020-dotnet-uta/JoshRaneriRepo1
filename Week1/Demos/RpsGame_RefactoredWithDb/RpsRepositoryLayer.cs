using System;
using System.Collections.Generic;
using System.Linq;

namespace RpsGame_NoDb
{
    public class RpsRepositoryLayer
    {
        // List<Player> players = new List<Player>();
        // List<Round> rounds = new List<Round>();
        // List<Match> matches = new List<Match>();
        RpsDbContext DbContext = new RpsDbContext();
        bool loggedIn = false;
        bool gameSelected = false;

        public void AddPlayer(Player p)
        {
            if (p == UserVerify(p))
            {
                DbContext.players.Add(p);
            }
            DbContext.SaveChanges();
        }
        public bool CheckLogged()
        {
            return loggedIn;
        }
        public void SetLogged(bool logStatus)
        {
            loggedIn = logStatus;
        }
        public bool CheckGameSelected()
        {
            return gameSelected;
        }
        public void SetGameSelected(bool gameStatus)
        {
            gameSelected = gameStatus;
        }
        public List<Match> GetMatches()
        {
            return DbContext.matches.ToList();
        }
        public List<Player> GetPlayers()
        {
            return DbContext.players.ToList();
        }
        public List<Round> GetRounds()
        {
            return DbContext.rounds.ToList();
        }
        public Player UserLogin()
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
        public Player UserVerify(Player pT)
        {
            Player p = new Player();
            // foreach (Player checkPlayer in GetPlayers())
            // {
            //     if ((checkPlayer.FName == pT.FName && checkPlayer.LName == pT.LName) || (checkPlayer.PlayerId == pT.PlayerId))
            //     {
            //         p = checkPlayer;
            //     }
            //     else
            //     {
            //         p = pT;
            //     }
            // }
            Player p1 = DbContext.players.Where(x => x.PlayerId == pT.PlayerId).FirstOrDefault();
            if (p1 == null)
            {
                DbContext.players.Add(pT);
                DbContext.SaveChanges();
            }
            return p;
        }
        public Match MatchVerify(Match mT)
        {
            Match m = new Match();
            if (GetMatches() != null)
            {
                foreach (Match checkMatch in GetMatches())
                {
                    if (checkMatch.MatchId == mT.MatchId)
                    {
                        m = checkMatch;
                        break;
                    }
                    else
                    {
                        m = mT;
                    }
                }
            }
            return m;
        }
        public Round RoundVerify(Round rT)
        {
            Round r = new Round();
            foreach (Round checkRound in GetRounds())
            {
                if (checkRound.RoundId == rT.RoundId)
                {
                    r = checkRound;
                    break;
                }
                else
                {
                    r = rT;
                }
            }
            return r;
        }
        public int CheckInt()
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
        public int SelectGame(Match match)
        {
            int selectedGame = CheckInt();

            switch (selectedGame)
            {
                case 1:
                    {
                        gameSelected = true;
                        return selectedGame;
                    }
                case 2:
                    {
                        gameSelected = true;
                        return selectedGame;
                    }
                case 3:
                    {
                        gameSelected = true;
                        return selectedGame;
                    }
                case 4:
                    {
                        gameSelected = false;
                        loggedIn = false;
                        return selectedGame;
                    }
                case 5:
                    Environment.Exit(0);
                    return 5;
                default:
                    {
                        // catches any invalid selectedGame
                        Console.WriteLine("Invalid Game Selection. Please enter a valid choice.\n");
                        gameSelected = false;
                        return -1;
                    }
            }

        }
        public Round OneRound(Match match)
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
        public Match NewMatch(Player p1, Player p2, Player tiePlayer)
        {
            Match match = new Match();
            match.Player1 = p1;
            match.Player2 = p2;
            match.TiePlayer = tiePlayer;
            return match;
        }
        public void DeclareAWinner(Match match, Round round)
        {
            match.RoundWinner(round.WinningPlayer);
            if (round.WinningPlayer == match.Player2)
            {
                Console.WriteLine("\n" + round.Player2Choice.ToString() + " beats " + round.Player1Choice.ToString() + ". You win!");
                match.Player2.AddWin();
                match.Player1.AddLoss();
            }
            else if (round.WinningPlayer == match.Player1)
            {
                Console.WriteLine("\n" + round.Player1Choice.ToString() + " beats " + round.Player2Choice.ToString() + ". You lose.");
                match.Player1.AddWin();
                match.Player2.AddLoss();
            }
            else if (round.WinningPlayer == match.TiePlayer)
            {
                if (!match.MatchWon)
                {
                    Console.WriteLine("\nBoth players chose " + round.Player2Choice.ToString() + ". Game is a tie.");
                }
            }
            Console.WriteLine("1");
            DbContext.rounds.Add(round);
            match.Rounds.Add(round);
            Console.WriteLine("2");            
            Match m1 = DbContext.matches.Where(y => y.MatchId == match.MatchId).FirstOrDefault();
            if (m1 == null)
            {
                DbContext.matches.Add(match);
            }
            Console.WriteLine("3");
            DbContext.SaveChanges();
            Console.WriteLine("4");
        }
        public bool AddCompletedMatch(Match match)
        {
            Match m1 = DbContext.matches.Where(x => x.MatchId == match.MatchId).FirstOrDefault();
            if (m1 == null)
            {
                DbContext.matches.Add(match);
                DbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}