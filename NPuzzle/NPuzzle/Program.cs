using System;
using System.Collections.Generic;
using System.Linq;
using n_puzzle;

namespace n_puzzle
{
    class Program
    {
        static void Main(string[] args)
        {
            //Puzzle startNode = new Puzzle(
            //    new int[,]
            //    {
            //        { 1, 2, 3 },
            //        { 0, 4, 6 },
            //        { 7, 5, 8 }
            //    }
            //);

            Puzzle startNode = new Puzzle(
                new int[,]
                {
                                { 1, 2, 0 },
                                { 4, 5, 3 },
                                { 7, 8, 6 }
                }
            );

            Puzzle goalNode = new Puzzle(
                 new int[,]
                 {
                     { 1, 2, 3 },
                     { 4, 5, 6 },
                     { 7, 8, 0 }
                 }
            );

            // Zbiór open
            List<Branch> open = new List<Branch>();

            // Zbiór closed zawiera wizytowane węzły
            List<Branch> closed = new List<Branch>();

            // INIT: Pobranie węzła startowego startNode i umieszczenie go w zbiorze open
            Branch branch = new Branch(0, startNode, goalNode, State.Start);
            open.Add(branch);

            while (true)
            {
                // Pobranie z open wezła n o najmniejszej wartości funkcji f(n) i umieszczenie go w zbiorze closed
                var n = open.OrderBy(b => b.F).Where(o => o.G == open.Max(m => m.G)).First();
                closed.Add(n);

                // Jeśli n jest węzłem końcowym to zakończenie
                if (n.Puzzle.Equals(goalNode))
                {
                    break;
                }
                else
                {

                    // Szukanie następców n
                    List<Branch> nextNodes = new List<Branch>();

                    foreach (State state in (State[])Enum.GetValues(typeof(State)))
                    {
                        switch (state)
                        {
                            case State.MoveLeft:
                                {
                                    var nLeft = n.Puzzle.Move(State.MoveLeft);
                                    if (nLeft != null)
                                    {
                                        if (open.Any(o => o.Puzzle.Equals(nLeft)) == false && closed.Any(c => c.Puzzle.Equals(nLeft)) == false)
                                        {
                                            open.Add(new Branch(n.G + 1, nLeft, goalNode, State.MoveLeft));
                                        }
                                    }

                                    break;
                                }
                            case State.MoveRight:
                                {
                                    var nRight = n.Puzzle.Move(State.MoveRight);
                                    if (nRight != null)
                                    {
                                        if (open.Any(o => o.Puzzle.Equals(nRight)) == false && closed.Any(c => c.Puzzle.Equals(nRight)) == false)
                                        {
                                            open.Add(new Branch(n.G + 1, nRight, goalNode, State.MoveRight));
                                        }
                                    }
                                    break;
                                }
                            case State.MoveUp:
                                {
                                    var nUp = n.Puzzle.Move(State.MoveUp);
                                    if (nUp != null)
                                    {
                                        if (open.Any(o => o.Puzzle.Equals(nUp)) == false && closed.Any(c => c.Puzzle.Equals(nUp)) == false)
                                        {
                                            open.Add(new Branch(n.G + 1, nUp, goalNode, State.MoveUp));
                                        }
                                    }
                                    break;
                                }
                            case State.MoveDown:
                                {
                                    var nDown = n.Puzzle.Move(State.MoveDown);
                                    if (nDown != null)
                                    {
                                        if (open.Any(o => o.Puzzle.Equals(nDown)) == false && closed.Any(c => c.Puzzle.Equals(nDown)) == false)
                                        {
                                            open.Add(new Branch(n.G + 1, nDown, goalNode, State.MoveDown));
                                        }
                                    }
                                    break;
                                }
                        }
                    }
                }
            }

            Dictionary<int, List<Branch>> tree = new Dictionary<int, List<Branch>>();
            foreach (var item in closed)
            {
                if (tree.ContainsKey(item.G))
                {
                    tree[item.G].Add(item);
                }
                else {
                    tree.Add(item.G, new List<Branch> { item });
                }
            }

            Display(tree);

            System.Console.Read();
        }

        private static void Display(Dictionary<int, List<Branch>> tree)
        {
            foreach (var level in tree)
            {
                List<string> lines = new List<string>();

                foreach (var brunch in level.Value)
                {
                    for (int i = 0; i < brunch.Puzzle.template.GetLength(0); i++)
                    {
                        string line = string.Empty;
                        for (int j = 0; j < brunch.Puzzle.template.GetLength(1); j++)
                        {
                            if (j == 0)
                            {
                                line += "\t";
                            }

                            line += " " + (brunch.Puzzle.template[i, j] < 10 ? "0" + brunch.Puzzle.template[i, j] : brunch.Puzzle.template[i, j].ToString());
                        }

                        if (lines.Count < i + 1)
                        {
                            lines.Add(line);
                        }
                        else
                        {
                            lines[i] += line;
                        }

                    }
                    if (lines.Count == brunch.Puzzle.template.GetLength(0) + 1)
                    {
                        lines[brunch.Puzzle.template.GetLength(0)] += "\t g=" + brunch.G + ",h=" + brunch.H + ",f=" + brunch.F;
                    }
                    else
                    {
                        lines.Add("g=" + brunch.G + ",h=" + brunch.H + ",f=" + brunch.F);
                    }

                }

                lines.Add("");

                foreach (var line in lines)
                {
                    Console.WriteLine(line.Trim());
                }
            }

            //Legenda
            //Console.ForegroundColor = ConsoleColor.Green;
            //Console.WriteLine("zielony : znaleziona ścieżka");
            //Console.ForegroundColor = ConsoleColor.White;
            //Console.WriteLine("biały : inne ścieżki");
        }

        int NumberOfTilesMisplaced()
        {
            return 0;
        }
    }


}
