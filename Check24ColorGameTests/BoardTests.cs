using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Check24ColorGame;
using System.Linq;

namespace Check24ColorGameTests
{
    [TestClass]
    public class BoardTests
    {

        #region Constructor Tests
        [TestMethod]
        public void InitializeBoard()
        {
            var board = new Board(3, 2);

            Assert.AreEqual(board.ValidColors, 2);
            Assert.AreEqual(board.BoardSize, 3);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void InitializeBoardInvalidSize()
        {
            var board = new Board(0, 3);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void InitializeBoardInvalidColorCount()
        {
            var board = new Board(3, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void InitilizeBoardWithInvalidState()
        {
            var board = new Board(new int[2, 1], 3);
        }

        [TestMethod]
        public void InitilizeBoardWithValidState()
        {
            var initialState = new int[2, 2];
            initialState[1, 1] = 1;

            var board = new Board(initialState, 3);

            CollectionAssert.AreEqual(initialState, board.BoardState);
        }
        #endregion

        #region Move generation and move making tests
        [TestMethod]
        public void GenerateMovesFromTrivialBoard()
        {
            int[,] boardState = new int[1, 1] { { 0 } };
            var board = new Board(boardState, 2);

            var legalMoves = board.GenerateValidMoves().ToList();

            var expectedMove = new Move(1, 1);

            Assert.AreEqual(1, legalMoves.Count);
            Assert.AreEqual(0, legalMoves[0].CompareTo(expectedMove));
        }

        [TestMethod]
        public void GenerateMovesForTwoByTwoBoardOnColumn()
        {
            int[,] boardState = new int[2, 2] { { 1,1 }, {0, 1} };
            var board = new Board(boardState, 2);

            var legalMoves = board.GenerateValidMoves().ToList();

            var expectedMove = new Move(0, 2);

            Assert.AreEqual(1, legalMoves.Count);
            Assert.AreEqual(0, legalMoves[0].CompareTo(expectedMove));
        }

        [TestMethod]
        public void GenerateMovesForTwoByTwoBoardOnRow()
        {
            int[,] boardState = new int[2, 2] { { 0, 0 }, { 1, 0 } };
            var board = new Board(boardState, 2);

            var legalMoves = board.GenerateValidMoves().ToList();

            var expectedMove = new Move(1, 2);

            Assert.AreEqual(1, legalMoves.Count);
            Assert.AreEqual(0, legalMoves[0].CompareTo(expectedMove));
        }

        [TestMethod]
        public void GenerateMovesForTwoByTwoBoardMoveInBothDirections()
        {
            int[,] boardState = new int[2, 2] { { 0, 1 }, { 1, 1 } };
            var board = new Board(boardState, 2);

            var legalMoves = board.GenerateValidMoves().ToList();

            var expectedMove = new Move(1, 4);

            Assert.AreEqual(1, legalMoves.Count);
            Assert.AreEqual(0, legalMoves[0].CompareTo(expectedMove));
        }

        #endregion

        #region End game tests
        [TestMethod]
        public void EndGameFalseTest()
        {
            var initialState = new int[2, 2];
            initialState[1, 1] = 1;

            var board = new Board(initialState, 3);

            Assert.AreEqual(false, board.IsGameOver());
        }

        [TestMethod]
        public void EndGameTrueTest()
        {
            var initialState = new int[2, 2];

            var board = new Board(initialState, 3);

            Assert.AreEqual(true, board.IsGameOver());
        }
        #endregion
    }
}
