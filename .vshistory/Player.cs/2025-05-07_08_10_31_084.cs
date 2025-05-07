using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rashed_Blackjack
{
    public class Player
    {
        //Fields
        private Hand _hand;
        public PlayerStats stats;
        private double _balance;
        private double _bet;
        private string _name;
        private bool _playing;
        private bool dealer;
        //Properties
        public Hand Hand
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
        public bool Playing
        {
            get
            {
                return _playing;
            }
            set
            {
                _playing = value;
            }
        }
        //Constructor
        public Player(string name, double initialSum, bool isDealer = false)
        {
            Name = name;
            Balance = initialSum;
            Hand = new Hand();
            stats = new PlayerStats();
            Playing = true;
            dealer = isDealer;


        }
        //Override operators
        public override string ToString()
        {
            if (!dealer)
            {
                string player;
                player = $"{Name} Balance:{Balance} \n{Hand.ToString()}\nCurrent hand value: {Hand.Value}\n";

                return player;
            }
            else
            {
                string dealer;
                dealer = $"Dealer \n{Hand.ToString()}\nCurrent hand value: {Hand.Value} \n";
                return dealer;
            }
           
        }

    }
}
