using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Check24ColorGame
{
    public sealed class Check24AstarPlayer : Check24Player
    {

        private class AstarGraphNode : IComparable<AstarGraphNode>
        {
            public AstarGraphNode(IBoard currentBoard)
            {
                this.Board = currentBoard;
            }
            public IBoard Board { get; set; }
            public Move MoveToMake { get; set; }
            public AstarGraphNode Previous { get; set; }
            public double EstimatedTotalCost 
            {
                get
                {
                    return this.CostSoFar +
                           Math.Pow(this.Board.BoardSize,2) -
                           this.Board.BoderSize;
                }
            
            }
            public double CostSoFar { get; set; }

            public int CompareTo(AstarGraphNode other)
            {
                if (this.EstimatedTotalCost > other.EstimatedTotalCost)
                    return 1;
                if (this.EstimatedTotalCost < other.EstimatedTotalCost)
                    return -1;
                return 0;
            }
        }

        public Check24AstarPlayer(IBoard game)
            : base(game)
        {

        }

        private SortedSet<AstarGraphNode> _openNodes = new SortedSet<AstarGraphNode>();

        private Dictionary<string, AstarGraphNode> _openNodeMap = new Dictionary<string, AstarGraphNode>();

        private Dictionary<string, AstarGraphNode> _closedNodes = new Dictionary<string, AstarGraphNode>();

        private AstarGraphNode FindStrategy()
        {
            AstarGraphNode currentNode = new AstarGraphNode(this._game);
            currentNode.CostSoFar = 0;
            currentNode.Previous = null;
            currentNode.MoveToMake = null;

            this._openNodes.Add(currentNode);
            this._openNodeMap.Add(currentNode.Board.ToString(), currentNode);

            while (this._openNodes.Count > 0)
            {
                currentNode = this._openNodes.ElementAt(0);

                if (currentNode.Board.IsGameOver())
                    return currentNode;

                this._openNodes.Remove(currentNode);
                this._openNodeMap.Remove(currentNode.Board.ToString());

                this._closedNodes.Add(currentNode.Board.ToString(), currentNode);

                var moves = currentNode.Board.GenerateValidMoves();

                foreach (var move in moves)
                {
                    var testBoard = new Board(currentNode.Board.BoardState, currentNode.Board.ValidColors);

                    testBoard.MakeMove(move);

                    if (this._closedNodes.ContainsKey(testBoard.ToString()))
                        continue;

                    var newSearchTreeNode = new AstarGraphNode(testBoard)
                    {
                        MoveToMake = move,
                        CostSoFar = currentNode.CostSoFar + 1,
                        Previous = currentNode,
                    };

                    var auxKey = newSearchTreeNode.Board.ToString();

                    if (!this._openNodeMap.ContainsKey(auxKey))
                    {
                        this._openNodeMap.Add(auxKey, newSearchTreeNode);
                        this._openNodes.Add(newSearchTreeNode);
                    }
                    else if (this._openNodeMap[auxKey].CostSoFar > currentNode.CostSoFar)
                    {
                        var modifiedNode = this._openNodeMap[auxKey];
                        modifiedNode.CostSoFar = newSearchTreeNode.CostSoFar;
                        modifiedNode.Previous = currentNode;
                    }

                }

            }
            throw new Exception("No solution found! This is impossible for this game, please report the bug to the developer!");
        }

        private List<Move> ReconstructMoveSequence(AstarGraphNode strategy)
        {
            var result = new List<Move>();

            while (strategy.MoveToMake != null)
            {
                result.Add(strategy.MoveToMake);
                strategy = strategy.Previous;
            }
            
            result.Reverse();
            
            return result;
        }

        public override string Name
        {
            get { return "A*"; }
        }

        public override IEnumerable<Move> GetStrategy()
        {
            List<Move> result = new List<Move>();

            var strategy = this.FindStrategy();

            result = this.ReconstructMoveSequence(strategy);

            return result;
        }

    }
}
