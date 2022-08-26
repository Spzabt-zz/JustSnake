using System.Collections.Generic;

namespace SnakeOnline
{
    public class SnakePosition
    {
        public List<Snake> Snake { get; private set; }
        public bool WIsPressed { get; set; }
        public bool SIsPressed { get; set; }
        public bool AIsPressed { get; set; }
        public bool DIsPressed { get; set; }

        public SnakePosition(List<Snake> snake)
        {
            Snake = snake;
        }

        public void GrowSnake(Snake snake)
        {
            Snake.Add(snake);
        }
    }
}