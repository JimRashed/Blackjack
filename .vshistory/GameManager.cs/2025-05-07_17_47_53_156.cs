using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Rashed_Blackjack
{
    public class GameManager
    {
        const int MINMENUCHOICE = 1;
        const int MAXMENUCHOICE = 5;
        private GameState game;
        private RoundStats stats;
        private bool gameActive;
        int playerCount;
        int hits, stands, doubleDowns, forfeits, busts = 0;
        public void Start()
        {
            Utility.PrintGameHeader();
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
                Console.WriteLine("1 - Begin a new game");
                Console.WriteLine("2 - Begin a new round");
                Console.WriteLine("3 - Check the leaderboard");
                Console.WriteLine("4 - Load a game");
                Console.WriteLine("5 - Exit Blackjack");

                int userChoice = Utility.GetIntInRange(MINMENUCHOICE, MAXMENUCHOICE);

                switch (userChoice)
                {
                    case 1:
                        game = new GameState();
                        gameActive = true;
                        NewGame();
                        break;
                    case 2:
                        if (gameActive)
                        {
                            NewRound();
                        }
                        else
                        {
                            Utility.AnimateWrite("There is currently no game in which to start a new round. Please create a game first.");
                            Console.ReadKey();
                            Console.Clear();
                        }
                            break;
                    case 3:
                        //Implement leaderboard display
                        break;
                    case 4:
                        //Implement loading
                        break;
                    case 5:
                        quit = true;
                        break;

                }
            } while (!quit);
           
        }
        private void NewGame()
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
                Utility.AnimateWrite($"What will be {name}'s starting balance? (must be greater than minimum bet (50)"); //Make this more descriptive
                balance = Utility.GetDoubleInRange(Constants.MINBET, 100000);
                tempPlayer = new Player(name, balance);
                game.players.Add(tempPlayer);
                Console.Clear();
            }
            NewRound();


        }//Populates the list of players in GameState, deals 2 cards to everyone
        private void NewRound()
        {
            game.round++;
            stats = new RoundStats();
            GetPlayerBets();
            InitialDeal();
            DisplayDealerAndPlayers();
            PlayPlayerTurns();
            PlayDealerTurn(game.dealer);
            Outcome();
            EndRound();



        } //After each round, add cards back to deck, then shuffle 3 times.
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
                Console.Clear();
                Player currentPlayer = game.players[currentPlayerNumber];
                currentPlayer.Playing = true; //Resets player playing status in case balance has changed
                if (currentPlayer.Balance < Constants.MINBET) //Checks if player has a sufficient balance to participate
                {
                    currentPlayer.Playing = false; //Excludes them if not
                }
                
                if (currentPlayer.Playing) //Checks if player is playing before prompting for bet
                {
                    Utility.AnimateWrite($"Current balance: {currentPlayer.Balance.ToString("C")}");
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
                    Console.ReadKey();
                }
              
            }
        }
        private void ClearAndDisplay()
        {
            Console.Clear();
            DisplayDealerAndPlayers();
        }
        private void DisplayDealerAndPlayers()
        {
            Console.Clear();
            Console.WriteLine($"{game.dealer.ToString()}");//Display dealer
            foreach (Player player in game.players) //Display cards
            {
                if (player.Playing)
                {
                    Console.WriteLine(player.ToString());
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(player.ToString());
                    Console.ForegroundColor = ConsoleColor.White;
                }
                
            }
        }
        private void PlayPlayerTurns()
        {
            int userChoice;
            for (int currentPlayerNumber = 0; currentPlayerNumber<playerCount; currentPlayerNumber++)
            {
                Player currentPlayer = game.players[currentPlayerNumber];
                if (currentPlayer.Playing)
                {
                    do
                    {
                        Console.Clear();
                        DisplayDealerAndPlayers();
                        Utility.AnimateWrite($"{currentPlayer.Name}, please choose a move.");
                        Console.WriteLine("");
                        Console.WriteLine("1 - Hit");
                        Console.WriteLine("2 - Stand");
                        Console.WriteLine("3 - Double Down");
                        Console.WriteLine("4 - Forfeit");
                        userChoice = Utility.GetIntInRange(Constants.MINPLAYERMOVE, Constants.MAXPLAYERMOVE);

                        switch (userChoice)
                        {
                            case 1:
                                Hit(currentPlayer.Hand);
                                ClearAndDisplay();
                                Utility.AnimateWrite($"{currentPlayer.Name} hits.");
                                Console.ReadKey();
                                break;
                            case 2:
                                //Stands
                                stats.stands++;
                                ClearAndDisplay();
                                Utility.AnimateWrite($"{currentPlayer.Name} stands.");
                                Console.ReadKey();
                                break;
                            case 3:
                                if (currentPlayer.Balance >= currentPlayer.Bet) //Checks if player has enough in their balance to double their bet
                                {
                                    //Double down
                                    stats.doubleDowns++;
                                    currentPlayer.Balance -= currentPlayer.Bet; //Removes the Bet from the player's balance
                                    currentPlayer.Bet *= 2; //Doubles the current bet
                                    Hit(currentPlayer.Hand); //Hits once
                                    userChoice = Constants.STAND; //Forces turn to end
                                    ClearAndDisplay(); //Updates board

                                }
                                else
                                {
                                    Utility.AnimateWrite($"{currentPlayer.Name} does not have the funds to double down");
                                    Console.ReadKey();
                                }
                                break;
                            case 4:
                                stats.forfeits++;
                                ClearAndDisplay(); //Forfeits
                                Utility.AnimateWrite($"{currentPlayer.Name} forfeits. Half their bet is lost.");
                                currentPlayer.Balance += currentPlayer.Bet / 2;
                                currentPlayer.Bet = 0;
                                currentPlayer.Playing = false;
                                Console.ReadKey();
                                break;
                            default:
                                break;
                        }
                        if (GameRules.Bust(currentPlayer.Hand))
                        {
                            stats.busts++;
                            Utility.AnimateWrite($"{currentPlayer.Name} busts.");
                            Console.ReadKey();
                        }
                    } while (currentPlayer.Playing == true && userChoice != Constants.STAND && !GameRules.Bust(currentPlayer.Hand)); //Menu keeps printing while player does not stand or does not bust
                }
                ClearAndDisplay();
            }
        }
        private void PlayDealerTurn(Player dealer)
        {
            if (!GameRules.NoOnePlaying(game.players))
            {
                dealer.Hand.hand[Constants.SECONDCARD].Hidden = false; //Dealer reveals his second card
                bool bust = false;
                Console.Clear();
                DisplayDealerAndPlayers();
                Utility.AnimateWrite("Dealer's turn!");
                Utility.AnimateWrite("The dealer reveals his second card.");
                Console.ReadKey();
                if (!GameRules.Blackjack(dealer.Hand))
                {
                    while (dealer.Hand.Value < Constants.DEALERMUSTHITLIMIT)
                    {
                        stats.hits++;
                        Hit(dealer.Hand);
                        Console.Clear();
                        DisplayDealerAndPlayers();
                        Utility.AnimateWrite("The dealer hits.");
                        Console.ReadKey();
                        if (GameRules.Bust(dealer.Hand))
                        {
                            stats.busts++;
                            bust = true;
                            Console.Clear();
                            DisplayDealerAndPlayers();
                            Utility.AnimateWrite("The dealer busts.");
                            Console.ReadKey();
                        }
                    }
                }
                if (!bust)
                {
                    ClearAndDisplay();
                    stats.stands++;
                    Utility.AnimateWrite("The dealer stands.");
                    Console.ReadKey();
                }

            }
            else
            {
                ClearAndDisplay();
                Utility.AnimateWrite("No players are actively playing. Dealer turn is skipped");
                Console.ReadKey();
            }
            


        }
        private void Outcome()
        {
            for (int currentPlayerNumber = 0; currentPlayerNumber < playerCount; currentPlayerNumber++)
            {
                Player currentPlayer = game.players[currentPlayerNumber];
                if (currentPlayer.Playing)
                {
                    switch (GameRules.Outcome(currentPlayer, game.dealer))
                    {
                        case "Win":
                            Utility.AnimateWrite($"{currentPlayer.Name} won! They received a payout of 2x their bet.");
                            currentPlayer.Balance += Constants.WINPAYOUTRATIO * currentPlayer.Bet;
                            break;
                        case "Loss":
                            Utility.AnimateWrite($"{currentPlayer.Name} lost. Their bet was lost.");
                            //No need to do anything, as their bet will be overriden next round.
                            break;
                        case "Tie":
                            Utility.AnimateWrite($"{currentPlayer.Name}'s hand tied. Their bet was returned to their balance");
                            currentPlayer.Balance += currentPlayer.Bet;
                            break;
                        case "Blackjack":
                            Utility.AnimateWrite($"{currentPlayer.Name} got a blackjack! They received a playout of 250% of their bet");
                            currentPlayer.Balance += currentPlayer.Bet * Constants.BLACKJACKPAYOUTRATIO;
                            break;
                        case "Bust":
                            Utility.AnimateWrite($"{currentPlayer.Name} busted earlier. Their bet was lost.");
                            //Once again, no need to do anything.
                            break;
                        default:
                            break;
                    }
                    currentPlayer.Bet = 0; //Resets the player's bet for the next round
                    Console.ReadKey();
                    ClearAndDisplay();
                    
                }
               
            }
        }
        private void Hit(Hand playerHand)
        {
            playerHand.hand.Add(game.cardDeck.Draw());
            stats.hits++;

        }
        private void EndRound()
        {
            Card cardToAdd;
            foreach (Player player in game.players) //Empties all player's hands back into the deck
            {
                for (int currentCard = player.Hand.hand.Count; currentCard > 0; currentCard--)
                {
                    cardToAdd = player.Hand.RemoveCard();
                    if (cardToAdd != null)
                    {
                        game.cardDeck.PlaceOnTop(cardToAdd);
                    }
                }
            }
            for (int currentCard = game.dealer.Hand.hand.Count; currentCard > 0; currentCard--) //Empties all the dealer's card into the deck
            {
                cardToAdd = game.dealer.Hand.RemoveCard();
                if (cardToAdd != null)
                {
                    game.cardDeck.PlaceOnTop(cardToAdd);
                }
            }
            game.cardDeck.Shuffle(3);
        }
        private void Summary()
        {
            Utility.AnimateWrite("Would you like to see a summary of actions taken this round? (Y/N)");
            bool userChoice = Utility.GetUserConsent()
        }
        

        
    }
}
