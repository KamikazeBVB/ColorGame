using System;
using Check24ColorGame;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Check24ColorGameTests
{
    [TestClass]
    public class MoveTests
    {
        #region Constructor tests


        #endregion

        #region Move equality tests
        [TestMethod]
        public void TestMoveEquality()
        {
            var coord1= new Coordinates(0,0);
            var coord2= new Coordinates(1,0);

            var border = new Dictionary<string,Coordinates>
            {
               {coord1.Key, coord1},
               {coord2.Key, coord2}
            };

            var move1 = new Move(0, border);
            var move2 = new Move(0, border);

            Assert.AreEqual(true, move1.Equals(move2));
        }

        [TestMethod]
        public void TestMoveInequality()
        {
            var coord1 = new Coordinates(0, 0);
            var coord2 = new Coordinates(1, 0);

            var border = new Dictionary<string, Coordinates>
            {
               {coord1.Key, coord1},
               {coord2.Key, coord2}
            };

            var border2 = new Dictionary<string, Coordinates>
            {
               {coord1.Key, coord1},
            };

            var move1 = new Move(0, border);
            var move2 = new Move(0, border2);

            Assert.AreEqual(false, move1.Equals(move2));
        }
        #endregion
    }
}
