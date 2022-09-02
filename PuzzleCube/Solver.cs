using PuzzleCube.Extensions;
using PuzzleCube.Helpers;
using PuzzleCube.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using static PuzzleCube.Cube;

namespace PuzzleCube
{
    public class Solver
    {
        public Cube StartingState { get; }
        public Cube DesiredState { get; }
        public IEnumerable<Move> Solution => _solution ?? Solve();

        internal List<Move> _solution;
        internal CubeComparer _comparer = new();
        internal HashSet<Cube> _solvedBoards = new HashSet<Cube>(new CubeComparer());
        internal Dictionary<Cube, SolveState> _visited = new Dictionary<Cube, SolveState>();
        internal SortedSet<SolveState> _next = new SortedSet<SolveState>(
            Comparer<SolveState>.Create((item1, item2) =>
            {
                return item2.Efficiency.CompareTo(item1.Efficiency);
            }));

        public Solver(Cube startingState, Cube desiredState)
        {
            StartingState = startingState ?? throw new ArgumentNullException(nameof(startingState));
            DesiredState = desiredState ?? throw new ArgumentNullException(nameof(desiredState));
        }

        public IEnumerable<Move> Solve()
        {
            if (_solution is not null) return _solution;

            _solution = new List<Move>();
            _solvedBoards.UnionWith(DesiredState.GetAllOrientations());   
            if (_solvedBoards.Contains(StartingState)) return _solution;

            var initialState = new SolveState(StartingState, _comparer.CalculateScore(StartingState, DesiredState), default);
            foreach (var cube in StartingState.GetAllOrientations()) { _visited.Add(cube, initialState); }

            foreach (var move in StartingState.GetAllMoves()) 
            {
                var nextCube = new Cube(StartingState);
                nextCube.ApplyMove(move);
                if (_solvedBoards.Contains(nextCube))
                {
                    _solution.Add(move);
                    return _solution;
                }
                _next.Add(new SolveState(nextCube, _comparer.CalculateScore(nextCube, DesiredState), move, initialState));
            }

            while (_next.Any())
            {
                var currentState = _next.First();
                _next.Remove(_next.First());

                if (_solution.Any() && currentState.Index >= _solution.Count) continue;
                
                var currentCube = currentState.Cube;

                foreach(var move in currentCube.GetAllMoves())
                {
                    var nextCube = new Cube(currentCube);
                    nextCube.ApplyMove(move);
                    var nextCubeMoves = currentState.Moves.Append(move).ToList();
                    
                    // Check whether it's solved.
                    if (_solvedBoards.Contains(nextCube))
                    {
                        var currentSolution = nextCubeMoves;

                        if (_solution.Any() && currentSolution.Count >= _solution.Count) continue;

                        _solution = currentSolution;

                        continue;
                    }

                    var nextState = new SolveState(nextCube, _comparer.CalculateScore(nextCube, DesiredState), move, currentState);

                    // Check whether we've been here before.
                    if (_visited.ContainsKey(nextCube))
                    {
                        // Is this a more efficient way to get here?
                        var previousVisit = _visited[nextCube];
                        if (nextCubeMoves.Count <= previousVisit.Moves.Count)
                        {
                            // Replace the parent state for the previous visit with the current state
                            // since this is a more efficient way to reach this state.
                            previousVisit.Previous = currentState;
                            continue;
                        }
                        else
                        {
                            // Just discard this state since it is a less efficient way to get to a
                            // state we have already seen.
                            continue;
                        }
                    } 
                    else
                    {
                        // Add all orientations of this state to the 'visited' collection.
                        _visited.AddAllOrientations(nextCube, nextState);
                    }

                    // Put the new state in the 'next' queue
                    _next.Add(nextState);
                }
            }

            if (!_solution.Any())
            {
                throw new InvalidOperationException("No solution exists.");
            }

            return _solution;
        }
    }
}