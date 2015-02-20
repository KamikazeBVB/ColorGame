using Check24ColorGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Check24ColorGameTests.Mocks
{
    public sealed class FakeBoard : IBoard
    {
        private int _moveIndex = 0;

        private List<List<Move>> _validMoves;

        public FakeBoard()
        {
            var randomCoords = new List<Coordinates>();

            for (int counter = 0; counter < 5; counter++)
            {
                randomCoords.Add(new Coordinates(counter, counter + 100));
            }

            var fakeBorder1 = new Dictionary<string, Coordinates>();
            var fakeBorder2 = new Dictionary<string, Coordinates>();
            var fakeBorder3 = new Dictionary<string, Coordinates>();

            randomCoords.Take(3).ToList().ForEach(item => fakeBorder1.Add(item.Key, item));

            randomCoords.Take(5).ToList().ForEach(item => fakeBorder2.Add(item.Key, item));
          

            this._validMoves = new List<List<Move>> 
            {
                new List<Move>
                {
                    new Move(1, fakeBorder1)
                },

                new List<Move>
                {
                     new Move(1, fakeBorder1),
                     new Move(2, fakeBorder1),
                     new Move(3, fakeBorder2),
                     new Move(4, fakeBorder2),
                }
            };


        }

        public IEnumerable<Move> GreedyResult
        {
            get
            {
                var result = new List<Move>
                {
                    this._validMoves[0][0],
                    this._validMoves[1][3],
                };

                return result;
            }
        }

        public IEnumerable<Move> GenerateValidMoves()
        {
            return _validMoves[this._moveIndex];
        }

        public bool IsGameOver()
        {
            return this._moveIndex == 2;
        }

        public void MakeMove(Move move)
        {
            this._moveIndex++;
        }
    }
}
