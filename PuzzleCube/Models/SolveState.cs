using System;
using System.Collections.Generic;
using System.Text;
using static PuzzleCube.Cube;

namespace PuzzleCube.Models
{
    internal class SolveState
    {
        public int Score { get; }

        public Cube Cube { get; }
        
        public List<Move> Moves => GetMoves();

        public int Index => Previous?.Index + 1 ?? 1;
        
        public double Efficiency => (double)Score / (double)Index;
        
        public SolveState Previous { get; set; }

        private Move _move { get; }

        public SolveState(Cube cube, int score, Move move, SolveState previous = null)
        {
            Cube = cube ?? throw new ArgumentNullException(nameof(cube));
            Score = score;
            _move = move;
            Previous = previous;
        }

        internal List<Move> GetMoves()
        {
            var moves = (Previous is not null)
                ? Previous.Moves
                : new List<Move>();
            
            moves.Add(_move);

            return moves;
        }
    }
}
