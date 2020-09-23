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
        public void Face_Rotation_With_Inside_Slice(int width, Axis faceAxis, int facePos, bool facePrime, Axis sliceAxis, int slicePos, bool slicePrime, string startState)
        {
            Cube subject = new Cube(startState);
            subject.ApplyMove(faceAxis, facePos, facePrime);
            subject.ApplyMove(sliceAxis, slicePos, slicePrime);

            Assert.That(subject.GameBoard, Is.EqualTo(StandardCube(width)));
        }
    }
}
