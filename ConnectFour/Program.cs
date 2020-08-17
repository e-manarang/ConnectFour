using System;

namespace ConnectFour
{
    class Program
    {
        /// <summary>
        /// Entry point of the program.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.Title = "Connect Four";
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("==============");
            Console.WriteLine(" Connect Four ");
            Console.WriteLine("==============");

            ConnectFour game;
            string play;

            do
            {
                game = new ConnectFour();
                game.NewGame();
                do
                {
                    Console.Write("Play Again [Y/N]? ");
                    play = Console.ReadLine().Trim().ToUpper();

                    if (play.Length == 0)
                        play = "X";
                }
                while (play[0] != 'Y' && play[0] != 'N');
                
            }
            while (play[0] == 'Y');
        }
    }
}
