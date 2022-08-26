using System;
using System.Collections.Generic;

namespace SnakeOnline
{
    public class Snake
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Dx { set; get; }
        public int Dy { get; set; }

        public char Skin { get; private set; }
        public ConsoleColor Color { get; private set; }

        private readonly List<Vector2> _snakeRoutsA = new List<Vector2>();
        private readonly List<Vector2> _snakeRoutsD = new List<Vector2>();
        private readonly List<Vector2> _snakeRoutsW = new List<Vector2>();
        private readonly List<Vector2> _snakeRoutsS = new List<Vector2>();

        public Snake(int x, int y, char skin, ConsoleColor color, int dx, int dy)
        {
            X = x;
            Y = y;
            Skin = skin;
            Color = color;
            Dx = dx;
            Dy = dy;
        }

        public void Move(char[,] map, ConsoleColor color, SnakePosition snakePosition)
        {
            //2 стыка
            ConsoleColor consoleDefaultColor = Console.BackgroundColor;
            Console.SetCursorPosition(snakePosition.Snake[snakePosition.Snake.Count - 1].Y,
                snakePosition.Snake[snakePosition.Snake.Count - 1].X);
            Console.Write(map[snakePosition.Snake[snakePosition.Snake.Count - 1].X,
                snakePosition.Snake[snakePosition.Snake.Count - 1].Y]);

            for (int i = 0; i < snakePosition.Snake.Count; i++)
            {
                if (snakePosition.AIsPressed)
                {
                    AddRout(_snakeRoutsA, snakePosition.Snake[i].X, snakePosition.Snake[i].Y);
                    snakePosition.AIsPressed = false;
                }

                if (snakePosition.DIsPressed)
                {
                    AddRout(_snakeRoutsD, snakePosition.Snake[i].X, snakePosition.Snake[i].Y);
                    snakePosition.DIsPressed = false;
                }

                if (snakePosition.WIsPressed)
                {
                    AddRout(_snakeRoutsW, snakePosition.Snake[i].X, snakePosition.Snake[i].Y);
                    snakePosition.WIsPressed = false;
                }

                if (snakePosition.SIsPressed)
                {
                    AddRout(_snakeRoutsS, snakePosition.Snake[i].X, snakePosition.Snake[i].Y);
                    snakePosition.SIsPressed = false;
                }

                snakePosition.Snake[i].X -= snakePosition.Snake[i].Dx;
                snakePosition.Snake[i].Y += snakePosition.Snake[i].Dy;

                Console.SetCursorPosition(snakePosition.Snake[i].Y, snakePosition.Snake[i].X);
                Console.BackgroundColor = color;
                Console.Write(snakePosition.Snake[i].Skin);
                Console.BackgroundColor = consoleDefaultColor;

                //todo: fix instant snake turn
                if (i == 0) continue;
                for (int j = 0; j < _snakeRoutsA.Count; j++)
                {
                    if (snakePosition.Snake[i].X == _snakeRoutsA[j].X && snakePosition.Snake[i].Y == _snakeRoutsA[j].Y)
                    {
                        snakePosition.Snake[i].Dx = snakePosition.Snake[i - 1].Dx;
                        snakePosition.Snake[i].Dy = snakePosition.Snake[i - 1].Dy;
                    }

                    if (snakePosition.Snake[snakePosition.Snake.Count - 1].X == _snakeRoutsA[j].X &&
                        snakePosition.Snake[snakePosition.Snake.Count - 1].Y == _snakeRoutsA[j].Y)
                        _snakeRoutsA.RemoveAt(j);
                }

                for (int j = 0; j < _snakeRoutsD.Count; j++)
                {
                    if (snakePosition.Snake[i].X == _snakeRoutsD[j].X && snakePosition.Snake[i].Y == _snakeRoutsD[j].Y)
                    {
                        snakePosition.Snake[i].Dx = snakePosition.Snake[i - 1].Dx;
                        snakePosition.Snake[i].Dy = snakePosition.Snake[i - 1].Dy;
                    }

                    if (snakePosition.Snake[snakePosition.Snake.Count - 1].X == _snakeRoutsD[j].X &&
                        snakePosition.Snake[snakePosition.Snake.Count - 1].Y == _snakeRoutsD[j].Y)
                        _snakeRoutsD.RemoveAt(j);
                }

                for (int j = 0; j < _snakeRoutsW.Count; j++)
                {
                    if (snakePosition.Snake[i].X == _snakeRoutsW[j].X && snakePosition.Snake[i].Y == _snakeRoutsW[j].Y)
                    {
                        snakePosition.Snake[i].Dx = snakePosition.Snake[i - 1].Dx;
                        snakePosition.Snake[i].Dy = snakePosition.Snake[i - 1].Dy;
                    }

                    if (snakePosition.Snake[snakePosition.Snake.Count - 1].X == _snakeRoutsW[j].X &&
                        snakePosition.Snake[snakePosition.Snake.Count - 1].Y == _snakeRoutsW[j].Y)
                        _snakeRoutsW.RemoveAt(j);
                }

                for (int j = 0; j < _snakeRoutsS.Count; j++)
                {
                    if (snakePosition.Snake[i].X == _snakeRoutsS[j].X && snakePosition.Snake[i].Y == _snakeRoutsS[j].Y)
                    {
                        snakePosition.Snake[i].Dx = snakePosition.Snake[i - 1].Dx;
                        snakePosition.Snake[i].Dy = snakePosition.Snake[i - 1].Dy;
                    }

                    if (snakePosition.Snake[snakePosition.Snake.Count - 1].X == _snakeRoutsS[j].X &&
                        snakePosition.Snake[snakePosition.Snake.Count - 1].Y == _snakeRoutsS[j].Y)
                        _snakeRoutsS.RemoveAt(j);
                }
            }
        }

        private static void AddRout(List<Vector2> snakeRouts, int x, int y)
        {
            snakeRouts.Add(new Vector2(x, y));
        }
    }
}