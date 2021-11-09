using System.Collections.Generic;
using static ConsoleTetris.ColDetection;

namespace ConsoleTetris
{
    public abstract class Piece
    {
        internal byte[,] BlockMatrix { get; set; }
        internal Position Position { get; set; }
        public void DrawBlock(byte[,] board, Position dir)
        {
            UndrawBlock(Position, board);
            var blockSegment = GetBlockSegments(Position, BlockMatrix);
            var checkSidesForCol = CheckSides(blockSegment, dir);
            if (checkSidesForCol)
            {
                CalculateNewPos(dir, board, false);
            }
            else
            {
                CalculateNewPos(dir, board);
            }
            var y = Position.Y;
            var x = Position.X;
            //there must be a better way to do this...
            for (int k = 0; k < BlockMatrix.GetLength(0); k++)
                for (int l = 0; l < BlockMatrix.GetLength(1); l++)
                {
                    if (BlockIsOccupied(k, l, BlockMatrix))
                    {
                        board[y + k, x + l] = BlockMatrix[k, l];
                    }
                }

        }

        public void UndrawBlock(Position position, byte[,] board)
        {
            var y = position.Y;
            var x = position.X;
            {
                for (int k = 0; k < BlockMatrix.GetLength(0); k++)
                for (int l = 0; l < BlockMatrix.GetLength(1); l++)
                {
                    if (BlockIsOccupied(k, l, BlockMatrix))
                    {
                        board[y + k, x + l] = 0;
                    }
                }
            }
        }

        public void CalculateNewPos(Position direction, byte[,] board, bool isValid = true)
        {
            if (!isValid)
            {
                Position = new Position(Position.Y + 1, Position.X);
                return;
            }

            bool hittingSides = false;

            for (int k = 0; k < BlockMatrix.GetLength(0); k++)
            for (int l = 0; l < BlockMatrix.GetLength(1); l++)
            {
                if (BlockIsEmpty(k, l, BlockMatrix)) continue;
                if (board[Position.Y + k, Position.X + l + direction.X] == 1)
                {
                    hittingSides = true;
                }
            }

            if (hittingSides)
            {
                Position = new Position(Position.Y + 1, Position.X);
                return;
            }
            Position += direction;
        }


        public bool CheckForCol(Position dir)
        {
            var blockSegments = GetBlockSegments(Position, BlockMatrix);
            return CheckEachBlockForCol(blockSegments, Position, BlockMatrix, dir);
        }

        private List<Position> GetBlockSegments(Position destination, byte[,]blockMatrix)
        {
            List<Position> blockSegments = new List<Position>();

            var height = blockMatrix.GetLength(0);
            var width = blockMatrix.GetLength(1);

            for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
            {
                if (BlockIsEmpty(y, x, blockMatrix)) continue;
                Position segmentPosition = destination;
                segmentPosition.X += x;
                segmentPosition.Y += y;

                blockSegments.Add(segmentPosition);
            }

            return blockSegments;
        }


        public void RotateBlockMatrix()
        {
            byte[,] newMatrix = new byte[BlockMatrix.GetLength(1), BlockMatrix.GetLength(0)];
            int newRow = 0;
            for (int oldColumn = BlockMatrix.GetLength(1) - 1; oldColumn >= 0; oldColumn--)
            {
                var newColumn = 0;
                for (int oldRow = 0; oldRow < BlockMatrix.GetLength(0); oldRow++)
                {
                    newMatrix[newRow, newColumn] = BlockMatrix[oldRow, oldColumn];
                    newColumn++;
                }
                newRow++;
            }
            //Rotating matrix until it either finds a fitting spot, or gives up.
            //if no match is found then the block is simply not rotated.
            var blockSegments = GetBlockSegments(Position, newMatrix);
            WallKick(blockSegments);
            var tests = TestAllRotations(newMatrix);
            if(!tests) return;
            BlockMatrix = newMatrix;
        }

        //single test, looking for overlaps.
        private bool TestRotation(byte[,] newMatrix)
        {
            var blockSegments = GetBlockSegments(Position, newMatrix);
            foreach (var blockSegment in blockSegments)
            {
                int x = blockSegment.X;
                int y = blockSegment.Y;
                if (BlockIsOccupied(y, x, MapMang.Manager.LandedArray))
                {
                    return false;
                }
            }

            return true;
        }

        //all the tests, will return if it finds a match. 
        private bool TestAllRotations(byte[,] newMatrix)
        {
            for (int i = 0; i < 4; i++)
            {
                bool testPosition = TestRotation(newMatrix);
                if (testPosition) return true;
            }

            return false;
        }

        //Will kick the block out of the wall if its rotation results in it being in the wall. 
        private void WallKick(List<Position> blockSegments)
        {
            foreach (var segment in blockSegments)
            {
                var y = segment.Y;
                var x = segment.X;
                if (NextToLeftWall(x))
                {
                    Position = new Position(Position.Y, Position.X + 1);
                }
                if (NextToRightWall(x))
                {
                    Position = new Position(Position.Y, Position.X - 1);
                }
            }
        }

        private static bool NextToLeftWall(int x)
        {
            return x < 1;
        }

        private static bool NextToRightWall(int x)
        {
            return x > 10;
        }
    }
}