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
             

                //filename to save under is received from the GameState saving method

                if (fileName == Constants.EXIT) //Allows player to exit program early if they changed their mind
                {
                    quit = true;
                }

                if (!quit)
                {


                    file = Constants.BOARDFILEPATH + fileName + Constants.FILEEXTENSION; //Creates a valid file path by concatenating its parts

                    sWriter = new StreamWriter(file);

                    for (int LBIndex = 0; LBIndex < entries.Count; LBIndex++) //Saves entire leaderboard to file line by line
                    {
                        LeaderboardEntry currentEntry = entries[LBIndex];
                        sWriter.WriteLine($"{currentEntry.Name},{currentEntry.Losses},{currentEntry.Wins},{currentEntry.Ties},{currentEntry.Score}");
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
            }
        }
        public void LoadBoard(string fileName)
        {

            string fileToLoad;
            bool fileExists = false;
            bool quit = false;
            string[] playerInfo;
            //It is important to note that name prompting and file verification will NOT happen here. That's GameState's job. When calling GameState's Save method,
            //2 files will be created with the same name, but in different folders. One for the game, one for the leaderboard. So, there's no need to check if a leaderboard file with a certain name exists,
            //as the name is already checked in GameState
            do //Guarantees an existing leaderboard file
            {
             
                quit = fileName == Constants.EXIT;


                fileToLoad = Constants.BOARDFILEPATH + fileName + Constants.FILEEXTENSION; //This line has to remain outside of the if statement for the streamreader to work.



                if (!quit)
                {

                    fileExists = DoesFileExist(fileToLoad);

                    if (!fileExists)
                    {
                        Console.WriteLine($"Error: No files in saved leaderboards are named {fileName}. Please try again.");
                    }
                }
                else
                {
                    break;
                }
            } while (!fileExists);

            if (!quit)
            {
                //Now that we have a guaranteed valid save file exists, we can delete the current leaderboard state before loading up the new one.
                ClearLeaderboard(leaderboard);

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
                        CreatePlayer(leaderboard, playerInfo);
                    }
                }

                catch (Exception e)
                {

                }

                finally
                {
                    sReader.Close();
                }
            }

            //Display updated leaderboard
            DisplayLeaderboard(leaderboard)
        }

    }
}
