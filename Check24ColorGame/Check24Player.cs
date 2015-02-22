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

        public abstract IEnumerable<Move> GetStrategy();

        public override string ToString()
        {
            return string.Format("I'm the {0} player", this.Name);
        }
    }
}
