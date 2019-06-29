using n_puzzle;
using NPuzzle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NPuzzle
{
    class AStar
    {
        private Puzzle initialState;
        private Puzzle goalState;

        public AStar(Puzzle _initialState, Puzzle _goalState)
        {
            valid(_initialState, _goalState);
            this.initialState = _initialState;
            this.goalState = _goalState;
        }

        public Result Run(CancellationToken cancellationToken)
        {
            // Zbiór open
            List<Branch> open = new List<Branch>();

            // Zbiór closed zawiera wizytowane węzły
            List<Branch> closed = new List<Branch>();

            // INIT: Pobranie węzła startowego initialState i umieszczenie go w zbiorze open
            Branch branch = new Branch(0, initialState, goalState, State.Start);
            open.Add(branch);

            while (open.Count > 0)
            {
                // Przerawnie
                cancellationToken.ThrowIfCancellationRequested();

                // Pobranie z open wezła n o najmniejszej wartości funkcji f(n) i umieszczenie go w zbiorze closed
                // var n = open.OrderBy(b => b.F).Where(o => o.G == open.Max(m => m.G)).First();
                var n = open.OrderBy(b => b.F).First();
                open.Remove(n);
                closed.Add(n);

                // Jeśli n jest węzłem końcowym to zakończenie
                if (n.Puzzle.Equals(goalState))
                {
                    break;
                }
                else
                {
                    // Szukanie następców n
                    foreach (State state in (State[])Enum.GetValues(typeof(State)))
                    {
                        switch (state)
                        {
                            case State.MoveLeft:
                                {
                                    var nLeft = n.Puzzle.Move(State.MoveLeft);

                                    if (nLeft != null)
                                    {
                                        var nLeftBranch = new Branch(n.G + 1, nLeft, goalState, State.MoveLeft);

                                        // Jeśli nLeft nie należy do zbioru OPEN ani do CLOSED to dodaj go do zbioru OPEN i ustaw: g(nLeft) = gLeft,  f(nLeft) = gLeft + h(nLeft)
                                        if (open.Any(o => o.Puzzle.Equals(nLeft)) == false && closed.Any(c => c.Puzzle.Equals(nLeft)) == false)
                                        {
                                            open.Add(nLeftBranch);
                                        }
                                        // Jeśli istniej m ≡ nLeft nalezy do zbioru OPEN lub CLOSED i g(m) > gLeft) 
                                        // To ustaw g(nLeft) = gLeft, f(nLeft) = gLeft + h(nLeft), usuń m, usuń ścieżkę od s do m dodaj nLeft do zbioru OPEN 
                                        else
                                        {
                                            if (open.Any(o => o.Puzzle.Equals(nLeft) && o.G > nLeftBranch.G))
                                            {
                                                open.Remove(open.Find(o => o.Puzzle.Equals(nLeft) && o.G > nLeftBranch.G));
                                                open.Add(nLeftBranch);
                                            }
                                            if (closed.Any(c => c.Puzzle.Equals(nLeft) && c.G > nLeftBranch.G))
                                            {
                                                closed.Remove(open.Find(c => c.Puzzle.Equals(nLeft) && c.G > nLeftBranch.G));
                                                open.Add(nLeftBranch);
                                            }
                                        }
                                    }

                                    break;
                                }
                            case State.MoveRight:
                                {
                                    var nRight = n.Puzzle.Move(State.MoveRight);

                                    if (nRight != null)
                                    {
                                        var nRightBranch = new Branch(n.G + 1, nRight, goalState, State.MoveRight);

                                        // Jeśli nRight nie należy do zbioru OPEN ani do CLOSED to dodaj go do zbioru OPEN i ustaw: g(nRight) = gRight,  f(nRight) = gRight + h(nRight)
                                        if (open.Any(o => o.Puzzle.Equals(nRight)) == false && closed.Any(c => c.Puzzle.Equals(nRight)) == false)
                                        {
                                            open.Add(nRightBranch);
                                        }
                                        // Jeśli istniej m ≡ nRight nalezy do zbioru OPEN lub CLOSED i g(m) > gRight) 
                                        // To ustaw g(nRight) = gRight, f(nRight) = gRight + h(nRight), usuń m, usuń ścieżkę od s do m dodaj nRight do zbioru OPEN 
                                        else
                                        {
                                            if (open.Any(o => o.Puzzle.Equals(nRight) && o.G > nRightBranch.G))
                                            {
                                                open.Remove(open.Find(o => o.Puzzle.Equals(nRight) && o.G > nRightBranch.G));
                                                open.Add(nRightBranch);
                                            }
                                            if (closed.Any(c => c.Puzzle.Equals(nRight) && c.G > nRightBranch.G))
                                            {
                                                closed.Remove(open.Find(c => c.Puzzle.Equals(nRight) && c.G > nRightBranch.G));
                                                open.Add(nRightBranch);
                                            }
                                        }
                                    }
                                    break;
                                }
                            case State.MoveUp:
                                {
                                    var nUp = n.Puzzle.Move(State.MoveUp);

                                    if (nUp != null)
                                    {
                                        var nUpBranch = new Branch(n.G + 1, nUp, goalState, State.MoveUp);

                                        // Jeśli nUp nie należy do zbioru OPEN ani do CLOSED to dodaj go do zbioru OPEN i ustaw: g(nUp) = gUp,  f(nUp) = gUp + h(nUp)
                                        if (open.Any(o => o.Puzzle.Equals(nUp)) == false && closed.Any(c => c.Puzzle.Equals(nUp)) == false)
                                        {
                                            open.Add(nUpBranch);
                                        }
                                        // Jeśli istniej m ≡ nUp nalezy do zbioru OPEN lub CLOSED i g(m) > gUp) 
                                        // To ustaw g(nUp) = gUp, f(nUp) = gUp + h(nUp), usuń m, usuń ścieżkę od s do m dodaj nUp do zbioru OPEN 
                                        else
                                        {
                                            if (open.Any(o => o.Puzzle.Equals(nUp) && o.G > nUpBranch.G))
                                            {
                                                open.Remove(open.Find(o => o.Puzzle.Equals(nUp) && o.G > nUpBranch.G));
                                                open.Add(nUpBranch);
                                            }
                                            if (closed.Any(c => c.Puzzle.Equals(nUp) && c.G > nUpBranch.G))
                                            {
                                                closed.Remove(open.Find(c => c.Puzzle.Equals(nUp) && c.G > nUpBranch.G));
                                                open.Add(nUpBranch);
                                            }
                                        }
                                    }
                                    break;
                                }
                            case State.MoveDown:
                                {
                                    var nDown = n.Puzzle.Move(State.MoveDown);

                                    if (nDown != null)
                                    {
                                        var nDownBranch = new Branch(n.G + 1, nDown, goalState, State.MoveDown);

                                        // Jeśli nDown nie należy do zbioru OPEN ani do CLOSED to dodaj go do zbioru OPEN i ustaw: g(nDown) = gDown,  f(nDown) = gDown + h(nDown)
                                        if (open.Any(o => o.Puzzle.Equals(nDown)) == false && closed.Any(c => c.Puzzle.Equals(nDown)) == false)
                                        {
                                            open.Add(nDownBranch);
                                        }
                                        // Jeśli istniej m ≡ nDown nalezy do zbioru OPEN lub CLOSED i g(m) > gDown) 
                                        // To ustaw g(nDown) = gDown, f(nDown) = gDown + h(nDown), usuń m, usuń ścieżkę od s do m dodaj nDown do zbioru OPEN 
                                        else
                                        {
                                            if (open.Any(o => o.Puzzle.Equals(nDown) && o.G > nDownBranch.G))
                                            {
                                                open.Remove(open.Find(o => o.Puzzle.Equals(nDown) && o.G > nDownBranch.G));
                                                open.Add(nDownBranch);
                                            }
                                            if (closed.Any(c => c.Puzzle.Equals(nDown) && c.G > nDownBranch.G))
                                            {
                                                closed.Remove(open.Find(c => c.Puzzle.Equals(nDown) && c.G > nDownBranch.G));
                                                open.Add(nDownBranch);
                                            }
                                        }
                                    }
                                    break;
                                }
                        }
                    }
                }
            }

            List<Branch> path = new List<Branch>();

            var lastState = closed.Find(c => c.Puzzle.Equals(goalState));
            path.Add(lastState);

            while (lastState.G != 0)
            {
                lastState = path[0];
                State reverseDirection = State.Start;

                switch (lastState.State)
                {
                    case State.MoveDown:
                        {
                            reverseDirection = State.MoveUp;
                            break;
                        }
                    case State.MoveUp:
                        {
                            reverseDirection = State.MoveDown;
                            break;
                        }
                    case State.MoveLeft:
                        {
                            reverseDirection = State.MoveRight;
                            break;
                        }
                    case State.MoveRight:
                        {
                            reverseDirection = State.MoveLeft;
                            break;
                        }
                }

                var prevPuzzle = lastState.Puzzle.Move(reverseDirection);
                lastState = closed.Find(c => c.G == (lastState.G - 1) && c.Puzzle.Equals(prevPuzzle));
                path.Insert(0, lastState);
            }

            return new Result{ Path = path, Open = open.Count, Closed = closed.Count};
        }

        private void valid(Puzzle initialState, Puzzle goalState)
        {
            List<int> initial = new List<int>();
            List<int> goal = new List<int>();

            for (int i = 0; i < initialState.template.GetLength(0); i++)
            {
                for (int j = 0; j < initialState.template.GetLength(1); j++)
                {
                    initial.Add(initialState.template[i, j]);
                    goal.Add(goalState.template[i, j]);
                }
            }

            initial.Sort();
            goal.Sort();

            bool test = initial.SequenceEqual(goal);

            if (!test)
            {
                System.ArgumentException argEx = new System.ArgumentException("Niepoprawne dane");
                throw argEx;
            }

        }
    }
}
