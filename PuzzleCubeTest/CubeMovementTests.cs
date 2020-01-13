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

        [TestCase(3, Axis.z, 2, false, Axis.x, 1, false, "white,green,white,white,green,white,red,red,red,green,green,green,yellow,yellow,yellow,green,green,green,yellow,red,red,blue,red,red,yellow,red,red,blue,white,blue,blue,white,blue,blue,white,blue,orange,orange,white,orange,orange,green,orange,orange,white,orange,orange,orange,yellow,blue,yellow,yellow,blue,yellow")]
        public void Face_Rotation_With_Inside_Slice(int width, Axis faceAxis, int facePos, bool facePrime, Axis sliceAxis, int slicePos, bool slicePrime, string startState)
        {
            Cube subject = new Cube(startState);
            subject.ApplyMove(faceAxis, facePos, facePrime);  // TODO: This move is totally broken.  Faces 2, 4, and 5 are incorrect.
            subject.ApplyMove(sliceAxis, slicePos, slicePrime);

            Assert.That(subject.GameBoard, Is.EqualTo(StandardCube(width)));
        }
    }
}
