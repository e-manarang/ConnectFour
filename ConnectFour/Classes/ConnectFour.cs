using System;

namespace ConnectFour
{
    /// <summary>
    ///  This class handles the flow and implements the rules regarding the game Connect Four
    /// </summary>
    public class ConnectFour
    {
        #region Members and Constructor

        /// <summary>
        /// This array records which position is held by a player. 
        ///     A value of 1 will be for First Player and -1 for Second Player.
        /// </summary>
        private sbyte[] _pieces;
        private byte _moveCount = 0;
        private Player _player1;
        private Player _player2;

        /// <summary>
        ///  Constructor of ConnectFour class.
        /// </summary>
        public ConnectFour()
        {
            // Initialize game board state
            _pieces = new sbyte[42];
            _moveCount = 0;
        }

        #endregion

        #region Public Method

        /// <summary>
        /// New Game will be the entry point of the game and will handle all parts of the game.
        /// </summary>
        public void NewGame()
        {
            // Setup both players
            SetupPlayer();
            SetupOpponent();

            // We selected a player randomly. This will be the second player as we need to change players
            //  everytime the loop starts.
            Player currentPlayer = SelectRandomPlayer();
            int index = 0;
            do
            {
                currentPlayer = GetOpponent(currentPlayer);

                if (currentPlayer.IsHuman)
                    Display.DisplayBoard(_pieces, true);

                index = currentPlayer.Move(_pieces);
                // if index is < 0, then the Player chose quit.
                if (index < 0)
                {
                    Quit(currentPlayer);
                    Console.WriteLine();
                    return;
                }
                else
                {
                    _pieces[index] = currentPlayer.Token;
                }

                _moveCount++;
            }
            while (!CheckEndGame(currentPlayer, index));
            Console.WriteLine();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Sets the information of Player.
        /// </summary>
        private void SetupPlayer()
        {
            string str;
            Console.WriteLine();

            // Get Player 1 Name
            do
            {
                Console.Write("Enter name of Player 1: ");
                str = Console.ReadLine().Trim();
            }
            while (str.Length == 0);
            
            // Initialize Player 1.
            _player1 = new PlayerHuman(str);
            _player1.PlayerColor = ConsoleColor.Red;
            _player1.Token = 1;
            Console.WriteLine();
        }

        /// <summary>
        /// Sets the information of the Opponent.
        /// </summary>
        private void SetupOpponent()
        {
            string str;

            // Ask if opponent is human or computer. Check if name is not whitespace.
            do
            {
                Display.MenuSelectOpponent(_player1.Name, _player1.PlayerColor);
                str = Console.ReadLine().Trim().ToUpper();
                if (str.Length == 0)
                    str = "X";
            }
            while (str[0] != 'H' && str[0] != 'C');
            Console.WriteLine();

            bool isHuman = str[0] == 'H' ? true : false;
            if (isHuman)
            {
                // If human, ask opponent name and check if not whitespace.
                do
                {
                    Console.Write("Enter name of Player 2: ");
                    str = Console.ReadLine().Trim();

                    // To avoid confusion, player names must be different.
                    if (str.ToUpper() == _player1.Name.ToUpper())
                    {
                        Console.WriteLine("Sorry, but {0} is already taken!", str);
                    }
                }
                while (str.Length == 0 || str.ToUpper() == _player1.Name.ToUpper());

                _player2 = new PlayerHuman(str);
            }
            else
            {
                // If computer, ask difficulty of opponent.
                do
                {
                    Display.MenuSelectDifficulty();
                    str = Console.ReadLine().Trim().ToUpper();
                    if (str.Length == 0)
                        str = "X";
                }
                while (str[0] != '0' && str[0] != '1' && str[0] != '2' && str[0] != '3');

                ComputerDifficulty level = ComputerDifficulty.Random;
                if (str[0] == '1')
                    level = ComputerDifficulty.Easy;
                else if (str[0] == '2')
                    level = ComputerDifficulty.Normal;
                else if (str[0] == '3')
                    level = ComputerDifficulty.Advanced;

                _player2 = new PlayerComputer("Computer(" + level.ToString() + ")", level);
            }

            _player2.PlayerColor = ConsoleColor.Yellow;
            _player2.Token = -1;
        }

        /// <summary>
        /// Randomly selects which player to move first.
        /// </summary>
        /// <returns></returns>
        private Player SelectRandomPlayer()
        {
            Random random = new Random();
            var select = random.Next(2);
            if (select == 0)
            {
                return _player1;
            }
            else
            {
                return _player2;
            }
                
        }

        /// <summary>
        /// Checks if game has already ended either by a win or by draw or by quitting.
        /// </summary>
        /// <returns>Returns true if a player wins of board is filled or a player quits, otherwise false.</returns>
        private bool CheckEndGame(Player player, int index)
        {
            // Check first if winning move.
            if (CheckWin(player, index))
                return true;
            // Then check if draw.
            else if (CheckDraw())
                return true;
            // Not end of the game.
            else
                return false;
        }

        /// <summary>
        ///Checks if there is already a winner.
        /// </summary>
        /// <param name="player">Current player.</param>
        /// <returns>True if player wins, else false.</returns>
        private bool CheckWin(Player player, int index)
        {
            bool isWin = false;

            if (Calculation.CheckVerticalWin(_pieces, player.Token, index))
                isWin = true;
            else if (Calculation.CheckHorizontalWin(_pieces, player.Token, index))
                isWin = true;
            else if (Calculation.CheckDiagonalWin1(_pieces, player.Token, index))
                isWin = true;
            else if (Calculation.CheckDiagonalWin2(_pieces, player.Token, index))
                isWin = true;

            if (isWin)
            {
                Display.DisplayBoard(_pieces, false);
                Display.MessageWin(player.Name, player.PlayerColor);
            }

            return isWin;
        }

        /// <summary>
        /// Checks if the board is filled.
        /// </summary>
        /// <returns></returns>
        private bool CheckDraw()
        {
            bool endinDraw = _moveCount >= 42;
            if (endinDraw)
            {
                Display.DisplayBoard(_pieces, false);
                Display.MessageDraw(_player1.Name, _player1.PlayerColor, _player2.Name, _player2.PlayerColor);
            }
            return endinDraw;
        }

        /// <summary>
        /// If a player quits, display message.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        private void Quit(Player player)
        {
            Player opponent = GetOpponent(player);
            Console.WriteLine();
            Display.MessageResignation(player.Name, player.PlayerColor, opponent.Name, opponent.PlayerColor);
        }

        /// <summary>
        /// Returns the opponent of the supplied player.
        /// </summary>
        /// <param name="player">Either the player or the opponent.</param>
        /// <returns>The player opposite the player supplied in the paramenter/</returns>
        private Player GetOpponent(Player player)
        {
            if (player == _player1)
                return _player2;
            else
                return _player1;
        }

        #endregion
    }
}
