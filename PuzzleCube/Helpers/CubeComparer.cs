using PuzzleCube.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PuzzleCube.Helpers
{
    public class CubeComparer : IEqualityComparer<Cube>
    {
        public bool Equals(Cube x, Cube y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x is null && y is null) return true;
            if (x is null ^ y is null) return false;
            if (x.Width != y.Width) return false;

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < x.Width; j++)
                {
                    for (int k = 0; k < x.Width; k++)
                    {
                        if (x.GameBoard[i][j][k] != y.GameBoard[i][j][k])
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        public int GetHashCode(Cube obj)
        {
            if (obj is null) return 0;

            int hash = 43;
            hash = hash.GetHashCode();

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < obj.Width; j++)
                {
                    for (int k = 0; k < obj.Width; k++)
                    {
                        hash = hash * obj.GameBoard[i][j][k].GetHashCode();
                    }
                }
            }

            return hash.GetHashCode();
        }

        internal int CalculateScore(Cube subject, IEnumerable<Cube> orientations)
        {
            _ = subject ?? throw new ArgumentNullException(nameof(subject));
            _ = orientations ?? throw new ArgumentNullException(nameof(orientations));

            int perfectScore = subject.Width * subject.Width * 6;
            int bestScore = -1;

            foreach (var cube in orientations)
            {
                var currentScore = perfectScore;

                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < subject.Width; j++)
                    {
                        for (int k = 0; k < subject.Width; k++)
                        {
                            if (subject.GameBoard[i][j][k] != cube.GameBoard[i][j][k])
                            {
                                currentScore--;
                                if (currentScore < bestScore)
                                {
                                    continue;
                                }
                            }
                        }
                    }
                }

                if (currentScore > bestScore)
                {
                    bestScore = currentScore;
                }
            }

            return bestScore;
        }

        internal int CalculateScore(Cube subject, Cube model)
        {
            _ = subject ?? throw new ArgumentNullException(nameof(subject));
            _ = model ?? throw new ArgumentNullException(nameof(model));

            return CalculateScore(subject, model.GetAllOrientations());
        }
    }
}
