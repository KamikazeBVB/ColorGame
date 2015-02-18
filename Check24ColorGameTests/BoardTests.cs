using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Check24ColorGame;
using System.Linq;
using System.Collections.Generic;

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

            var board = new Board(initialState, 2);

            CollectionAssert.AreEqual(initialState, board.BoardState);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void InitilizeBoardWithInvalidColor()
        {
            var initialState = new int[2, 2];
            initialState[1, 1] = 4;

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

            Assert.AreEqual(1, legalMoves.Count);
        }

        [TestMethod]
        public void GenerateMovesForTwoByTwoBoardMoveInBothDirections()
        {
            int[,] boardState = new int[2, 2] { { 0, 1 }, { 1, 1 } };
            var board = new Board(boardState, 2);

            var legalMoves = board.GenerateValidMoves().ToList();

            Assert.AreEqual(1, legalMoves.Count);
        }

        [TestMethod]
        public void GenerateMovesForComplexBoardValid()
        {
            int[,] boardState = new int[4, 4] 
            { { 1, 0, 0, 0}, 
              { 0, 0, 0, 0 },
              { 0, 0, 0, 0 },
              { 0, 0, 1, 0 } 
            };
            var board = new Board(boardState, 2);

            var legalMoves = board.GenerateValidMoves().ToList();

            var boarder = new Dictionary<string, Coordinates>();

            for(int row = 0; row < 4; row++)
                for (int col = 0; col < 4; col++)
                {
                    var coord = new Coordinates(row,col);
                    boarder.Add(coord.Key, coord);
                }

            boarder.Remove("3_2");

            var expectedMove = new Move(0, boarder);

            Assert.AreEqual(1, legalMoves.Count);
            Assert.AreEqual(true, legalMoves[0].Equals(expectedMove));
        }

        [TestMethod]
        public void GenerateMovesForComplexBoardInvalid()
        {
            int[,] boardState = new int[4, 4] 
            { { 1, 0, 0, 0}, 
              { 0, 0, 0, 0 },
              { 0, 0, 0, 0 },
              { 0, 0, 1, 0 } 
            };
            var board = new Board(boardState, 2);

            var legalMoves = board.GenerateValidMoves().ToList();

            var boarder = new Dictionary<string, Coordinates>();

            for (int row = 0; row < 4; row++)
                for (int col = 0; col < 4; col++)
                {
                    var coord = new Coordinates(row, col);
                    boarder.Add(coord.Key, coord);
                }

            var expectedMove = new Move(0, boarder);

            Assert.AreEqual(1, legalMoves.Count);
            Assert.AreEqual(false, legalMoves[0].Equals(expectedMove));
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void TryToMakeInvalidMove()
        {
            int[,] boardState = new int[4, 4] 
            { { 1, 0, 0, 0}, 
              { 0, 0, 0, 0 },
              { 0, 0, 0, 0 },
              { 0, 0, 1, 0 } 
            };
            var board = new Board(boardState, 2);

            var boarder = new Dictionary<string, Coordinates>();

            for (int row = 0; row < 4; row++)
                for (int col = 0; col < 4; col++)
                {
                    var coord = new Coordinates(row, col);
                    boarder.Add(coord.Key, coord);
                }

            var badMove = new Move(0, boarder);

            board.MakeMove(badMove);
        }
        
        #endregion

        #region End game tests
        [TestMethod]
        public void EndGameFalseTest()
        {
            var initialState = new int[2, 2];
            initialState[1, 1] = 1;

            var board = new Board(initialState, 2);

            Assert.AreEqual(false, board.IsGameOver());
        }

        [TestMethod]
        public void EndGameTrueTest()
        {
            var initialState = new int[2, 2];

            var board = new Board(initialState, 2);

            Assert.AreEqual(true, board.IsGameOver());
        }
        #endregion
    }
}
