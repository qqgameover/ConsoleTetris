using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTetris
{
    class IPiece : Piece
    {
        public IPiece()
        {
            BlockMatrix = new byte[,] { { 0, 0, 0, 0 }, { 2, 2, 2, 2} };
            Position = new Position(0, 5);
        }
    }
}
