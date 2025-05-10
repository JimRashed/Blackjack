using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Rashed_Blackjack
{
    public class GameState
    {
        //Fields
        public List<Player> players;
        public Player dealer;
        public Deck cardDeck;
        public Leaderboard leaderboard;
        public int round;
        //Constructor
        public GameState()
        {
            players = new List<Player>();
            dealer = new Player("Dealer", 1000, true); //The 1000 is a placeholder. The Dealer never changes balance.
            cardDeck = new Deck();
            leaderboard = new Leaderboard();
        }

        //Methods for file loading and saving will happen in this class
        public void Save()
        {
            string fileName;
            string file;
            bool quit = false;


            Console.WriteLine("Saving current leaderboard state to file...");
            Console.WriteLine("Please input the name of the file where leaderboard will be saved (Do not enter file extension. Including the file extension voids any save success guarantee.) Enter 'exit' to abort.");
            fileName = Utility.GetNonNullString();


            if (fileName == Constants.EXIT) //Allows player to exit program early if they changed their mind
            {
                quit = true;
            }
            else
            {
                file = Constants.GAMEFILEPATH + fileName + Constants.FILEEXTENSION;
                
            }

        }



    }
}
