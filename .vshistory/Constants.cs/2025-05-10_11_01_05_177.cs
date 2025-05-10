using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rashed_Blackjack
{
    public static class Constants
    {
        public const int MINPLAYERCOUNT = 1;
        public const int MAXPLAYERCOUNT = 4;
        public const int MINPLAYERMOVE = 1;
        public const int MAXPLAYERMOVE = 4;
        public const int BLACKJACK = 21;
        public const int INITIALCARDAMOUNT = 2;
        public const double MAXBET = 100000;
        public const double MINBET = 50;
        public const int STAND = 2;
        public const int SECONDCARD = 1;
        public const int DEALERMUSTHITLIMIT = 17;
        public const int BLACKJACKCARDAMOUNT = 2;
        public const double WINPAYOUTRATIO = 2;
        public const double BLACKJACKPAYOUTRATIO = 2.5;
        public const string EXIT = "exit";
        public const string BOARDFILEPATH = "../../../SavedBoards/";
        public const string GAMEFILEPATH = "../../../SavedGames/";
        public const string FILEXTENSION = ".csv";
    }
}
