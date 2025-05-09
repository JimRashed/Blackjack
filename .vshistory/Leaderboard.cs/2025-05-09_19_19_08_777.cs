using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rashed_Blackjack
{
    public class Leaderboard
    {
        private List<LeaderboardEntry> entries;

        public Leaderboard()
        {
            entries = new List<LeaderboardEntry>();
        }
        public void AddOrModifyEntry(string name, Outcomes outcome)
        {
            bool entryExists = false;
            if (entries.Count != 0)//Checks if list is null before accessing
            {
                foreach (LeaderboardEntry entry in entries) //Scrolls through list entries
                {
                    if (entry.Name == name)//Checks if name matches
                    {
                        entryExists = true;
                        entry.Modify(outcome); //Updates entry based on outcome
                        
                    }
                }
            }

            if (!entryExists)
            {
                LeaderboardEntry newEntry = new LeaderboardEntry()
            }
            
            



        }

    }
}
