using System;
namespace Check24ColorGame
{
    public interface IBoard
    {
        System.Collections.Generic.IEnumerable<Move> GenerateValidMoves();
        bool IsGameOver();
        void MakeMove(Move move);
    }
}
