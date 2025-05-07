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
        private GameState game;
        private bool gameActive;
        int playerCount;
        public void Start()
        {
            Utility.AnimateWrite("Welcome to blackjack!");
            Thread.Sleep(500);
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
            playerCount = Utility.GetIntInRange(Constants.MINPLAYERCOUNT, Constants.MAXPLAYERCOUNT);
            for (int currentPlayer = 0; currentPlayer< playerCount; currentPlayer++)
            {
                
                Utility.AnimateWrite($"What will be the name of player {currentPlayer + 1}?");
                name = Utility.GetNonNullString();
                Utility.AnimateWrite($"What will be {name}'s starting balance? (must be greater than minimum bet (50)"); //Fix bet logic. Whole lot is wrong...
                balance = Utility.GetDoubleInRange(Constants.MINBET, 100000);
                tempPlayer = new Player(name, balance);
                game.players.Add(tempPlayer);
                Console.Clear();
            }

            GetPlayerBets();
            InitialDeal();
            DisplayDealerAndPlayers();
            PlayPlayerTurns();
            PlayDealerTurn(game.dealer);


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
            bool validBet = false;
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
                    do
                    {
                        bet = Utility.GetDoubleInRange(Constants.MINBET, Constants.MAXBET>currentPlayer.Balance?currentPlayer.Balance : Constants.MAXBET);
                        //Gets a bet greater than min bet and lesser than Max bet or player balance, whichever is smaller. ADJUST ERROR MESSAGE

                        validBet = bet <= currentPlayer.Balance;
                        if (!validBet)
                        {
                            Utility.AnimateWrite("The bet you attempted to place exceeded your balance");
                        }
                    } while (!validBet);
                    currentPlayer.Balance -= bet; //Removes the bet from the player's balance
                    currentPlayer.Bet = bet; //adds the bet to the player's bet amount
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
                Player currentPlayer = game.players[currentPlayerNumber];
                do
                {
                    Console.Clear();
                    DisplayDealerAndPlayers();
                    Utility.AnimateWrite($"{currentPlayer.Name}, please choose a move.");
                    Console.WriteLine("");
                    Console.WriteLine("1 - Hit");
                    Console.WriteLine("2 - Stand");
                    Console.WriteLine("3 - Double Down");
                    Console.WriteLine("3 - Forfeit");
                    userChoice = Utility.GetIntInRange(1, 3);

                    switch (userChoice)
                    {
                        case 1:
                            Hit(currentPlayer.Hand)
                            Console.Clear();
                            DisplayDealerAndPlayers();
                            Utility.AnimateWrite($"{currentPlayer.Name} hits.");
                            Thread.Sleep(1000);
                            break;
                        case 2:
                            //Stands
                            break;
                        case 3:
                            if (currentPlayer.Balance >= currentPlayer.Bet) //Checks if player has enough in their balance to double their bet
                            {
                                //Double down
                                currentPlayer.Balance -= currentPlayer.Bet; //Removes the Bet from the player's balance
                                currentPlayer.Bet *= 2; //Doubles the current bet
                                Hit(currentPlayer.Hand); //Hits once
                                userChoice = Constants.STAND; //Forces turn to end

                            }
                            else
                            {
                                Utility.AnimateWrite("You do not have the funds to double down");
                                Thread.Sleep(1000);
                            }
                                break;
                    }
                } while (userChoice != Constants.STAND && !GameRules.Bust(currentPlayer.Hand)); //Menu keeps printing while player does not stand or does not bust
                Console.Clear();
                DisplayDealerAndPlayers();
            }
        }
        private void PlayDealerTurn(Player dealer)
        {
            dealer.Hand.hand[Constants.SECONDCARD].Hidden = false; //Dealer reveals his second card
            bool stand = false;
            Console.Clear();
            DisplayDealerAndPlayers();
            Thread.Sleep(1000);
            while (dealer.Hand.Value < Constants.DEALERMUSTHITLIMIT)
            {
                Hit(dealer.Hand);
                Console.Clear();
                DisplayDealerAndPlayers();
                Utility.AnimateWrite("The dealer hits.");
                Thread.Sleep(1000);
            }
            Console.Clear();
            DisplayDealerAndPlayers();
            Utility.AnimateWrite("The dealer stands.");
            Thread.Sleep(1000);
            


        }
        private void Hit(Hand playerHand)
        {
            playerHand.hand.Add(game.cardDeck.Draw());

        }
        

        
    }
}
