using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Rashed_Blackjack
{
    public class GameManager
    {
        const int MINMENUCHOICE = 1;
        const int MAXMENUCHOICE = 4;
        const int MINPLAYERCOUNT = 2;
        const int MAXPLAYERCOUNT = 4;
        private GameState game;
        private bool gameActive;
        int playerCount;


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
                        Setup();
                        GamePlay();
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

        private void Setup()
        {
            Player tempPlayer;
            string name;
            double balance;
            Utility.AnimateWrite("How many players would you like to include in the game? (2-4)"); 
            playerCount = Utility.GetIntInRange(MINPLAYERCOUNT, MAXPLAYERCOUNT);
            for (int currentPlayer = 0; currentPlayer< playerCount; currentPlayer++)
            {
                
                Utility.AnimateWrite($"What will be the name of player {currentPlayer + 1}?");
                name = Utility.GetNonNullString();
                Utility.AnimateWrite($"What will be {name}'s starting balance? (cannot exceed 100000)");
                balance = Utility.GetDoubleInRange(0, 100000);
                tempPlayer = new Player(name, balance);
                game.players.Add(tempPlayer);
                Console.Clear();
            }

            GetPlayerBets();
            InitialDeal();
            DisplayDealerAndPlayers();
            PlayPlayerTurns();


        }//Populates the list of players in GameState, deals 2 cards to everyone

        private void InitialDeal()
        {
            //2 cards dealt to the dealer
            for (int currentCard = 0; currentCard < Constants.INITIALCARDAMOUNT; currentCard++)
            {
                game.dealer.Hand.AddCard(game.cardDeck.Draw());//Adds card to dealer's hand
            }
            game.dealer.Hand.Sort(); //Sorts the dealer's hand

            //Ensure the dealer's second card is hidden
            game.dealer.Hand.hand[game.dealer.Hand.Size - 1].Hidden = true;


            //2 cards dealt to each active player
            for (int currentPlayer = 0; currentPlayer<game.players.Count; currentPlayer++)//Scrolls through all players
            {
                if (game.players[currentPlayer].Playing)
                {
                    for (int currentCard = 0; currentCard < Constants.INITIALCARDAMOUNT; currentCard++) //Scrolls 2 times
                    {
                        game.players[currentPlayer].Hand.AddCard(game.cardDeck.Draw());
                        
                    }
                    game.players[currentPlayer].Hand.Sort();
                }
                
            }
            
        }
        private void GetPlayerBets()
        {
            bool validBet;
            double bet;
            for (int currentPlayerNumber = 0; currentPlayerNumber < playerCount; currentPlayerNumber++)
            {
                Player currentPlayer = game.players[currentPlayerNumber];
                if (currentPlayer.Balance < Constants.MINBET) //Checks if player has a sufficient balance to participate
                {
                    currentPlayer.Playing = false; //Excludes them if not
                }
                
                if (currentPlayer.Playing) //Checks if player is playing before prompting for bet
                {
                    Utility.AnimateWrite($"{currentPlayer.Name}, please enter your bet for this round");
                    currentPlayer.Bet = Utility.GetDoubleInRange(Constants.MINBET, Constants.MAXBET);
                }
                else
                {
                    Utility.AnimateWrite($"{currentPlayer.Name} does not have the funds to participate.");
                }
              
            }
        }
        private void DisplayDealerAndPlayers()
        {
            Console.Clear();
            Console.WriteLine($"{game.dealer.ToString()}");
            foreach (Player player in game.players)
            {
                Console.WriteLine(player.ToString());
            }
        }
        private void PlayPlayerTurns()
        {
            int userChoice;
            for (int currentPlayerNumber = 0; currentPlayerNumber<playerCount; currentPlayerNumber++)
            {
                Console.Clear();
                DisplayDealerAndPlayers();
                Player currentPlayer = game.players[currentPlayerNumber];
                do
                {
                    Utility.AnimateWrite($"{currentPlayer.Name}, please choose a move.");
                    Console.WriteLine("");
                    Console.WriteLine("1 - Hit");
                    Console.WriteLine("2 - Stand");
                    Console.WriteLine("3 - Forfeit");
                    userChoice = Utility.GetIntInRange(1, 3);

                    switch (userChoice)
                    {
                        case 1:
                            currentPlayer.Hand = Hit(currentPlayer.Hand);
                            break;
                    }
                } while (userChoice != 2 || GameRules.Bust(currentPlayer.Hand)); //Menu keeps printing while player does not stand or does not bust
                
            }
        }
        private Hand Hit(Hand playerHand)
        {
            player
        }

        
    }
}
