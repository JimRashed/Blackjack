using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rashed_Blackjack
{
    public class Player
    {
        //Fields
        private List<Card> _hand;
        public struct Stats
        {
            public int wins = 0;
            public int losses = 0;
            public int ties = 0;

            public double winRatio;
            winRatio 
            
        }
        public double balance;
        public double bet;
        private string _name;
        //Properties
        public List<Card> Hand
        {
            get
            {
                return _hand;
            }
            set
            {
                _hand = value;
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
       
        //Constructor
      
        //Override operators
        
    }
}
