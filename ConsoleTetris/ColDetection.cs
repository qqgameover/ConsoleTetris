using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTetris
{
    public static class ColDetection
    {
        public static bool CheckSides(List<Position> segments, Position dir)
        {
            foreach (var blockSegment in segments)
            {
                int dirY = dir.Y;
                int dirX = dir.X;
                int x = blockSegment.X;
                int y = blockSegment.Y;
                if (IntersectWithSides(x))
                {
                    return true;
                }

                if (BlockIsOccupied(y + dirY, x + dirX, MapMang.Manager.LandedArray)) return true;
            }

            return false;
        }

        public static bool CheckEachBlockForCol(List<Position> blockSegments,
            Position position, byte[,] blockMatrix, Position dir)

        {
            bool hasCollision = false;
            foreach (var blockSegment in blockSegments)
            {
                int x = blockSegment.X;
                int y = blockSegment.Y + dir.Y;
                if (BlockIsOccupied(y, x, MapMang.Manager.LandedArray))
                {
                    hasCollision = true;
                }
                if (hasCollision)PutMatrixInLandedArray(position, blockMatrix);
            }
            return hasCollision;
        }

        private static void PutMatrixInLandedArray(Position position, byte[,] blockMatrix)
        {
            for (int k = 0; k < blockMatrix.GetLength(0); k++)
                for (int l = 0; l < blockMatrix.GetLength(1); l++)
                {
                    if (BlockIsOccupied(k, l, blockMatrix))
                    {
                        MapMang.Manager.LandedArray[position.Y + k, position.X + l] = blockMatrix[k, l];
                    }
                }
            MapMang.Manager.BoardArray = (byte[,]) MapMang.Manager.LandedArray.Clone();
        }

        public static bool BlockIsOccupied(int y, int x, byte[,] matrix)
        {
            return matrix[y, x] > 0;
        }
        public static bool BlockIsEmpty(int y, int x, byte[,] matrix)
        {
            return matrix[y, x] == 0;
        }

        private static bool IntersectWithSides(int posX)
        {
            return (posX > 12 || posX < 1);
        }
    }
}
