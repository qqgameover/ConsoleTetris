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
            BlockMatrix = new byte[,] { { 8, 8, 0, 0 }, { 0, 8, 8, 0 }, { 0, 0, 0, 0 } };
            Position = new Position(1, 5);
        }
    }
}
