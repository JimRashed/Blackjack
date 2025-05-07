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
         
        
    }
}
