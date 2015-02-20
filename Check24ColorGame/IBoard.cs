using System;
using System.Collections.Generic;
namespace Check24ColorGame
{
    public interface IBoard
    {
        IEnumerable<Move> GenerateValidMoves();
        bool IsGameOver();
        void MakeMove(Move move);
    }
}
