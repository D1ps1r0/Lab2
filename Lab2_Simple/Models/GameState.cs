using System;
using System.Collections.Generic;

namespace Lab2_TicTacToe.Models
{
    public class GameState
    {
        public const int Size = 3;
        private char[,] board = new char[Size, Size];
        private Random random = new Random();

        public GameState()
        {
            Clear();
        }

        public void Clear()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    board[i, j] = ' ';
                }
            }
        }

        public char GetCell(int index)
        {
            int row = index / Size;
            int col = index % Size;
            return board[row, col];
        }

        public bool MakeMove(int index, char symbol)
        {
            int row = index / Size;
            int col = index % Size;

            if (board[row, col] != ' ')
                return false;

            board[row, col] = symbol;
            return true;
        }

        public List<int> GetFreeCells()
        {
            List<int> cells = new List<int>();

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (board[i, j] == ' ')
                        cells.Add(i * Size + j);
                }
            }

            return cells;
        }

        public char CheckWinner()
        {
            for (int i = 0; i < Size; i++)
            {
                if (board[i, 0] != ' ' &&
                    board[i, 0] == board[i, 1] &&
                    board[i, 1] == board[i, 2])
                    return board[i, 0];

                if (board[0, i] != ' ' &&
                    board[0, i] == board[1, i] &&
                    board[1, i] == board[2, i])
                    return board[0, i];
            }

            if (board[0, 0] != ' ' &&
                board[0, 0] == board[1, 1] &&
                board[1, 1] == board[2, 2])
                return board[0, 0];

            if (board[0, 2] != ' ' &&
                board[0, 2] == board[1, 1] &&
                board[1, 1] == board[2, 0])
                return board[0, 2];

            return ' ';
        }

        public bool IsDraw()
        {
            return CheckWinner() == ' ' && GetFreeCells().Count == 0;
        }

        public int GetComputerMove(int level)
        {
            List<int> free = GetFreeCells();
            if (free.Count == 0)
                return -1;

            // 3 рівень: спочатку пробує виграти
            if (level == 3)
            {
                int move = FindWinningMove('O');
                if (move != -1)
                    return move;
            }

            // 2 і 3 рівні: пробує заблокувати гравця
            if (level >= 2)
            {
                int move = FindWinningMove('X');
                if (move != -1)
                    return move;
            }

            // 1 рівень: випадковий хід
            return free[random.Next(free.Count)];
        }

        private int FindWinningMove(char symbol)
        {
            List<int> free = GetFreeCells();

            foreach (int index in free)
            {
                int row = index / Size;
                int col = index % Size;

                board[row, col] = symbol;

                if (CheckWinner() == symbol)
                {
                    board[row, col] = ' ';
                    return index;
                }

                board[row, col] = ' ';
            }

            return -1;
        }
    }
}
