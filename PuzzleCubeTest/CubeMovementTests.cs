using System;

using NUnit.Framework;

using PuzzleCube;
using static PuzzleCube.Cube;

namespace PuzzleCubeTest
{
    class CubeMovementTests : CubeTestsFixture
    {
        [TestCase(Axis.x, false, "green,yellow,red,white,orange,blue")]
        [TestCase(Axis.x, true, "blue,white,red,yellow,orange,green")]
        [TestCase(Axis.y, false, "white,orange,green,red,blue,yellow")]
        [TestCase(Axis.y, true, "white,red,blue,orange,green,yellow")]
        [TestCase(Axis.z, false, "red,green,yellow,blue,white,orange")]
        [TestCase(Axis.z, true, "orange,green,white,blue,yellow,red")]
        public void One_By_One_Movement(Axis axis, bool prime, string startState)
        {
            Cube subject = new Cube(startState);
            subject.ApplyMove(axis, 0, prime);

            Assert.That(subject.GameBoard, Is.EqualTo(Standard_1x1));
        }
        
        [TestCase(3, Axis.x, 1, false, "white,green,white,white,green,white,white,green,white,green,yellow,green,green,yellow,green,green,yellow,green,red,red,red,red,red,red,red,red,red,blue,white,blue,blue,white,blue,blue,white,blue,orange,orange,orange,orange,orange,orange,orange,orange,orange,yellow,blue,yellow,yellow,blue,yellow,yellow,blue,yellow")]
        [TestCase(3, Axis.x, 1, true, "white,blue,white,white,blue,white,white,blue,white,green,white,green,green,white,green,green,white,green,red,red,red,red,red,red,red,red,red,blue,yellow,blue,blue,yellow,blue,blue,yellow,blue,orange,orange,orange,orange,orange,orange,orange,orange,orange,yellow,green,yellow,yellow,green,yellow,yellow,green,yellow")]
        [TestCase(3, Axis.y, 1, false, "white,white,white,white,white,white,white,white,white,green,green,green,orange,orange,orange,green,green,green,red,red,red,green,green,green,red,red,red,blue,blue,blue,red,red,red,blue,blue,blue,orange,orange,orange,blue,blue,blue,orange,orange,orange,yellow,yellow,yellow,yellow,yellow,yellow,yellow,yellow,yellow")]
        [TestCase(3, Axis.y, 1, true, "white,white,white,white,white,white,white,white,white,green,green,green,red,red,red,green,green,green,red,red,red,blue,blue,blue,red,red,red,blue,blue,blue,orange,orange,orange,blue,blue,blue,orange,orange,orange,green,green,green,orange,orange,orange,yellow,yellow,yellow,yellow,yellow,yellow,yellow,yellow,yellow")]
        [TestCase(3, Axis.z, 1, false, "white,white,white,red,red,red,white,white,white,green,green,green,green,green,green,green,green,green,red,yellow,red,red,yellow,red,red,yellow,red,blue,blue,blue,blue,blue,blue,blue,blue,blue,orange,white,orange,orange,white,orange,orange,white,orange,yellow,yellow,yellow,orange,orange,orange,yellow,yellow,yellow")]
        [TestCase(3, Axis.z, 1, true, "white,white,white,orange,orange,orange,white,white,white,green,green,green,green,green,green,green,green,green,red,white,red,red,white,red,red,white,red,blue,blue,blue,blue,blue,blue,blue,blue,blue,orange,yellow,orange,orange,yellow,orange,orange,yellow,orange,yellow,yellow,yellow,red,red,red,yellow,yellow,yellow")]
        public void Inside_Slice_Single(int width, Axis axis, int pos, bool prime, string startState)
        {
            Cube subject = new Cube(startState);
            subject.ApplyMove(axis, pos, prime);

            Assert.That(subject.GameBoard, Is.EqualTo(StandardCube(width)));
        }

