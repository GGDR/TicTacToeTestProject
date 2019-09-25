using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TicTacToe
{
    public static class TicTacToeUtility
    {
        public static WinResult CheckWinner(int[,] grid, int gridSize)
        {
            int diagonalSum = 0;
            int antiDiagonalSum = 0;

            for (int r = 0; r < gridSize; r++)
            {
                int rowSum = 0;
                int columnSum = 0;

                for (int c = 0; c < gridSize; c++)
                {
                    // Sum row
                    rowSum += grid[r, c];

                    // Sum column
                    columnSum += grid[c, r];

                    // Sum diagonal
                    if (r == c)
                    {
                        diagonalSum += grid[r, c];
                    }

                    // Sum antiDiagonal
                    if ((r + c) == (gridSize - 1))
                    {
                        antiDiagonalSum += grid[r, c];
                    }

                    // Columns
                    if (columnSum == gridSize)
                    {
                        //Debug.Log("Column #" + r);
                        return new WinResult(1, LineType.Vertical, r);
                    }
                    else if (columnSum == -gridSize)
                    {
                        //Debug.Log("Column #" + r);
                        return new WinResult(-1, LineType.Vertical, r); ;
                    }
                }

                // Rows
                if (rowSum == gridSize)
                {
                    //Debug.Log("Row #" + r);
                    return new WinResult(1, LineType.Horizontal, r);
                }
                else if (rowSum == -gridSize)
                {
                    //Debug.Log("Row #" + r);
                    return new WinResult(-1, LineType.Horizontal, r);
                }
            }

            // Diagonal
            if (diagonalSum == gridSize)
            {
                //Debug.Log("Diagonal");
                return new WinResult(1, LineType.Diagonal, 0);
            }
            else if (diagonalSum == -gridSize)
            {
                //Debug.Log("Diagonal");
                return new WinResult(-1, LineType.Diagonal, 0);
            }

            // Anti-Diagonal
            else if (antiDiagonalSum == gridSize)
            {
                //Debug.Log("AntiDiagonal");
                return new WinResult(1, LineType.AntiDiagonal, 0);
            }
            else if (antiDiagonalSum == -gridSize)
            {
                //Debug.Log("AntiDiagonal");
                return new WinResult(-1, LineType.AntiDiagonal, 0);
            }

            else return new WinResult(0, LineType.None, 0); ;
        }

        public static bool CheckForEmptyCells(int [,] grid)
        {
            foreach (var cell in grid)
            {
                if (cell == 0) return true;
            }
            return false;
        }

        public static Vector2Int[] GetEmptyPositions(int [,] grid)
        {
            List<Vector2Int> cells = new List<Vector2Int>();
            for (int r = 0; r < grid.GetLength(0); r++)
            {
                for (int c = 0; c < grid.GetLength(1); c++)
                {
                    if (grid[r, c] == 0) cells.Add(new Vector2Int(r, c));
                }
            }
            return cells.ToArray();
        }

        public static PositionScore MiniMax(int[,] grid, int depth, bool ai)
        {
            Vector2Int[] emptyPositions = GetEmptyPositions(grid);

            if (CheckWinner(grid, grid.GetLength(0)).score == -1)
                return new PositionScore(new Vector2Int(0, 0), -1, 10);
            else if (CheckWinner(grid, grid.GetLength(0)).score == 1)
                return new PositionScore(new Vector2Int(0, 0), 1, -10);
            else if (depth == 0 || emptyPositions.Length == 0)
                return new PositionScore(new Vector2Int(0, 0), 0, 0);

            List<PositionScore> moves = new List<PositionScore>();

            for (int i = 0; i < emptyPositions.Length; i++)
            {
                PositionScore move;
                move.position = emptyPositions[i];

                if (ai)
                {
                    grid[emptyPositions[i].x, emptyPositions[i].y] = -1;
                    move.index = -1;
                    move.score = MiniMax(grid, depth - 1, false).score;
                }
                else
                {
                    grid[emptyPositions[i].x, emptyPositions[i].y] = 1;
                    move.index = 1;
                    move.score = MiniMax(grid, depth - 1, true).score;
                }

                grid[emptyPositions[i].x, emptyPositions[i].y] = 0;

                moves.Add(move);                
            }

            int bestMoveIndex = 0;
            if (ai)
            {
                int bestScore = -10000;
                for (int i = 0; i < moves.Count; i++)
                {
                    if (moves[i].score > bestScore)
                    {
                        bestScore = moves[i].score;
                        bestMoveIndex = i;
                    }
                }
            }
            else
            {
                int bestScore = 10000;
                for (int i = 0; i < moves.Count; i++)
                {
                    if (moves[i].score < bestScore)
                    {
                        bestScore = moves[i].score;
                        bestMoveIndex = i;
                    }
                }
            }

            return moves[bestMoveIndex];
        }

        public static void SaveDifficulty(Difficulty difficulty)
        {
            switch (difficulty)
            {
                case Difficulty.Easy:
                    PlayerPrefs.SetInt("Difficulty", 1);
                    break;
                case Difficulty.Normal:
                    PlayerPrefs.SetInt("Difficulty", 2);
                    break;
                case Difficulty.Hard:
                    PlayerPrefs.SetInt("Difficulty", 3);
                    break;
                default:
                    break;
            }
            PlayerPrefs.Save();
        }

        public static Difficulty GetDifficulty()
        {
            switch (PlayerPrefs.GetInt("Difficulty", 1))
            {
                case 1:
                    return Difficulty.Easy;
                case 2:
                    return Difficulty.Normal;
                case 3:
                    return Difficulty.Hard;
                default:
                    return Difficulty.Easy;
            }
        }

    } // class
} // namespace
