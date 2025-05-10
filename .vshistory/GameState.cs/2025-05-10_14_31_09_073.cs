using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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


            Utility.AnimateWrite("Saving current leaderboard state to file...");
            Console.WriteLine("Please input the name of the file where leaderboard will be saved (Do not enter file extension. Including the file extension voids any save success guarantee.) Enter 'exit' to abort.");
            fileName = Utility.GetNonNullString();


            if (fileName == Constants.EXIT) //Allows player to exit program early if they changed their mind
            {
                quit = true;
            }
            else
            {
                StreamWriter gameRecorder = null;
                try
                {
                    file = Constants.GAMEFILEPATH + fileName + Constants.FILEEXTENSION;
                    gameRecorder = new StreamWriter(file);
                    //Save round number
                    gameRecorder.WriteLine(round);
                    //Save numbers of players (for loading)
                    gameRecorder.WriteLine(players.Count);
                    //Save players
                    foreach (Player player in players)
                    {
                        gameRecorder.WriteLine($"{player.Name},{player.Balance}"); 
                        //No need to save hands, there will be no mid-round saves
                    }
                    //No need to save dealer, a new one will be created in the new round
                    //Save deck
                    gameRecorder.WriteLine(cardDeck.CardsLeft);
                    foreach (Card card in cardDeck.cardList)
                    {
                        gameRecorder.WriteLine($"{card.Rank},{card.Suit}"); 
                    }
                    //Save Leaderboard
                    leaderboard.SaveBoard(fileName);
                    Utility.AnimateWrite($"Game saved! Save file name: {fileName}");
                    Console.ReadKey();
                }
                catch (Exception e)
                {
                    Utility.AnimateWrite("Error saving gamestate...");
                }
                finally
                {
                    if (gameRecorder != null)
                    {
                        gameRecorder.Close();
                    }
                }
               


            }

        }
        public void Load()
        {
            string fileName;
            string fileToLoad;
            bool fileExists = false;
            bool quit = false;
            string[] playerInfo;
            string[] cardInfo;
            Utility.AnimateWrite("Beginning game loading...");
            do //Guarantees an existing leaderboard file
            {
                Console.WriteLine("Please input the name of the save file to load (no file extensions, name ONLY) or 'exit' to abort");
                fileName = Utility.GetNonNullString();
                quit = fileName == Constants.EXIT;
                fileToLoad = Constants.GAMEFILEPATH + fileName + Constants.FILEEXTENSION; 
                if (!quit)
                {

                    fileExists = Utility.DoesFileExist(fileToLoad);

                    if (!fileExists)
                    {
                        Utility.AnimateWrite($"Error: No files in saved games are named {fileName}. Please try again.");
                        Console.ReadKey();
                    }
                }
                else
                {
                    break;
                }
            } while (!fileExists);

            if (!quit)
            {
                //Now that we have a guaranteed valid save file exists, GameState's current data can be wiped.
                if (cardDeck.cardList.Count != 0)
                {
                    cardDeck.cardList.Clear();
                }
                players.Clear();

                StreamReader gameReader = null;
                try
                {
                    gameReader = new StreamReader(fileToLoad);

                    //Load round number

                    round = int.Parse(gameReader.ReadLine());

                    //Load player profiles (using number of players)
                    int playerCount = int.Parse(gameReader.ReadLine());
                    for (int currentPlayer = 0; currentPlayer < playerCount; currentPlayer++)
                    {
                        playerInfo = gameReader.ReadLine().Split(',');
                        Player tempPlayer = new Player(playerInfo[0], int.Parse(playerInfo[1]));
                        players.Add(tempPlayer);
                    }

                    //Load deck
                    int deckCards = int.Parse(gameReader.ReadLine());
                    for (int currentCard = 0; currentCard < deckCards; currentCard++)
                    {
                        cardInfo = gameReader.ReadLine().Split(',');
                        Card tempCard = new Card((Rank)cardInfo[0],)

                    }
                    
                }

                catch (Exception e)
                {

                }

                finally
                {
                    if (gameReader != null)
                    {
                        gameReader.Close();
                    }
                   
                }
            }
        }


    }
}
