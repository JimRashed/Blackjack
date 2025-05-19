using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rashed_Blackjack
{
    public class LeaderboardEntry
    {
        private string _name;
        private int _losses;
        private int _wins;
        private int _ties;
        private int _score;

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
        public int Losses
        {
            get
            {
                return _losses;
            }
            set
            {
                _losses = value;
            }
        }
        public int Wins
        {
            get
            {
                return _wins;
            }
            set
            {
                _wins = value;
            }
        }
        public int Ties
        {
            get
            {
                return _ties;
            }
            set
            {
                _ties = value;
            }
        }
        public int Score
        {
            get
            {
                return _score; 
            }
            set
            {
                _score = value;
            }
        }
        public double winRatio
        {
            get
            {
                int matchesPlayed = Losses + Wins + Ties;
                //return Losses + Wins + Ties == 0 ? 0 : (Wins / Losses + Wins + Ties); //faulty ass line of code.
                if (matchesPlayed == 0)
                {
                    return 0;
                }
                else
                {
                    return (double)Wins / (double)matchesPlayed;
                }
            }
        }
        public LeaderboardEntry()
        {
            Name = null;
            Losses = 0;
            Wins = 0;
            Ties = 0;
            Score = 0;
        }
        public LeaderboardEntry(string name, int losses = 0, int wins = 0, int ties = 0, int score = 0)
        {
            Name = name;
            Losses = losses;
            Wins = wins;
            Ties = ties;
            Score = score;
        }
        /*
       *************************************************
       * Modify(Outcomes outcome)
       *************************************************
       * Purpose: Updates the score, wins, losses, or ties of a leaderboard entry based on the outcome of a round.
       *************************************************
       * @Algorithm:
       * 1. Increase the Score by the integer value of the outcome.
       * 2. If the outcome is Blackjack or Win, increment the Wins count.
       * 3. Else if the outcome is Loss or Bust, increment the Losses count.
       * 4. Else (outcome is Tie), increment the Ties count.
       *************************************************
       * @Param
       * outcome: The result of the round (Blackjack, Win, Loss, Bust, or Tie).
       *************************************************
       * @Exceptions
       * None
       *************************************************
       * @Returns
       * Returns nothing.
       *************************************************
       * @Pseudocode:
       * Score = Score + (integer value of outcome)
       * if outcome is Blackjack or outcome is Win
       * Wins = Wins + 1
       * else if outcome is Loss or outcome is Bust
       * Losses = Losses + 1
       * else // outcome is Tie
       * Ties = Ties + 1
       *************************************************
       */
        public void Modify(Outcomes outcome)
        {
            Score += (int)outcome;
            if (outcome == Outcomes.Blackjack || outcome == Outcomes.Win)
            {
                Wins++;
            }
            else if (outcome == Outcomes.Loss || outcome == Outcomes.Bust)
            {
                Losses++;
            }
            else
            {
                Ties++;
            }
        }
        //Returns a string containing formatted LeaderboardEntry data
        public override string ToString()
        {
            return $" {Name} | {Score} | {winRatio*100}% | {Wins} | {Ties} | {Losses} |";
        }
    }
}
