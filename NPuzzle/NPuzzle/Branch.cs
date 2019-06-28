using System;

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

        public Branch(int _g, Puzzle _puzzle, Puzzle _goal, State _state)
        {
            g = _g;
            h = this.SetH(_puzzle, _goal);
            puzzle = _puzzle;
            goal = _goal;
            state = _state;
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

        private int SetH(Puzzle puzzle, Puzzle goal)
        {
            int h = 0;
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
            return h;
        }

        private int ManhattanDistance(Point S, Point G)
        {
            return Math.Abs(S.X - G.X) + Math.Abs(S.Y - G.Y);
        }
    }


}
