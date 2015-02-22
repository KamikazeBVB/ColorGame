using System;
using Check24ColorGame;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Check24ColorGameTests
{
    [TestClass]
    public class Check24AstarTests
    {
        [TestMethod]
        public void TestIfTheAlgorithmFindsASolution()
        {
            //The example from the exercise sheet
            int[,] boardState = new int[6, 6] 
            { { 1, 0, 1, 2, 1, 0}, 
              { 2, 1, 2, 0, 2, 0 },
              { 2, 2, 0, 2, 2, 2},
              { 2, 0, 1, 2, 1, 0 },
              { 2, 0, 1, 0, 0, 0 },
              { 1, 2, 1, 0, 2, 1 },
            };

            Board board = new Board(boardState, 3);

            Check24Player player = new Check24AstarPlayer(board);

            var moves = player.GetStrategy();

            foreach (var move in moves)
            {
                board.MakeMove(move);
            }

            Assert.IsTrue(board.IsGameOver());
        }
    }
}
