using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTetris
{
    public struct Position
    {
        public int Y;
        public int X;
        public Position(int y, int x)
        {
            Y = y;
            X = x;
        }

        public static Position operator +(Position lhs, Position rhs)
        {
            return new Position(lhs.Y + rhs.Y, lhs.X + rhs.X);
        }
    }
}
