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
        public static Vector2 HandleInput(Piece p)
        {
            if (Console.KeyAvailable == false) return new Vector2(0f, 1f);
            var cki = Console.ReadKey(true);
            if (cki.Key == ConsoleKey.W)
            {
                p.UndrawBlock(p.Position, MapMang.Manager.BoardArray);
                p.RotateBlockMatrix();
                return new Vector2(0f, 1f);
            }
            if (cki.Key == ConsoleKey.R)
            {
                return new Vector2(0f, 1f);
            }
            if (cki.Key == ConsoleKey.A)
            {
                return new Vector2(-1f, 0f);
            }
            if (cki.Key == ConsoleKey.S)
            {
                return new Vector2(1f, 0f);
            }
            return new Vector2(0f, 1f);
        }
    }
}
