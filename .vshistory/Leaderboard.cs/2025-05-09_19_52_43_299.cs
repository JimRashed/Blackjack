using System;
using System.Collections.Generic;
using System.Drawing;
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
            //Checks if entry already exists
            if (entries.Count != 0)//Checks if list is null before accessing
            {
                foreach (LeaderboardEntry entry in entries) //Scrolls through list entries
                {
                    if (entry.Name == name)//Checks if name matches
                    {
                        entryExists = true;
                        entry.Modify(outcome); //Updates entry based on outcome
                        entries.Sort();//Sorts leaderboard in case ranking change occurs
                        
                    }
                }
            }
            //If entry doesn't already exist, create a new one
            if (!entryExists)
            {
                LeaderboardEntry newEntry = new LeaderboardEntry(name);
                newEntry.Modify(outcome);
                entries.Add(newEntry);
                entries.Sort();
            }

            //Sort leaderboard to ensure valid order
            Sort(); 
        }
        //Selection sort based on score
        public void Sort()
        {
            int lowestIndex;
            LeaderboardEntry tempStorage;
            for (int outerLoop = 0; outerLoop < entries.Count; outerLoop++)
            {
                lowestIndex = outerLoop;

                for (int innerLoop = outerLoop + 1; innerLoop < entries.Count; innerLoop++)
                {
                    if (entries[innerLoop].Score < entries[lowestIndex].Score)
                    {
                        lowestIndex = innerLoop;
                    }
                }
                tempStorage = entries[outerLoop];
                entries[outerLoop] = entries[lowestIndex];
                entries[lowestIndex] = tempStorage;
            }
        }
        //Full leaderboard output
        public void ToString()
        {
            Console.WriteLine("----------------LEADERBOARD--------------------");
            Console.WriteLine("NAME|SCORE|WINRATIO|LOSSES|TIES|WINS");
            foreach (LeaderboardEntry entry in entries)
            {
                Console.WriteLine(entry.ToString());
            }
        }

    }
}
