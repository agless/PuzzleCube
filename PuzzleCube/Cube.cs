using System;
using System.Linq;

namespace PuzzleCube
{
    public class Cube
    {
        #region Properties

        /// <summary>
        /// Gets the puzzle cube data model.
        /// </summary>
        /// <remarks>
        /// This structure contains six n-by-n grids representing a puzzle cube.
        /// The structure is GameBoard[face][row][column].
        /// 
        /// Faces for a standard cube in the starting state:
        ///     
        ///     0 = White
        ///     1 = Green
        ///     2 = Red
        ///     3 = Blue
        ///     4 = Orange
        ///     5 = Yellow
        /// 
        /// The (real-life) puzzle cube is essentially a two-dimensional structure 
        /// wrapped around a three-dimensional object (the interior of the cube 
        /// is not part of the 'game board').  In order to visualize the data structure,
        /// imagine 'unwrapping' a puzzle cube to form a 'T'-shaped sheet:
        /// 
        ///     [0] White
        ///     [1] Green    [2] Red    [3] Blue    [4] Orange
        ///     [5] Yellow
        /// </remarks>
        public Color[][][] GameBoard { get; private set; }

        /// <summary>
        /// The n-by-n size of the puzzle cube.
        /// </summary>
        public int Width => GameBoard[0].Length;

        /// <summary>
        /// The standard puzzle cube colors.
        /// </summary>
        public enum Color { White, Green, Red, Blue, Orange, Yellow }

        /// <summary>
        /// The three axes around which layers of pieces may be turned.
        /// </summary>
        /// <remarks> 
        /// With GameBoard[0] facing 'up':
        ///     Axis.x = The horizontal axis spanning left to right
        ///     Axis.y = The vertical axis spanning top to bottom
        ///     Axis.z = The horizontal axis spanning front to back
        /// </remarks>
        public enum Axis { x, y, z }

        /// <summary>
        /// A structure for describing a single, quarter-turn move.
        /// </summary>
        /// <remarks>
        /// Set <see cref="Move.Axis"/> according to the axis that the
        /// layer of pieces will rotate around.
        /// 
        /// Set <see cref="Move.Position"/> as follows: 
        /// (with GameBoard[0] on top and GameBoard[1] facing the observer:
        ///     Axis.x:  Position zero is the left-most column.
        ///     Axis.y:  Position zero is the top row.
        ///     Axis.z:  Position zero is the face farthest from the observer.
        ///     
        /// Set <see cref="Move.Prime"/> to <code>true</code> if the layer to
        /// move shold be turned counter-clockwise if the following face were
        /// turned toward the observer:
        ///     Axis.x: GameBoard[4]
        ///     Axis.y: GameBoard[0]
        ///     Axis.z: GameBoard[1]
        /// </remarks>
        public struct Move
        {
            public Axis Axis;
            public int Position;
            public bool Prime;

            public Move(Axis axis, int position, bool prime = false)
            {
                if (position < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(position), $"{position} cannot be negative.");
                }

                Axis = axis;
                Position = position;
                Prime = prime;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new <see cref="PuzzleCube"/>.
        /// </summary>
        /// <param name="width">The n-by-n size of the cube.  Must be positive.</param>
        public Cube(int width = 3)
        {
            if (width <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(width), $"Cube cannot be size {width}");
            }

