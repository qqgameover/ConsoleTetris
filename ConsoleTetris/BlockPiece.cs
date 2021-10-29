using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTetris
{
    class BlockPiece : Piece
    {
        public BlockPiece()
        {
            BlockMatrix = new byte[,] {{0, 4, 4, 0}, { 0, 4, 4, 0 }};
            Position = new Vector2(5f, 1f);
        }
    }
}
