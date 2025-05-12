using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rashed_Blackjack
{
    /*
  * Programming 2 – Project – Winter 2025
  * Created by: Jimmy Rashed, 6291812
  * Tested by: Daniel Oleinic
  * Relationship: Friend
  * Date: May 11th, 2025
  *
  * Description: The goal of this class is to store the all information pertaining to a single card. It holds a card's rank, suit, color, and visibility. It contains methods for display, equality checking, and hashcode generation
  */
    public class Card
    {
        //Fields
        private Rank _rank;
        private Suit _suit;
        private ConsoleColor _color;
        private bool _hidden;
        //Properties
        public Rank Rank
        {
            get
            {
                return _rank;
            }
            set
            {
                _rank = value;
            }
        }
        public Suit Suit
        {
            get
            {
                return _suit;
            }
            set
            {
                _suit = value;
            }
        }
        public ConsoleColor Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
            }
        }
        public bool Hidden
        {
            get
            {
                return _hidden;
            }
            set
            {
                _hidden = value;
            }
        }
        //Constructor
        public Card(Rank rank, Suit suit, bool hidden = false)
        {
            Rank = rank;
            Suit = suit;
            //Derive color based on suit
            if ((int)Suit%2 == 0)
            {
                Color = ConsoleColor.DarkGray;
            }
            else
            {
                Color = ConsoleColor.DarkRed;
            }
            Hidden = hidden;
        }
        //Override methods
        /*  ToString() 
         *************************************************
         Purpose: returns the contents of a card as a string
         *************************************************
         @Algorithm:
         Check if card is hidden, return [HIDDEN] if so
         Otherwise
         Add rank and symbol of card to a string
         return string
         *************************************************
         @Param
         Receives no parameters
         *************************************************
         @Exceptions
         None
         *************************************************
         @Returns
         Returns a string containing the contents of the card.
         *************************************************
         @Examples
          Console.WriteLine(mycard.ToString());
         *************************************************
         @Pseudocode
         Check if card.Hidden is true, return [HIDDEN if so]
         else
         Perform switch statement on card suit
               return rank and appropriate suit symbol for each suit
         ************************************************* 
         */
        public override string ToString()
        {
            if (!Hidden)
            {
                string rankString;
                switch (Rank)
                {
                    case Rank.Jack:
                        rankString = "Jack";
                        break;
                    case Rank.Queen:
                        rankString = "Queen";
                        break;
                    case 
                        
                }
                switch ((int)Suit)
                {
                    case 2: //Spades
                        return (Rank + "\u2660");
                    case 3: //Hearts
                        return (Rank + "\u2665");
                    case 1: //Diamonds
                        return (Rank + "\u2666");
                    case 0: //Clubs
                        return (Rank + "\u2663");
                    default:
                        return null;
                }
            }
            else
            {
                return "[Hidden]";
            }
            
        }
        /*  Equals() 
        *************************************************
        Purpose: Returns a boolean value based on the equality 
        of the card with another
        *************************************************
        @Algorithm:
        Check if received object is a card
         if not, return false
        else
        Compare each property of both cards
        if all are equal, return true
        if not all are equal, return false
        *************************************************
        @Param
        Receives 1 parameter: Object obj; holds the potential card to be compared
        *************************************************
        @Exceptions
        None
        *************************************************
        @Returns
        Returns an boolean value based on card equality
        *************************************************
        @Pseudocode
        If statement: if obj not null or not a Card
               return false
        Else
               Convert obj to card
               return equality of all properties
        ************************************************* 
        */
        public override bool Equals(object? obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }
            else
            {
                Card tempCard = (Card)obj;

                return (this.Rank == tempCard.Rank && this.Suit == tempCard.Suit);
            }
        }
        /*  GetHashCode() 
         *************************************************
         Purpose: Returns a unique code for each instance of the object
         *************************************************
         @Algorithm:
         Perform prime multiplication on each field in the class, add each to previous result. Return result
         *************************************************
         @Param
         Receives no parameters
         *************************************************
         @Exceptions
         None
         *************************************************
         @Returns
         Returns a unique int code
         *************************************************
         @Pseudocode
         Declare int hash, set to a prime number
         Set hash to the sum of itself * a prime number * field.GetHashCode()
         repeat operation for each field in the class
         return hash
         ************************************************* 
         */
        public override int GetHashCode()
        {
            int hash = 31;

            hash += hash * 17 * Rank.GetHashCode();
            hash += hash * 17 * Suit.GetHashCode();

            return hash;
        }
        //Override operators
        //All operator overrides perform comparisons using enum values of Suit and Rank.
        public static bool operator >(Card card1, Card card2)
        {
            if (card1.Suit > card2.Suit)
            {
                return true;
            }
            else if (card1.Suit == card2.Suit)
            {
                return card1.Rank > card2.Rank;
            }
            else
            {
                return false;
            }

        }
        public static bool operator <(Card card1, Card card2)
        {
            if (card1.Suit < card2.Suit)
            {
                return true;
            }
            else if (card1.Suit == card2.Suit)
            {
                return card1.Rank < card2.Rank;
            }
            else
            {
                return false;
            }
        }
        public static bool operator >=(Card card1, Card card2)
        {
            return !(card1 < card2);
        }
        public static bool operator <=(Card card1, Card card2)
        {
            return !(card1 > card2);
        }
        public static bool operator ==(Card card1, Card card2)
        {
            return card1.Equals(card2);
        }
        public static bool operator !=(Card card1, Card card2)
        {
            return !card1.Equals(card2);
        }


    }
}
