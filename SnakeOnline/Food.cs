using System;

namespace SnakeOnline
{
    public class Food
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Food(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void SpawnFood(char[,] map, char symbol)
        {
            map[X, Y] = symbol;
            Console.SetCursorPosition(Y, X);
            Console.Write(map[X, Y]);
        }
    }
}