using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTetris
{
    class MapMang
    {
        // static singleton
        private static MapMang manager = new MapMang();
        private MapMang() { }

        public static MapMang Manager => manager;

        public byte[,] BoardArray;
        public byte[,] LandedArray;
    }
}