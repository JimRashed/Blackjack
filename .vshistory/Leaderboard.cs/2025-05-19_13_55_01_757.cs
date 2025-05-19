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
        /* AddOrModifyEntry(string name, Outcomes outcome)
         *************************************************
         * Purpose: Adds a new entry to the leaderboard or modifies an existing entry.
         *************************************************
         * @Algorithm:
         * 1. Determine if a leaderboard entry for the given name exists.
         * 2. If it exists, update the entry's statistics based on the outcome.
         * 3. If not, create a new entry with the name and the outcome, then add it to the leaderboard.
         * 4. If the leaderboard contains more than one entry, sort it by score.
         *************************************************
         * @Param
         * string name: The name of the player.
         * enum outcome: The outcome of the round.
         *************************************************
         * @Exceptions
         * None
         *************************************************
         * @Returns
         * Returns nothing.
         *************************************************
         * @Pseudocode:
         * entryExists = false
             * if entries.Count > 0
                 * for each entry in entries
                  * if entry.Name == name
                     * entryExists = true
                     * entry.Modify(outcome)
         * if !entryExists
            * newEntry = new LeaderboardEntry(name)
            * newEntry.Modify(outcome)
            * entries.Add(newEntry)
         * if entries.Count > 1
            * Sort()
         *************************************************
         */
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
        /* Sort()
        *************************************************
        * Purpose: Sorts the leaderboard entries by score in descending order.
        *************************************************
        * @Algorithm:
        * 1. Iterate through the list of entries.
        * 2. In each iteration, find the entry with the highest score in the unsorted portion of the list.
        * 3. Swap the current entry with the highest-scoring entry found.
        * 4. Repeat this process until the entire list is sorted.
        *************************************************
        * @Param
        * Receives no parameters.
        *************************************************
        * @Exceptions
        * None
        *************************************************
        * @Returns
        * Returns nothing.
        *************************************************
        * @Pseudocode:
        * for outerLoop from 0 to entries.Count - 2
            * highestIndex = outerLoop
            * for innerLoop from outerLoop + 1 to entries.Count - 1
                  * if entries[innerLoop].Score > entries[highestIndex].Score
                  * highestIndex = innerLoop
        * swap entries[outerLoop] with entries[highestIndex]
        *************************************************
        */
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
        /* Clear()
        *************************************************
        * Purpose: Removes all entries from the leaderboard.
        *************************************************
        * @Algorithm:
        *Remove all entries from the list of leaderboard entries
        *************************************************
        * @Param
        * Receives no parameters.
        *************************************************
        * @Exceptions
        * None
        *************************************************
        * @Returns
        * Returns nothing.
        *************************************************
        * @Pseudocode:
        * entries.Clear() 
        *************************************************
        */
        public void Clear()
        {
            entries.Clear();
        }
        //Full leaderboard output
        /* PrintLeaderboard()
         *************************************************
         * Purpose: Prints the leaderboard in a formatted manner
         *************************************************
         * @Algorithm:
         * 1. Display a header for the leaderboard.
         * 2. Display the column headers.
         * 3. Scroll through each entry in the leaderboard and output its details.
         *************************************************
         * @Param
         * Receives no parameters.
         *************************************************
         * @Exceptions
         * None
         *************************************************
         * @Returns
         * Returns nothing.
         *************************************************
         * @Pseudocode:
         * Output leaderboard header
         * Output table legend
         * for each leaderboardentry entry in entries
         * Output entry.ToString()
         *************************************************
         */
        public void PrintLeaderboard()
        {
            Console.WriteLine("----------------LEADERBOARD--------------------");
            Console.WriteLine(" NAME | SCORE | WINRATIO | WINS | TIES | LOSSES");
            foreach (LeaderboardEntry entry in entries)
            {
                Console.WriteLine(entry.ToString());
            }
        }
        /* SaveBoard(string fileName)
        *************************************************
        * Purpose: Saves the leaderboard data to a file.
        *************************************************
        * @Algorithm:
        * 1. Iterate through each leaderboard entry 
        * 2. Output entry data in a formatted manner 
        *************************************************
        * @Param
        * fileName: The name of the file to save to.
        *************************************************
        * @Exceptions
        * None; all exceptions handled locally.
        *************************************************
        * @Returns
        * Returns nothing.
        *************************************************
        * @Pseudocode:
        * filePath = Constants.BOARDFILEPATH + fileName + Constants.FILEEXTENSION
        * try
            * sWriter = new StreamWriter(filePath)
            * for LBIndex from 0 to entries.Count - 1
            * currentEntry = entries[LBIndex]
            * sWriter.WriteLine($"{currentEntry.Name},{currentEntry.Losses},{currentEntry.Wins},{currentEntry.Ties},{currentEntry.Score}")
          catch exception e
            Output error message
        * finally
            * if sWriter != null
            * sWriter.Close()
        *************************************************
        */
        public void SaveBoard(string fileName)
        {
            StreamWriter sWriter = null; //Declaring sWriter without assigning it causes an error during closing, so sWriter is simply assigned to null
            try
            {
                string file;

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
                string line; ;
                sReader = new StreamReader(fileToLoad);
                while ((line = sReader.ReadLine()) != null)
                {
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
                if (sReader != null)
                {
                    sReader.Close();
                }
            }
        }

    }

}

