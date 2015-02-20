using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Check24ColorGame;
using Check24ColorGameTests.Mocks;
using System.Linq;

namespace Check24ColorGameTests
{
    [TestClass]
    public class Check24GreedyPlayerTests
    {
        [TestMethod]
        public void TestGreedyStrategy()
        {
            FakeBoard fakeBoard = new FakeBoard();

            Check24Player player = new Check24GreedyPlayer(fakeBoard);

            var realStrategy = player.GetStrategy().ToList();

            var expectedStrategy = fakeBoard.GreedyResult.ToList();

            Assert.AreEqual(expectedStrategy.Count, realStrategy.Count);

            for (int index = 0; index < realStrategy.Count; index++)
            {
                Assert.AreEqual(expectedStrategy[index], realStrategy[index]);
            }
        }
    }
}
