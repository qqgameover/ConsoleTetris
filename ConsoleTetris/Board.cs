using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTetris
{
    public class Board
    {
        public byte[,] BoardArray { get => MapMang.Manager.BoardArray; set => MapMang.Manager.BoardArray = value; }
        public byte[,] LandedArray { get => MapMang.Manager.LandedArray; set => MapMang.Manager.LandedArray = value; }
        public int Points { get; private set; }

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetConsoleMode(IntPtr hConsoleHandle, int mode);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool GetConsoleMode(IntPtr handle, out int mode);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetStdHandle(int handle);

        public Board()
        {
            MapMang.Manager.BoardArray = new byte[22, 12];
            MapMang.Manager.LandedArray = new byte[22, 12];
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
            var handle = GetStdHandle(-11);
            int mode;
            GetConsoleMode(handle, out mode);
            SetConsoleMode(handle, mode | 0x4);
            for (int i = 0; i < BoardArray.GetLength(0); i++)
            {
                if (i != 0) Console.WriteLine();
                for (int j = 0; j < BoardArray.GetLength(1); j++)
                {
                    if (BoardArray[i, j] == 0)
                    {
                        Console.Write( "\x1b[38;5;" + 241 + "m" );
                        Console.Write( "\x1b[48;5;" + 232 + "m+" );
                        Console.ResetColor();
                    }
                    if (BoardArray[i, j] == 1)
                    {
                        Console.Write("\x1b[38;5;" + 241 + "m");
                        Console.Write("█");
                        Console.ResetColor();
                    }
                    if (BoardArray[i, j] == 2)
                    {
                        //better red
                        Console.Write("\x1b[38;5;" + 196 + "m");
                        Console.Write("█");
                        Console.ResetColor();
                    }
                    if (BoardArray[i, j] == 3)
                    {
                        //Better purple
                        Console.Write("\x1b[38;5;" + 135 + "m");
                        Console.Write("█");
                        Console.ResetColor();
                    }
                    if (BoardArray[i, j] == 4)
                    {
                        //Better light blue
                        Console.Write("\x1b[38;5;" + 39 + "m");
                        Console.Write("█");
                        Console.ResetColor();
                    }
                    if (BoardArray[i, j] == 5)
                    {
                        //Better dark blue
                        Console.Write("\x1b[38;5;" + 20 + "m");
                        Console.Write("█");
                        Console.ResetColor();
                    }
                    if (BoardArray[i, j] == 6)
                    {
                        //Better yellow
                        Console.Write("\x1b[38;5;" + 226 + "m");
                        Console.Write("█");
                        Console.ResetColor();
                    }
                    if (BoardArray[i, j] == 7)
                    {
                        //Better green
                        Console.Write("\x1b[38;5;" + 28 + "m");
                        Console.Write("█");
                        Console.ResetColor();
                    }
                    if (BoardArray[i, j] == 8)
                    {
                        //Better pink
                        Console.Write("\x1b[38;5;" + 201 + "m");
                        Console.Write("█");
                        Console.ResetColor();
                    }
                }

                if (i == BoardArray.GetLength(0) - 1)
                {
                    Console.WriteLine("\n");
                    Console.WriteLine($"{Points} Points");
                }
            }
        }

        public void CheckForTetris()
        {
            bool[] FilledLines = new bool[22];
            for (int i = 0; i < BoardArray.GetLength(0); i++)
            {
                if(i == BoardArray.GetLength(0) - 1) continue;
                if (i == 0) continue;
                var fullLine = Enumerable.Range(0, BoardArray.GetLength(1))
                    .Select(x => BoardArray[i, x]).ToArray().All(i => i > 0);
                if (!fullLine) continue;
                RemoveRow(i);
                FilledLines[i] = fullLine;
            }

            var lines = 0;
            foreach (var line in FilledLines)
            {
                if (line) lines++;
            }

            if (lines == 1) Points += 40;
            if (lines == 2) Points += 100;
            if (lines == 3) Points += 300;
            if (lines == 4) Points += 1200;
        }

        public void RemoveRow(int rowToRemove)
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
            list.RemoveAt(rowToRemove);
            list.InsertRange(1, new List<byte[]>
            {
                new byte[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}
            });
            var x = list.ToArray();
            BoardArray = To2D(x);
            LandedArray = (byte[,])BoardArray.Clone();
        }

        static byte[,] To2D(byte[][] source)
        {
            //turns a rect jagged array into a 2d array.
            //will throw error if source is not rectangular.
            int FirstDim = source.Length;
            int SecondDim = source.GroupBy(row => row.Length).Single().Key;

            var result = new byte[FirstDim, SecondDim];
            for (int i = 0; i < FirstDim; ++i)
            for (int j = 0; j < SecondDim; ++j) result[i, j] = source[i][j];
            return result;
        }
    }
}
