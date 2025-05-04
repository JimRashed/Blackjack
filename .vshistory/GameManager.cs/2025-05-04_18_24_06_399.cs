using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Rashed_Blackjack
{
    public class GameManager
    {
        const int MINMENUCHOICE = 1;
        const int MAXMENUCHOICE = 4;
        const int MINPLAYERNUMBER = 2;
        const int MAXPLAYERNUMBER = 4;
        private GameState game;
        private bool gameActive;
        

        public void Start()
        {
            MainMenu();
            Utility.AnimateWrite("Thank you very much for playing!");
            Console.ReadKey();
        }

        private void MainMenu()
        {
            bool quit = false;
            do
            {
                Utility.AnimateWrite("Please choose an option from the menu below:");
                Console.WriteLine("");
                Console.WriteLine("1 - Begin a new round");
                Console.WriteLine("2 - Check the leaderboard");
                Console.WriteLine("3 - Load a game");
                Console.WriteLine("4 - Exit Blackjack");

                int userChoice = Utility.GetIntInRange(MINMENUCHOICE, MAXMENUCHOICE);

                switch (userChoice)
                {
                    case 1:
                        game = new GameState();
                        gameActive = true;
                        GameSetup();
                        break;
                    case 2:
                        //Implement leaderboard display
                        break;
                    case 3:
                        //Implement loading
                        break;
                    case 4:
                        quit = true;
                        break;

                }
            } while (!quit);
           
        }

        private void GameSetup()
        {
            Player tempPlayer;
            int playerNumber;
            string name;
            double balance;
            Utility.AnimateWrite("How many players would you like to include in the game? (2-4)");
            playerNumber = Utility.GetIntInRange(MINPLAYERNUMBER, MAXPLAYERNUMBER);
            for (int currentPlayer = 0; currentPlayer< playerNumber; currentPlayer++)
            {
                tempPlayer = new Player("John doe", 0);
                Utility.AnimateWrite($"What will be the name of player {currentPlayer + 1}?");
                name = Utility.GetNonNullString();
                Utility.AnimateWrite($"What will be {name}'s starting balance?");
                balance = Utility.GetDoubleInRange(0, double.MaxValue);

                


            }


        }
        private void GamePlay()
        {
            bool gameOver = false;
        }

        
    }
}
