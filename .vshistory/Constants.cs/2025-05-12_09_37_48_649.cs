using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rashed_Blackjack
{
    //A class designed to hold constant objects for ease of access and readability
    public static class Constants
    {
        //The minimum allowed number of players in the game.
        public const int MINPLAYERCOUNT = 1; //This is off for testing purposes. Change it to 2 later.
        //The maximum allowed number of players in the game.
        public const int MAXPLAYERCOUNT = 4;
        //The minimum legal move of a player in a menu
        public const int MINPLAYERMOVE = 1;
        //The maximum legal move of a player in a menu
        public const int MAXPLAYERMOVE = 4;
        //The value of a hand that counts as blackjack
        public const int BLACKJACK = 21;
        //The initial amount of cards to be distributed to each player.
        public const int INITIALCARDAMOUNT = 2;
        //The maximum allowed bet for a player.
        public const double MAXBET = 100000; 
        //The minimum allowed bet for a player.
        public const double MINBET = 50;
        //The menu number choice corresponding to the "stand" option.
        public const int STAND = 2;
        //The array of index of the second card in the hand
        public const int SECONDCARD = 1;
        //The limit until which the dealer must hit.
        public const int DEALERMUSTHITLIMIT = 17;
        //The number of cards needed for a blackjack
        public const int BLACKJACKCARDAMOUNT = 2;
        //The ratio of a win payout (i.e. X times your money back)
        public const double WINPAYOUTRATIO = 2;
        //The ratio of a blackjack payout (i.e. X times your money back)
        public const double BLACKJACKPAYOUTRATIO = 2.5;
        //Self-explanatory.
        public const string EXIT = "exit";
        //The folder in which leaderboard save file are stored
        public const string BOARDFILEPATH = "../../../SavedBoards/";
        //The folder in which game save files are stored
        public const string GAMEFILEPATH = "../../../SavedGames/";
        //The file extension to use in save files.
        public const string FILEEXTENSION = ".csv";
    }
}
