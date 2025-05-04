using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rashed_Blackjack
{
    public class GameState()
    {
        //Fields
        public List<Player> players;
        public Player dealer;
        public Deck cardDeck;
        public int turn;

        //Constructor
        public GameState(int playerNumber)
        {
            dealer = new Player("Dealer", 999);
            cardDeck = new Deck();
            for (int currentPlayer; currentPlayer<playerNumber; currentPlayer++)
            {
                
            }
        }
        //Another constructor will have to be made for file loading.
        


    }
}
