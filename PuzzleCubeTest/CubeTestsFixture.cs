using System;

using NUnit.Framework;

using PuzzleCube;
using PuzzleCube.Helpers;
using static PuzzleCube.Cube;

namespace PuzzleCubeTest
{
    [TestFixture]
    public class CubeTestsFixture
    {
        internal CubeComparer _comparer = new CubeComparer();

        public static Color[][][] Standard_1x1 = new Color[6][][]
        {
            new Color[1][] { new Color[1] { Color.White } },
            new Color[1][] { new Color[1] { Color.Green } },
            new Color[1][] { new Color[1] { Color.Red } },
            new Color[1][] { new Color[1] { Color.Blue } },
            new Color[1][] { new Color[1] { Color.Orange } },
            new Color[1][] { new Color[1] { Color.Yellow } }
        };

        public static Color[][][] Standard_2x2 = new Color[6][][]
        {
            new Color[2][] { new Color[2] { Color.White, Color.White }, new Color[2] { Color.White, Color.White } },
            new Color[2][] { new Color[2] { Color.Green, Color.Green }, new Color[2] { Color.Green, Color.Green } },
            new Color[2][] { new Color[2] { Color.Red, Color.Red }, new Color[2] { Color.Red, Color.Red } },
            new Color[2][] { new Color[2] { Color.Blue, Color.Blue }, new Color[2] { Color.Blue, Color.Blue } },
            new Color[2][] { new Color[2] { Color.Orange, Color.Orange }, new Color[2] { Color.Orange, Color.Orange } },
            new Color[2][] { new Color[2] { Color.Yellow, Color.Yellow }, new Color[2] { Color.Yellow, Color.Yellow } }
        };
        
        public static Color[][][] Standard_3x3 = new Color[6][][]
        {
            new Color[3][]{ new Color[3] { Color.White, Color.White, Color.White }, new Color[3] { Color.White, Color.White, Color.White }, new Color[3] { Color.White, Color.White, Color.White } },
            new Color[3][] { new Color[3] { Color.Green, Color.Green, Color.Green }, new Color[3] { Color.Green, Color.Green, Color.Green }, new Color[3] { Color.Green, Color.Green, Color.Green } },
            new Color[3][] { new Color[3] { Color.Red, Color.Red, Color.Red }, new Color[3] { Color.Red, Color.Red, Color.Red }, new Color[3] { Color.Red, Color.Red, Color.Red } },
            new Color[3][] { new Color[3] { Color.Blue, Color.Blue, Color.Blue }, new Color[3] { Color.Blue, Color.Blue, Color.Blue }, new Color[3] { Color.Blue, Color.Blue, Color.Blue } },
            new Color[3][] { new Color[3] { Color.Orange, Color.Orange, Color.Orange }, new Color[3] { Color.Orange, Color.Orange, Color.Orange }, new Color[3] { Color.Orange, Color.Orange, Color.Orange } },
            new Color[3][] { new Color[3] { Color.Yellow, Color.Yellow, Color.Yellow }, new Color[3] { Color.Yellow, Color.Yellow, Color.Yellow }, new Color[3] { Color.Yellow, Color.Yellow, Color.Yellow } }
        };

        public static Color[][][] Standard_4x4 = new Color[6][][]
        {
            new Color[4][] { new Color[4] { Color.White, Color.White, Color.White, Color.White }, new Color[4] { Color.White, Color.White, Color.White, Color.White }, new Color[4] { Color.White, Color.White, Color.White, Color.White }, new Color[4] { Color.White, Color.White, Color.White, Color.White } },
            new Color[4][] { new Color[4] { Color.Green, Color.Green, Color.Green, Color.Green }, new Color[4] { Color.Green, Color.Green, Color.Green, Color.Green }, new Color[4] { Color.Green, Color.Green, Color.Green, Color.Green }, new Color[4] { Color.Green, Color.Green, Color.Green, Color.Green } },
            new Color[4][] { new Color[4] { Color.Red, Color.Red, Color.Red, Color.Red }, new Color[4] { Color.Red, Color.Red, Color.Red, Color.Red }, new Color[4] { Color.Red, Color.Red, Color.Red, Color.Red }, new Color[4] { Color.Red, Color.Red, Color.Red, Color.Red } },
            new Color[4][] { new Color[4] { Color.Blue, Color.Blue, Color.Blue, Color.Blue }, new Color[4] { Color.Blue, Color.Blue, Color.Blue, Color.Blue }, new Color[4] { Color.Blue, Color.Blue, Color.Blue, Color.Blue }, new Color[4] { Color.Blue, Color.Blue, Color.Blue, Color.Blue } },
            new Color[4][] { new Color[4] { Color.Orange, Color.Orange, Color.Orange, Color.Orange }, new Color[4] { Color.Orange, Color.Orange, Color.Orange, Color.Orange }, new Color[4] { Color.Orange, Color.Orange, Color.Orange, Color.Orange }, new Color[4] { Color.Orange, Color.Orange, Color.Orange, Color.Orange } },
            new Color[4][] { new Color[4] { Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow }, new Color[4] { Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow }, new Color[4] { Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow }, new Color[4] { Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow } }
        };

