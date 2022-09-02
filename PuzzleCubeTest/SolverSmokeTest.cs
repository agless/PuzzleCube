using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

using PuzzleCube;
using PuzzleCube.Extensions;
using PuzzleCube.Helpers;

using static PuzzleCube.Cube;

namespace PuzzleCubeTest
{
    
    internal class SolverSmokeTest : CubeTestsFixture
    {
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        [TestCase(10)]
        public void SingleTurnSolves(int cubeSize)
        {
            var solvedCube = new Cube(cubeSize);
            foreach (var move in solvedCube.GetAllMoves())
            {
                var undoMove = new Move(move.Axis, move.Position, !move.Prime);

                var unsolvedCube = new Cube(solvedCube);
                unsolvedCube.ApplyMove(move);

                var subject = new Solver(unsolvedCube, solvedCube);

                Assert.DoesNotThrow(() => subject.Solve());
                Assert.NotNull(subject.Solution);
                Assert.AreEqual(1, subject.Solution.ToList().Count);
                Assert.True(subject.Solution.Contains(undoMove));
            }
        }

        [Test]
        public void Solve()
        {
            var solvedCube = new Cube(3);
            var scrambledCube = new Cube(solvedCube);

            scrambledCube.ApplyMove(new Move(Axis.x, 2));
            scrambledCube.ApplyMove(new Move(Axis.z, 0));
            scrambledCube.ApplyMove(new Move(Axis.y, 2));
            scrambledCube.ApplyMove(new Move(Axis.z, 0));
            scrambledCube.ApplyMove(new Move(Axis.y, 2));
            //scrambledCube.ApplyMove(new Move(Axis.z, 0));

            var subject = new Solver(scrambledCube, solvedCube);

            Assert.DoesNotThrow(() => subject.Solve());
            Assert.NotNull(subject.Solution);
            // Assert.AreEqual(2, subject.Solution.ToList().Count);
        }

        // [Test]
        // public void SolutionLength__IsLessThan__ScrambleLength()
        // {
        //     for (int cubeSize = 2; cubeSize < 5; cubeSize++)
        //     {
                
        //         var solvedCube = new Cube(cubeSize);
        //         var scrambledCube = new Cube(solvedCube);
                
        //         var random = new Random();
        //         var scrambleList = new List<Move>();

        //         for (int scrambleMoves = 2; scrambleMoves < cubeSize * 3; scrambleMoves++)
        //         {
        //             var axis = random.NextDouble() switch 
        //             {
        //                 < 0.333 => Axis.x,
        //                 >= 0.333 and < 0.666 => Axis.y,
        //                 >= 0.666 => Axis.z,
        //                 _ => Axis.z  
        //             };

        //             int index = (int)(cubeSize * random.NextDouble());
        //             bool prime = random.NextDouble() switch
        //             {
        //                 < .5 => true,
        //                 >= .5 => false,
        //                 _ => false
        //             };

        //             scrambleList.Add(new Move(axis, index, prime));
        //         }

        //         foreach(var move in scrambleList)
        //         {
        //             scrambledCube.ApplyMove(move);
        //         }

        //         var subject = new Solver(scrambledCube, solvedCube);

        //         Assert.DoesNotThrow(() => subject.Solve());
        //         Assert.That(subject.Solution.Count(), Is.LessThan(scrambleList.Count()));
        //     }
        // }
    }
}
