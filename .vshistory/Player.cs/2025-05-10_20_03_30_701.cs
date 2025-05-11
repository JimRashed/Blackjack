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
            Playing = true;
            dealer = isDealer;


        }
        //Override operators
        public override string ToString() //Adjust output to be more descriptive. Say BUST if they get a bust.
        {
            if (!dealer)
            {
                string player;
                player = $"{Name} Balance:{Balance.ToString("C")} \nCurrent bet: {Bet.ToString("C")}\nHand: {Hand.ToString()}\nCurrent hand value: {Hand.Value}\n";

                return player;
            }
            else
            {
                string dealer;
                if (ContainsHiddenCards())
                {
                    dealer = $"Dealer \nHand:{Hand.ToString()}\nCurrent hand value: [UNKNOWN] \n";
                }
                else
                {
                    dealer = $"Dealer \nHand:{Hand.ToString()}\nCurrent hand value: {Hand.Value} \n";
                }
                return dealer;
            }
           
        }
        public void Print()
        {
            if (!dealer)
            {
                Console.WriteLine($"{Name} Balance: {Balance.ToString("C")}");
                Console.WriteLine($"Current bet: {Bet.ToString("C")}");
                Console.Write($"Hand: ");
                //Print each hand in appropriate color
                for (int currentCard = 0; currentCard < Hand.Size; currentCard++)
                {
                    Console.ForegroundColor = Hand.hand[currentCard].Color;
                    Console.Write($"{Hand.hand[currentCard].ToString()} ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.Write("\n");
                
                Console.Write($"Current hand value: ");
                if (Hand.Value > Constants.BLACKJACK)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(Hand.Value);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (Hand.Value == Constants.BLACKJACK)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(Hand.Value);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.Write(Hand.Value);
                }
                Console.WriteLine("");

                if (GameRules.Blackjack(Hand))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("BLACKJACK!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            else
            {

            }
        }
        public bool ContainsHiddenCards()
        {
            bool hasHidden = false;
            for (int currentCard = 0; currentCard< Hand.Size; currentCard++)
            {
                if (Hand.hand[currentCard].Hidden == true)//Checks if hand contains any cards that are hidden
                {
                    hasHidden = true;
                }
            }
            return hasHidden;
        }

    }
}