        [TestCase(3, Axis.z, 0, false, Axis.x, 1, false, "white,green,white,white,green,white,red,red,red,green,green,green,yellow,yellow,yellow,green,green,green,yellow,red,red,blue,red,red,yellow,red,red,blue,white,blue,blue,white,blue,blue,white,blue,orange,orange,white,orange,orange,green,orange,orange,white,orange,orange,orange,yellow,blue,yellow,yellow,blue,yellow")]
        [TestCase(3, Axis.z, 0, true, Axis.x, 1, true, "white,blue,white,white,blue,white,orange,orange,orange,green,green,green,white,white,white,green,green,green,white,red,red,blue,red,red,white,red,red,blue,yellow,blue,blue,yellow,blue,blue,yellow,blue,orange,orange,yellow,orange,orange,green,orange,orange,yellow,red,red,red,yellow,green,yellow,yellow,green,yellow")]
        [TestCase(3, Axis.x, 0, false, Axis.z, 1, false, "green,white,white,green,red,red,green,white,white,yellow,green,green,orange,green,green,yellow,green,green,red,yellow,red,red,yellow,red,red,yellow,red,blue,blue,white,blue,blue,red,blue,blue,white,orange,orange,orange,white,white,white,orange,orange,orange,blue,yellow,yellow,blue,orange,orange,blue,yellow,yellow")]
        [TestCase(3, Axis.x, 0, true, Axis.z, 1, true, "blue,white,white,blue,orange,orange,blue,white,white,white,green,green,orange,green,green,white,green,green,red,white,red,red,white,red,red,white,red,blue,blue,yellow,blue,blue,red,blue,blue,yellow,orange,orange,orange,yellow,yellow,yellow,orange,orange,orange,green,yellow,yellow,green,red,red,green,yellow,yellow")]
        [TestCase(3, Axis.y, 0, false, Axis.x, 1, false, "white,white,white,green,green,green,white,white,white,orange,orange,orange,green,yellow,green,green,yellow,green,green,yellow,green,red,red,red,red,red,red,red,red,red,blue,white,blue,blue,white,blue,blue,white,blue,orange,orange,orange,orange,orange,orange,yellow,blue,yellow,yellow,blue,yellow,yellow,blue,yellow")]
        [TestCase(3, Axis.y, 0, true, Axis.x, 1, true, "white,white,white,blue,blue,blue,white,white,white,red,red,red,green,white,green,green,white,green,blue,yellow,blue,red,red,red,red,red,red,orange,orange,orange,blue,yellow,blue,blue,yellow,blue,green,white,green,orange,orange,orange,orange,orange,orange,yellow,green,yellow,yellow,green,yellow,yellow,green,yellow")]
        [TestCase(4, Axis.x, 0, false, Axis.z, 2, false, "green, white,white,white,green,red,red,red,green,white,white,white,green,white,white,white,yellow,green,green,green,yellow,green,green,green,orange,green,green,green,yellow,green,green,green,red,red,yellow,red,red,red,yellow,red,red,red,yellow,red,red,red,yellow,red,blue,blue,blue,white,blue,blue,blue,white,blue,blue,blue,red,blue,blue,blue,white,orange,orange,orange,orange,orange,orange,orange,orange,white,white,white,white,orange,orange,orange,orange,blue,yellow,yellow,yellow,blue,yellow,yellow,yellow,blue,orange,orange,orange,blue,yellow,yellow,yellow")]
        public void Face_Rotation_With_Inside_Slice(int width, Axis faceAxis, int facePos, bool facePrime, Axis sliceAxis, int slicePos, bool slicePrime, string startState)
        {
            Cube subject = new Cube(startState);
            subject.ApplyMove(faceAxis, facePos, facePrime);
            subject.ApplyMove(sliceAxis, slicePos, slicePrime);

            Assert.That(subject.GameBoard, Is.EqualTo(StandardCube(width)));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void Do_And_Undo(int width)
        {
            Cube subject = new Cube(width);
            Assert.That(subject.GameBoard, Is.EqualTo(StandardCube(width)));

            for (int j = 0; j < subject.Width; j++)
            {
                subject.ApplyMove(new Move(Axis.x, j, false));
                subject.ApplyMove(new Move(Axis.y, j, false));
                subject.ApplyMove(new Move(Axis.z, j, false));
            }

            Assert.That(subject.GameBoard, Is.Not.EqualTo(StandardCube(width)));

            for (int j = subject.Width - 1; j >= 0; j--)
            {
                subject.ApplyMove(new Move(Axis.z, j, true));
                subject.ApplyMove(new Move(Axis.y, j, true));
                subject.ApplyMove(new Move(Axis.x, j, true));
            }

            Assert.That(subject.GameBoard, Is.EqualTo(StandardCube(width)));
        }

        [TestCase(Axis.x, false, "blue,yellow,blue,white,white,yellow,green,yellow,blue,red,red,yellow,orange,green,green,blue,white,green,orange,blue,white,red,red,yellow,yellow,orange,red,red,orange,orange,green,blue,red,green,red,green,white,green,yellow,white,orange,white,orange,orange,red,yellow,blue,orange,blue,yellow,green,white,blue,white", "green,red,green,red,blue,green,orange,orange,red,blue,yellow,blue,white,white,yellow,green,yellow,blue,white,yellow,red,blue,red,orange,orange,red,yellow,white,blue,white,green,yellow,blue,orange,blue,yellow,orange,white,white,orange,orange,green,red,white,yellow,red,red,yellow,orange,green,green,blue,white,green")]
        [TestCase(Axis.x, true, "blue,yellow,blue,white,white,yellow,green,yellow,blue,red,red,yellow,orange,green,green,blue,white,green,orange,blue,white,red,red,yellow,yellow,orange,red,red,orange,orange,green,blue,red,green,red,green,white,green,yellow,white,orange,white,orange,orange,red,yellow,blue,orange,blue,yellow,green,white,blue,white", "red,red,yellow,orange,green,green,blue,white,green,yellow,blue,orange,blue,yellow,green,white,blue,white,yellow,red,orange,orange,red,blue,red,yellow,white,blue,yellow,green,yellow,white,white,blue,yellow,blue,yellow,white,red,green,orange,orange,white,white,orange,green,red,green,red,blue,green,orange,orange,red")]
        [TestCase(Axis.y, false, "blue,yellow,blue,white,white,yellow,green,yellow,blue,red,red,yellow,orange,green,green,blue,white,green,orange,blue,white,red,red,yellow,yellow,orange,red,red,orange,orange,green,blue,red,green,red,green,white,green,yellow,white,orange,white,orange,orange,red,yellow,blue,orange,blue,yellow,green,white,blue,white", "green,white,blue,yellow,white,yellow,blue,yellow,blue,orange,blue,white,red,red,yellow,yellow,orange,red,red,orange,orange,green,blue,red,green,red,green,white,green,yellow,white,orange,white,orange,orange,red,red,red,yellow,orange,green,green,blue,white,green,orange,green,white,blue,yellow,blue,yellow,blue,white")]
        [TestCase(Axis.y, true, "blue,yellow,blue,white,white,yellow,green,yellow,blue,red,red,yellow,orange,green,green,blue,white,green,orange,blue,white,red,red,yellow,yellow,orange,red,red,orange,orange,green,blue,red,green,red,green,white,green,yellow,white,orange,white,orange,orange,red,yellow,blue,orange,blue,yellow,green,white,blue,white", "blue,yellow,blue,yellow,white,yellow,blue,white,green,white,green,yellow,white,orange,white,orange,orange,red,red,red,yellow,orange,green,green,blue,white,green,orange,blue,white,red,red,yellow,yellow,orange,red,red,orange,orange,green,blue,red,green,red,green,white,blue,yellow,blue,yellow,blue,white,green,orange")]
        [TestCase(Axis.z, false, "blue,yellow,blue,white,white,yellow,green,yellow,blue,red,red,yellow,orange,green,green,blue,white,green,orange,blue,white,red,red,yellow,yellow,orange,red,red,orange,orange,green,blue,red,green,red,green,white,green,yellow,white,orange,white,orange,orange,red,yellow,blue,orange,blue,yellow,green,white,blue,white", "orange,white,white,orange,orange,green,red,white,yellow,blue,orange,red,white,green,red,green,green,yellow,green,white,blue,yellow,white,yellow,blue,yellow,blue,orange,red,green,orange,blue,red,red,green,green,white,blue,yellow,blue,yellow,blue,white,green,orange,yellow,red,orange,orange,red,blue,red,yellow,white")]
        [TestCase(Axis.z, true, "blue,yellow,blue,white,white,yellow,green,yellow,blue,red,red,yellow,orange,green,green,blue,white,green,orange,blue,white,red,red,yellow,yellow,orange,red,red,orange,orange,green,blue,red,green,red,green,white,green,yellow,white,orange,white,orange,orange,red,yellow,blue,orange,blue,yellow,green,white,blue,white", "white,yellow,red,blue,red,orange,orange,red,yellow,yellow,green,green,red,green,white,red,orange,blue,orange,green,white,blue,yellow,blue,yellow,blue,white,green,green,red,red,blue,orange,green,red,orange,blue,yellow,blue,yellow,white,yellow,blue,white,green,yellow,white,red,green,orange,orange,white,white,orange")]
        public void RotateCube(Axis axis, bool prime, string startState, string expectedResult)
        {
            Cube subject = new Cube(startState);
            subject.RotateCube(axis, prime);

            Cube expected = new Cube(expectedResult);

            Assert.That(subject.GameBoard, Is.EqualTo(expected.GameBoard));
        }
    }
}
