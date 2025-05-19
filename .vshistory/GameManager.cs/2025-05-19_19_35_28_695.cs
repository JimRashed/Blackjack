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
        const int MAXMENUCHOICE = 7;
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
                Console.WriteLine("6 - View Blackjack rules");
                Console.WriteLine("7 - Exit Blackjack");

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
                        PrintGameRules();
                        break;
                    case 7:
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
        /* DisplayDealerAndPlayers()
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
         * Call Console.Clear()
         * Call game.dealer.Print() 
         * Console.WriteLine(seperator)
         * For each player in game.players
             * If player.Playing is true
                * player.Print() 
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
        }
        /* PlayPlayerTurns()
         *************************************************
         * Purpose: Allows each active player to take their turn by choosing to hit, stand, double down, or forfeit.
         *************************************************
         * @Algorithm:
         * 1. Iterate through each player in the game.
         * 2. If the player is currently playing:
         * 3 Enter a loop that continues until the player chooses to stand or busts.
         * 4. Clear and display the current game state.
         * 5. Prompt the player to choose a move (hit, stand, double down, forfeit).
         * 6. Based on the player's choice, perform the corresponding action (deal a card, end turn, double bet and hit once, or forfeit).
         * 7. If the player busts (hand value exceeds 21), their turn ends.
         * 8. After all players have taken their turns, the method concludes.
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
         * For each player in game.players
             * If player is playing
                 * Repeat
                 * Display game state
                 * Get player's choice (Hit, Stand, Double Down, Forfeit)
                 * If choice is Hit:
                  * Deal card to player
                 * Else if choice is Stand:
                     * End player's turn
                 * Else if choice is Double Down:
                     * Check if player has enough funds
                         * If yes, double bet, deal one card, end turn
                         * Else, inform player
                 * Else if choice is Forfeit:
                     * Player loses half bet, end turn
                 * If player's hand value > 21, player busts, end turn
                 * Until player has stood or busted
                 * Display final player hand
         *************************************************
         */
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
        /* PlayDealerTurn(Player dealer)
        *************************************************
        * Purpose: Simulates the dealer's turn according to standard Blackjack rules.
        *************************************************
        * @Algorithm:
        * 1. Check if any players are still actively playing. If not, skip the dealer's turn.
        * 2. Reveal the dealer's hidden second card.
        * 3. While the dealer's hand value is less than the dealer's hit limit (typically 17):
        * 4. Deal another card to the dealer's hand.
        * 5. Check if the dealer has busted (hand value exceeds 21).
        * 6. Once the dealer's hand value is 17 or more, or if the dealer busts, their turn ends.
        * 7. Display the final state of the dealer's hand.
        *************************************************
        * @Param
        * dealer: The Player object representing the dealer.
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
        * If no players are actively playing
            * Display message explaining that turn will be skipped.
        * Else
        * Reveal dealer's hidden card
        * While dealer's hand value < 17
            * Deal card to dealer
            * If dealer's hand value > 21
             * Dealer busts, end turn
        * Display dealer's final hand
        *************************************************
        */
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
        /* Outcome()
       *************************************************
       * Purpose: Determines the outcome of the round for each active player and updates their balance and the leaderboard accordingly.
       *************************************************
       * @Algorithm:
       * 1. Iterate through each player in the game.
       * 2. If the player is currently playing:
       * 4. Determine the outcome of their hand compared to the dealer's hand using game rules.
       * 5. Clear and display the game state.
       * 6. Based on the outcome (Win, Loss, Tie, Blackjack, Bust):
       * 7. Display a message describing the outcome.
       * 8. Update the player's balance based on the payout rules.
       * 9. Record the outcome on the leaderboard.
       * 10. Reset the player's bet to zero for the next round.
       * 11 Wait for user input before continuing.
       * 12  Clear and redisplay the game state.
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
       * For each player in game.players
       * If player is playing
         * Determine outcome using GameRules.Outcome(player, dealer)
       * Clear and display game state
       * Switch (outcome)
       * Case Win:
           * Display win message
           * Update player balance (increase by 2 * bet)
           * Update leaderboard (add/modify entry)
       * Case Loss:
           * Display loss message
           * Update leaderboard (add/modify entry)
       * Case Tie:
           * Display tie message
           * Update player balance (return bet)
           * Update leaderboard (add/modify entry)
       * Case Blackjack:
           * Display blackjack message
           * Update player balance (increase by 2.5 * bet)
           * Update leaderboard (add/modify entry)
       * Case Bust:
           * Display bust message
           * Update leaderboard (add/modify entry)
       * Reset player's bet to 0
       * Wait for user input
       * Clear and display game state
       *************************************************
       */
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
        /*  Hit(Hand playerHand)
       *************************************************
       * Purpose: Deals a single card from the game's deck to the specified player's hand and records the hit.
       *************************************************
       * @Algorithm:
       * 1. Draw a card from the top of the game's card deck.
       * 2. Add the drawn card to the provided player's hand.
       * 3. Increment the count of hits for the current round.
       *************************************************
       * @Param
       * playerHand: The Hand object of the player who is receiving the card.
       *************************************************
       * @Exceptions
       * None
       *************************************************
       * @Returns
       * Returns void.
       *************************************************
       *************************************************
       * @Pseudocode
       * Draw a card from game.cardDeck
       * Add card to playerHand.hand
       * Increment stats.hits
       *************************************************
       */
        private void Hit(Hand playerHand)
        {
            playerHand.hand.Add(game.cardDeck.Draw());
            stats.hits++;

        }
        /* EndRound()
         *************************************************
         * Purpose: Cleans up after a round by returning all played cards to the deck and shuffling it.
         *************************************************
         * @Algorithm:
         * 1. For each player in the game:
             * 2. While the player's hand contains cards:
                 * 3. Remove a card from the player's hand.
                 * 4. Ensure the card's hidden status is reset to false.
                 * 5. Place the card back on top of the game's card deck.
         * 6. While the dealer's hand contains cards:
             * 7. Remove a card from the dealer's hand.
             * 8. Ensure the card's hidden status is reset to false.
             * 9. Place the card back on top of the game's card deck.
         * 10. Shuffle the game's card deck.
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
         * For each player in game.players
             * While player's hand card count > 0
                 * Remove card from player's hand
                 * Set card's Hidden to false
                 * Place card on top of game.cardDeck
         * While dealer's hand has cards
             * Remove card from dealer's hand
             * Set card's Hidden to false
             * Place card on top of game.cardDeck
         * Shuffle game.cardDeck (2 times)
         *************************************************
         */
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
        /* Summary()
         *************************************************
         * Purpose: Displays a summary of the round, including an option to view detailed actions.
         *************************************************
         * @Algorithm:
         * 1. Display the current round number to the user.
         * 2. Ask the user if they would like to see the detailed actions taken during the round.
         * 3. If the user consents:
         * 4. Display the round statistics (e.g., hits, stands, busts).
         * 5. Wait for user input to continue.
         * 6. Clear the console.
         * 7. Redisplay the hands of the dealer and players.
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
         * call AnimateWrite($"End of round {game.round}!")
         * If User agrees to see round actions
             * Call AnimateWrite(stats.ToString())
             * Call Console.ReadKey()
             * Call ClearAndDisplay()
         *************************************************
         */
        private void Summary()
        {
            Utility.AnimateWrite($"End of round {game.round}!");
            if(Utility.GetUserConsent("Would you like to see the actions taken this round? (Y/N)", true))
            {
                Utility.AnimateWrite(stats.ToString());
                Console.ReadKey();
                ClearAndDisplay();
            }
        }
        /* GetUniqueName()
        *************************************************
        * Purpose: Prompts the user for a player name and ensures that the entered name is unique among the current players in the game.
        *************************************************
        * @Algorithm:
        * 1. Initialize a boolean to indicate if a unique name has been obtained.
        * 2. Declare a variable to store the player's name.
        * 3. Repeat the following until a unique name is entered:
        * 4. Prompt the user to enter a name (ensuring it's not null or empty).
        * 5 Check if the entered name already exists among the current players.
        * 6. If the name is not unique, inform the user and prompt again.
        * 7. Return the unique name provided by the user.
        *************************************************
        * @Param
        * Receives no parameters.
        *************************************************
        * @Exceptions
        * None
        *************************************************
        * @Returns
        * Returns the unique string representing the player's name.
        *************************************************
        * @Examples
        *************************************************
        * @Pseudocode
        * Declare bool isUnique = false
        * Do
            * name = GetNonNullString()
            * isUnique = IsNameUnique(name)
                * If !isUnique
                * Output error message
        * While !isUnique
        * Return name
        *************************************************
        */
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
        /* IsNameUnique(string name)
       *************************************************
       * Purpose: Checks if a given name is unique among the names of the current players in the game.
       *************************************************
       * @Algorithm:
       * 1. Iterate through each player in the game's list of players.
       * 2. For each player, compare their name to the provided name.
          * 3. If a player is found with a name that matches the provided name, return false 
       * 4. If the loop completes without finding a matching name, return true
       *************************************************
       * @Param
       * name: The string representing the name to check for uniqueness.
       *************************************************
       * @Exceptions
       * None
       *************************************************
       * @Returns
       * Returns a boolean declaring if the name was unique or not
       *************************************************
       * @Examples
       *************************************************
       * @Pseudocode
       * For each player in game.players
           * If player.Name is equal to name
              * Return false
       * Return true
       *************************************************
       */
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
        private void PrintGameRules()
        {
            Utility.NewPage();
            Utility.AnimateWrite("Welcome to Blackjack!");
            Console.WriteLine("In this game, players fight off individually against a dealer. While multiple players may compete at the same table, their matches are entirely independent of one another.");
            Console.WriteLine("The goal of the game is to get your hand's value as close to 21 without exceeding it.");
            Console.WriteLine("------------------------------------");
            Console.WriteLine(" ");
            Console.WriteLine("Hand values are calculated in the following way: number cards (2-10) are worth their respective number, face cards are worth 10, and Aces are worth 11 if the hand's value is under 21 and 1 if it exceeds it.");
            Console.WriteLine(" ");
            Console.WriteLine("When a round begins, 2 cards are dealt to each player and to the dealer. The dealer's second card is initially hidden.");
            Console.WriteLine("If a player's hand is worth 21 during the initial deal, that is called a 'Blackjack', the best possible hand. This guarantees that the player or the dealer with a blackjack will either win or tie.");
            Console.WriteLine(" ");
            Console.WriteLine("Now that you know the goal of the game, press enter to read about the functioning of player turns");
            Console.ReadKey();

            Utility.NewPage();
            Utility.AnimateWrite("Turn rules");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("When it is a player's turn, that player will have 4 options:");
            Console.WriteLine("1 - Hit: Draw a card from the deck");
            Console.WriteLine("2 - Stand: End the player's turn.");
            Console.WriteLine("3 - Double down: Double your bet and hit only once, automatically standing after");
            Console.WriteLine("4 - Forfeit: Choose to give up, granting the player half their bet back");
            Console.WriteLine("A player's turn will keep going until they either get a hand value greater than 21 or choose to stand/forfeit");
            Console.WriteLine(" ");
            Console.WriteLine("Once all player's turns have ended, the dealer plays their turn.");
            Console.WriteLine("The dealer first reveals their second card, then must keep hitting until the value of their hand is equal or greater than 17");
        }
       
        

        
    }
}
