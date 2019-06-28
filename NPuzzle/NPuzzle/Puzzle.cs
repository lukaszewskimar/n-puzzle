using System.Linq;

namespace n_puzzle
{
    public class Puzzle
    {
        public int[,] template;

        public Puzzle(int[,] _template)
        {
            template = _template;
        }

        // zwraca null jeżeli nie da się przesunąć 
        public Puzzle Move(State state)
        {
            var newPuzzle = template.Clone() as int[,];

            for (int i = 0; i < newPuzzle.GetLength(0); i++)
            {
                for (int j = 0; j < newPuzzle.GetLength(1); j++)
                {
                    if (newPuzzle[i, j] == 0)
                    {
                        switch (state)
                        {
                            case State.MoveLeft:
                                {
                                    if (j > 0)
                                    {
                                        newPuzzle[i, j] = newPuzzle[i, j - 1];
                                        newPuzzle[i, j - 1] = 0;
                                        return new Puzzle(newPuzzle);
                                    }
                                    return null;
                                }
                            case State.MoveRight:
                                {
                                    if (j < newPuzzle.GetLength(1) - 1)
                                    {
                                        newPuzzle[i, j] = newPuzzle[i, j + 1];
                                        newPuzzle[i, j + 1] = 0;
                                        return new Puzzle(newPuzzle);
                                    }
                                    return null;
                                }
                            case State.MoveUp:
                                {
                                    if (i > 0)
                                    {
                                        newPuzzle[i, j] = newPuzzle[i - 1, j];
                                        newPuzzle[i - 1, j] = 0;
                                        return new Puzzle(newPuzzle);
                                    }
                                    return null;
                                }
                            case State.MoveDown:
                                {
                                    if (i < newPuzzle.GetLength(0) - 1)
                                    {
                                        newPuzzle[i, j] = newPuzzle[i + 1, j];
                                        newPuzzle[i + 1, j] = 0;
                                        return new Puzzle(newPuzzle);
                                    }
                                    return null;
                                }
                            default:
                                {
                                    return null;
                                }
                        }
                    }
                }
            }
            return null;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (this.GetType() != obj.GetType()) return false;

            Puzzle p = (Puzzle)obj;

            return this.template.Rank == p.template.Rank &&
            Enumerable.Range(0, this.template.Rank).All(dimension => this.template.GetLength(dimension) == p.template.GetLength(dimension)) &&
            this.template.Cast<int>().SequenceEqual(p.template.Cast<int>());
        }

    }
}
