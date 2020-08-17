using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectFour
{
    /// <summary>
    /// This class contains functions used for displaying the Board and Messages.
    /// </summary>
    internal static class Display
    {
        #region Constants

        private const int topLeft = 0x2554;
        private const int topRight = 0x2557;
        private const int bottomLeft = 0x255A;
        private const int bottomRight = 0x255D;
        private const int horizontal = 0x2550;
        private const int vertical = 0x2551;
        private const int topMid = 0x2566;
        private const int bottomMid = 0x2569;
        private const int leftMid = 0x2560;
        private const int rightMid = 0x2563;
        private const int mid = 0x256c;

        #endregion

        #region Board State

        /// <summary>
        /// This function will display the current Game Board state based on the position of pieces
        ///     in the pieces array.
        /// </summary>
        /// <param name="pieces">State of the board game (places of game pieces).</param>
        public static void DisplayBoard(sbyte[] pieces, bool includeOptions)
        {
            Console.Clear();
            Console.WriteLine();
            // Print the top horizontal border of the board.
            PrintBoardHorizontal(BoardBorder.Top);

            int pieceCount = 0;
            // Print the middle part of the board
            for(int row = 0; row < 6; row++)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;

                for (int column = 0; column < 7; column++)
                {
                    pieceCount = (row * 7) + column;
                    Console.Write((char)vertical);
                    PrintPiece(pieces[pieceCount]);
                }

                Console.WriteLine((char)vertical);

                if (row < 5)
                    PrintBoardHorizontal(BoardBorder.Middle);
            }

            // Print the bottom horizontal border of the board.
            PrintBoardHorizontal(BoardBorder.Bottom);

            if (includeOptions)
            {
                // Print all the row columns. If row is filled to the top, do not include number.
                Console.ForegroundColor = ConsoleColor.White;
                for (int num = 0; num < 7; num++)
                {
                    if (pieces[num] == 0)
                        Console.Write("  {0} ", num + 1);
                    else
                        Console.Write("    ");
                }
                Console.WriteLine("   [Q] Quit " );
            }
            
        }

        /// <summary>
        /// Prints the border of the board at either top, bottom, or middle.
        /// </summary>
        /// <param name="pos">1 if top, -1 if bottom, 0 if divider</param>
        private static void PrintBoardHorizontal(BoardBorder pos)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.BackgroundColor = ConsoleColor.Black;

            // Check which part of theboard to print because they use different characters.
            if (pos == BoardBorder.Top)
                Console.Write((char)topLeft);
            else if (pos == BoardBorder.Bottom)
                Console.Write((char)bottomLeft);
            else
                Console.Write((char)leftMid);

            // Print the horizontal character lines.
            for (int slots = 1; slots <= 7; slots++)
            {
                Console.Write((char)horizontal);
                Console.Write((char)horizontal);
                Console.Write((char)horizontal);

                // Check which part of theboard to print because they use different characters.
                if (slots < 7)
                {
                    if (pos == BoardBorder.Top)
                        Console.Write((char)topMid);
                    else if (pos == BoardBorder.Bottom)
                        Console.Write((char)bottomMid);
                    else
                        Console.Write((char)mid);
                }
            }

            // Check which part of theboard to print because they use different characters.
            if (pos == BoardBorder.Top)
                Console.WriteLine((char)topRight);
            else if (pos == BoardBorder.Bottom)
                Console.WriteLine((char)bottomRight);
            else
                Console.WriteLine((char)rightMid);

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// This function will print the corresponding piece on the board.
        /// </summary>
        /// <param name="val">0 is Empty, 1 is Red(X), -1 is Yellow(O)</param>
        private static void PrintPiece(sbyte piece)
        {
            if (piece > 0)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.Red;
                Console.Write(" X ");
            }
            else if (piece < 0)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.Write(" O ");
            }
            else
                Console.Write("   ");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.BackgroundColor = ConsoleColor.Black;

        }

        #endregion

        #region Messages

        /// <summary>
        /// Prints the player name with the corresponding color.
        /// </summary>
        /// <param name="name">Player name.</param>
        /// <param name="color">Player color.</param>
        private static void WritePlayerName(string name, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write("Player {0}", name);
            Console.ForegroundColor = ConsoleColor.White;

        }

        /// <summary>
        /// Displays a reminder that the player need to make a move.
        /// </summary>
        /// <param name="name">Player name.</param>
        /// <param name="color">Player color.</param>
        public static void MessagePlayerTurn(string name, ConsoleColor color)
        {
            Console.Write("Your move ");
            WritePlayerName(name, color);
            Console.Write(" : ");
        }

        /// <summary>
        /// Display a message to select an opponent.
        /// </summary>
        /// <param name="name">Player name.</param>
        /// <param name="color">Player color.</param>
        public static void MenuSelectOpponent(string name, ConsoleColor color)
        {
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("Hello ");
            WritePlayerName(name, color);
            Console.WriteLine("! Please select your opponent... ");
            Console.WriteLine(" [H] Human");
            Console.WriteLine(" [C] Computer");
            Console.Write("Choice : ");
        }

        /// <summary>
        /// Displays message to ask for computer difficulty.
        /// </summary>
        public static void MenuSelectDifficulty()
        {
            Console.WriteLine("Please select Computer difficulty...");
            Console.WriteLine(" [0] Very Easy (Moves randomly.)");
            Console.WriteLine(" [1] Easy (Will try to ConnectFour and block a little.)");
            Console.WriteLine(" [2] Normal (Will try to ConnectFour and block defensively.)");
            Console.WriteLine(" [3] Advance (Will try to ConnectFour, block and plan a little.)");
            Console.Write("Choice : ");
        }

        /// <summary>
        /// Displays message that the player selected to quit game.
        /// </summary>
        /// <param name="playerName">Player name.</param>
        /// <param name="playerColor">Player color.</param>
        /// <param name="opponentName">Opponent name.</param>
        /// <param name="opponentColor">opponent color.</param>
        public static void MessageResignation(string playerName, ConsoleColor playerColor,
            string opponentName, ConsoleColor opponentColor)
        {
            WritePlayerName(playerName, playerColor);
            Console.Write(" quits...");
            WritePlayerName(opponentName, opponentColor);
            Console.WriteLine(" wins!");
        }

        /// <summary>
        /// Displays a message that the game ended in a draw.
        /// </summary>
        /// <param name="playerName">Player 1 name.</param>
        /// <param name="playerColor">Player 1 color.</param>
        /// <param name="opponentName"></param>
        /// <param name="opponentColor"></param>
        public static void MessageDraw(string playerName, ConsoleColor playerColor,
            string opponentName, ConsoleColor opponentColor)
        {
            Console.Write("Game between ");
            WritePlayerName(playerName, playerColor);
            Console.Write(" and ");
            WritePlayerName(opponentName, opponentColor);
            Console.WriteLine(" ends in a Draw!");
        }

        /// <summary>
        /// Displays a message that the player won the game.
        /// </summary>
        /// <param name="name">Winner's name.</param>
        /// <param name="color">Winner's color.</param>
        public static void MessageWin(string name, ConsoleColor color)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("      =================");
            Console.WriteLine("       CONNECT FOUR!!! ");
            Console.WriteLine("      =================");
            Console.WriteLine();
            WritePlayerName(name, color);
            Console.WriteLine(" wins!");
        }
        #endregion
    }
}
