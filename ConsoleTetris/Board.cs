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

        //added this to get more colors.
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleMode(IntPtr hConsoleHandle, int mode);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool GetConsoleMode(IntPtr handle, out int mode);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int handle);

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
                    //wall or roof
                    board[0, i] = 1;
                    board[height - 1, i] = 1;
                    if (Board.IsAWall(i, width))
                    {
                        board[j, i] = 1;
                    }
                }
            }
        }

        private static bool IsAWall(int i, int width)
        {
            return i == 0 || i == width - 1;
        }

        public void DrawBoard()
        {
            Console.SetCursorPosition(0, 0);
            Console.CursorVisible = false;
            var handle = GetStdHandle(-11);
            GetConsoleMode(handle, out var mode);
            SetConsoleMode(handle, mode | 0x4);
            for (int i = 0; i < BoardArray.GetLength(0); i++)
            {
                if (i != 0) Console.WriteLine();
                for (int j = 0; j < BoardArray.GetLength(1); j++)
                {
                    if (IsEmpty(i, j))
                    {
                        //better gray and black
                        WriteWithBetterColors(241, "");
                        Console.Write( "\x1b[48;5;" + 232 + "m+" );
                        Console.ResetColor();
                    }
                    if (IsWall(i, j))
                    {
                        //better gray
                        WriteWithBetterColors(241);
                    }
                    if (IsIPiece(i, j))
                    {
                        //better red
                        WriteWithBetterColors(196);
                    }
                    if (IsLPiece(i, j))
                    {
                        //Better purple
                        WriteWithBetterColors(135);
                    }
                    if (IsBlockPiece(i, j))
                    {
                        //Better light blue
                        WriteWithBetterColors(39);
                    }
                    if (IsSPiece(i, j))
                    {
                        //Better dark blue
                        WriteWithBetterColors(20);
                    }
                    if (IsTPiece(i, j))
                    {
                        //Better yellow
                        WriteWithBetterColors(226);
                    }
                    if (IsJPiece(i, j))
                    {
                        WriteWithBetterColors(28);
                    }
                    if (IsZPiece(i, j))
                    {
                        //Better pink
                        WriteWithBetterColors(201);
                    }
                }

                if (i == BoardArray.GetLength(0) - 1)
                {
                    Console.WriteLine("\n");
                    Console.WriteLine($"{Points} Points");
                }
            }
        }

        private void WriteWithBetterColors(int color, string sym = "█")
        {
            Console.Write("\x1b[38;5;" + color + "m");
            if (sym != "")
            {
                Console.Write(sym);
                Console.ResetColor();
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
        private bool IsZPiece(int i, int j)
        {
            return BoardArray[i, j] == 8;
        }

        private bool IsJPiece(int i, int j)
        {
            return BoardArray[i, j] == 7;
        }

        private bool IsTPiece(int i, int j)
        {
            return BoardArray[i, j] == 6;
        }

        private bool IsSPiece(int i, int j)
        {
            return BoardArray[i, j] == 5;
        }

        private bool IsBlockPiece(int i, int j)
        {
            return BoardArray[i, j] == 4;
        }

        private bool IsLPiece(int i, int j)
        {
            return BoardArray[i, j] == 3;
        }

        private bool IsIPiece(int i, int j)
        {
            return BoardArray[i, j] == 2;
        }

        private bool IsWall(int i, int j)
        {
            return BoardArray[i, j] == 1;
        }

        private bool IsEmpty(int i, int j)
        {
            return BoardArray[i, j] == 0;
        }
    }
}
