﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTetris
{
    public abstract class Piece
    {
        internal byte[,] BlockMatrix { get; set; }
        internal Vector2 Position { get; set; }
        public void DrawBlock(byte[,] board, Vector2 dir)
        {
            UndrawBlock(Position, board);
            //CheckForCol(board, Position);
            var blockSegment = GetBlockSegments(Position);
            var checkSidesForCol = CheckSides(blockSegment);
            if (checkSidesForCol)
            {
                CalculateNewPos(dir, board, false);
            }
            else
            {
                CalculateNewPos(dir, board);
            }
            var y = (int)Position.Y;
            var x = (int)Position.X;
            //there must be a better way to do this...
            for (int k = 0; k < BlockMatrix.GetLength(0); k++)
                for (int l = 0; l < BlockMatrix.GetLength(1); l++)
                {
                    if (BlockMatrix[k, l] != 0)
                    {
                        board[y + k, x + l] = BlockMatrix[k, l];
                    }
                }

        }

        public bool CheckSides(List<Vector2> segments)
        {
            foreach (Vector2 blockSegment in segments)
            {
                int x = (int) blockSegment.X;
                int y = (int) blockSegment.Y;
                if (x > 15 || x < 1)
                {
                    return true;
                }

                if (MapMang.Manager.LandedArray[y, x + 1] > 0) return true;
                if (MapMang.Manager.LandedArray[y, x - 1] > 0) return true;
            }

            return false;
        }

        public void UndrawBlock(Vector2 position, byte[,] board)
        {
            var y = (int) position.Y;
            var x = (int) position.X;
            {
                for (int k = 0; k < BlockMatrix.GetLength(0); k++)
                for (int l = 0; l < BlockMatrix.GetLength(1); l++)
                {
                    if (BlockMatrix[k, l] != 0)
                    {
                        board[y + k, x + l] = 0;
                    }
                }
            }
        }
        public void CalculateNewPos(Vector2 direction, byte[,] board, bool isValid = true)
        {
            if (!isValid)
            {
                Position = new Vector2(Position.X, Position.Y + 1f);
                return;
            }

            bool hittingSides = false;

            for (int k = 0; k < BlockMatrix.GetLength(0); k++)
            for (int l = 0; l < BlockMatrix.GetLength(1); l++)
            {
                if (BlockMatrix[k, l] == 0) continue;
                if (board[(int)Position.Y + k, (int)Position.X + l + (int)direction.X] == 1)
                {
                    hittingSides = true;
                }
            }

            if (hittingSides)
            {
                Position = new Vector2(Position.X, Position.Y + 1f);
                return;
            }
            Position = new Vector2(Position.X + direction.X, Position.Y + direction.Y);
        }

        public bool CheckForCol(byte[,] boardArray, Vector2 dir)
        {
            return CheckEachBlockForCol(dir, Position);
        }
        private bool CheckEachBlockForCol(Vector2 dir, Vector2 destination)
        {
            var blockSegments = GetBlockSegments(destination);

            bool hasCollision = false;
            foreach (Vector2 blockSegment in blockSegments)
            {
                int x = (int)blockSegment.X;
                int y = (int)blockSegment.Y;
                if (y + 1 < 21)
                {
                    if (MapMang.Manager.LandedArray[y + 1, x] > 0)
                    {
                        hasCollision = true;
                    }
                }
                else
                {
                    if (MapMang.Manager.LandedArray[y, x] > 0)
                    {
                        hasCollision = true;
                    }
                }
                if (hasCollision)
                {
                    for (int k = 0; k < BlockMatrix.GetLength(0); k++)
                    for (int l = 0; l < BlockMatrix.GetLength(1); l++)
                    {
                        if (BlockMatrix[k, l] > 0)
                        {
                            MapMang.Manager.LandedArray[(int) Position.Y + k, (int) Position.X + l] = BlockMatrix[k, l];
                        }
                    }
                    MapMang.Manager.BoardArray = (byte[,])MapMang.Manager.LandedArray.Clone();
                }
            }
            return hasCollision;
        }

        private List<Vector2> GetBlockSegments(Vector2 destination)
        {
            List<Vector2> blockSegments = new List<Vector2>();

            var height = BlockMatrix.GetLength(0);
            var width = BlockMatrix.GetLength(1);

            for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
            {
                if (BlockMatrix[y, x] == 0) continue;
                Vector2 segmentPosition = destination;
                segmentPosition.X += x;
                segmentPosition.Y += y;

                blockSegments.Add(segmentPosition);
            }

            return blockSegments;
        }


        //what the fuck...
        public void RotateBlockMatrix()
        {
            byte[,] newMatrix = new byte[BlockMatrix.GetLength(1), BlockMatrix.GetLength(0)];
            int newColumn, newRow = 0;
            for (int oldColumn = BlockMatrix.GetLength(1) - 1; oldColumn >= 0; oldColumn--)
            {
                newColumn = 0;
                for (int oldRow = 0; oldRow < BlockMatrix.GetLength(0); oldRow++)
                {
                    newMatrix[newRow, newColumn] = BlockMatrix[oldRow, oldColumn];
                    newColumn++;
                }
                newRow++;
            }
            BlockMatrix = newMatrix;
        }
    }
}