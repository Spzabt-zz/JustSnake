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
        
        private bool _isChangedDirection;
        private int _tempI;

        private readonly List<Vector2> _snakeRoutsA = new List<Vector2>();
        private readonly List<Vector2> _snakeRoutsD = new List<Vector2>();
        private readonly List<Vector2> _snakeRoutsW = new List<Vector2>();
        private readonly List<Vector2> _snakeRoutsS = new List<Vector2>();

        private readonly Vector2 _tempVector = new Vector2();

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

                //todo: fix instant snake turn
                if (i == 0) continue;


                SnakeTurn(_snakeRoutsA, i, snakePosition);
                SnakeTurn(_snakeRoutsD, i, snakePosition);
                SnakeTurn(_snakeRoutsW, i, snakePosition);
                SnakeTurn(_snakeRoutsS, i, snakePosition);
            }
        }

        private void SnakeTurn(List<Vector2> snakeRout, int i, SnakePosition snakePosition)
        {
            for (int j = 0; j < snakeRout.Count; j++)
            {
                if (snakePosition.Snake[i].X == snakeRout[j].X &&
                    snakePosition.Snake[i].Y == snakeRout[j].Y)
                {
                    if (snakePosition.Snake.Count > 2 && i < snakePosition.Snake.Count - 1)
                    {
                        for (int k = 0; k < _snakeRoutsA.Count; k++)
                        {
                            if (snakePosition.Snake[i + 1].X - snakePosition.Snake[i + 1].Dx == _snakeRoutsA[k].X &&
                                snakePosition.Snake[i + 1].Y + snakePosition.Snake[i + 1].Dy == _snakeRoutsA[k].Y)
                            {
                                _tempVector.X = snakePosition.Snake[i].Dx;
                                _tempVector.Y = snakePosition.Snake[i].Dy;
                                _isChangedDirection = true;
                                _tempI = i;
                            }
                        }

                        for (int k = 0; k < _snakeRoutsD.Count; k++)
                        {
                            if (snakePosition.Snake[i + 1].X - snakePosition.Snake[i + 1].Dx == _snakeRoutsD[k].X &&
                                snakePosition.Snake[i + 1].Y + snakePosition.Snake[i + 1].Dy == _snakeRoutsD[k].Y)
                            {
                                _tempVector.X = snakePosition.Snake[i].Dx;
                                _tempVector.Y = snakePosition.Snake[i].Dy;
                                _isChangedDirection = true;
                                _tempI = i;
                            }
                        }

                        for (int k = 0; k < _snakeRoutsW.Count; k++)
                        {
                            if (snakePosition.Snake[i + 1].X - snakePosition.Snake[i + 1].Dx == _snakeRoutsW[k].X &&
                                snakePosition.Snake[i + 1].Y + snakePosition.Snake[i + 1].Dy == _snakeRoutsW[k].Y)
                            {
                                _tempVector.X = snakePosition.Snake[i].Dx;
                                _tempVector.Y = snakePosition.Snake[i].Dy;
                                _isChangedDirection = true;
                                _tempI = i;
                            }
                        }

                        for (int k = 0; k < _snakeRoutsS.Count; k++)
                        {
                            if (snakePosition.Snake[i + 1].X - snakePosition.Snake[i + 1].Dx == _snakeRoutsS[k].X &&
                                snakePosition.Snake[i + 1].Y + snakePosition.Snake[i + 1].Dy == _snakeRoutsS[k].Y)
                            {
                                _tempVector.X = snakePosition.Snake[i].Dx;
                                _tempVector.Y = snakePosition.Snake[i].Dy;
                                _isChangedDirection = true;
                                _tempI = i;
                            }
                        }
                    }

                    snakePosition.Snake[i].Dx = snakePosition.Snake[i - 1].Dx;
                    snakePosition.Snake[i].Dy = snakePosition.Snake[i - 1].Dy;

                    if (_isChangedDirection && i == _tempI + 1)
                    {
                        snakePosition.Snake[i].Dx = _tempVector.X;
                        snakePosition.Snake[i].Dy = _tempVector.Y;
                        _isChangedDirection = false;
                        _tempI = 0;
                    }
                }

                if (snakePosition.Snake[snakePosition.Snake.Count - 1].X == snakeRout[j].X &&
                    snakePosition.Snake[snakePosition.Snake.Count - 1].Y == snakeRout[j].Y)
                    snakeRout.RemoveAt(j);
            }
        }

        private static void AddRout(List<Vector2> snakeRouts, int x, int y)
        {
            snakeRouts.Add(new Vector2(x, y));
        }
    }
}