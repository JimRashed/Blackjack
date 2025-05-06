using System;
using System.Collections.Generic;
using System.Linq;
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
        public int turn;
        public double bets;

        //Constructor
        public GameState()
        {
            players = new List<Player>();
            dealer = new Player("Dealer", 999);
            cardDeck = new Deck();
        }
        
        //Methods for file loading and saving will happen in this class
        


    }
}
