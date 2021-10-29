using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTetris
{
    public class Board
    {
        public byte[,] BoardArray { get => MapMang.Manager.BoardArray; set => MapMang.Manager.BoardArray = value; }
        public byte[,] LandedArray { get => MapMang.Manager.LandedArray; set => MapMang.Manager.LandedArray = value; }

        public Board()
        {
            MapMang.Manager.BoardArray = new byte[22, 16];
            MapMang.Manager.LandedArray = new byte[22, 16];
            CreateWalls(BoardArray);
            CreateWalls(LandedArray);

        }

        private void CreateWalls(byte[,] board)
        {
            var height = board.GetLength(0);
            var width = board.GetLength(1);
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    board[0, i] = 1;
                    board[height - 1, i] = 1;
                    if (i == 0 || i == width - 1)
                    {
                        board[j, i] = 1;
                    }
                }
            }
        }

        public void DrawBoard()
        {
            Console.SetCursorPosition(0, 0);
            Console.CursorVisible = false;
            //Console.BackgroundColor = ConsoleColor.Black;
            for (int i = 0; i < BoardArray.GetLength(0); i++)
            {
                //Console.BackgroundColor = ConsoleColor.Black;
                if (i != 0) Console.WriteLine();
                for (int j = 0; j < BoardArray.GetLength(1); j++)
                {
                    if (BoardArray[i, j] == 0) Console.Write(" ");
                    if (BoardArray[i, j] == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("█");
                        Console.ResetColor();
                    }
                    if (BoardArray[i, j] == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("█");
                        Console.ResetColor();
                    }
                    if (BoardArray[i, j] == 3)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.Write("█");
                        Console.ResetColor();
                    }
                    if (BoardArray[i, j] == 4)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("█");
                        Console.ResetColor();
                    }
                    if (BoardArray[i, j] == 5)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.Write("█");
                        Console.ResetColor();
                    }
                    if (BoardArray[i, j] == 6)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("█");
                        Console.ResetColor();
                    }
                }
            }
        }

        public void CheckForTetris()
        {
            for (int i = 0; i < BoardArray.GetLength(0); i++)
            {
                if(i == BoardArray.GetLength(0) - 1) continue;
                if (i == 0) continue;
                var x = Enumerable.Range(0, BoardArray.GetLength(1))
                    .Select(x => BoardArray[i, x])
                    .ToArray();
                var checkAll = x.All(i => i > 0);
                if (checkAll)
                {
                    RemoveRow(i);
                }
            }
        }

        public void RemoveRow(int RowToRemove)
        {
            byte[,] tmp = BoardArray;
            List<byte[]> list = new List<byte[]>();
            for (int i = 0; i < tmp.GetLength(0); i++)
            {
                byte[] temp = new byte[tmp.GetLength(1)];
                for (int n = 0; n < temp.Length; n++)
                {
                    temp[n] = tmp[i, n];
                }

                list.Add(temp);
            }
            list.RemoveAt(RowToRemove);
            list.InsertRange(1, new List<byte[]>
            {
                new byte[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}
            });
            var x = list.ToArray();
            BoardArray = To2D(x);
        }

        static byte[,] To2D(byte[][] source)
        {
            try
            {
                int FirstDim = source.Length;
                int SecondDim = source.GroupBy(row => row.Length).Single().Key; // throws InvalidOperationException if source is not rectangular

                var result = new Byte[FirstDim, SecondDim];
                for (int i = 0; i < FirstDim; ++i)
                for (int j = 0; j < SecondDim; ++j)
                    result[i, j] = source[i][j];

                return result;
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("The given jagged array is not rectangular.");
            }
        }
    }
}
