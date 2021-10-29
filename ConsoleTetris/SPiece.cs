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
            BlockMatrix = new byte[,] { { 0, 0, 5, 5 }, { 0, 5, 5, 0 } };
            Position = new Vector2(7f, 1f);
        }
    }
}
