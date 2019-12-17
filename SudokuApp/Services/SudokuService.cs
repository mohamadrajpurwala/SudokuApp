using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SudokuApp.Services.Interfaces;

namespace SudokuApp.Services
{
    public class SudokuService : ISudokuService
    {
        public SudokuService()
        {

        }

        /// <summary>
        /// Create Sudoku.
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public int[,] Create(string board)
        {
            var updatedDefaultValue = board.Replace('.', '0');
            int index = 0;
            var defaultSudoku = new int[9, 9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    defaultSudoku[i, j] = Convert.ToInt32(updatedDefaultValue[index].ToString());
                    index++;
                }
            }
            return defaultSudoku;
        }

        /// <summary>
        /// Solve Sudoku.
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public int[,] Solve(int[,] board)
        {
            var currentPosition = findEmptyPosition(board);

            if (currentPosition == null)
            {
                return board;
            }

            for (int number = 1; number < 10; number++)
            {
                if (CheckValidConditions(board, number, currentPosition.Value))
                {
                    board[currentPosition.Value.row, currentPosition.Value.col] = number;

                    if (Solve(board) != null)
                    {
                        return board;
                    }

                    board[currentPosition.Value.row, currentPosition.Value.col] = 0;
                }
            }

            return null;
        }

        /// <summary>
        /// Find first empty position within a board.
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        private (int row, int col)? findEmptyPosition(int[,] board)
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (board[row, col] == 0)
                    {
                        return (row, col);
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Check condition within a board to fill correct number.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="number"></param>
        /// <param name="currentPosition"></param>
        /// <returns></returns>
        private bool CheckValidConditions(int[,] board, int number, (int row, int col) currentPosition)
        {
            // check in rows
            for (int row = 0; row < 9; row++)
            {
                if (board[currentPosition.row, row] == number && currentPosition.col != row)
                {
                    return false;
                }
            }

            // check in columns
            for (int col = 0; col < 9; col++)
            {
                if (board[col, currentPosition.col] == number && currentPosition.row != col)
                {
                    return false;
                }
            }

            // check in sub part
            var subBoardRow = (int)currentPosition.row / 3;
            var subBoardColumn = (int)currentPosition.col / 3;

            for (int row = subBoardRow * 3; row < subBoardRow * 3 + 3; row++)
            {
                for (int col = subBoardColumn * 3; col < subBoardColumn * 3 + 3; col++)
                {
                    if (board[row, col] == number && currentPosition != (row, col))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
