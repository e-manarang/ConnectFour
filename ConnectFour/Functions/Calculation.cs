using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectFour
{
    /// <summary>
    /// This class contains functions used for calculating figures needed for the board.
    /// </summary>
    static class Calculation
    {
        /// <summary>
        /// Gets the index value of the lowest available slot in the column.
        /// </summary>
        /// <param name="pieces">Array representing the board.</param>
        /// <param name="choice">Selected column index (zero-based).</param>
        /// <returns></returns>
        public static int GetAvailableIndex(sbyte[] pieces, int colIndex)
        {
            int index = colIndex;
            for (int i = colIndex; i <= 41; i += 7)
            {
                if (pieces[i] == 0)
                    index = i;
                else
                    break;
            }

            return index;
        }

        /// <summary>
        /// This function checks if there is a Connect Four in a row containing the index.
        /// </summary>
        /// <param name="pieces">Array representing board state.</param>
        /// <param name="token">Token of player saved in the array.</param>
        /// <param name="index">Index where a new token is placed.</param>
        /// <returns>True if there is a connect four.</returns>
        public static bool CheckHorizontalWin(sbyte[] pieces, sbyte token, int index)
        {
            // Limit is 3 spaces from the index.
            int lowerLimit = GetLimitFromReference(index, Direction.Left);
            int upperLimit = GetLimitFromReference(index, Direction.Right);

            // Check if there is a connect four.
            for (int i = lowerLimit; i <= upperLimit - 3; i++)
            {
                if (pieces[i] + pieces[i + 1] + pieces[i + 2] + pieces[i + 3] == 4 * token)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// This function checks if there is a Connect Four in a column containing the index.
        /// </summary>
        /// <param name="pieces">Array representing board state.</param>
        /// <param name="token">Token of player saved in the array.</param>
        /// <param name="index">Index where a new token is placed.</param>
        /// <returns>True if there is a connect four.</returns>
        public static bool CheckVerticalWin(sbyte[] pieces, sbyte token, int index)
        {
            // Limit is 3 spaces from the index.
            int lowerLimit = GetLimitFromReference(index, Direction.Up);
            int upperLimit = GetLimitFromReference(index, Direction.Down);

            // Check if there is a connect four.
            for (int i = lowerLimit; i <= upperLimit - 21; i+= 7)
            {
                if (pieces[i] + pieces[i + 7] + pieces[i + 14] + pieces[i + 21] == 4 * token)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// This function checks if there is a Connect Four in a diagonal (from upper left to lower right) 
        ///     containing the index.
        /// </summary>
        /// <param name="pieces">Array representing board state.</param>
        /// <param name="token">Token of player saved in the array.</param>
        /// <param name="index">Index where a new token is placed.</param>
        /// <returns>True if there is a connect four.</returns>
        public static bool CheckDiagonalWin1(sbyte[] pieces, sbyte token, int index)
        {
            int lowerLimit = GetLimitFromReference(index, Direction.UpLeft);
            int upperLimit = GetLimitFromReference(index, Direction.DownRight);
            
            // Check if there is a connect four.
            for (int i = lowerLimit; i <= upperLimit - 24; i += 8)
            {
                if (pieces[i] + pieces[i + 8] + pieces[i + 16] + pieces[i + 24] == 4 * token)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// This function checks if there is a Connect Four in a diagonal (from upper right to lower left) 
        ///     containing the index.
        /// </summary>
        /// <param name="pieces">Array representing board state.</param>
        /// <param name="token">Token of player saved in the array.</param>
        /// <param name="index">Index where a new token is placed.</param>
        /// <returns>True if there is a connect four.</returns>
        public static bool CheckDiagonalWin2(sbyte[] pieces, sbyte token, int index)
        {
            int lowerLimit = GetLimitFromReference(index, Direction.UpRight);
            int upperLimit = GetLimitFromReference(index, Direction.DownLeft);

            // Check if there is a connect four.
            for (int i = lowerLimit; i <= upperLimit - 18; i += 6)
            {
                if (pieces[i] + pieces[i + 6] + pieces[i + 12] + pieces[i + 18] == 4 * token)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// This will give the left-most index of the row where the given index is included.
        /// </summary>
        /// <param name="index">Zero-based index on the board.</param>
        /// <returns>Left-most index on the row.</returns>
        private static int LimitLeft(int index)
        {
            return (index / 7) * 7;
        }

        /// <summary>
        /// This will give the right-most index of the row where the given index is included.
        /// </summary>
        /// <param name="index">Zero-based index on the board.</param>
        /// <returns>Right-most index on the row.</returns>
        private static int LimitRight(int index)
        {
            return LimitLeft(index) + 6;
        }

        /// <summary>
        /// This will give the limit index when traversing upper-left of the given index.
        /// </summary>
        /// <param name="index">Zero-based index on the board.</param>
        /// <returns>Upper-left most index in the board from reference index.</returns>
        private static int LimitUpLeft(int index)
        {
            int leftSteps = index - LimitLeft(index);
            int upSteps = (index - LimitUp(index)) / 7;
            int steps = (leftSteps <= upSteps ? leftSteps : upSteps);
            return index - steps - (steps * 7);
        }

        /// <summary>
        /// This will give the limit index when traversing upper-right of the given index.
        /// </summary>
        /// <param name="index">Zero-based index on the board.</param>
        /// <returns>Upper-right most index in the board from reference index.</returns>
        private static int LimitUpRight(int index)
        {
            int rightSteps = LimitRight(index) - index;
            int upSteps = (index - LimitUp(index)) / 7;
            int steps = (rightSteps <= upSteps ? rightSteps : upSteps);
            return index + steps - (steps * 7);
        }

        /// <summary>
        /// This will give the limit index when traversing bottom-left of the given index.
        /// </summary>
        /// <param name="index">Zero-based index on the board.</param>
        /// <returns>Bottom-left most index in the board from reference index.</returns>
        private static int LimitDownLeft(int index)
        {
            int leftSteps = index - LimitLeft(index);
            int downSteps = (LimitDown(index) - index) / 7;
            int steps = (leftSteps <= downSteps ? leftSteps : downSteps);
            return index - steps + (steps * 7);
        }

        /// <summary>
        /// This will give the limit index when traversing bottom-right of the given index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private static int LimitDownRight(int index)
        {
            int rightSteps = LimitRight(index) - index;
            int downSteps = (LimitDown(index) - index) / 7;
            int steps = (rightSteps <= downSteps ? rightSteps : downSteps);
            return index + steps + (steps * 7);
        }

        /// <summary>
        /// This will give the top-most index of the column the given index is included.
        /// </summary>
        /// <param name="index">Zero-based index on the board.</param>
        /// <returns>Top-most index of the column.</returns>
        private static int LimitUp(int index)
        {
            return index % 7;
        }

        /// <summary>
        /// This will give the bottom-most index of the column the given index is included.
        /// </summary>
        /// <param name="index">Zero-based index on the board.</param>
        /// <returns>Bottom-most index of the column.</returns>
        private static int LimitDown(int index)
        {
            return LimitUp(index) + 35;
        }

        /// <summary>
        /// Gets the index next to a specified index depending on steps and direction.
        /// </summary>
        /// <param name="index">Reference index.</param>
        /// <param name="steps">Distance from reference index.</param>
        /// <param name="direction">Direction from reference index.</param>
        /// <returns>Index given the number of steps and direction. Will return -1 if it exeeds the limit.</returns>
        public static int GetNextIndex(int index, int steps, Direction direction)
        {
            int newIndex = -1;
            
            switch(direction)
            {
                case Direction.Up:
                    newIndex = index - (steps * 7);
                    newIndex = (newIndex < LimitUp(index) ? -1 : newIndex);
                    break;
                case Direction.Down:
                    newIndex = index + (steps * 7);
                    newIndex = (newIndex > LimitDown(index) ? -1 : newIndex);
                    break;
                case Direction.Left:
                    newIndex = index - steps;
                    newIndex = (newIndex < LimitLeft(index) ? -1 : newIndex);
                    break;
                case Direction.Right:
                    newIndex = index + steps;
                    newIndex = (newIndex > LimitRight(index) ? -1 : newIndex);
                    break;
                case Direction.UpLeft:
                    // Check first if we are exceeding the limit.
                    if (index - (steps * 7) < LimitUp(index))
                    {
                        break;
                    }
                    // Check first if we are exceeding the limit.
                    if (index - steps < LimitLeft(index))
                    {
                        break;
                    }

                    newIndex = index - steps - (steps * 7);
                    break;
                case Direction.UpRight:
                    // Check first if we are exceeding the limit.
                    if (index - (steps * 7) < LimitUp(index))
                    {
                        break;
                    }
                    // Check first if we are exceeding the limit.
                    if (index + steps > LimitRight(index))
                    {
                        break;
                    }

                    newIndex = index + steps - (steps * 7);
                    break;
                case Direction.DownLeft:
                    // Check first if we are exceeding the limit.
                    if (index + (steps * 7) > LimitDown(index))
                    {
                        break;
                    }
                    // Check first if we are exceeding the limit.
                    if (index - steps < LimitLeft(index))
                    {
                        break;
                    }

                    newIndex = index - steps + (steps * 7);
                    break;
                case Direction.DownRight:
                    // Check first if we are exceeding the limit.
                    if (index + (steps * 7) > LimitDown(index))
                    {
                        break;
                    }
                    // Check first if we are exceeding the limit.
                    if (index + steps > LimitRight(index))
                    {
                        break;
                    }

                    newIndex = index + steps + (steps * 7);
                    break;
            }

            return newIndex;
        }

        /// <summary>
        /// Get the limit from reference index (that means until the edge of board game or 3 spaces away).
        /// </summary>
        /// <param name="index">Reference zero-based index.</param>
        /// <param name="direction">Direction of limit.</param>
        /// <returns>The limit index.</returns>
        public static int GetLimitFromReference(int index, Direction direction)
        {
            int limit = -1;

            switch(direction)
            {
                // Direction is vertical.
                case Direction.Up:
                    limit = (LimitUp(index) > index - 21) ? LimitUp(index) : index - 21;
                    break;
                case Direction.Down:
                    limit = (LimitDown(index) < index + 21) ? LimitDown(index) : index + 21;
                    break;
                case Direction.Left:
                    limit = (LimitLeft(index) > index - 3) ? LimitLeft(index) : index - 3;
                    break;
                case Direction.Right:
                    limit = (LimitRight(index) < index + 3) ? LimitRight(index) : index + 3;
                    break;
                case Direction.UpLeft:
                    limit = (LimitUpLeft(index) > index - 24) ? LimitUpLeft(index) : index - 24;
                    break;
                case Direction.UpRight:
                    limit = (LimitUpRight(index) > index - 18) ? LimitUpRight(index) : index - 18;
                    break;
                case Direction.DownLeft:
                    limit = (LimitDownLeft(index) < index + 18) ? LimitDownLeft(index) : index + 18;
                    break;
                case Direction.DownRight:
                    limit = (LimitDownRight(index) < index + 24) ? LimitDownRight(index) : index + 24;
                    break;
            }

            return limit;
        }

        /********************************************
         * This part of the code is for the AI use
         * ******************************************/

        // This section are the scores we have for passing a check. To give priority to other parts, we need to adjust the scores.
        const int Score_OneToken = 1;                   // You have a token in a line of empty spaces.
        const int Score_TwoToken = 10;                  // You have 2 tokens in a line of empty spaces.
        const int Score_ThreeToken = 100;               // You have 3 tokens in a line of empty spaces.
        const int Score_ConnectFour = 99999;            // Of course, if a move will garner a connect score, do that move.
        const int Score_BlockOne = 5;                   // If there is only one enemy block, try to move beside it.
        const int Score_BlockTwo = 50;                  // Block two in a line.
        const int Score_BlockThree = 9999;              // Block a connect four or you will lose;
        const int Score_EnemyWillGetThree = -50;        // Moving on a spot will give an enemy the change to get three in a line.     
        const int Score_EnemyWillConnectFour = -9999;   // Move will give a chance for enemy to get ConnectFour.
        const int Score_LoseOppotunityThree = -30;      // Move will make you lose an opportuniy for thre in a row.
        const int Score_LoseConnectFour = -9999;        // Move will make you lose an opportunity to connect four.
            
        /// <summary>
        /// This function checks if a line can be made into a connect four. It must be empty or have a token of the player.
        /// </summary>
        /// <param name="token">Token of the player (in this case, AI).</param>
        /// <param name="val1">Value of the first slot.</param>
        /// <param name="val2">Value of the second slot.</param>
        /// <param name="val3">Value of the third slot.</param>
        /// <param name="val4">Value of the fourth slot.</param>
        /// <param name="numberOfTokens">Number of tokens we want in our four slots.</param>
        /// <returns>If the line only has the number of token we want for the line.</returns>
        public static bool CheckCountInLine(sbyte token, int val1, int val2, int val3, int val4, int numberOfTokens)
        {
            if ((val1 == token || val1 == 0) && 
                (val2 == token || val2 == 0) && 
                (val3 == token || val3 == 0) &&
                (val4 == token || val4 == 0))
            {
                return (val1 + val2 + val3 + val4 == token * numberOfTokens);
            }

            return false;
        }
               
        /// <summary>
        /// This function gives a corresponding score depending on the number of tokens in a line,
        /// </summary>
        /// <param name="tokenCount">The number of player tokens we want in a line.</param>
        /// <returns>The score.</returns>
        private static int GetPlacementScore(int tokenCount)
        {
            switch(tokenCount)
            {
                case 1:
                    return Score_OneToken;
                case 2:
                    return Score_TwoToken;
                case 3:
                    return Score_ThreeToken;
                case 4:
                    return Score_ConnectFour;
                default:
                    return 0;
                    //we can't have more than four, so this is an error if we have other values that 1 to 4
            }
        }

        /// <summary>
        /// This function will check how may connect fours can a move make in the future, given that only the index has
        /// a value and the 3 other indeces are empty.
        /// </summary>
        /// <param name="token">Player token.</param>
        /// <param name="board">The current game board.</param>
        /// <param name="moves">List of possible moves per column.</param>
        public static void ScoreForPlacement(sbyte token, sbyte[] board, ref Dictionary<int, int> moves)
        {
            int score = 0;
            int lower = 0;
            int upper = 0;
            List<int> keys = new List<int>(moves.Keys);
            foreach(int index in keys)
            {
                // Simulate a move using our dummy board
                board[index] = token;

                score = 0;

                //Check for everything, one token to connectfour
                for (int tokenCount = 1; tokenCount <= 4; tokenCount++)
                {
                    // Count the horizontal.
                    lower = GetLimitFromReference(index, Direction.Left);
                    upper = GetLimitFromReference(index, Direction.Right);
                    for (int i = lower; i + 3 <= upper; i++)
                    {
                        if (CheckCountInLine(token, board[i], board[i + 1], board[i + 2], board[i + 3], tokenCount))
                            score += GetPlacementScore(tokenCount);
                            
                    }

                    // Count vertical.
                    lower = GetLimitFromReference(index, Direction.Up);
                    upper = GetLimitFromReference(index, Direction.Down);
                    for (int i = lower; i + 21 <= upper; i+= 7)
                    {
                        if (CheckCountInLine(token, board[i], board[i + 7], board[i + 14], board[i + 21], tokenCount))
                            score += GetPlacementScore(tokenCount);
                    }

                    // Count diagonal1.
                    lower = GetLimitFromReference(index, Direction.UpLeft);
                    upper = GetLimitFromReference(index, Direction.DownRight);
                    for (int i = lower; i + 24 <= upper; i += 8)
                    {
                        if (CheckCountInLine(token, board[i], board[i + 8], board[i + 16], board[i + 24], tokenCount))
                            score += GetPlacementScore(tokenCount);
                    }

                    // Count diagonal2.
                    lower = GetLimitFromReference(index, Direction.UpRight);
                    upper = GetLimitFromReference(index, Direction.DownLeft);
                    for (int i = lower; i + 18 <= upper; i += 6)
                    {
                        if (CheckCountInLine(token, board[i], board[i + 6], board[i + 12], board[i + 18], tokenCount))
                            score += GetPlacementScore(tokenCount);
                    }
                }

                // Save score for a certain index.
                moves[index] = moves[index] + score;

                //Clear our board
                board[index] = 0;
            }
        }

        /// <summary>
        /// This function gives a corresponding score depending on the number of tokens in a line.
        /// </summary>
        /// <param name="tokenCount">The number of player tokens we want in a line.</param>
        /// <returns>The score.</returns>
        private static int GetBlockScore(int tokenCount)
        {
            switch (tokenCount)
            {
                case 2:
                    return Score_BlockOne;
                case 3:
                    return Score_BlockTwo;
                case 4:
                    return Score_BlockThree;
                default:
                    return 0;
                    //we can't have more than four, so this is an error if we have other values that 1 to 4
            }
        }

        /// <summary>
        /// This will give a score for blocking an enemy move, but will only trigger when enemy has two in a line.
        /// </summary>
        /// <param name="token">Player token.</param>
        /// <param name="board">The current game board.</param>
        /// <param name="moves">List of possible moves per column.</param>
        public static void ScoreForEasyBlocking(sbyte token, sbyte[] board, ref Dictionary<int, int> moves)
        {
            int score = 0;
            int lower = 0;
            int upper = 0;
            List<int> keys = new List<int>(moves.Keys);
            foreach (int index in keys)
            {
                sbyte opponentToken = Convert.ToSByte(-1 * token);
                // Simulate a move using our dummy board
                board[index] = opponentToken;

                score = 0;

                // Check if enemy will get a line for score.
                for (int tokenCount = 3; tokenCount <= 4; tokenCount++)
                {
                    // Count the horizontal.
                    lower = GetLimitFromReference(index, Direction.Left);
                    upper = GetLimitFromReference(index, Direction.Right);
                    for (int i = lower; i + 3 <= upper; i++)
                    {
                        if (CheckCountInLine(opponentToken, board[i], board[i + 1], board[i + 2], board[i + 3], tokenCount))
                            score += GetBlockScore(tokenCount);

                    }

                    // Count vertical.
                    lower = GetLimitFromReference(index, Direction.Up);
                    upper = GetLimitFromReference(index, Direction.Down);
                    for (int i = lower; i + 21 <= upper; i += 7)
                    {
                        if (CheckCountInLine(opponentToken, board[i], board[i + 7], board[i + 14], board[i + 21], tokenCount))
                            score += GetBlockScore(tokenCount);
                    }

                    // Count diagonal1.
                    lower = GetLimitFromReference(index, Direction.UpLeft);
                    upper = GetLimitFromReference(index, Direction.DownRight);
                    for (int i = lower; i + 24 <= upper; i += 8)
                    {
                        if (CheckCountInLine(opponentToken, board[i], board[i + 8], board[i + 16], board[i + 24], tokenCount))
                            score += GetBlockScore(tokenCount);
                    }

                    // Count diagonal2.
                    lower = GetLimitFromReference(index, Direction.UpRight);
                    upper = GetLimitFromReference(index, Direction.DownLeft);
                    for (int i = lower; i + 18 <= upper; i += 6)
                    {
                        if (CheckCountInLine(opponentToken, board[i], board[i + 6], board[i + 12], board[i + 18], tokenCount))
                            score += GetBlockScore(tokenCount);
                    }
                }

                // Save score for a certain index.
                moves[index] = moves[index] + score;

                //Clear our board
                board[index] = 0;
            }
        }

        /// <summary>
        /// This will give a score for blocking an enemy move.
        /// </summary>
        /// <param name="token">Player token.</param>
        /// <param name="board">The current game board.</param>
        /// <param name="moves">List of possible moves per column.</param>
        public static void ScoreForBlocking(sbyte token, sbyte[] board, ref Dictionary<int, int> moves)
        {
            int score = 0;
            int lower = 0;
            int upper = 0;
            List<int> keys = new List<int>(moves.Keys);
            foreach (int index in keys)
            {
                sbyte opponentToken = Convert.ToSByte(-1 * token);
                // Simulate a move using our dummy board
                board[index] = opponentToken;

                score = 0;

                // Check if enemy will get a line for score.
                for (int tokenCount = 2; tokenCount <= 4; tokenCount++)
                {
                    // Count the horizontal.
                    lower = GetLimitFromReference(index, Direction.Left);
                    upper = GetLimitFromReference(index, Direction.Right);
                    for (int i = lower; i + 3 <= upper; i++)
                    {
                        if (CheckCountInLine(opponentToken, board[i], board[i + 1], board[i + 2], board[i + 3], tokenCount))
                            score += GetBlockScore(tokenCount);

                    }

                    // Count vertical.
                    lower = GetLimitFromReference(index, Direction.Up);
                    upper = GetLimitFromReference(index, Direction.Down);
                    for (int i = lower; i + 21 <= upper; i += 7)
                    {
                        if (CheckCountInLine(opponentToken, board[i], board[i + 7], board[i + 14], board[i + 21], tokenCount))
                            score += GetBlockScore(tokenCount);
                    }

                    // Count diagonal1.
                    lower = GetLimitFromReference(index, Direction.UpLeft);
                    upper = GetLimitFromReference(index, Direction.DownRight);
                    for (int i = lower; i + 24 <= upper; i += 8)
                    {
                        if (CheckCountInLine(opponentToken, board[i], board[i + 8], board[i + 16], board[i + 24], tokenCount))
                            score += GetBlockScore(tokenCount);
                    }

                    // Count diagonal2.
                    lower = GetLimitFromReference(index, Direction.UpRight);
                    upper = GetLimitFromReference(index, Direction.DownLeft);
                    for (int i = lower; i + 18 <= upper; i += 6)
                    {
                        if (CheckCountInLine(opponentToken, board[i], board[i + 6], board[i + 12], board[i + 18], tokenCount))
                            score += GetBlockScore(tokenCount);
                    }
                }

                // Save score for a certain index.
                moves[index] = moves[index] + score;

                //Clear our board
                board[index] = 0;
            }
        }

        /// <summary>
        /// This function gives a corresponding deduction depending on the number of opponents tokens in a line.
        /// </summary>
        /// <param name="tokenCount">The number of player tokens we want in a line.</param>
        /// <returns>The score.</returns>
        private static int GetGivingScore(int tokenCount)
        {
            switch (tokenCount)
            {
                case 3:
                    return Score_EnemyWillGetThree;
                case 4:
                    return Score_EnemyWillConnectFour;
                default:
                    return 0;
                    //we can't have more than four, so this is an error if we have other values that 1 to 4
            }
        }

        /// <summary>
        /// This will give a deduction for making move that will make the enemy gain a spot.
        /// </summary>
        /// <param name="token">Player token.</param>
        /// <param name="board">The current game board.</param>
        /// <param name="moves">List of possible moves per column.</param>
        public static void ScoreForGiving(sbyte token, sbyte[] board, ref Dictionary<int, int> moves)
        {
            int opponentIndex = 0;
            sbyte opponentToken = Convert.ToSByte(-1 * token);
            int score = 0;
            int lower = 0;
            int upper = 0;

            List<int> keys = new List<int>(moves.Keys);
            foreach (int index in keys)
            {
                if (index == LimitUp(index))
                    continue;

                score = 0;

                // Let us simulate a move
                board[index] = token;

                // Set opponent's next move.
                opponentIndex = GetNextIndex(index, 1, Direction.Up);
                board[index] = opponentToken;
                
                // Check if enemy will get a line for score.
                for (int tokenCount = 3; tokenCount <= 4; tokenCount++)
                {
                    // Count the horizontal.
                    lower = GetLimitFromReference(opponentIndex, Direction.Left);
                    upper = GetLimitFromReference(opponentIndex, Direction.Right);
                    for (int i = lower; i + 3 <= upper; i++)
                    {
                        if (CheckCountInLine(opponentToken, board[i], board[i + 1], board[i + 2], board[i + 3], tokenCount))
                            score += GetGivingScore(tokenCount);

                    }

                    // Count vertical.
                    lower = GetLimitFromReference(opponentIndex, Direction.Up);
                    upper = GetLimitFromReference(opponentIndex, Direction.Down);
                    for (int i = lower; i + 21 <= upper; i += 7)
                    {
                        if (CheckCountInLine(opponentToken, board[i], board[i + 7], board[i + 14], board[i + 21], tokenCount))
                            score += GetGivingScore(tokenCount);
                    }

                    // Count diagonal1.
                    lower = GetLimitFromReference(opponentIndex, Direction.UpLeft);
                    upper = GetLimitFromReference(opponentIndex, Direction.DownRight);
                    for (int i = lower; i + 24 <= upper; i += 8)
                    {
                        if (CheckCountInLine(opponentToken, board[i], board[i + 8], board[i + 16], board[i + 24], tokenCount))
                            score += GetBlockScore(tokenCount);
                    }

                    // Count diagonal2.
                    lower = GetLimitFromReference(opponentIndex, Direction.UpRight);
                    upper = GetLimitFromReference(opponentIndex, Direction.DownLeft);
                    for (int i = lower; i + 18 <= upper; i += 6)
                    {
                        if (CheckCountInLine(opponentToken, board[i], board[i + 6], board[i + 12], board[i + 18], tokenCount))
                            score += GetBlockScore(tokenCount);
                    }
                }

                // Save score for a certain index.
                moves[index] = moves[index] + score;

                //Clear our board
                board[index] = 0;
                board[opponentIndex] = 0;
            }
        }

        /// <summary>
        /// This function gives a corresponding deduction if you lose an opportunity in making a line.
        /// </summary>
        /// <param name="tokenCount">The number of player tokens we want in a line.</param>
        /// <returns>The score.</returns>
        private static int GetLoseScore(int tokenCount)
        {
            switch (tokenCount)
            {
                case 3:
                    return Score_LoseOppotunityThree;
                case 4:
                    return Score_LoseConnectFour;
                default:
                    return 0;
                    //we can't have more than four, so this is an error if we have other values that 1 to 4
            }
        }

        /// <summary>
        /// This will give a deduction for making move that will make the enemy gain a spot.
        /// </summary>
        /// <param name="token">Player token.</param>
        /// <param name="board">The current game board.</param>
        /// <param name="moves">List of possible moves per column.</param>
        public static void ScoreForLosing(sbyte token, sbyte[] board, ref Dictionary<int, int> moves)
        {
            int score = 0;
            int lower = 0;
            int upper = 0;
            int nextIndex;

            List<int> keys = new List<int>(moves.Keys);
            foreach (int index in keys)
            {
                if (index == LimitUp(index))
                    continue;

                score = 0;

                // Let us simulate a move
                board[index] = token;

                // Set opponent's next move.
                nextIndex = GetNextIndex(index, 1, Direction.Up);
                board[index] = token;

                // Check if enemy will get a line for score.
                for (int tokenCount = 3; tokenCount <= 4; tokenCount++)
                {
                    // Count the horizontal.
                    lower = GetLimitFromReference(nextIndex, Direction.Left);
                    upper = GetLimitFromReference(nextIndex, Direction.Right);
                    for (int i = lower; i + 3 <= upper; i++)
                    {
                        if (CheckCountInLine(token, board[i], board[i + 1], board[i + 2], board[i + 3], tokenCount))
                            score += GetGivingScore(tokenCount);

                    }

                    // Count vertical.
                    lower = GetLimitFromReference(nextIndex, Direction.Up);
                    upper = GetLimitFromReference(nextIndex, Direction.Down);
                    for (int i = lower; i + 21 <= upper; i += 7)
                    {
                        if (CheckCountInLine(token, board[i], board[i + 7], board[i + 14], board[i + 21], tokenCount))
                            score += GetGivingScore(tokenCount);
                    }

                    // Count diagonal1.
                    lower = GetLimitFromReference(nextIndex, Direction.UpLeft);
                    upper = GetLimitFromReference(nextIndex, Direction.DownRight);
                    for (int i = lower; i + 24 <= upper; i += 8)
                    {
                        if (CheckCountInLine(token, board[i], board[i + 8], board[i + 16], board[i + 24], tokenCount))
                            score += GetBlockScore(tokenCount);
                    }

                    // Count diagonal2.
                    lower = GetLimitFromReference(nextIndex, Direction.UpRight);
                    upper = GetLimitFromReference(nextIndex, Direction.DownLeft);
                    for (int i = lower; i + 18 <= upper; i += 6)
                    {
                        if (CheckCountInLine(token, board[i], board[i + 6], board[i + 12], board[i + 18], tokenCount))
                            score += GetBlockScore(tokenCount);
                    }
                }

                // Save score for a certain index.
                moves[index] = moves[index] + score;

                //Clear our board
                board[index] = 0;
                board[nextIndex] = 0;
            }
        }
    }
}
