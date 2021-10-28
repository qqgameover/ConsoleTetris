using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTetris
{
    public class TPiece : Piece
    {
        public TPiece()
        {
            BlockMatrix = new byte[,] { { 0, 1, 0, 0 }, { 1, 1, 1, 0 } };
            Position = new Vector2(8f, 1f);
        }
    }
}
