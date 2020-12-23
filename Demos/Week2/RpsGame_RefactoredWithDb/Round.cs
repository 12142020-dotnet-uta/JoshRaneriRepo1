using System;
using System.ComponentModel.DataAnnotations;

namespace RpsGame_NoDb
{
    public class Round
    {
        
        private Guid roundId = Guid.NewGuid();
        [Key]
        public Guid RoundId { get { return roundId; } set { RoundId = value; }}
        public Choice Player1Choice { get; set; } //always computer by default
        public Choice Player2Choice { get; set; } //always user
        public Player WinningPlayer { get; set; }
 
    }
}