using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Check24ColorGame
{
    public sealed class Move : IComparable<Move>
    {
        public Move(int moveColor)
        {
            this.MoveColor = moveColor;
        }

        public Move(int moveColor, int moveScore)
        {
            this.MoveColor = moveColor;
            this.MoveScore = moveScore;
        }

        public int MoveColor { get; private set; }

        public int MoveScore { get; set; }

        public int CompareTo(Move other)
        {
            return this.MoveColor == other.MoveColor && this.MoveScore == other.MoveScore ? 0 : 1;
        }
    }
}
