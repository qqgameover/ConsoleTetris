using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTetris
{
    public class Controls
    {
        public static Position HandleInput(Piece p)
        {
            if (Console.KeyAvailable == false) return new Position(1, 0);
            var cki = Console.ReadKey(true);
            if (cki.Key == ConsoleKey.UpArrow)
            {
                p.UndrawBlock(p.Position, MapMang.Manager.BoardArray);
                p.RotateBlockMatrix();
                return new Position(1, 0);
            }
            if (cki.Key == ConsoleKey.DownArrow)
            {
                return new Position(1, 0);
            }
            if (cki.Key == ConsoleKey.LeftArrow)
            {
                return new Position(1, -1);
            }
            if (cki.Key == ConsoleKey.RightArrow)
            {
                return new Position(1, 1);
            }
            return new Position(1, 0);
        }
    }
}
