using System;

using NUnit.Framework;

using PuzzleCube;
using static PuzzleCube.Cube;

namespace PuzzleCubeTest
{
    public class CubeTests : CubeTestsFixture
    {
        [Test]
        public void Constructor_Default_Returns_Standard_Cube()
        {
            Cube subject = new Cube();
            Assert.That(subject.GameBoard, Is.EqualTo(Standard_3x3));
        }

        [Test]
        public void Constructor_GameBoard_Returns_Expected_GameBoard()
        {
            Color[][][][] subjects = new Color[5][][][]
            {
                Standard_1x1,
                Standard_2x2,
                Standard_3x3,
                Standard_4x4,
                Standard_5x5
            };

            foreach (Color[][][] expectedGameBoard in subjects)
            {
                Cube subject = new Cube(expectedGameBoard);
                Assert.That(subject.GameBoard, Is.EqualTo(expectedGameBoard));
            }
        }

        [Test]
        public void Constructor_Int_Returns_Expected_GameBoard()
        {            
            Assert.Multiple(() => 
            { 
                for (int i = 1; i < 6; i++)
                {
                    Cube subject = new Cube(i);
                    Assert.That(subject.GameBoard, Is.EqualTo(StandardCube(i)));
                }
            });
        }

        [TestCase("white,white,white,white,white,white,white,white,white,green,green,green,green,green,green,green,green,green,red,red,red,red,red,red,red,red,red,blue,blue,blue,blue,blue,blue,blue,blue,blue,orange,orange,orange,orange,orange,orange,orange,orange,orange,yellow,yellow,yellow,yellow,yellow,yellow,yellow,yellow,yellow")]
        [TestCase("White,White,White,White,White,White,White,White,White,Green,Green,Green,Green,Green,Green,Green,Green,Green,Red,Red,Red,Red,Red,Red,Red,Red,Red,Blue,Blue,Blue,Blue,Blue,Blue,Blue,Blue,Blue,Orange,Orange,Orange,Orange,Orange,Orange,Orange,Orange,Orange,Yellow,Yellow,Yellow,Yellow,Yellow,Yellow,Yellow,Yellow,Yellow")]
        [TestCase("white white white white white white white white white green green green green green green green green green red red red red red red red red red blue blue blue blue blue blue blue blue blue orange orange orange orange orange orange orange orange orange yellow yellow yellow yellow yellow yellow yellow yellow yellow")]
        [TestCase("White White White White White White White White White Green Green Green Green Green Green Green Green Green Red Red Red Red Red Red Red Red Red Blue Blue Blue Blue Blue Blue Blue Blue Blue Orange Orange Orange Orange Orange Orange Orange Orange Orange Yellow Yellow Yellow Yellow Yellow Yellow Yellow Yellow Yellow")]
        [TestCase("white, white, white, white, white, white, white, white, white, green, green, green, green, green, green, green, green, green, red, red, red, red, red, red, red, red, red, blue, blue, blue, blue, blue, blue, blue, blue, blue, orange, orange, orange, orange, orange, orange, orange, orange, orange, yellow, yellow, yellow, yellow, yellow, yellow, yellow, yellow, yellow")]
        [TestCase("White, White, White, White, White, White, White, White, White, Green, Green, Green, Green, Green, Green, Green, Green, Green, Red, Red, Red, Red, Red, Red, Red, Red, Red, Blue, Blue, Blue, Blue, Blue, Blue, Blue, Blue, Blue, Orange, Orange, Orange, Orange, Orange, Orange, Orange, Orange, Orange, Yellow, Yellow, Yellow, Yellow, Yellow, Yellow, Yellow, Yellow, Yellow")]
        public void Constructor_String_Returns_Expected_GameBoard(string colors)
        {
            Cube subject = new Cube(colors);
            Assert.That(subject.GameBoard, Is.EqualTo(Standard_3x3));
        }

        [TestCase(-3)]
        [TestCase(0)]
        public void Constructor_Int_Throws_On_Impossible_Size(int size)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => { Cube subject = new Cube(size); });
        }

        [Test]
        public void Constructor_GameBoard_Throws_On_Null_GameBoard()
        {
            Color[][][] nullGameBoard = null;
            Assert.Throws<ArgumentNullException>(() => { Cube subject = new Cube(nullGameBoard); });
        }

        [Test]
        public void Constructor_String_Throws_On_Null_String()
        {
            string nullString = null;
            Assert.Throws<ArgumentNullException>(() => { Cube subject = new Cube(nullString); });
        }
        
        [TestCase("")]
        [TestCase("Red,White,Blue")]
        [TestCase("These,Are,Not,Six,Color,Names")]
        public void Constructor_String_Throws_On_Invalid_String(string invalidString)
        {
            Assert.Throws<ArgumentException>(() => { Cube subject = new Cube(invalidString); });
        }

        [Test]
        public void Move_Constructor_Throws_On_Negative_Position()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => { Move subject = new Move(Axis.x, -1); });
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void Width_Returns_Expected_Value(int width)
        {
            Cube subject = new Cube(width);
            Assert.That(subject.Width, Is.EqualTo(width));
        }
    }
}