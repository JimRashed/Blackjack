using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rashed_Blackjack
{
    public class LeaderboardEntry
    {
        private string _name;
        private int _losses;
        private int _wins;
        private int _ties;
        private int _score;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        public int Losses
        {
            get
            {
                return _losses;
            }
            set
            {
                _losses = value;
            }
        }
        public int Wins
        {
            get
            {
                return _wins;
            }
            set
            {
                _wins = value;
            }
        }
        public int Ties
        {
            get
            {
                return _ties;
            }
            set
            {
                _ties = value;
            }
        }
    }
}
