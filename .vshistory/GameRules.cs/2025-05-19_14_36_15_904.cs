using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace Rashed_Blackjack
{
    public static class GameRules
    {
        /*Bust(Hand hand)
        *************************************************
        * Purpose: Determines if a hand's value exceeds the Blackjack limit.
        *************************************************
        * @Algorithm:
        * 1. Check if the value of the hand is greater than the constant BLACKJACK.
        * 2. Return true if it is, false otherwise.
        *************************************************
        * @Param
        * hand: The hand to check.
        *************************************************
        * @Exceptions
        * None
        *************************************************
        * @Returns
        * Boolean declaring if hand value is over 21
        *************************************************
        * @Pseudocode:
        * return hand.Value > Constants.BLACKJACK
        *************************************************
        */
        public static bool Bust(Hand hand)
        {
            return (hand.Value > Constants.BLACKJACK);
        }
        /*Blackjack(Hand hand)
       *************************************************
       * Purpose: Determines if a hand qualifies as a Blackjack (two cards totaling 21).
       *************************************************
       * @Algorithm:
       * 1. Check if the hand size is equal to 2
       * 2. Check if the hand value is equal to 21
       * 3. Return true if both conditions are met, false otherwise.
       *************************************************
       * @Param
       * hand: The hand to check.
       *************************************************
       * @Exceptions
       * None
       *************************************************
       * @Returns
       * boolean declaring if hand is a blackjack 
       *************************************************
       * @Pseudocode:
       * return hand.Size == Constants.BLACKJACKCARDAMOUNT and hand.Value == Constants.BLACKJACK
       *************************************************
       */
        public static bool Blackjack(Hand hand)
        {
            return (hand.Size == Constants.BLACKJACKCARDAMOUNT && hand.Value == Constants.BLACKJACK);
        }
        public static Outcomes Outcome(Player player, Player dealer)
        {
            if (Bust(player.Hand)){
                return Outcomes.Bust;
            }
            else if (Bust(dealer.Hand))
            {
                if (Blackjack(player.Hand))
                {
                    return Outcomes.Blackjack;
                }
                else
                {
                    return Outcomes.Win;
                }
            }
            else if (Blackjack(player.Hand))
            {
                return Outcomes.Blackjack;
            }
            else if (player.Hand.Value > dealer.Hand.Value)
            {
                return Outcomes.Win;
            }
            else if (player.Hand.Value < dealer.Hand.Value)
            {
                return Outcomes.Loss;
            }
            else
            {
                return Outcomes.Tie;
            }
        }
        public static bool NoOnePlaying(List<Player> players)
        {
            bool noOnePlaying = true;
            for (int currentPlayer = 0; currentPlayer< players.Count; currentPlayer++)
            {
                if (players[currentPlayer].Playing)
                {
                    noOnePlaying = false;
                }
            }
            return noOnePlaying;
        }
        
    }
}
