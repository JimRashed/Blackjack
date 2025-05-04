using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rashed_Blackjack
{
    public struct PlayerStats
    {
        public int wins;
        public int losses;
        public int ties;

        public double winRatio;
        public PlayerStats()
        {
            wins = 0;
            losses = 0;
            ties = 0;
            winRatio = (wins + losses + ties == 0) ? 0 : (wins / wins + losses + ties);
        }
    }
    
}