            GameBoard = AllocateGameBoard(width);
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    for (int k = 0; k < width; k++)
                    {
                        GameBoard[i][j][k] = (Color)i;
                    }
                }
            }
        }

        /// <summary>
        /// Initializes a new <see cref="PuzzleCube"/>.
        /// </summary>
        /// <param name="faces">A 3D array of <see cref="Color"/>, with 6 n-by-n faces.</param>
        public Cube(Color[][][] faces)
        {
            _ = faces ?? throw new ArgumentNullException(nameof(faces));

            if (faces.Length != 6)
            {
                throw new ArgumentOutOfRangeException(nameof(faces), "Only six-sided CUBES are supported");
            }

            int width = faces[0].Length;

            foreach (Color[][] face in faces)
            {
                if (face.Length != width)
                {
                    throw new ArgumentOutOfRangeException(nameof(faces), "Only CUBES are supported");
                }

                for (int i = 0; i < width; i++)
                {
                    if (face[i].Length != width)
                    {
                        throw new ArgumentOutOfRangeException(nameof(faces), "Only CUBES are supported");
                    }
                }
            }

            GameBoard = faces;
        }

        // TODO: Add constructor accepting string[][][]

        // TODO: Add constructor accepting Color[]

        // TODO: Add constructor accepting string[]

        // TODO: Add constructor accepting int[][][]

        // TODO: Add constructor accepting int[]

        /// <summary>
        /// Initializes a new <see cref="PuzzleCube"/>.
        /// </summary>
        /// <param name="colors">A space or comma-separated <code>string</code> of
        /// <see cref="Color"/> names.</param>
        public Cube(string colors)
        {
            _ = colors ?? throw new ArgumentNullException(nameof(colors));

            string[] colorsSplit = colors
                                    .Split(new char[] { ' ', ',' })
                                    .Select(item => item.Trim())
                                    .Where(item => !string.IsNullOrEmpty(item))
                                    .ToArray();

            int width = Convert.ToInt32(Math.Sqrt(colorsSplit.Length / 6));
            if (width == 0 || width * width * 6 != colorsSplit.Length)
            {
                throw new ArgumentException(nameof(colors), "Incorrect number of colors.  Cube must have 6 n-by-n grids.");
            }

            GameBoard = AllocateGameBoard(width);

            int count = 0;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    for (int k = 0; k < width; k++)
                    {
                        try
                        {
                            GameBoard[i][j][k] = (Color)Enum.Parse(typeof(Color), colorsSplit[count++], true);
                        }
                        catch (ArgumentException)
                        {
                            throw new ArgumentException(nameof(colors), $"Could not parse {colorsSplit[count - 1]}");
                        }
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// Applies a move.
        /// </summary>
        /// <returns>The new state of the <see cref="GameBoard"/>.</returns>
        public Color[][][] ApplyMove(Move move)
        {
            if (move.Position >= Width)
            {
                throw new ArgumentOutOfRangeException(nameof(move.Position), $"{move.Position} cannot be greater or equal to {Width}");
            }

            switch (move.Axis)
            {
                case Axis.x:
                    MoveColumn(move.Position, move.Prime);
                    break;
                case Axis.z:
                    MoveFacing(move.Position, move.Prime);
                    break;
                case Axis.y:
                    MovePlatter(move.Position, move.Prime);
                    break;
                default:
                    throw new NotSupportedException($"{move} is not a valid Move for this operation");
            }

            return GameBoard;
        }

        /// <summary>
        /// Applies a move.
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="position"></param>
        /// <param name="prime"></param>
        /// <returns>The new state of the <see cref="GameBoard"/>.</returns>
        public Color[][][] ApplyMove(Axis axis, int position, bool prime = false) => ApplyMove(new Move(axis, position, prime));

        private void MoveColumn(int pos, bool prime = false)
        {
            if (prime)
            {
                MoveColumnPrime(pos);
                return;
            }

            // 0 => 1 => 5 => 3 => 0
            Color[] temp = GetColumnCopy(0, pos);
            SetColumn(0, pos, GetColumnCopy(3, pos).Reverse().ToArray());
            SetColumn(3, pos, GetColumnCopy(5, pos).Reverse().ToArray());
            SetColumn(5, pos, GetColumnCopy(1, pos));
            SetColumn(1, pos, temp);

            if (pos == 0)
            {
                RotateFace(4);
            }

            if (pos == Width - 1)
            {
                RotateFace(2, true);
            }
        }

        private void MoveColumnPrime(int pos)
        {
            // 0 => 3 => 5 => 1 => 0
            Color[] temp = GetColumnCopy(0, pos);
            SetColumn(0, pos, GetColumnCopy(1, pos));
            SetColumn(1, pos, GetColumnCopy(5, pos));
            SetColumn(5, pos, GetColumnCopy(3, pos).Reverse().ToArray());
            SetColumn(3, pos, temp.Reverse().ToArray());

            if (pos == 0)
            {
                RotateFace(4, true);
            }

            if (pos == Width - 1)
            {
                RotateFace(2);
            }
        }

        private void MovePlatter(int pos, bool prime = false)
        {
            if (prime)
            {
                MovePlatterPrime(pos);
                return;
            }

            // 1 => 4 => 3 => 2 => 1
            Color[] temp = GetRowCopy(1, pos);
            GameBoard[1][pos] = GetRowCopy(2, pos);
            GameBoard[2][pos] = GetRowCopy(3, pos);
            GameBoard[3][pos] = GetRowCopy(4, pos);
            GameBoard[4][pos] = temp;

            if (pos == 0)
            {
                RotateFace(0);
            }

            if (pos == Width - 1)
            {
                RotateFace(5, true);
            }
        }

        private void MovePlatterPrime(int pos)
        {
            // 1 => 2 => 3 => 4 => 1
            Color[] temp = GetRowCopy(1, pos);
            GameBoard[1][pos] = GetRowCopy(4, pos);
            GameBoard[4][pos] = GetRowCopy(3, pos);
            GameBoard[3][pos] = GetRowCopy(2, pos);
            GameBoard[2][pos] = temp;

            if (pos == 0)
            {
                RotateFace(0, true);
            }

            if (pos == Width - 1)
            {
                RotateFace(5);
            }
        }

        private void MoveFacing(int pos, bool prime = false)
        {
            if (prime)
            {
                MoveFacingPrime(pos);
                return;
            }

            // 0 => 2 => 5 => 4 => 0
            Color[] temp = GetRowCopy(0, pos);
            GameBoard[0][pos] = GetColumnCopy(4, pos).Reverse().ToArray();
            SetColumn(4, pos, GetRowCopy(5, Width - 1 - pos));
            GameBoard[5][Width - 1 - pos] = GetColumnCopy(2, Width - 1 - pos).Reverse().ToArray();
            SetColumn(2, Width - 1 - pos, temp);

            if (pos == 0)
            {
                RotateFace(3, true);
            }

            if (pos == Width -1)
            {
                RotateFace(1);
            }
        }

        private void MoveFacingPrime(int pos)
        {
            // 0 => 4 => 5 => 2 => 0
            Color[] temp = GetRowCopy(0, pos);
            GameBoard[0][pos] = GetColumnCopy(2, Width - 1 - pos);
            SetColumn(2, Width - 1 - pos, GetRowCopy(5, Width - 1 - pos).Reverse().ToArray());
            GameBoard[5][Width - 1 - pos] = GetColumnCopy(4, pos);
            SetColumn(4, pos, temp.Reverse().ToArray());

            if (pos == 0)
            {
                RotateFace(3);
            }

            if (pos == Width - 1)
            {
                RotateFace(1, true);
            }
        }

        private void RotateFace(int face, bool prime = false)
        {
            if (prime)
            {
                RotateFacePrime(face);
                return;
            }

            Color[][] faceCopy = GetFaceCopy(face);
            for (int i = 0; i < Width; i++)
            {
                SetColumn(face, Width - 1 - i, faceCopy[i]);
            }
        }

        private void RotateFacePrime(int face)
        {
            Color[][] faceCopy = GetFaceCopy(face);
            for (int i = 0; i < Width; i++)
            {
                faceCopy[i] = faceCopy[i].Reverse().ToArray();
                SetColumn(face, i, faceCopy[i]);
            }
        }

        private Color[][][] AllocateGameBoard(int width)
        {
            Color[][][] result = new Color[6][][];

            for (int i = 0; i < 6; i++)
            {
                result[i] = AllocateFace(width);
            }

            return result;
        }

        private Color[][] AllocateFace(int width)
        {
            Color[][] result = new Color[width][];

            for (int i = 0; i < width; i++)
            {
                result[i] = new Color[width];
            }

            return result;
        }

        private Color[] GetColumnCopy(int face, int pos)
        {
            Color[] result = new Color[Width];
            for (int i = 0; i < Width; i++)
            {
                result[i] = GameBoard[face][i][pos];
            }

            return result;
        }

        private Color[] GetRowCopy(int face, int pos)
        {
            Color[] result = new Color[Width];
            for (int i = 0; i < Width; i++)
            {
                result[i] = GameBoard[face][pos][i];
            }

            return result;
        }

        private Color[][] GetFaceCopy(int face)
        {
            Color[][] result = AllocateFace(Width);
            Color[][] source = GameBoard[face];

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    result[i][j] = source[i][j];
                }
            }

            return result;
        }

        private void SetColumn(int face, int pos, Color[] colors)
        {
            for (int i = 0; i < Width; i++)
            {
                GameBoard[face][i][pos] = colors[i];
            }
        }

    }
}
