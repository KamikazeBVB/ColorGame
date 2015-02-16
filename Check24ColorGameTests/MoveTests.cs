using System;
using Check24ColorGame;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Check24ColorGameTests
{
    [TestClass]
    public class MoveTests
    {
        #region Constructor tests
        [TestMethod]
        public void TestMoveColorConstructor()
        {
            var move = new Move(3);

            Assert.AreEqual(3,move.MoveColor);
        }

        [TestMethod]
        public void TestMoveColorAndMoveScoreConstructor()
        {
            var move = new Move(3,2);

            Assert.AreEqual(3, move.MoveColor);
            Assert.AreEqual(2, move.MoveScore);
        }

        #endregion

        #region Move equality tests
        [TestMethod]
        public void TestMoveEquality()
        {
            var move = new Move(3, 2);

            Assert.AreEqual(0, move.CompareTo(new Move(3,2)));

        }

        [TestMethod]
        public void TestMoveInequality()
        {
            var move = new Move(3, 2);

            Assert.AreEqual(1, move.CompareTo(new Move(3,1)));

        }
        #endregion
    }
}
