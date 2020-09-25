using System;
using System.Collections.Generic;

using static PuzzleCube.Cube;

namespace PuzzleCube
{
    public class Solver
    {
        Cube Subject { get; set; }
        
        public Solver(Cube subject)
        {
            Subject = subject ?? throw new ArgumentNullException(nameof(subject));
        }

        public IEnumerable<Move> Solve()
        {
            Dictionary<Color[][][], SolveState> visited = new Dictionary<Color[][][], SolveState>();
            SortedSet<SolveState> next = new SortedSet<SolveState>(
                Comparer<SolveState>.Create((item1, item2) => 
                {
                    return (item1.Score < item2.Score) ? -1 : (item1.Score > item2.Score) ? 1 : 0;
                }));

            int solvedScore = CalculateScore(new Cube(Subject.Width).GameBoard);
            SolveState first = new SolveState(CalculateScore(Subject.GameBoard), Subject, new List<Move>());
            next.Add(first);

            List<Move> bestSolution = null;
            IEnumerable<Move> allMoves = GetAllMoves(Subject.Width);

            while(next.Count > 0)
            {
                SolveState current = next.Max;
                next.Remove(current);

                if ((bestSolution != null) && (current.Moves.Count + 1 >= bestSolution.Count))
                {
                    continue;
                }

                foreach (Move move in allMoves)
                {
                    Cube nextCube = new Cube(current.Cube.GameBoard);
                    nextCube.ApplyMove(move);
                    int score = CalculateScore(nextCube.GameBoard);
                    List<Move> moves = new List<Move>(current.Moves);
                    moves.Add(move);

                    // Check whether this state has already been visited.
                    if (visited.ContainsKey(nextCube.GameBoard))
                    {
                        // Depending on number of moves to get here,
                        // either discard this state or cull the visited item and its children
                        SolveState previous = visited[nextCube.GameBoard];
                        if (previous.Moves.Count <= moves.Count)
                        {
                            continue;
                        }
                        else
                        {
                            UpdateSolveStateAndDescendants(previous);
                        }

                    }

                    if (score == solvedScore)
                    {
                        bestSolution = moves;
                        continue;
                    }

                    SolveState nextState = new SolveState(score / moves.Count, nextCube, moves);
                    current.Children.Add(nextState);
                    next.Add(nextState);
                    visited.Add(nextState.Cube.GameBoard, nextState);
                }
            }

            if (bestSolution == null)
            {
                throw new ArgumentException("No solution is possible.");
            }

            return bestSolution;
        }

        private int CalculateScore(Color[][][] gameBoard)
        {
            int score = 0;
            for (int i = 0; i < 6; i++)
            {
                int[] colorDistribution = new int[Enum.GetNames(typeof(Color)).Length];
                for (int j = 0; j < gameBoard[0].Length - 1; j++)
                {
                    for (int k = 0; k < gameBoard[0].Length - 1; k++)
                    {
                        Color current = gameBoard[i][j][k];
                        colorDistribution[(int)current]++;

                        if (((j > 0) && (gameBoard[i][j - 1][k] == current)) ||
                            ((j < gameBoard[0].Length - 1) && (gameBoard[i][j + 1][k] == current)) ||
                            ((k > 0) && (gameBoard[i][j][k - 1] == current) ||
                            ((k < gameBoard[0].Length - 1) && (gameBoard[i][j][k + 1] == current))))
                        {
                            score++;
                        }
                    }
                }

                int maxColor = 0;
                for (int l = 0; l < colorDistribution.Length; l++)
                {
                    if (colorDistribution[l] > maxColor)
                    {
                        maxColor = colorDistribution[l];
                    }
                }

                score += maxColor;
            }

            return score;
        }

        private IEnumerable<Move> GetAllMoves(int width)
        {
            List<Move> result = new List<Move>();
            
            foreach (Axis axis in Enum.GetValues(typeof(Axis)))
            {
                for (int i = 0; i < width; i++)
                {
                    result.Add(new Move(axis, i, true));
                    result.Add(new Move(axis, i, false));
                }
            }

            return result;
        }

        private void UpdateSolveStateAndDescendants(SolveState state)
        {
            
        }

        internal class SolveState
        {
            public int Score { get; }
            public Cube Cube { get; }
            public List<Move> Moves { get; }
            public List<SolveState> Children { get; set; }

            public SolveState(int score, Cube cube, List<Move> moves)
            {
                Score = score;
                Cube = cube;
                Moves = moves;
            }
        }
    }
}