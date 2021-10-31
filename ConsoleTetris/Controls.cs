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
            if (cki.Key == ConsoleKey.UpArrow)
            {
                p.UndrawBlock(p.Position, MapMang.Manager.BoardArray);
                p.RotateBlockMatrix();
                return new Vector2(0f, 1f);
            }
            if (cki.Key == ConsoleKey.DownArrow)
            {
                return new Vector2(0f, 1f);
            }
            if (cki.Key == ConsoleKey.LeftArrow)
            {
                return new Vector2(-1f, 1f);
            }
            if (cki.Key == ConsoleKey.RightArrow)
            {
                return new Vector2(1f, 1f);
            }
            return new Vector2(0f, 1f);
        }
    }
}
