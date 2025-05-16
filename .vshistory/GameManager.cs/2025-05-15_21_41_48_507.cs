using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Rashed_Blackjack
{
    /*
   * Programming 2 – Project – Winter 2025
   * Created by: Jimmy Rashed, 6291812
   * Tested by: Daniel Oleinic
   * Relationship: Friend
   * Date: May 11th, 2025
   *
   * Description: The goal of this program is to create a fully playable version of the card game "Blackjack" in C# .NET with additional file saving/loading features and a leaderboard. 
   */
    public class GameManager
    {
        const int MINMENUCHOICE = 1;
        const int MAXMENUCHOICE = 6;
        private GameState game;
        private RoundStats stats;
        private bool gameActive;
        int hits, stands, doubleDowns, forfeits, busts = 0;
        /* Start()
      *************************************************
      * Purpose: Initializes the game by displaying the header and navigating to the main menu.
      *************************************************
      * @Algorithm:
      * 1. Display the game's header.
      * 2. Briefly pause.
      * 3. Present the main menu to the user.
      * 4. Display a thank you message upon exiting the main menu.
      * 5. Wait for user input before closing.
      *************************************************
      * @Param
      * Receives no parameters.
      *************************************************
      * @Exceptions
      * None
      *************************************************
      * @Returns
      * Returns nothing
      *************************************************
      * @Examples
      * GameManager gameManager = new GameManager();
      * gameManager.Start();
      *************************************************
      * @Pseudocode
      * Call PrintGameHeader()
      * Sleep for 500 ms
      * Call MainMenu()
      * Call PrintNewGamePage()
      * AnimateWrite("Thank you very much for playing!")
      * ReadKey()
      *************************************************
      */
        public void Start()
        {
            Utility.PrintGameHeader();
            Thread.Sleep(500);
            MainMenu();
            Utility.PrintNewGamePage();
            Utility.AnimateWrite("Thank you very much for playing!");
            Console.ReadKey();
        }
        /* MainMenu()
        *************************************************
        * Purpose: Displays the main menu and handles user choices.
        *************************************************
        * @Algorithm:
        * 1. Initialize a loop that continues until the user chooses to exit.
        * 2. Clear the console and display the main menu options.
        * 3. Get a valid menu choice from the user.
        * 4. Perform actions based on the user's choice:
        * - Start a new game.
        * - Start a new round (if a game is active).
        * - Display the leaderboard (if a game is active).
        * - Save the current game (if a game is active).
        * - Load a saved game.
        * - Exit the application.
        *************************************************
        * @Param
        * Receives no parameters.
        *************************************************
        * @Exceptions
        * None
        *************************************************
        * @Returns
        * Returns nothing
        *************************************************
        * @Examples
        * gameManager.MainMenu();
        *************************************************
        * @Pseudocode
        * Do
            * NewPage()
            * AnimateWrite("Please choose an option...")
            * Print menu options
            * userChoice = GetIntInRange(MINMENUCHOICE, MAXMENUCHOICE)
            * Switch (userChoice)
            * Case 1: Create new GameState, set gameActive to true, call NewGame()
            * Case 2: If gameActive, call NewRound(), else display error
            * Case 3: If gameActive, clear console, print header, print leaderboard, ReadKey(), else display error
            * Case 4: If gameActive, call game.Save(), else display error
            * Case 5: If !gameActive, create new GameState, call game.Load(), set gameActive to true
            * Case 6: set quit to true
        * While !quit
        *************************************************
        */
        private void MainMenu()
        {
            bool quit = false;
            do
            {
                Utility.NewPage();
                Utility.AnimateWrite("Please choose an option from the menu below:");
                Console.WriteLine("");
                Console.WriteLine("1 - Begin a new game");
                Console.WriteLine("2 - Begin a new round");
                Console.WriteLine("3 - Check the leaderboard");
                Console.WriteLine("4 - Save the current game");
                Console.WriteLine("5 - Load a game");
                Console.WriteLine("6 - Exit Blackjack");

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
                        if (gameActive)
                        {
                            Console.Clear();
                            Utility.PrintGameHeader();
                            game.leaderboard.PrintLeaderboard();
                            Console.ReadKey();
                        }
                        else
                        {
                            Utility.AnimateWrite("There is currently no leaderboard to display. Please create or load a game first.");
                            Console.ReadKey();
                            Console.Clear();
                        }
                            break;
                    case 4:
                        if (gameActive)
                        {
                            game.Save();
                        }
                        else
                        {
                            Utility.AnimateWrite("There is currently no game to save. Please create a game first.");
                            Console.ReadKey();
                        }
                            break;
                    case 5:
                        if (!gameActive)
                        {
                            game = new GameState();
                        }
                        game.Load();
                        gameActive = true;
                        break;
                    case 6:
                        quit = true;
                        break;

                }
            } while (!quit);
           
        }
        /*  NewGame()
        *************************************************
        * Purpose: Initializes a new game by setting up players and starting the first round.
        *************************************************
        * @Algorithm:
        * 1. Clear any existing players from a previous game.
        * 2. Clear the leaderboard from a previous game.
        * 3. Prompt the user for the number of players (between 2 and 4).
        * 4. For each player:
        * 5. Prompt for their name, ensuring it's unique.
        * 6. Prompt for their starting balance (must be greater than the minimum bet).
        * 7. Create a new player with the given name and balance.
        * 8. Add the new player to the game.
        * 9. Start the first round of the game.
        *************************************************
        * @Param
        * Receives no parameters.
        *************************************************
        * @Exceptions
        * None
        *************************************************
        * @Returns
        * Returns void.
        *************************************************
        * @Examples
        * gameManager.NewGame();
        *************************************************
        * @Pseudocode
        * Call game.players.Clear()
        * Callgame.leaderboard.Clear()
        * AnimateWrite("How many players...")
        * playerCount = GetIntInRange(MINPLAYERCOUNT, MAXPLAYERCOUNT)
        * For currentPlayer from 0 to playerCount - 1
        * Call NewPage()
        * AnimateWrite($"What will be the name...")
        * name = GetUniqueName()
        * AnimateWrite($"What will be {name}'s starting balance...")
        * balance = GetDoubleInRange(MINBET, 100000)
        * tempPlayer = new Player(name, balance)
        * game.players.Add(tempPlayer)
        * NewRound()
        *************************************************
        */
        private void NewGame()
        {
            //Clears any players left over from previous game 
            game.players.Clear();
            //Clears leaderboard left over from previous game
            game.leaderboard.Clear();
            Player tempPlayer;
            string name; 
            double balance;
            Utility.AnimateWrite("How many players would you like to include in the game? (2-4)"); 
            int playerCount = Utility.GetIntInRange(Constants.MINPLAYERCOUNT, Constants.MAXPLAYERCOUNT);
            for (int currentPlayer = 0; currentPlayer< playerCount; currentPlayer++)
            {
                Utility.NewPage();
                
                Utility.AnimateWrite($"What will be the name of player {currentPlayer + 1}?");
                name = GetUniqueName();
                Utility.AnimateWrite($"What will be {name}'s starting balance? (must be greater than minimum bet (50)"); //Make this more descriptive
                balance = Utility.GetDoubleInRange(Constants.MINBET, 100000);
                tempPlayer = new Player(name, balance);
                game.players.Add(tempPlayer);
            }
            NewRound();


        }//Populates the list of players in GameState, deals 2 cards to everyone
        /*  NewRound()
    *************************************************
    * Purpose: Starts a new round of the game by handling bets, dealing cards, and initiating player and dealer turns.
    *************************************************
    * @Algorithm:
    * 1. Increment the round counter.
    * 2. Create a new object to track round statistics.
    * 3. Get bets from each active player.
    * 4. Deal the initial two cards to each active player and the dealer (one of the dealer's cards is hidden).
    * 5. Display the hands of the dealer (one card hidden) and the players.
    * 6. Allow each active player to take their turn (hit, stand, double down, or forfeit).
    * 7. Play the dealer's turn according to the game rules.
    * 8. Determine the outcome of the round for each player.
    * 9. Perform end-of-round cleanup (e.g., collect cards).
    * 10. Display a summary of the round.
    *************************************************
    * @Param
    * Receives no parameters.
    *************************************************
    * @Exceptions
    * None
    *************************************************
    * @Returns
    * Returns nothing
    *************************************************
    * @Examples
    * gameManager.NewRound();
    *************************************************
    * @Pseudocode
    * game.round++
    * stats = new RoundStats()
    * CallGetPlayerBets()
    * Call InitialDeal()
    * Call DisplayDealerAndPlayers()
    * Call PlayPlayerTurns()
    * Call PlayDealerTurn(game.dealer)
    * Call Outcome()
    * Call EndRound()
    * Call Summary()
    *************************************************
    */
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
            Summary();



        } //After each round, add cards back to deck, then shuffle 3 times.
        /* InitialDeal()
        *************************************************
        * Purpose: Deals the initial two cards to the dealer and all active players at the start of a round.
        *************************************************
        * @Algorithm:
        * 1. Deal two cards to the dealer, ensuring the second card is initially hidden.
        * 2. For each player who is currently playing:
        * a. Deal two cards to the player.
        * 3. Sort the hands of the dealer and each player.
        *************************************************
        * @Param
        * Receives no parameters.
        *************************************************
        * @Exceptions
        * None
        *************************************************
        * @Returns
        * Returns nothing
        *************************************************
        * @Examples
        * gameManager.InitialDeal();
        *************************************************
        * @Pseudocode
        * For currentCard from 0 to INITIALCARDAMOUNT - 1
        * game.dealer.Hand.AddCard(game.cardDeck.Draw())
        * game.dealer.Hand.Sort()
        * game.dealer.Hand.hand[game.dealer.Hand.Size - 1].Hidden = true
        * For each currentPlayer in game.players
        * If currentPlayer.Playing is true
        * For currentCard from 0 to INITIALCARDAMOUNT - 1
        * game.players[currentPlayer].Hand.AddCard(game.cardDeck.Draw())
        * game.players[currentPlayer].Hand.Sort()
        *************************************************
        */
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
            
        } //Deals 2 cards to everyone
        /* GetPlayerBets()
         *************************************************
         * Purpose: Prompts each active player to place their bet for the current round.
         *************************************************
         * @Algorithm:
         * 1. Iterate through each player in the game.
         * 2. For each player:
         * a. Reset their playing status to true.
         * b. Check if the player has sufficient balance to meet the minimum bet. If not, set their playing status to false.
         * c. If the player is playing:
         *  Display their current balance.
         *  Prompt them to enter their bet, ensuring it's within the allowed range and does not exceed their balance.
         * Deduct the bet from their balance.
         * . Record their bet for the current round.
         * d. If the player is not playing (due to insufficient funds), inform them.
         *************************************************
         * @Param
         * Receives no parameters.
         *************************************************
         * @Exceptions
         * None
         *************************************************
         * @Returns
         * Returns nothing
         *************************************************
         * @Examples
         * // This method is called at the beginning of each new round.
         * // gameManager.GetPlayerBets();
         *************************************************
         * @Pseudocode
         * For each currentPlayerNumber from 0 to game.players.Count - 1
         * NewPage()
         * currentPlayer = game.players[currentPlayerNumber]
         * currentPlayer.Playing = true
         * If currentPlayer.Balance < MINBET
              * currentPlayer.Playing = false
         * If currentPlayer.Playing is true
             * AnimateWrite($"Current balance: ...")
             * AnimateWrite($"{currentPlayer.Name}, please enter your bet...")
             * Do
                 * bet = GetDoubleInRange(MINBET, ...)
                 * validBet = bet <= currentPlayer.Balance
                     * If !validBet
                     * AnimateWrite("The bet you attempted...")
             * While !validBet
             * currentPlayer.Balance -= bet
             * currentPlayer.Bet = bet
             * Else
                 * AnimateWrite($"{currentPlayer.Name} does not have the funds...")
                 * ReadKey()
         *************************************************
         */
        private void GetPlayerBets()
        {
            bool validBet = false;
            double bet;
            for (int currentPlayerNumber = 0; currentPlayerNumber < game.players.Count; currentPlayerNumber++)
            {
                Utility.NewPage();
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
        /* ClearAndDisplay()
         *************************************************
         * Purpose: Clears the console and then displays the current state of the game (dealer and players' hands).
         *************************************************
         * @Algorithm:
         * 1. Clear the console.
         * 2. Display the dealer's hand 
         * 3. Display the hand and information for each player.
         *************************************************
         * @Param
         * Receives no parameters.
         *************************************************
         * @Exceptions
         * None
         *************************************************
         * @Returns
         * Returns nothing
         *************************************************
         * @Examples
         * 
         *************************************************
         * @Pseudocode
         * Console.Clear()
         * DisplayDealerAndPlayers()
         *************************************************
         */
        private void ClearAndDisplay()
        {
            Console.Clear();
            DisplayDealerAndPlayers();
        }
        /*
         *************************************************
         * DisplayDealerAndPlayers()
         *************************************************
         * Purpose: Displays the hands and relevant information for the dealer and all players.
         *************************************************
         * @Algorithm:
         * 1. Display the dealer's information and hand 
         * 2. For each player:
         * . If the player is active, display their hand and bet.
         * . If the player is not active, display their name 
         * 3. Print the separator line after each player.
         *************************************************
         * @Param
         * Receives no parameters.
         *************************************************
         * @Exceptions
         * None
         *************************************************
         * @Returns
         * Returns nothing
         *************************************************
         * @Examples
         *************************************************
         * @Pseudocode
         * Console.Clear()
         * game.dealer.Print() // Assuming a Print() method exists for Player
         * Console.WriteLine(seperator)
         * For each player in game.players
         * If player.Playing is true
         * player.Print() // Assuming a Print() method exists for Player
         * Else
         * Set Console.ForegroundColor to Red
         * Console.WriteLine(player.ToString())
         * Set Console.ForegroundColor to White
         * Console.WriteLine(seperator)
         *************************************************
         */
        private void DisplayDealerAndPlayers()
        {
            string seperator = "-------------------------------";
            Console.Clear();
            //Console.WriteLine($"{game.dealer.ToString()}");
            game.dealer.Print();//Display dealer
            Console.WriteLine(seperator);
            foreach (Player player in game.players) //Display cards
            {
               
                if (player.Playing)
                {
                    //Console.WriteLine(player.ToString());
                    player.Print();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(player.ToString()); //Using ToString instead of print here as Print() messes with colors. The whole player profile needs to be monotone.
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine(seperator);

            }
        } //Consider replacing this with a "Display game" method that would keep the blackjack header at the top and add colors and indicate when blackjack occurs and stuff. Definitely better organize positioning. I'd like the layout like a blackjack table : dealer at top, players side by side
        private void PlayPlayerTurns()
        {
            int userChoice;
            for (int currentPlayerNumber = 0; currentPlayerNumber<game.players.Count; currentPlayerNumber++)
            {
                Player currentPlayer = game.players[currentPlayerNumber];
                if (currentPlayer.Playing)
                {
                    do
                    {
                        ClearAndDisplay();
                        if (game.players.Count < 4)
                        {
                            Utility.AnimateWrite($"{currentPlayer.Name}, please choose a move.");
                            Console.WriteLine("");
                            Console.WriteLine("1 - Hit");
                            Console.WriteLine("2 - Stand");
                            Console.WriteLine("3 - Double Down");
                            Console.WriteLine("4 - Forfeit");
                        }
                        else
                        {
                            Utility.AnimateWrite($"{currentPlayer.Name}, choose a move: 1-Hit, 2-Stand, 3-Double down, 4-Forfeit");
                        }
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
                                    Utility.AnimateWrite($"{currentPlayer.Name} doubles down.");
                                    Console.ReadKey();

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
                            ClearAndDisplay();
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
                ClearAndDisplay();
                Utility.AnimateWrite("Dealer's turn!");
                Utility.AnimateWrite("The dealer reveals his second card.");
                Console.ReadKey();
                if (!GameRules.Blackjack(dealer.Hand))
                {
                    while (dealer.Hand.Value < Constants.DEALERMUSTHITLIMIT)
                    {
                        stats.hits++;
                        Hit(dealer.Hand);
                        ClearAndDisplay();
                        Utility.AnimateWrite("The dealer hits.");
                        Console.ReadKey();
                        if (GameRules.Bust(dealer.Hand))
                        {
                            stats.busts++;
                            bust = true;
                            ClearAndDisplay();
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
            for (int currentPlayerNumber = 0; currentPlayerNumber < game.players.Count; currentPlayerNumber++)
            {
                Player currentPlayer = game.players[currentPlayerNumber];
                if (currentPlayer.Playing)
                {
                    Outcomes outcome = GameRules.Outcome(currentPlayer, game.dealer);
                    ClearAndDisplay();
                    switch (outcome)
                    {
                        case Outcomes.Win:
                            Utility.AnimateWrite($"{currentPlayer.Name} won! They received a payout of 2x their bet.");
     
                            currentPlayer.Balance += Constants.WINPAYOUTRATIO * currentPlayer.Bet;
                            game.leaderboard.AddOrModifyEntry(currentPlayer.Name, outcome);
                            break;
                        case Outcomes.Loss: 
                            Utility.AnimateWrite($"{currentPlayer.Name} lost. Their bet was lost.");
                            game.leaderboard.AddOrModifyEntry(currentPlayer.Name, outcome);

                            //No need to do anything, as their bet will be overriden next round.
                            break;
                        case Outcomes.Tie:
                            Utility.AnimateWrite($"{currentPlayer.Name}'s hand tied. Their bet was returned to their balance");
                            currentPlayer.Balance += currentPlayer.Bet;
                            game.leaderboard.AddOrModifyEntry(currentPlayer.Name, outcome);

                            break;
                        case Outcomes.Blackjack:
                            Utility.AnimateWrite($"{currentPlayer.Name} got a blackjack! They received a playout of 250% of their bet");
                            currentPlayer.Balance += currentPlayer.Bet * Constants.BLACKJACKPAYOUTRATIO;
                            game.leaderboard.AddOrModifyEntry(currentPlayer.Name, outcome);

                            break;
                        case Outcomes.Bust:
                            Utility.AnimateWrite($"{currentPlayer.Name} busted earlier. Their bet was lost.");
                            game.leaderboard.AddOrModifyEntry(currentPlayer.Name, outcome);

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
                /*
                for (int currentCard = player.Hand.hand.Count; currentCard > 0; currentCard--)
                {
                    cardToAdd = player.Hand.RemoveCard();
                    if (cardToAdd != null)
                    {
                        game.cardDeck.PlaceOnTop(cardToAdd);
                    }
                }*/
                while (player.Hand.hand.Count > 0)
                {
                    cardToAdd = player.Hand.RemoveCard();
                    cardToAdd.Hidden = false; //Resets hidden status, just in case
                    game.cardDeck.PlaceOnTop(cardToAdd);
                }
            }
            /*
            for (int currentCard = game.dealer.Hand.hand.Count; currentCard > 0; currentCard--) //Empties all the dealer's card into the deck
            {
                cardToAdd = game.dealer.Hand.RemoveCard();
                if (cardToAdd != null)
                {
                    game.cardDeck.PlaceOnTop(cardToAdd);
                }
            }*/
            while (game.dealer.Hand.hand.Count > 0)
            {
                cardToAdd = game.dealer.Hand.RemoveCard();
                cardToAdd.Hidden = false;
                game.cardDeck.PlaceOnTop(cardToAdd);
            }
            game.cardDeck.Shuffle(2);
        }
        private void Summary()
        {
            Utility.AnimateWrite($"End of round {game.round}!");
            if(Utility.GetUserConsent("Would you like to see the actions taken this round? (Y/N)", true))
            {
                Utility.AnimateWrite(stats.ToString());
                Console.ReadKey();
                Console.Clear();
                DisplayDealerAndPlayers();
            }
        }
        private string GetUniqueName()
        {
            bool isUnique = false;
            string name;
            do
            {
                name = Utility.GetNonNullString();
                isUnique = IsNameUnique(name);
                if (!isUnique)
                {
                    Utility.AnimateWrite($"The name {name} is already taken! Please choose another one.");
                }
            } while (!isUnique);
            return name;
        }
        private bool IsNameUnique(string name)
        {
            foreach (Player player in game.players)
            {
                if (player.Name == name)
                {
                    return false;
                }
            }
            return true;
        }
       
        

        
    }
}
