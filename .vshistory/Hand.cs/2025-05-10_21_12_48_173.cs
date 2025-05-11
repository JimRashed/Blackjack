using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rashed_Blackjack
{
    public class Hand
    {
        
        //Fields
        public List<Card> hand;
        //Properties
        public int Size
        {
            get
            {
                return hand.Count;
            }
        }
        public int Value
        {
            get
            {
                //Add logic to hide value if a card is hidden
                
                int valueSum = 0;
                for (int i = 0; i < Size; i++)
                {
                    valueSum += (int)hand[i].Rank; //Sums up the value of each card in the hand
                }
                if (valueSum > Constants.BLACKJACK) //If the value of the hand is over 21 (blackjack), count aces as 1 (by substracting 10 for every ace in the hand)
                {
                    for (int i = 0; i < Size; i++)
                    {
                        
                        if ((int)hand[i].Rank == 11)
                        {
                            valueSum -= 10;
                        }
                    }
                }
                return valueSum;
            } 
        }
        //Constructors
        public Hand()
        {
            hand = new List<Card>();
        }
        //Override methods
        /*ToString()
         * takes no parameters
         * returns the concatenated value of the ToString method of each card in the hand as a single string
         */
        public override string ToString()
        {
            string handCardString = "";

            for (int currentCard = 0; currentCard < hand.Count; currentCard++)
            {
                handCardString += hand[currentCard].ToString() + " "; //Joins each card ToString return value into a single, returnable string
            }


            return handCardString;
        }
        //Methods
        /*  AddCard() 
          *************************************************
          Purpose: Add a card to the hand
          *************************************************
          @Algorithm:
          Add received parameter to the list of cards
          Sort list 
          *************************************************
          @Param
          Receives 1 parameter: Card
          Card to be added to the hand
          *************************************************
          @Exceptions
          None
          *************************************************
          @Returns
          None
          *************************************************
          @Pseudocode
          Receive Card card
          add card to hand
          Sort()
          ************************************************* 
          */
        public void AddCard(Card card)
        {
            hand.Add(card);
            this.Sort();
        }
        /*  RemoveCard() 
          *************************************************
          Purpose: Removes the last card from the hand
          *************************************************
          @Algorithm:
          Store last card in temporary variable
          remove last card from hand
          order hand
          return temporary variable
          *************************************************
          @Param
          None
          *************************************************
          @Exceptions
          None
          *************************************************
          @Returns
          tempCard: the last card in the deck
          *************************************************
          @Pseudocode
          Declare Card tempCard, set to last card in hand (count-1)
          Remove last card in hand (count-1)
          Call Suit() on this instance of Hand
          return tempCard
          ************************************************* 
          */
        public Card RemoveCard() //Removes the last card in the hand
        {
            if (hand.Count != 0)
            {
                Card tempCard = hand[hand.Count - 1];
                hand.RemoveAt(hand.Count - 1);
                this.Sort();
                return tempCard;
            }
            else
            {
                Console.WriteLine("Attempted to remove a card from an empty hand!");
                return null;
            }
        }
        /*  Contains() 
          *************************************************
          Purpose: Checks if a hand contains a received card
          *************************************************
          @Algorithm:
          Receive a card 
          Scroll through hand
                Check for card equality, return result
          *************************************************
          @Param
          Card target: card to check for in hand
          *************************************************
          @Exceptions
          None
          *************************************************
          @Returns
          presence of the target card in deck as a boolean
          *************************************************
          @Pseudocode
          Begin for loop: for (int currentCard = 0; currentCard< amount of cards in hand; currentCard++)
                Begin if statement: if (hand[currentCard].Equals(target)
                        return true
                End of if
          End of for
            return false
          ************************************************* 
          */
        public bool Contains(Card target)
        {
            for (int currentCard = 0; currentCard < hand.Count; currentCard++)
            {
                if (hand[currentCard].Equals(target))
                {
                    return true;
                }
            }
            return false;
        }
        /* Sort()
          *************************************************
          Purpose: Sorts the cards in the hand from lowest to highest 
          using the selection sort algorithm.
          *************************************************
          @Algorithm:
          Use a nested loop to find the lowest card in the unsorted portion  
          Swap the lowest card with the first unsorted card  
          Repeat until all cards are sorted
          *************************************************
          @Param
          Receives no parameters
          *************************************************
          @Exceptions
          None
          *************************************************
          @Returns
          Returns nothing (void)
          *************************************************
          @Pseudocode
          For outerLoop from 0 to Size - 1
              Set lowestIndex to outerLoop
              For innerLoop from outerLoop + 1 to Size
                  If hand[innerLoop] is smaller than hand[lowestIndex]
                      Set lowestIndex to innerLoop
              Swap hand[outerLoop] and hand[lowestIndex] using temporary variable tempStorage to avoid overwriting
          *************************************************
        */
        public void Sort()
        {
            int lowestIndex;
            Card tempStorage;
            for (int outerLoop = 0; outerLoop < Size; outerLoop++)
            {
                lowestIndex = outerLoop;

                for (int innerLoop = outerLoop + 1; innerLoop < Size; innerLoop++)
                {
                    if (hand[innerLoop] < hand[lowestIndex])
                    {
                        lowestIndex = innerLoop;
                    }
                }
                tempStorage = hand[outerLoop];
                hand[outerLoop] = hand[lowestIndex];
                hand[lowestIndex] = tempStorage;
            }
        }

    }

}
