using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Check24ColorGame
{
    public sealed class Check24GreedyPlayer : Check24Player
    {
        public Check24GreedyPlayer(Board game)
            : base(game)
        {

        }
        public override string Name
        {
            get { return "Greedy"; }
        }

        protected override Move ChooseMove(IEnumerable<Move> availableMoves)
        {
            Move result;
           
            int maxBorderExtension = availableMoves.Max(item => item.Border.Count);

            var movesWithMaximumBorderExtension = availableMoves.Where(item => item.Border.Count == maxBorderExtension).ToList();

            if (movesWithMaximumBorderExtension.Count > 1)
            {
                int maxColorRank = movesWithMaximumBorderExtension.Max(item => item.Color);
                result = movesWithMaximumBorderExtension.First(item => item.Color == maxColorRank);
            }
            else
            {
                result = movesWithMaximumBorderExtension.First();
            }
            return result;
        }
    }
}
