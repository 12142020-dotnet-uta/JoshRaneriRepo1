using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RpsGame_NoDb
{
    public class Match
    {
        
        private Guid matchId = Guid.NewGuid();
        [Key]
        public Guid MatchId { get { return MatchId; } set { MatchId = value; }}
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public Player TiePlayer { get; set; }
        public bool MatchWon { get; set; }
        public List<Round> Rounds = new List<Round>();
        public int p1RoundWins { get; set; } = 0;
        public int p2RoundWins { get; set; } = 0;
        public int ties { get; set; } = 0;

        /// <summary>
        /// Takes optional Player object and increments wins or losses
        /// </summary>
        /// <param name="p">Player object</param>
        public void RoundWinner(Player p)
        {
            if (p.PlayerId == Player1.PlayerId){
                p1RoundWins++;
            }
            else if (p.PlayerId == Player2.PlayerId){
                p2RoundWins++;
            }
            else{
                ties++;
            }
        }

        public Player MatchWinner(int roundsToWin)
        {
            if(p1RoundWins >= roundsToWin && roundsToWin > 1)
            {
                MatchWon = true;
                return Player1;
            }
            else if(p2RoundWins >= roundsToWin && roundsToWin > 1)
            {
                MatchWon = true;
                return Player2;
            }
            else
            {
                MatchWon = false;
                return TiePlayer;
            }
        }

        public int GetPlayerWins(Player p)
        {
            if (p.PlayerId == Player1.PlayerId){
                return p1RoundWins;
            }
            else if (p.PlayerId == Player2.PlayerId){
                return p2RoundWins;
            }
            else{
                return ties;
            }
        }
    }
}