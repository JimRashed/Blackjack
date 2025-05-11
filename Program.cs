using System.Text;

namespace Rashed_Blackjack
        {/*
        * Programming 2 – Project – Winter 2025
        * Created by: Jimmy Rashed, 6291812
        * Tested by: Daniel Oleinic
        * Relationship: Friend
        * Date: May 11th, 2025
        *
        * Description: The goal of this program is to create a fully playable version of the card game "Blackjack" in C# .NET with additional file saving/loading features and a leaderboard. 
        */

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            GameManager manager = new GameManager();
            Utility.PrintHeader(); //Prints program header
            manager.Start(); //Stars the game proper.
        }
    }
}
