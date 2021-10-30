using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTetris
{
    class JPiece : Piece
    {
        public JPiece()
        {
            BlockMatrix = new byte[,] { { 7, 0, 0, 0 }, { 7, 7, 7, 0 }, { 0, 0, 0, 0 } };
            Position = new Vector2(5f, 1f);
        }
    }
}
