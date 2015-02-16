using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Check24ColorGame
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] boardState = new int[1, 1] { { 0 } };
            var board = new Board(boardState, 2);

            var legalMoves = board.GenerateValidMoves().ToList();

            var expectedMove = new Move(1, 1);

            
        }
    }
}
