﻿using System;

namespace n_puzzle
{
    public class Branch
    {
        // Koszt (liczba kroków podjętych do bieżącego stanu)
        private readonly int g;
        // Szacowana odległość do celu
        private readonly int h;

        private readonly Puzzle puzzle;
        private readonly Puzzle goal;
        private readonly State state;
        private readonly Heuristic heuristic;

        public Branch(int _g, Puzzle _puzzle, Puzzle _goal, State _state, Heuristic _heuristic)
        {
            g = _g;
            h = this.SetH(_puzzle, _goal, _heuristic);
            puzzle = _puzzle;
            goal = _goal;
            state = _state;
            heuristic = _heuristic;
        }

        public int G
        {
            get { return g; }
        }

        public int H
        {
            get { return h; }
        }

        public int F
        {
            get { return g + h; }
        }

        public Puzzle Puzzle
        {
            get { return puzzle; }
        }

        public State State
        {
            get { return state; }
        }

        private int SetH(Puzzle puzzle, Puzzle goal, Heuristic heuristic)
        {
            int h = 0;
            switch (heuristic)
            {
                case Heuristic.ManhattanDistance:
                    {
                        for (int i = 0; i < puzzle.template.GetLength(0); i++)
                        {
                            for (int j = 0; j < puzzle.template.GetLength(1); j++)
                            {
                                if (puzzle.template[i, j] != goal.template[i, j])
                                {
                                    Point pointS = new Point(j, i);

                                    for (int k = 0; k < goal.template.GetLength(0); k++)
                                    {
                                        for (int l = 0; l < goal.template.GetLength(1); l++)
                                        {
                                            if (puzzle.template[i, j] == goal.template[k, l] && puzzle.template[i, j] != 0)
                                            {
                                                Point pointG = new Point(l, k);
                                                h += this.ManhattanDistance(pointS, pointG);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    }
                case Heuristic.HammingDistance:
                    {
                        for (int i = 0; i < puzzle.template.GetLength(0); i++)
                        {
                            for (int j = 0; j < puzzle.template.GetLength(1); j++)
                            {
                                if (puzzle.template[i, j] != goal.template[i, j] && puzzle.template[i, j] != 0)
                                {
                                    h++;            
                                }
                            }
                        }
                        break;
                    }
            }

         

            return h;
        }

        private int ManhattanDistance(Point S, Point G)
        {
            return Math.Abs(S.X - G.X) + Math.Abs(S.Y - G.Y);
        }
    }


}
