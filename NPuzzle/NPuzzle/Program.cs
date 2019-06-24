using System;
using System.Collections.Generic;
using n_puzzle;

namespace n_puzzle
{
    class Program
    {

        static void Main(string[] args)
        {
            Puzzle puzzle = new Puzzle(
                new int[,]
                {
                    { 1, 2, 3 },
                    { 0, 4, 6 },
                    { 7, 5, 8 }
                }
            );

            Puzzle goal = new Puzzle(
                 new int[,]
                 {
                     { 1, 2, 3 },
                     { 4, 5, 6 },
                     { 7, 8, 0 }
                 }
            );

            Branch branch = new Branch(0, puzzle, goal, State.Start);
            Dictionary<int, List<Branch>> tree = new Dictionary<int, List<Branch>>();
            tree.Add(0, new List<Branch> { branch });

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

        }

        int NumberOfTilesMisplaced()
        {
            return 0;
        }
    }


}
