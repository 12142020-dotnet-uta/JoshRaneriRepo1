using System;

namespace RpsGame_NoDb
{
    class Round
    {
        private Guid roundId = Guid.NewGuid();
        public Guid RoundId { get { return roundId; } }
        public Choice Player1Choice { get; set; } //always computer by default
        public Choice Player2Choice { get; set; } //always user
        public Player WinningPlayer { get; set; }
 
    }
}