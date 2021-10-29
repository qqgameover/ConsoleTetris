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
            BlockMatrix = new byte[,] { { 2, 2, 2, 2} };
            Position = new Vector2(8f, 1f);
        }
    }
}
