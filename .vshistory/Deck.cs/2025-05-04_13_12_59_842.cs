using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rashed_Blackjack
{
    /*Deck class
     * 
     * 
    *Programming 2 - Assignment 5 – Winter 2025
    * Created by: Jimmy Rashed 6291812
    * Tested by: Daniel Oleinic
    * Relationship: friend
    * Date: April 15, 2025
    *
    * Description: The goal of this class is to define the behavior of a card deck. it holds a list of cards as a property and a random for card shuffling. It holds 4 properties: Draw, peek, placeOnTop, and shuffle.
    */
    public class Deck
    {
        //Fields
        public List<Card>? cardList;
        private static Random random = new Random();

        //Properties
        public int CardsLeft
        {
            get
            {
                return cardList.Count;
            }
        }

        //Constructors
        public Deck()
        {
            cardList = new List<Card>(); //Creates a full, shuffled deck of 52 cards.
            Card tempCard;
            /*
            for (int i = 1; i <= 52; i++) //Ensures that 52 different cards are created in order.  1 to 52.
            {
                Card tempCard = new Card(i);
                cardList.Add(tempCard);
            }
            this.Shuffle(); //Shuffles cards in deck
            */
            foreach (Suit suit in Enum.GetValues(typeof(Suit))){ //Documentation for enum iteration found at https://stackoverflow.com/questions/972307/how-to-loop-through-all-enum-values-in-c
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    tempCard = new Card(rank, suit);
                    cardList.Add(tempCard);
                }
            }
            this.Shuffle();
        } 
        //Override methods
        /*ToString()
         * takes no parameters
         * returns the return value of the CardsLeft property as a string.
         */
        public override string ToString()
        {
            return Convert.ToString(CardsLeft);
        }
        //Methods
        /*  Draw() 
          *************************************************
          Purpose: Removes the top card of the deck and returns it.
          *************************************************
          @Algorithm:
          Store top card in a temporary variable
          remove top card from list
          return temporary variable
          *************************************************
          @Param
          Receives no parameters
          *************************************************
          @Exceptions
          None
          *************************************************
          @Returns
          Returns a Card corresponding to the top card of the deck
          *************************************************
          @Pseudocode
          Declare Card tempCard, set to top card in (count-1)
          Perform removeAt on list with last index (count-1(
          return tempCard
          ************************************************* 
          */
        public Card Draw()
        {
            Card tempCard = cardList[cardList.Count - 1]; //Stores the top card in the deck in a temporary variable
            cardList.RemoveAt(cardList.Count - 1);
            return tempCard;
        }
        /*  Peek() 
          *************************************************
          Purpose: returns the top card of the deck without removing it
          *************************************************
          @Algorithm:
          Check if cards are left in deck
                If yes, return last card
                if not, return null
          *************************************************
          @Param
          Receives no parameters
          *************************************************
          @Exceptions
          None
          *************************************************
          @Returns
          Returns a card corresponding to the top card of the deck
          *************************************************
          @Pseudocode
          if (cardsLeft != 0)
                return top card of list (count-1)
          else
                return null
          ************************************************* 
          */
        public Card Peek()
        {
            if (CardsLeft != 0)
            {
                return cardList[cardList.Count - 1]; //Returns the last card in the last (top card)
            }
            else
            {
                return null;
            }
        }
        /*  PlaceOnTop() 
          *************************************************
          Purpose: Add a card to the deck
          *************************************************
          @Algorithm:
          Add received parameter to the list
          *************************************************
          @Param
          Receives 1 parameter: Card
          Card to be added to the deck
          *************************************************
          @Exceptions
          None
          *************************************************
          @Returns
          None
          *************************************************
          @Pseudocode
          Receive Card cardToAdd
          Add cardToAdd to cardList
          ************************************************* 
          */
        public void PlaceOnTop(Card cardToAdd)
        {
            cardList.Add(cardToAdd);
        }
        /* Shuffle() 
          *************************************************
          Purpose: Randomly shuffles cards in the deck
          *************************************************
          @Algorithm:
          Create temporary card
          Create variable to hold the current index being switched
          Scroll through deck
                for current card, set targeted index to a random number from the current card index to the end of the deck
                Store card at targeted index in temporary card variable
                replace card at targeted index with current card
                replace current card index with temporary card
          *************************************************
          @Param
          None
          *************************************************
          @Exceptions
          None
          *************************************************
          @Returns
          None
          *************************************************
          @Pseudocode
          Declare Card tempCard
          Declare int targetedIndex
          Begin for loop : (currentCard = 0; currentCard< length of list; currentCard++)
                set targetedIndex to random.Next(currentCard, cardList.count)
                set tempCard to cardList[targetedIndex]
                set cardList[targetedIndex] to cardList[currentCard]
                set cardList[currentCard] to tempCard
          ************************************************* 
          */
        public void Shuffle()
        {
            Card tempCard;
            int targetedIndex;
            for (int currentCard = 0; currentCard < cardList.Count; currentCard++)
            {
                //Switches current card with a card at a random index greater than itself (no need to shuffle twice)
                targetedIndex = random.Next(currentCard, cardList.Count);

                tempCard = cardList[targetedIndex]; //Store card

                cardList[targetedIndex] = cardList[currentCard]; //Replace card

                cardList[currentCard] = tempCard; //Switch card with stored card

            }
        }
    }
}
