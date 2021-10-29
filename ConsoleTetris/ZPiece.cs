using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTetris
{
    class ZPiece : Piece
    {
        public ZPiece()
        {
            BlockMatrix = new byte[,] { { 0, 5, 5, 0 }, { 0, 0, 5, 5 }, { 0, 0, 0, 0 } };
            Position = new Vector2(5f, 1f);
        }
    }
}
