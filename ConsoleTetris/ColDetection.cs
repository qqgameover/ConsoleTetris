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
        public static bool CheckSides(List<Vector2> segments, Vector2 dir)
        {
            foreach (Vector2 blockSegment in segments)
            {
                int dirY = (int) dir.Y;
                int dirX = (int)dir.X;
                int x = (int)blockSegment.X;
                int y = (int)blockSegment.Y;
                if (IntersectWithSides(x))
                {
                    return true;
                }

                if (BlockIsOccupied(y + dirY, x + dirX, MapMang.Manager.LandedArray)) return true;
            }

            return false;
        }

        private static bool IntersectWithSides(int posX)
        {
            return (posX > 12 || posX < 1);
        }

        public static bool CheckEachBlockForCol(List<Vector2> blockSegments,
            Vector2 position, byte[,] blockMatrix, Vector2 dir)

        {
            bool hasCollision = false;
            foreach (Vector2 blockSegment in blockSegments)
            {
                int x = (int) blockSegment.X;
                int y = (int) (blockSegment.Y + dir.Y);
                if (BlockIsOccupied(y, x, MapMang.Manager.LandedArray))
                {
                    hasCollision = true;
                }
                if (hasCollision)PutMatrixInLandedArray(position, blockMatrix);
            }
            return hasCollision;
        }

        private static void PutMatrixInLandedArray(Vector2 position, byte[,] blockMatrix)
        {
            for (int k = 0; k < blockMatrix.GetLength(0); k++)
                for (int l = 0; l < blockMatrix.GetLength(1); l++)
                {
                    if (BlockIsOccupied(k, l, blockMatrix))
                    {
                        MapMang.Manager.LandedArray[(int) position.Y + k, (int) position.X + l] = blockMatrix[k, l];
                    }
                }
            MapMang.Manager.BoardArray = (byte[,]) MapMang.Manager.LandedArray.Clone();
        }

        private static bool BlockIsOccupied(int y, int x, byte[,] matrix)
        {
            return matrix[y, x] > 0;
        }
    }
}
