using System;
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
            Console.SetWindowSize(16, 22);
            Console.SetBufferSize(400, 200);
            CurrentPiece = new BlockPiece();
            Board.DrawBoard();
            while (true)
            {
                Thread.Sleep(200);
                var x = Controls.HandleInput(CurrentPiece);
                var collided = CurrentPiece.CheckForCol(x);
                if (collided)
                {
                    Board.CheckForTetris();
                    Board.DrawBoard();
                    var r = rng.Next(0, PieceArray.Length);
                    CurrentPiece = PieceArray[r];
                    CurrentPiece.Position = new Vector2(8f, 1f);
                    if(MapMang.Manager.LandedArray[2, 8] > 0) break;
                }
                CurrentPiece.DrawBlock(Board.BoardArray, x);
                Board.DrawBoard();
            }
        }
    }
}
