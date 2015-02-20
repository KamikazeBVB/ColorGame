using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Check24ColorGame
{
    public sealed class Move : IEquatable<Move>
    {
        private Dictionary<string, Coordinates> _border;

        public Move(int moveColor, Dictionary<string, Coordinates> border)
        {
            this.Color = moveColor;
            this._border = border;
        }

        public int Color { get; private set; }

        public Dictionary<string, Coordinates> Border 
        {
            get
            {
                Dictionary<string, Coordinates> result = new Dictionary<string, Coordinates>();

                this._border.Values.ToList().ForEach(item => result.Add(item.Key, new Coordinates(item.Row, item.Column)));

                return result;
            }
          
        }

        public bool Equals(Move other)
        {
            if (this.Color == other.Color
                     && this.Border.Count == other.Border.Count)
            {
                var discriminatingItem = this.Border.FirstOrDefault(item => !other.Border.ContainsKey(item.Key));
                return discriminatingItem.Equals(default(KeyValuePair<string, Coordinates>));
            }
            return false;
        }

    }
}

