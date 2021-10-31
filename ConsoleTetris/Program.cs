using System;
using System.Runtime.InteropServices;

namespace ConsoleTetris
{
    class Program
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetConsoleMode(IntPtr hConsoleHandle, int mode);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool GetConsoleMode(IntPtr handle, out int mode);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetStdHandle(int handle);
        static void Main()
        {
            var game = new Game();
            game.GamePlayLoop();
        }
    }
}
