using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rashed_Blackjack
{
    public class Card
    {
        //Fields
        private Rank _rank;
        private Suit _suit;
        private ConsoleColor _color;
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
        //Constructor
        public Card(Rank rank, Suit suit)
        {
            Rank = rank;
            Suit = suit;
            //Derive color based on suit
            if ((int)Suit%2 == 0)
            {
                Color = ConsoleColor.Black;
            }
            else
            {
                Color = ConsoleColor.Red;
            }
        }
       //public Card (int cardNumber)

        //Override methods
        /*  ToString() 
         *************************************************
         Purpose: returns the contents of a card as a string
         *************************************************
         @Algorithm:
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
         Returns an integer greater than minValue and less
         er than maxValue
         *************************************************
         @Examples
          Console.WriteLine(mycard.ToString());
         *************************************************
         @Pseudocode
         Perform switch statement on card suit
               return rank and appropriate suit symbol for each suit
               if no suit, return appropriate joker color
         ************************************************* 
         */
        public override string ToString()
        {
            /*
            return (Rank.ToString() + " of " + Suit.ToString());
            */
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
