using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PuzzleCube;
using PuzzleCube.Extensions;
using static PuzzleCube.Cube;

namespace PuzzleCubeTest.ExtensionsTests
{
    internal class CubeExtensionsTests : CubeTestsFixture
    {
        [Test]
        public void GetAllMoves__ReturnsAllPossibleMoves()
        {
            // Test cubes of size 1 through 100
            for (int i = 1; i < 100; i++)
            {
                Cube subject = new Cube(i);
                var actualResult = subject.GetAllMoves();

                Assert.That(actualResult, Is.Not.Null);

                // Check that we have moves for each axis
                foreach(var axis in Enum.GetValues(typeof(Axis)))
                {
                    // Check that we have moves for each position on each axis
                    for (int j = 0; j < i; j++)
                    {
                        // Check that we have both the normal and prime direction for each move
                        var expectedMove = new Move((Axis)axis, j);
                        var expectedMovePrime = new Move((Axis)axis, j, true);

                        Assert.Contains(expectedMove, (System.Collections.ICollection)actualResult);
                        Assert.Contains(expectedMovePrime, (System.Collections.ICollection)actualResult);
                    }
                }
            }
        }

        [Test]
        public void GetAllOrientations__ReturnsAllPossibleOrientations()
        {
            // Construct a cube with a single, off-center, red dot, pointing "up" (relative to the face) on the "top" (zero) face.
            var subject = new Cube("wrwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww");

            var expectedResult = new Stack<Cube>();
            
            // Shift the red dot to construct every possible orientation of the subject cube;
            expectedResult.Push(new Cube("wrwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww")); // (Dot points) up on 0
            expectedResult.Push(new Cube("wwwwwrwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww")); // right on 0
            expectedResult.Push(new Cube("wwwwwwwrwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww")); // down on 0 
            expectedResult.Push(new Cube("wwwrwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww")); // left on 0
            expectedResult.Push(new Cube("wwwwwwwwwwrwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww")); // up on 1
            expectedResult.Push(new Cube("wwwwwwwwwwwwwwrwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww")); // right on 1
            expectedResult.Push(new Cube("wwwwwwwwwwwwwwwwrwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww")); // down on 1
            expectedResult.Push(new Cube("wwwwwwwwwwwwrwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww")); // left on 1
            expectedResult.Push(new Cube("wwwwwwwwwwwwwwwwwwwrwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww")); // up on 2
            expectedResult.Push(new Cube("wwwwwwwwwwwwwwwwwwwwwwwrwwwwwwwwwwwwwwwwwwwwwwwwwwwwww")); // right on 2
            expectedResult.Push(new Cube("wwwwwwwwwwwwwwwwwwwwwwwwwrwwwwwwwwwwwwwwwwwwwwwwwwwwww")); // down on 2
            expectedResult.Push(new Cube("wwwwwwwwwwwwwwwwwwwwwrwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww")); // left on 2
            expectedResult.Push(new Cube("wwwwwwwwwwwwwwwwwwwwwwwwwwwwrwwwwwwwwwwwwwwwwwwwwwwwww")); // up on 3
            expectedResult.Push(new Cube("wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwrwwwwwwwwwwwwwwwwwwwww")); // right on 3
            expectedResult.Push(new Cube("wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwrwwwwwwwwwwwwwwwwwww")); // down on 3
            expectedResult.Push(new Cube("wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwrwwwwwwwwwwwwwwwwwwwwwww")); // left on 3
            expectedResult.Push(new Cube("wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwrwwwwwwwwwwwwwwww")); // up on 4
            expectedResult.Push(new Cube("wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwrwwwwwwwwwwww")); // right on 4
            expectedResult.Push(new Cube("wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwrwwwwwwwwww")); // down on 4
            expectedResult.Push(new Cube("wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwrwwwwwwwwwwwwww")); // left on 4
            expectedResult.Push(new Cube("wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwrwwwwwww")); // up on 5
            expectedResult.Push(new Cube("wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwrwww")); // right on 5
            expectedResult.Push(new Cube("wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwrw")); // down on 5
            expectedResult.Push(new Cube("wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwrwwwww")); // left on 5

            var actualResult = subject.GetAllOrientations();

            Assert.AreEqual(expectedResult.Count, actualResult.Count());

            while (expectedResult.Any())
            {
                var expectedOrientation = expectedResult.Pop();

                Assert.True(actualResult.Any((result) => _comparer.Equals(result, expectedOrientation)));                
            }

            Assert.AreEqual(0, expectedResult.Count);
        }
    }
}
