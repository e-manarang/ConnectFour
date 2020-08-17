using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectFour
{
    // <summary>
    /// This is the PlayerComputer class that will handle the information of the AI player.
    /// </summary>
    public class PlayerComputer : Player
    {
        private ComputerDifficulty _level;
        private Random _rand;
        
        public ComputerDifficulty ComputerLevel
        {
            get { return _level;  }
        }

        /// <summary>
        /// The PlayerComputer class constructor. Set isHuman to false.
        /// </summary>
        /// <param name="name">Name of the player.</param>
        public PlayerComputer(string name, ComputerDifficulty level) : base(name)
        {
            _level = level;
            _isHuman = false;
            _rand = new Random();
        }

        /// <summary>
        /// This method returns the computer's move. Move based on the difficulty level.
        /// </summary>
        /// <param name="pieces">Array representing pieces on the board.</param>
        /// <returns>Index in the array corresponding to the user's choice.</returns>
        public override int Move(sbyte[] pieces)
        {
            int index = 0;

            switch(_level)
            {
                case ComputerDifficulty.Random:
                    index = Calculation.GetAvailableIndex(pieces, RandomMove(pieces));
                    break;
                case ComputerDifficulty.Easy:
                    index = EasyMove(pieces);
                    break;
                case ComputerDifficulty.Normal:
                    index = NormalMove(pieces);
                    break;
                case ComputerDifficulty.Advanced:
                    index = AdvancedMove(pieces);
                    break;
            }

            return index;
        }

        /// <summary>
        /// Gives a random move depending on column availability.
        /// </summary>
        /// <param name="pieces">Array representing pieces on the board.</param>
        /// <returns>Column index (zero-based).</returns>
        private int RandomMove(sbyte[] pieces)
        {
            List<int> validColumns = new List<int>();
            for(int i = 0; i < 7; i++)
            {
                if (pieces[i] == 0)
                    validColumns.Add(i);
            }

            return validColumns[_rand.Next(0, validColumns.Count - 1)];
        }

        /// <summary>
        /// Returns a move based on easy difficulty AI. Will try to form four tokens in a line.
        /// </summary>
        /// <param name="pieces">Array representing the board.</param>
        /// <returns>Column index (zero-based).</returns>
        private int EasyMove(sbyte[] pieces)
        {
            // Copy array as to not touch the original game array.
            sbyte[] copy = new sbyte[42];
            Array.Copy(pieces, copy, 42);

            // This will track the scores per indeces.
            Dictionary<int, int> indexScore = GetValidIndeces(pieces);

            // Get score for placement.
            Calculation.ScoreForPlacement(Token, copy, ref indexScore);
            // Block if opponent has 2 in a line.
            Calculation.ScoreForEasyBlocking(Token, copy, ref indexScore);

            // Get highest score and return that index.
            int index = 0;
            int highest = 0;
            foreach(int key in indexScore.Keys)
            {
                if (indexScore[key] >= highest)
                {
                    highest = indexScore[key];
                    index = key;
                }
            }

            if (highest == 0) //everything has equal worth and is zero which has a very low possibility of happening.
                index = Calculation.GetAvailableIndex(pieces, RandomMove(pieces));
            return index;
        }

        /// <summary>
        /// Returns a move based on normal difficulty. Will try to form four tokens in a row and block an opponent.
        /// </summary>
        /// <param name="pieces">Array representing the board.</param>
        /// <returns>Column index (zero-based).</returns>
        private int NormalMove(sbyte[] pieces)
        {
            // Copy array as to not touch the original game array.
            sbyte[] copy = new sbyte[42];
            Array.Copy(pieces, copy, 42);

            // This will track the scores per indeces.
            Dictionary<int, int> indexScore = GetValidIndeces(pieces);

            // Get score for placement.
            Calculation.ScoreForPlacement(Token, copy, ref indexScore);
            // Get score for blocking.
            Calculation.ScoreForBlocking(Token, copy, ref indexScore);

            // Get highest score and return that index.
            int index = 0;
            int highest = 0;
            foreach (int key in indexScore.Keys)
            {
                if (indexScore[key] >= highest)
                {
                    highest = indexScore[key];
                    index = key;
                }
            }

            if (highest == 0) //everything has equal worth and is zero which has a very low possibility of happening
                index = Calculation.GetAvailableIndex(pieces, RandomMove(pieces));

            return index;
        }

        /// <summary>
        /// Returns a move based on advanced difficulty. Will try to form four tokens in a row, block enemy move, and plan a little.
        /// </summary>
        /// <param name="pieces">Array representing the board.</param>
        /// <returns>Column index (zero-based).</returns>
        private int AdvancedMove(sbyte[] pieces)
        {
            // Copy array as to not touch the original game array.
            sbyte[] copy = new sbyte[42];
            Array.Copy(pieces, copy, 42);

            // This will track the scores per indeces.
            Dictionary<int, int> indexScore = GetValidIndeces(pieces);

            // Get score for placement.
            Calculation.ScoreForPlacement(Token, copy, ref indexScore);
            // Get score for blocking.
            Calculation.ScoreForBlocking(Token, copy, ref indexScore);
            // Get deduction for giving opportunity to opponent.
            Calculation.ScoreForGiving(Token, copy, ref indexScore);
            // Get deduction for losing an opporunity with a move.
            Calculation.ScoreForLosing(Token, copy, ref indexScore);

            // Get highest score and return that index.
            int index = 0;
            int highest = 0;
            foreach (int key in indexScore.Keys)
            {
                if (indexScore[key] >= highest)
                {
                    highest = indexScore[key];
                    index = key;
                }
            }

            if (highest == 0) //everything has equal worth and is zero which has a very low possibility of happening
                index = Calculation.GetAvailableIndex(pieces, RandomMove(pieces));

            return index;
        }

        /// <summary>
        /// This function will get all valid indeces where a token can be placed (one per column). 
        /// </summary>
        /// <param name="pieces">Array representing the board.</param>
        /// <returns>List of all possible token placed on a specific turn.</returns>
        private Dictionary<int, int> GetValidIndeces(sbyte[] pieces)
        {
            Dictionary<int, int> validIndeces = new Dictionary<int, int>();

            for (int i = 0; i <= 6; i++)
            {
                if (pieces[i] == 0)
                {
                    // Get index and enter 0 for current score.
                    validIndeces.Add(Calculation.GetAvailableIndex(pieces, i), 0);
                }
            }

            return validIndeces;
        }
    }
}
