using System;
using System.ComponentModel.DataAnnotations;

namespace RpsGame_NoDb
{
    public class Player
    {
        public Player(){}
        public Player(string fname = "null", string lname = "null")
        {
            this.FName = fname;
            this.LName = lname;
        }        
        private Guid playerId = Guid.NewGuid();
        [Key]
        public Guid PlayerId { get { return playerId; } set { playerId = value; }}
        private string fName;
        public string FName
        {
            get { return fName; }
            set
            { 
                if (value is string && value.Length < 20 && value.Length > 0)
                  {
                      fName = value;
                  }
                else
                {
                    throw new Exception("Value is bad.");
                }
            }
        }
        private string lName;
        public string LName
        {
            get { return lName; }
            set
            { 
                if (value is string && value.Length < 20 && value.Length > 0)
                  {
                      lName = value;
                  }
                else
                {
                    throw new Exception("Value is bad.");
                }
            }
        }
        private int numWins;
        private int numLosses;        
        public void AddWin()
        {
            numWins++;
        }
        public void AddLoss()
        {
            numLosses++;
        }
        public int[] GetWinLossRecord()
        {
            int[] winsAndLosses = new int[2];
            winsAndLosses[0] = numWins;
            winsAndLosses[1] = numLosses;
            return winsAndLosses;
        }
    }
}