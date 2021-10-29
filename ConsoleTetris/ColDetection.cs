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
        public static bool CheckSides(List<Vector2> segments)
        {
            foreach (Vector2 blockSegment in segments)
            {
                int x = (int)blockSegment.X;
                int y = (int)blockSegment.Y;
                if (x > 10 || x < 1)
                {
                    return true;
                }

                //if (MapMang.Manager.LandedArray[y, x + 1] > 0) return true;
                //if (MapMang.Manager.LandedArray[y, x - 1] > 0) return true;
            }

            return false;
        }

        public static bool CheckEachBlockForCol(List<Vector2> blockSegments,
            Vector2 position, byte[,] blockMatrix, Vector2 dir)

        {
            bool hasCollision = false;
            foreach (Vector2 blockSegment in blockSegments)
            {
                int x = (int) blockSegment.X;
                int y = (int) (blockSegment.Y + dir.Y);
                if (MapMang.Manager.LandedArray[y, x] > 0)
                {
                    hasCollision = true;
                }
                if (hasCollision)MatrixLoop(position, blockMatrix);
            }

            return hasCollision;
        }

        private static void MatrixLoop(Vector2 position, byte[,] blockMatrix)
        {
            for (int k = 0; k < blockMatrix.GetLength(0); k++)
                for (int l = 0; l < blockMatrix.GetLength(1); l++)
                {
                    if (blockMatrix[k, l] > 0)
                    {
                        MapMang.Manager.LandedArray[(int) position.Y + k, (int) position.X + l] = blockMatrix[k, l];
                    }
                }
            MapMang.Manager.BoardArray = (byte[,]) MapMang.Manager.LandedArray.Clone();
        }
    }
}
