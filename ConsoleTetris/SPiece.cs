using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTetris
{
    class SPiece : Piece
    {
        public SPiece()
        {
            BlockMatrix = new byte[,] { { 0, 0, 1, 1 }, { 1, 1, 0, 0 } };
            Position = new Vector2(8f, 1f);
        }
    }
}
