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
        public static bool Bust(Hand hand)
        {
            return (hand.Value > Constants.BLACKJACK);
        }
        public static bool Blackjack(Hand hand)
        {
            return (hand.Size == Constants.BLACKJACKCARDAMOUNT && hand.Value == Constants.BLACKJACK);
        }
        
        public static string Outcome(Player player, Player dealer)
        {
            if (Bust(player.Hand)){
                return "Loss";
            }
            else if (Bust(dealer.Hand))
            {
                if (Blackjack(player.Hand))
                {
                    return "Blackjack";
                }
                else
                {
                    return "Win";
                }
            }
            else if (player.Hand.Value > dealer.Hand.Value)
            {
                return "Win";
            }
            else if (player.Hand.Value < dealer.Hand.Value)
            {
                return "Loss";
            }
            else
            {
                return "Tie";
            }
        }
        public static double Payout(Player player, Player dealer)
        {
            switch (Outcome(player, dealer))
            {
                case "Win":
                    player.Balance += (player.Bet * 2); //Adds 2x the player's bet to their balance
                    player.Bet = 0; //Clear the player's bet
                    break;
                case "Blackjack":
                    player.Balance += (player.Bet * 2.5)//Adds 2.5X the player's bet to their balance
                    break;
                case "Loss":
                    
            }
        }
         
        
    }
}
