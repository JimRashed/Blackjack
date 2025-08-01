﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rashed_Blackjack
{
    //An enum containing the possible outcomes of a game. The names are self-explanatory, and the values are used in the calculation of a player's score.
    public enum Outcomes
    {
        Win = 1, //A win will raise the player's score by 1. Same logic applies to the rest of the elements.
        Loss = -1,
        Tie = 0,
        Blackjack = 2,
        Bust = -2,
    }
}
