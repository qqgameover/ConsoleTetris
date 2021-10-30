using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Media;
using System.Runtime.InteropServices;
using Random = System.Random;

namespace ConsoleTetris
{
    public class Game
    {

        public Controls Controls { get; }
        public Board Board { get; private set; }
        public Piece CurrentPiece { get; private set; }

        public Game()
        {
            Controls = new Controls();
            Board = new Board();
        }

        public void GamePlayLoop()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                ConsoleStuff();
                PlaySong();
            }
            GetNewRandomPiece();
            Board.DrawBoard();
            while (true)
            {
                Thread.Sleep(250);
                var x = Controls.HandleInput(CurrentPiece);
                var collided = CurrentPiece.CheckForCol(x);
                if (collided)
                {
                    Board.CheckForTetris();
                    Board.DrawBoard();
                    GetNewRandomPiece();
                    if (MapMang.Manager.LandedArray[2, 5] > 0) break;
                }
                CurrentPiece.DrawBlock(Board.BoardArray, x);
                Board.DrawBoard();
            }
        }

        private static void PlaySong()
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return;
            SoundPlayer player = new SoundPlayer("Tetris.wav");
            player.PlayLooping();

        }

        private static void ConsoleStuff()
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return;
            Console.SetWindowSize(12, 25);
            Console.SetBufferSize(400, 200);
        }

        private void GetNewRandomPiece()
        {
            Random rng = new Random();
            var r = rng.Next(0, 7);
            CurrentPiece = r switch
            {
                0 => new BlockPiece(),
                1 => new IPiece(),
                2 => new LPiece(),
                3 => new SPiece(),
                4 => new TPiece(),
                5 => new JPiece(),
                6 => new ZPiece(),
                _ => CurrentPiece
            };
        }
    }
}
