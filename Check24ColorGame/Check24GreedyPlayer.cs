using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Check24ColorGame
{
    public sealed class Check24GreedyPlayer : Check24Player
    {
        public Check24GreedyPlayer(IBoard game)
            : base(game)
        {

        }
       
        private Move ChooseMove(IEnumerable<Move> availableMoves)
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

        public override string Name
        {
            get { return "Greedy"; }
        }

        public override IEnumerable<Move> GetStrategy()
        {
            var strategy = new List<Move>();

            while (!this._game.IsGameOver())
            {
                var availableMoves = this._game.GenerateValidMoves();

                var moveToMake = this.ChooseMove(availableMoves);

                this._game.MakeMove(moveToMake);

                strategy.Add(moveToMake);
            }

            return strategy;
        }

    }
}
