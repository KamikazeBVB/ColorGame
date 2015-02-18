using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Check24ColorGame
{
    public abstract class Check24Player
    {
        protected IBoard _game;

        public Check24Player(IBoard game)
        {
            this._game = game;
        }
        
        public abstract string Name { get; }

        public IEnumerable<Move> GetStrategy()
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

        protected abstract Move ChooseMove(IEnumerable<Move> availableMoves);
        
        public override string ToString()
        {
            return string.Format("I'm the {0} player", this.Name);
        }
    }
}
