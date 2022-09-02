using System;
using System.Collections.Generic;
using System.Text;
using static PuzzleCube.Cube;

namespace PuzzleCube.Extensions
{
    public static class CubeExtensions
    {
        public static IEnumerable<Move> GetAllMoves(this Cube @this)
        {
            _ = @this ?? throw new ArgumentNullException(nameof(@this));
            
            List<Move> result = new List<Move>();

            foreach (Axis axis in Enum.GetValues(typeof(Axis)))
            {
                for (int i = 0; i < @this.Width; i++)
                {
                    result.Add(new Move(axis, i, true));
                    result.Add(new Move(axis, i, false));
                }
            }

            return result;
        }

        public static IEnumerable<Cube> GetAllOrientations(this Cube @this)
        {
            _ = @this ?? throw new ArgumentNullException(nameof(@this));
            
            var result = new List<Cube>();

            // Make six clones -- one for each side to face "up"
            var zeroUp = new Cube(@this);
            var oneUp = new Cube(@this);
            oneUp.RotateCube(Axis.x, true);
            var twoUp = new Cube(@this);
            twoUp.RotateCube(Axis.z, true);
            var threeUp = new Cube(@this);
            threeUp.RotateCube(Axis.x);
            var fourUp = new Cube(@this);
            fourUp.RotateCube(Axis.z);
            var fiveUp = new Cube(@this);
            fiveUp.RotateCube(Axis.x);
            fiveUp.RotateCube(Axis.x);

            // Helper to add permutations to the result
            void AddCubesToResult()
            {
                result.Add(new Cube(zeroUp));
                result.Add(new Cube(oneUp));
                result.Add(new Cube(twoUp));
                result.Add(new Cube(threeUp));
                result.Add(new Cube(fourUp));
                result.Add(new Cube(fiveUp));
            }

            // Rotates each clone one turn
            void RotateCubes()
            {
                zeroUp.RotateCube(Axis.y);
                oneUp.RotateCube(Axis.y);
                twoUp.RotateCube(Axis.y);
                threeUp.RotateCube(Axis.y);
                fourUp.RotateCube(Axis.y);
                fiveUp.RotateCube(Axis.y);
            }

            AddCubesToResult();

            for (int i = 0; i < 3; i++)
            {
                RotateCubes();
                AddCubesToResult();
            }

            return result;
        } 
    }
}
