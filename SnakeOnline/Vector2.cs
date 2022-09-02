namespace SnakeOnline
{
    public class Vector2
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Dx { get; set; }
        public int Dy { get; set; }
        public int TempI { get; set; }

        public Vector2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Vector2(int dx, int dy, int tempI)
        {
            Dx = dx;
            Dy = dy;
            TempI = tempI;
        }

        public Vector2()
        {
        }
    }
}