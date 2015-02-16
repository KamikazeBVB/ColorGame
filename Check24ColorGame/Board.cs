using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Check24ColorGame
{
    public sealed class Board
    {
        private class Coordinates
        {
            public Coordinates(int row, int column)
            {
                this.Row = row;
                this.Column = column;
            }

            public int Row { get; set; }
            public int Column { get; set; }
        }

        private int[,] _boardState;
        private int _boardSize;
        private int _validColorsCount;

        public Board(int boardSize, int validColorCount)
        {
            this.BoardSize = boardSize;

            this.ValidColors = validColorCount;

            this._boardState = new int[boardSize, boardSize];

            Random rand = new Random(DateTime.Now.Millisecond);
            for (int row = 0; row < boardSize; row++)
            {
                for (int col = 0; col < boardSize; col++)
                {
                    _boardState[row, col] = rand.Next(validColorCount);
                }
            }
        }

        public Board(int[,] boardState, int validColorCount)
        {
            if (boardState == null || boardState.GetLength(0) != boardState.GetLength(1) || boardState.GetLength(0) < 1)
                throw new ArgumentException("The board must contain n*n tiles and needs to contain at least one tile!");
            this.ValidColors = validColorCount;
            this.BoardSize = boardState.GetLength(0);
            this._boardState = (int[,])boardState.Clone();
        }

        private bool CanGoDown(Coordinates coord, int currentColor, bool[,] visitedSites)
        {
            return coord.Row + 1 < this.BoardSize
                && this._boardState[coord.Row + 1, coord.Column] == currentColor
                && !visitedSites[coord.Row + 1, coord.Column];
        }

        private bool CanGoRight(Coordinates coord, int currentColor, bool[,] visitedSites)
        {
            return coord.Column + 1 < this.BoardSize
                 && this._boardState[coord.Row, coord.Column + 1] == currentColor
                 && !visitedSites[coord.Row, coord.Column + 1];
        }

        private int CalculateMoveScore(int moveColor)
        {
            Stack<Coordinates> coordinateStack = new Stack<Coordinates>();

            coordinateStack.Push(new Coordinates(0, 0));

            bool[,] visitedSites = new bool[this.BoardSize, this.BoardSize];

            int result = 1;
            while (coordinateStack.Count > 0)
            {
                var currentPosition = coordinateStack.Pop();

                if (CanGoDown(currentPosition, moveColor, visitedSites))
                {
                    var newCoord = new Coordinates(currentPosition.Row + 1, currentPosition.Column);
                    coordinateStack.Push(newCoord);
                    visitedSites[newCoord.Row, newCoord.Column] = true;
                    result++;
                }

                if (CanGoRight(currentPosition, moveColor, visitedSites))
                {
                    var newCoord = new Coordinates(currentPosition.Row, currentPosition.Column + 1);
                    coordinateStack.Push(newCoord);
                    visitedSites[newCoord.Row, newCoord.Column] = true;
                    result++;
                }
            }

            return result;
        }

        public int BoardSize
        {
            get
            {
                return _boardSize;
            }
            private set
            {
                if (value < 1)
                    throw new ArgumentException("The board size cannot be saller than 1!");
                this._boardSize = value;
            }
        }

        public int[,] BoardState
        {
            get
            {
                return (int[,])_boardState.Clone();
            }
        }

        public int ValidColors
        {
            get
            {
                return _validColorsCount;
            }
            private set
            {
                if (value < 1)
                    throw new ArgumentException("We need to play the game using at least one color!");
                this._validColorsCount = value;
            }
        }

        public IEnumerable<Move> GenerateValidMoves()
        {
            int originColor = this._boardState[0, 0];

            var result = new List<Move>();

            for (int currentCollor = 0; currentCollor < this.ValidColors; currentCollor++)
            {
                if (currentCollor == originColor)
                    continue;

                int moveScore = this.CalculateMoveScore(currentCollor);

                result.Add(new Move(currentCollor, moveScore));
            }

            return result;
        }

        public bool IsGameOver()
        {
            int originColor = this._boardState[0, 0];

            for (int row = 0; row < this.BoardSize; row++)
            {
                for (int col = 0; col < this.BoardSize; col++)
                {
                    if (originColor != this._boardState[row, col])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

    }
}
