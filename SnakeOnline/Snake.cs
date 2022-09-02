using System;
using System.Collections.Generic;
using System.Linq;

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

        private readonly List<Vector2> _tempVectors = new List<Vector2>();

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

                if (i == 0) continue;
                SnakeTurn(_snakeRoutsA, i, snakePosition);
                SnakeTurn(_snakeRoutsD, i, snakePosition);
                SnakeTurn(_snakeRoutsW, i, snakePosition);
                SnakeTurn(_snakeRoutsS, i, snakePosition);
            }
        }

        private void SnakeTurn(IList<Vector2> snakeRout, int i, SnakePosition snakePosition)
        {
            for (int j = 0; j < snakeRout.Count; j++)
            {
                if (snakePosition.Snake[i].X == snakeRout[j].X &&
                    snakePosition.Snake[i].Y == snakeRout[j].Y)
                {
                    if (snakePosition.Snake.Count > 2 && i < snakePosition.Snake.Count - 1)
                    {
                        foreach (var turn in _snakeRoutsA.Where(turn =>
                                     snakePosition.Snake[i + 1].X - snakePosition.Snake[i + 1].Dx == turn.X &&
                                     snakePosition.Snake[i + 1].Y + snakePosition.Snake[i + 1].Dy == turn.Y))
                        {
                            _tempVectors.Add(new Vector2(snakePosition.Snake[i].Dx, snakePosition.Snake[i].Dy, i));
                        }

                        foreach (var turn in _snakeRoutsD.Where(turn =>
                                     snakePosition.Snake[i + 1].X - snakePosition.Snake[i + 1].Dx == turn.X &&
                                     snakePosition.Snake[i + 1].Y + snakePosition.Snake[i + 1].Dy == turn.Y))
                        {
                            _tempVectors.Add(new Vector2(snakePosition.Snake[i].Dx, snakePosition.Snake[i].Dy, i));
                        }

                        foreach (var turn in _snakeRoutsW.Where(turn =>
                                     snakePosition.Snake[i + 1].X - snakePosition.Snake[i + 1].Dx == turn.X &&
                                     snakePosition.Snake[i + 1].Y + snakePosition.Snake[i + 1].Dy == turn.Y))
                        {
                            _tempVectors.Add(new Vector2(snakePosition.Snake[i].Dx, snakePosition.Snake[i].Dy, i));
                        }

                        foreach (var turn in _snakeRoutsS.Where(turn =>
                                     snakePosition.Snake[i + 1].X - snakePosition.Snake[i + 1].Dx == turn.X &&
                                     snakePosition.Snake[i + 1].Y + snakePosition.Snake[i + 1].Dy == turn.Y))
                        {
                            _tempVectors.Add(new Vector2(snakePosition.Snake[i].Dx, snakePosition.Snake[i].Dy, i));
                        }
                    }

                    snakePosition.Snake[i].Dx = snakePosition.Snake[i - 1].Dx;
                    snakePosition.Snake[i].Dy = snakePosition.Snake[i - 1].Dy;

                    for (int k = 0; k < _tempVectors.Count; k++)
                    {
                        if (i != _tempVectors[k].TempI + 1) continue;
                        snakePosition.Snake[i].Dx = _tempVectors[k].Dx;
                        snakePosition.Snake[i].Dy = _tempVectors[k].Dy;
                        _tempVectors.RemoveAt(k);
                    }
                }

                if (snakePosition.Snake[snakePosition.Snake.Count - 1].X == snakeRout[j].X &&
                    snakePosition.Snake[snakePosition.Snake.Count - 1].Y == snakeRout[j].Y)
                    snakeRout.RemoveAt(j);
            }
        }

        private static void AddRout(ICollection<Vector2> snakeRouts, int x, int y)
        {
            snakeRouts.Add(new Vector2(x, y));
        }
    }
}