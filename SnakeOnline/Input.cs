using System;

namespace SnakeOnline
{
    public class Input
    {
        private SnakePosition _snake;

        public Input(SnakePosition snake)
        {
            _snake = snake;
        }

        public SnakePosition SnakeController(ConsoleKeyInfo key, SnakePosition snakePosition)
        {
            switch (key.Key)
            {
                case ConsoleKey.A:
                    if (snakePosition.Snake[0].Dy != 1 && snakePosition.Snake[0].Dy != -1)
                    {
                        snakePosition.Snake[0].Dx = 0;
                        snakePosition.Snake[0].Dy = -1;

                        snakePosition.AIsPressed = true;
                        snakePosition.DIsPressed = false;
                        snakePosition.SIsPressed = false;
                        snakePosition.WIsPressed = false;
                    }

                    break;
                case ConsoleKey.D:
                    if (snakePosition.Snake[0].Dy != -1 && snakePosition.Snake[0].Dy != 1)
                    {
                        snakePosition.Snake[0].Dx = 0;
                        snakePosition.Snake[0].Dy = 1;

                        snakePosition.AIsPressed = false;
                        snakePosition.DIsPressed = true;
                        snakePosition.SIsPressed = false;
                        snakePosition.WIsPressed = false;
                    }

                    break;
                case ConsoleKey.W:
                    if (snakePosition.Snake[0].Dx != -1 && snakePosition.Snake[0].Dx != 1)
                    {
                        snakePosition.Snake[0].Dy = 0;
                        snakePosition.Snake[0].Dx = 1;

                        snakePosition.AIsPressed = false;
                        snakePosition.DIsPressed = false;
                        snakePosition.SIsPressed = false;
                        snakePosition.WIsPressed = true;
                    }

                    break;
                case ConsoleKey.S:
                    if (snakePosition.Snake[0].Dx != 1 && snakePosition.Snake[0].Dx != -1)
                    {
                        snakePosition.Snake[0].Dy = 0;
                        snakePosition.Snake[0].Dx = -1;

                        snakePosition.AIsPressed = false;
                        snakePosition.DIsPressed = false;
                        snakePosition.SIsPressed = true;
                        snakePosition.WIsPressed = false;
                    }

                    break;
            }

            _snake = snakePosition;

            return _snake;
        }
    }
}