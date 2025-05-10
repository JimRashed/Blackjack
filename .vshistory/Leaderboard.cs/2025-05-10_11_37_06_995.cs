using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO.Enumeration;
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
        public void SaveBoard(string fileName)
        {
            StreamWriter sWriter = null; //Declaring sWriter without assigning it causes an error during closing, so sWriter is simply assigned to null
            try
            {
                string file;
                bool quit = false;

                //filename to save under is received from the GameState saving method and already validated
                file = Constants.BOARDFILEPATH + fileName + Constants.FILEEXTENSION; //Creates a valid file path by concatenating its parts
                sWriter = new StreamWriter(file);
                for (int LBIndex = 0; LBIndex < entries.Count; LBIndex++) //Saves entire leaderboard to file line by line
                {
                    LeaderboardEntry currentEntry = entries[LBIndex];
                    sWriter.WriteLine($"{currentEntry.Name},{currentEntry.Losses},{currentEntry.Wins},{currentEntry.Ties},{currentEntry.Score}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured during file saving.");
            }
            finally
            {
                if (sWriter != null) //Ensures function never ends with StreamWriter still open
                {
                    sWriter.Close();
                }
            }
        }
        public void LoadBoard(string fileName)
        {

            string fileToLoad;
            string[] playerInfo;
            //It is important to note that name prompting and file verification will NOT happen here. That's GameState's job. When calling GameState's Save method,
            //2 files will be created with the same name, but in different folders. One for the game, one for the leaderboard. So, there's no need to check if a leaderboard file with a certain name exists,
            //as the name is already checked in GameState

            fileToLoad = Constants.BOARDFILEPATH + fileName + Constants.FILEEXTENSION;

            //Clear the old leaderboard before loading up the new one
            entries.Clear();

            //Create all players and place them in list from file info
            StreamReader sReader = null;
            try
            {
                string line = "Placeholder";
                sReader = new StreamReader(fileToLoad);
                while (line != null)
                {
                    line = sReader.ReadLine();
                    playerInfo = line.Split(',');
                    LeaderboardEntry tempEntry = new LeaderboardEntry(playerInfo[0], int.Parse(playerInfo[1]), int.Parse(playerInfo[2]), int.Parse(playerInfo[3]), int.Parse(playerInfo[4]));
                    entries.Add(tempEntry);//No need to call AddOrModify entry here, as the board is guaranteed to be empty at this point.
                }
                Sort();
            }

            catch (Exception e)
            {

            }

            finally
            {
                sReader.Close();
            }
        }

    }

}

