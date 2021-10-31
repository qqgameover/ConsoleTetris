using System;
using System.Runtime.InteropServices;

namespace ConsoleTetris
{
    class Program
    {
        static void Main()
        {
            var game = new Game();
            game.GamePlayLoop();
        }
    }
}
