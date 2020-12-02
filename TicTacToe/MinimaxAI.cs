using System;

namespace TicTacToe
{
    public class MinimaxAI
    {
        /// <summary>
        /// Win combinations to check all possible wins for Symbol[] Board
        /// Board has 9 cells from 0 to 8
        /// </summary>
        private static int[,] winCombinations = new int[8, 3]{
            { 0, 1, 2 },
            { 3, 4, 5 },
            { 6, 7, 8 },
            { 0, 3, 6 },
            { 1, 4, 7 },
            { 2, 5, 8 },
            { 0, 4, 8 },
            { 2, 4, 6 }};

        /// <summary>
        /// Finds the best move for existing board using the minimax algorithm
        /// </summary>
        /// <param name="board">Current state of the TicTacToe board</param>
        /// <returns></returns>
        public static int findBestMove(SymbolType[] board)
        {
            int bestMove = -1;
            int bestScore = int.MinValue;
            int score;

            for (int i = 0; i < board.Length; i++)
            {
                if (board[i] == SymbolType.Free) 
                {
                    board[i] = SymbolType.O; //Performing the move simulation.

                    score = Minimax(board, 0, false);  //Entering the minimax recursion, increasing depth and changing isMaximizing player.

                    board[i] = SymbolType.Free;  ////Clears the spot, cause the move was a simulation, not a real move.

                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMove = i;
                    }
                }
            }
            return bestMove;
        }

        /// <summary>
        /// Recursive minimax algorithm. Looks for the best score of every possible move made by both players on the given board.
        /// </summary>
        /// <param name="board">Current state of the TicTacToe board.</param>
        /// <param name="depth">Depth of the recursive game. Number of turn simulations for both players.</param>
        /// <param name="isMax">Is Maximizing player - looks for the highest score. If false(Minimizing) - looks for the lowest score.</param>
        /// <returns></returns>
        static int Minimax(SymbolType[] board, int depth, bool isMax)
        {
            //Checking score every time we enter the minimax algorithm.
            int score = checkWinner(board, depth);

            if (score != 0)
                return score;
            else if (checkGameEnd(board))
                return 0;

            if (isMax)
            {
                int bestScore = int.MinValue;

                for (int i = 0; i < board.Length; i++)
                {
                    //Perform move simulation for every free spot on the board
                    if (board[i] == SymbolType.Free)
                    {
                        board[i] = SymbolType.O; //Performing the move simulation.

                        score = Minimax(board, depth + 1, false); //Entering the minimax recursion, increasing depth and changing isMaximizing player.

                        board[i] = SymbolType.Free; //Clears the spot, cause the move was a simulation, not a real move.

                        bestScore = Math.Max(score, bestScore); //Sets bestScore to highest for Maximizing player
                    }
                }
                return bestScore;
            }

            else
            {
                int bestScore = int.MaxValue;
                for (int i = 0; i < board.Length; i++)
                {
                    if (board[i] == SymbolType.Free)
                    {
                        board[i] = SymbolType.X; //Performing the move simulation

                        score = Minimax(board, depth + 1, true); //Entering the minimax recursion, increasing depth and changing isMaximizing player.

                        board[i] = SymbolType.Free; //Clears the spot, cause the move was a simulation, not a real move.

                        bestScore = Math.Min(score, bestScore); //Sets bestScore to lowest for Minimizing player
                    }
                }
                return bestScore;
            }
        }

        /// <summary>
        /// Checks if the board has a winner and returns the score for the minimax algorithm.
        /// </summary>
        /// <param name="board">Current state of the TicTacToe board.</param>
        /// <param name="depth">Depth will show the number of turns for every simulation.</param>
        /// <returns></returns>
        static int checkWinner(SymbolType[] board, int depth)
        {
            for (int i = 0; i < 8; i++)
            {
                if (
                        board[winCombinations[i, 0]] == SymbolType.O &&
                        board[winCombinations[i, 1]] == SymbolType.O &&
                        board[winCombinations[i, 2]] == SymbolType.O
                  )
                    return 10 - depth;      //By substracting depth from score minimax algorithm will take number of turns into account.
                                            //To win in less number of turns, or to prolong the game as much as possible.
            }

            for (int i = 0; i < 8; i++)
            {
                if (
                        board[winCombinations[i, 0]] == SymbolType.X &&
                        board[winCombinations[i, 1]] == SymbolType.X &&
                        board[winCombinations[i, 2]] == SymbolType.X
                  )
                    return depth - 10;       //By substracting score from depth  minimax algorithm will take number of turns into account.
                                             //To win in less number of turns, or to prolong the game as much as possible.
            }
            return 0;
        }

        /// <summary>
        /// Checks if the game has ended by checking if there are still free spots on the board.
        /// </summary>
        /// <param name="board">Current state of the board.</param>
        /// <returns></returns>
        private static bool checkGameEnd(SymbolType[] board)
        {
            foreach (SymbolType s in board) if (s == SymbolType.Free) return false;
            return true;
        }
    }
}
