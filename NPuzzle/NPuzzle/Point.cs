namespace n_puzzle
{
    class Point
    {
        private readonly int x;
        private readonly int y;

        public Point(int _x, int _y)
        {
            x = _x;
            y = _y;
        }

        public int X
        {
            get { return x; }
        }

        public int Y
        {
            get { return y; }
        }
    }


}
