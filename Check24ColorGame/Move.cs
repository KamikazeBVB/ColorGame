using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Check24ColorGame
{
    public sealed class Move
    {
        public Move(int moveColor, Dictionary<string, Coordinates> border)
        {
            this.Color = moveColor;
            this.Border = border;
        }

        public int Color { get; private set; }

        public Dictionary<string, Coordinates> Border { get; private set; }

        public override bool Equals(object obj)
        {
            if (obj is Move)
            {
                var other = obj as Move;
                if (other != null)
                {
                    var discriminatingItem = this.Border.FirstOrDefault(item => !other.Border.ContainsKey(item.Key));

                    return this.Color == other.Color
                        && discriminatingItem.Equals(default(KeyValuePair<string, Coordinates>));
                }
            }
            return false;
        }
    }
}
