using System.Text;

namespace Rashed_Blackjack
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            GameManager manager = new GameManager();
            Utility.PrintHeader();
            manager.Start();
        }
    }
}
