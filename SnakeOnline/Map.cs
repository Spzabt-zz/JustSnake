using System;

namespace SnakeOnline
{
    public class Map
    {
        private readonly char[,] _field;

        public Map()
        {
            _field = new char[20, 40];
        }

        public char[,] CreateMap()
        {
            for (int i = 0; i < _field.GetLength(0); i++)
            {
                for (int j = 0; j < _field.GetLength(1); j++)
                {
                    if (i == 0 || i == _field.GetLength(0) - 1)
                    {
                        _field[i, j] = '#';
                    }
                    else if (i > 0 && i < _field.GetLength(0) - 1)
                    {
                        if (j == 0 || j == _field.GetLength(1) - 1)
                        {
                            _field[i, j] = '#';
                        }
                    }
                }
            }

            return _field;
        }

        public void DrawMap(char[,] map)
        {
            Console.Clear();

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j]);
                }

                Console.WriteLine();
            }
        }
    }
}