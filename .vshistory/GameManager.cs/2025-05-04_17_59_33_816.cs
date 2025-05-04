using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rashed_Blackjack
{
    public class GameManager
    {
        private GameState game;
        private bool gameActive;

        public void Start()
        {
            PrintMenu();
        }

        private void PrintMenu()
        {
            Utility.AnimateWrite("Please choose an option from the menu below:");
            Console.WriteLine("");
            Console.WriteLine("1 - Begin a new round");
            Console.WriteLine("2 - Check the leaderboard");
            Console.WriteLine("3 - Load a game");
            Console.WriteLine("4 - Exit Blackjack");
        }

        
    }
}
