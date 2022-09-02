using System;
using System.Collections.Generic;
using System.Threading;

namespace SnakeOnline
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.Title = "Snake online";

            Random random = new Random();

            Snake snake = new Snake(random.Next(1, 19), random.Next(1, 39), '@', ConsoleColor.Cyan, 0, 0);
            SnakePosition snakePosition = new SnakePosition(new List<Snake>());
            snakePosition.GrowSnake(snake);

            Map map = new Map();
            Input input = new Input(snakePosition);
            Food food = new Food(random.Next(1, 19), random.Next(1, 39));

            char[,] gameField = map.CreateMap();
            map.DrawMap(gameField);

            food.SpawnFood(gameField, '*');

            bool isPlaying = true;
            while (isPlaying)
            {
                if (gameField[food.X, food.Y] ==
                    gameField[snakePosition.Snake[0].X, snakePosition.Snake[0].Y])
                {
                    snakePosition.GrowSnake(new Snake(
                        snakePosition.Snake[snakePosition.Snake.Count - 1].X +
                        snakePosition.Snake[snakePosition.Snake.Count - 1].Dx,
                        snakePosition.Snake[snakePosition.Snake.Count - 1].Y -
                        snakePosition.Snake[snakePosition.Snake.Count - 1].Dy,
                        snakePosition.Snake[snakePosition.Snake.Count - 1].Skin,
                        snakePosition.Snake[snakePosition.Snake.Count - 1].Color,
                        snakePosition.Snake[snakePosition.Snake.Count - 1].Dx,
                        snakePosition.Snake[snakePosition.Snake.Count - 1].Dy));

                    HandleFood(gameField, food, random);

                    foreach (var snakePart in snakePosition.Snake)
                        if (gameField[food.X, food.Y] == gameField[snakePart.X, snakePart.Y])
                            HandleFood(gameField, food, random);
                }

                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    snakePosition = input.SnakeController(key, snakePosition);
                }

                if (gameField[snakePosition.Snake[0].X - snakePosition.Snake[0].Dx,
                        snakePosition.Snake[0].Y + snakePosition.Snake[0].Dy] != '#')
                    snake.Move(gameField, snakePosition.Snake[0].Color, snakePosition);
                else
                    isPlaying = false;

                for (int i = 1; i < snakePosition.Snake.Count; i++)
                    if (snakePosition.Snake[0].X == snakePosition.Snake[i].X &&
                        snakePosition.Snake[0].Y == snakePosition.Snake[i].Y)
                        isPlaying = false;

                Thread.Sleep(150);
            }

            Console.ReadKey(true);
        }

        private static void HandleFood(char[,] gameField, Food food, Random random)
        {
            gameField[food.X, food.Y] = ' ';
            food.X = random.Next(1, 19);
            food.Y = random.Next(1, 39);
            food.SpawnFood(gameField, '*');
        }
    }
}