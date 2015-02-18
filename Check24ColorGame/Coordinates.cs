using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Check24ColorGame
{
    public sealed class Coordinates
    {
        public Coordinates(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }

        public int Row { get; private set;}
        public int Column { get; private set;}

        public string Key
        {
            get
            {
                return string.Format("{0}_{1}", Row, Column);
            }
        }
    }
}
