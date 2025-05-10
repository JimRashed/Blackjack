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
                    }
                }
            }
            //If entry doesn't already exist, create a new one
            if (!entryExists)
            {
                LeaderboardEntry newEntry = new LeaderboardEntry(name);
                newEntry.Modify(outcome);
                entries.Add(newEntry);
            }

            //Sort leaderboard to ensure valid order if there's more than 1 entry
            if (entries.Count > 1)
            {
                Sort();
            }
            
        }
        //Selection sort based on score
        public void Sort()
        {
            int highestIndex;
            LeaderboardEntry tempStorage;
            for (int outerLoop = 0; outerLoop < entries.Count; outerLoop++)
            {
                highestIndex = outerLoop;

                for (int innerLoop = outerLoop + 1; innerLoop < entries.Count; innerLoop++)
                {
                    if (entries[innerLoop].Score > entries[highestIndex].Score)
                    {
                        highestIndex = innerLoop;
                    }
                }
                tempStorage = entries[outerLoop];
                entries[outerLoop] = entries[highestIndex];
                entries[highestIndex] = tempStorage;
            }
        }
        public void Clear()
        {
            entries.Clear();
        }
        //Full leaderboard output
        public void ToString()
        {
            Console.WriteLine("----------------LEADERBOARD--------------------");
            Console.WriteLine("NAME|SCORE|WINRATIO|WINS|TIES|LOSSES");
            foreach (LeaderboardEntry entry in entries)
            {
                Console.WriteLine(entry.ToString());
            }
        }
        public static void SaveBoard(string filename)
        {
            StreamWriter sWriter = null; //Declaring sWriter without assigning it causes an error during closing, so sWriter is simply assigned to null
            try
            {
                string file;
                bool quit = false;
                Player currentPlayer = new Player();

                Console.WriteLine("Saving current leaderboard state to file...");
                Console.WriteLine("Please input the name of the file where leaderboard will be saved (Do not enter file extension. Including the file extension voids any save success guarantee.) Enter 'exit' to abort.");
                fileName = GetNonNullString();

                if (fileName == EXIT) //Allows player to exit program early if they changed their mind
                {
                    quit = true;
                }

                if (!quit)
                {


                    file = FILEPATH + fileName + FILEEXTENSION;

                    sWriter = new StreamWriter(file);

                    for (int LBIndex = 0; LBIndex < leaderboard.Count; LBIndex++) //Saves entire leaderboard to file line by line
                    {
                        currentPlayer = leaderboard[LBIndex];
                        sWriter.WriteLine($"{currentPlayer.name},{currentPlayer.score},{Convert.ToString(currentPlayer.finishTime)},{currentPlayer.gamesWon},{currentPlayer.age},{currentPlayer.favoriteAnimal},");
                    }

                    Console.WriteLine($"Leaderboard state saved under file {fileName}.csv");

                    Console.ReadKey();
                }


            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured during file saving. Please verify that your input was correct and contained no illegal characters (/, <, !, \", etc.");
            }
            finally
            {
                if (sWriter != null) //Ensures function never ends with StreamWriter still open
                {
                    sWriter.Close();
                }
                DisplayLeaderboard(leaderboard); //Refreshes leaderboard
            }
        }

    }
}
