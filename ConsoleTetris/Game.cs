﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTetris
{
    public class Game
    {
        public Controls Controls { get; }
        public Board Board { get; private set; }
        public Piece CurrentPiece { get; private set; }

        public Piece[] PieceArray { get; }

        public Game()
        {
            Controls = new Controls();
            Board = new Board();
            PieceArray = new Piece[] {new BlockPiece(), new IPiece(), new LPiece(), new TPiece(), new SPiece()};
        }

        public void GamePlayLoop()
        {
            Random rng = new Random();
            //var ran = rng.Next(0, PieceArray.Length);
            CurrentPiece = PieceArray[0];
            CurrentPiece = new BlockPiece();
            Board.DrawBoard();
            while (true)
            {
                var x = Controls.HandleInput(CurrentPiece);
                var collided = CurrentPiece.CheckForCol(Board.BoardArray, x);
                if (collided)
                {
                    Board.CheckForTetris();
                    Board.DrawBoard();
                    var r = rng.Next(0, PieceArray.Length);
                    CurrentPiece = PieceArray[r];
                    CurrentPiece.Position = new Vector2(8f, 1f);
                }
                CurrentPiece.DrawBlock(Board.BoardArray, x);
                Board.DrawBoard();
                Thread.Sleep(200);
            }
        }
    }
}