using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Media;
using Random = System.Random;

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
            Console.SetWindowSize(10, 22);
            Console.SetBufferSize(400, 200);
            GetNewRandomPiece();
            SoundPlayer player = new SoundPlayer("Tetris.wav");
            player.PlayLooping();

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
                    GetNewRandomPiece();
                    if (MapMang.Manager.LandedArray[2, 4] > 0) break;
                }
                CurrentPiece.DrawBlock(Board.BoardArray, x);
                Board.DrawBoard();
            }
        }

        private void GetNewRandomPiece()
        {
            Random rng = new Random();
            var r = rng.Next(0, PieceArray.Length);
            if (r == 0) CurrentPiece = new BlockPiece();
            if (r == 1) CurrentPiece = new IPiece();
            if (r == 2) CurrentPiece = new LPiece();
            if (r == 3) CurrentPiece = new SPiece();
            if (r == 4) CurrentPiece = new TPiece();
        }
    }
}
