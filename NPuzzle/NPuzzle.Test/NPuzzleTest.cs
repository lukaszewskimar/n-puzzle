using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using n_puzzle;

namespace NPuzzle.Test
{
    [TestClass]
    public class NPuzzleTest
    {
        [TestMethod]
        public void RunAStar_8Puzzle_ThreeNodes()
        {
             // Arrange
             Puzzle initialState = new Puzzle(
                new int[,]
                {
                    { 1, 2, 0 },
                    { 4, 5, 3 },
                    { 7, 8, 6 }
                }
            );

            Puzzle goalState = new Puzzle(
                 new int[,]
                 {
                     { 1, 2, 3 },
                     { 4, 5, 6 },
                     { 7, 8, 0 }
                 }
            );

            // Act
            List<Branch> closed = Program.RunAStar(initialState, goalState);

            // Assert
            Assert.AreEqual(3, closed.Count);
        }
    }
}
