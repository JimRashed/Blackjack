using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rashed_Blackjack
{
    public struct RoundStats
    {
        int hits;
        int stands;
        int doubleDowns;
        int forfeits;
        int busts;
        public RoundStats(int hits, int stands, int doubleDowns, int forfeits, int busts)
        {
            this.hits = hits;
            this.stands = stands;
            this.doubleDowns = doubleDowns;
            this.forfeits = forfeits;
            this.busts = busts;
        }
    }
}
