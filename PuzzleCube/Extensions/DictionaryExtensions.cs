using PuzzleCube.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PuzzleCube.Extensions
{
    internal static class DictionaryExtensions
    {
        internal static void AddAllOrientations(this Dictionary<Cube, SolveState> @this, Cube cube, SolveState solveState)
        {
            _ = @this ?? throw new ArgumentNullException(nameof(@this));
            _ = cube ?? throw new ArgumentNullException(nameof(cube));
            _ = solveState ?? throw new ArgumentNullException(nameof(solveState));

            foreach (var orientation in cube.GetAllOrientations())
            {
                @this.Add(orientation, solveState);
            }
        }
    }
}
