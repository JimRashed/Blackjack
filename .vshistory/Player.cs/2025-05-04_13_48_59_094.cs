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
        public PlayerStats stats;
        private double _balance;
        private double _bet;
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
        public double Balance
        {
            get
            {
                return _balance;
            }
            set
            {
                _balance = value;
            }
        }
        public double Bet
        {
            get
            {
                return _bet;
            }
            set
            {
                _bet = value;
            }
        }       
        //Constructor
        public Player(string name, double initialSum)
        {
            Name = name;
            Balance = initialSum;
        }
      
        //Override operators
        
    }
}