        public static Color[][][] Standard_5x5 = new Color[6][][]
        {
            new Color[5][] { new Color[5] { Color.White, Color.White, Color.White, Color.White, Color.White }, new Color[5] { Color.White, Color.White, Color.White, Color.White, Color.White }, new Color[5] { Color.White, Color.White, Color.White, Color.White, Color.White }, new Color[5] { Color.White, Color.White, Color.White, Color.White, Color.White },  new Color[5] { Color.White, Color.White, Color.White, Color.White, Color.White } },
            new Color[5][] { new Color[5] { Color.Green, Color.Green, Color.Green, Color.Green, Color.Green }, new Color[5] { Color.Green, Color.Green, Color.Green, Color.Green, Color.Green }, new Color[5] { Color.Green, Color.Green, Color.Green, Color.Green, Color.Green }, new Color[5] { Color.Green, Color.Green, Color.Green, Color.Green, Color.Green },  new Color[5] { Color.Green, Color.Green, Color.Green, Color.Green, Color.Green } },
            new Color[5][] { new Color[5] { Color.Red, Color.Red, Color.Red, Color.Red, Color.Red }, new Color[5] { Color.Red, Color.Red, Color.Red, Color.Red, Color.Red }, new Color[5] { Color.Red, Color.Red, Color.Red, Color.Red, Color.Red }, new Color[5] { Color.Red, Color.Red, Color.Red, Color.Red, Color.Red },  new Color[5] { Color.Red, Color.Red, Color.Red, Color.Red, Color.Red } },
            new Color[5][] { new Color[5] { Color.Blue, Color.Blue, Color.Blue, Color.Blue, Color.Blue }, new Color[5] { Color.Blue, Color.Blue, Color.Blue, Color.Blue, Color.Blue }, new Color[5] { Color.Blue, Color.Blue, Color.Blue, Color.Blue, Color.Blue }, new Color[5] { Color.Blue, Color.Blue, Color.Blue, Color.Blue, Color.Blue },  new Color[5] { Color.Blue, Color.Blue, Color.Blue, Color.Blue, Color.Blue } },
            new Color[5][] { new Color[5] { Color.Orange, Color.Orange, Color.Orange, Color.Orange, Color.Orange }, new Color[5] { Color.Orange, Color.Orange, Color.Orange, Color.Orange, Color.Orange }, new Color[5] { Color.Orange, Color.Orange, Color.Orange, Color.Orange, Color.Orange }, new Color[5] { Color.Orange, Color.Orange, Color.Orange, Color.Orange, Color.Orange },  new Color[5] { Color.Orange, Color.Orange, Color.Orange, Color.Orange, Color.Orange } },
            new Color[5][] { new Color[5] { Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow }, new Color[5] { Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow }, new Color[5] { Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow }, new Color[5] { Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow },  new Color[5] { Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow } }
        };

        public Color[][][] StandardCube(int num)
        {
            switch (num)
            {
                case 1:
                    return Standard_1x1;
                case 2:
                    return Standard_2x2;
                case 3:
                    return Standard_3x3;
                case 4:
                    return Standard_4x4;
                case 5:
                    return Standard_5x5;
                default:
                    return new Color[0][][];
            }
        }
    }
}
