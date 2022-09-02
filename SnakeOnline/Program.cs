using System;
using System.Collections.Generic;
using System.Threading;

namespace SnakeOnline
{
    internal static class Program
    {
        private static Random _random;
        private static Snake _snake;
        private static SnakePosition _snakePosition;
        private static Map _map;
        private static Input _input;
        private static Food _food;
        private static char[,] _gameField;
        private static int _highestScore;

        public static void Main(string[] args)
        {
            SetGame();

            bool isPlaying = true;
            int score = 0;
            while (isPlaying)
            {
                Console.SetCursorPosition(41, 0);
                Console.Write($"Score - {score}\n");
                Console.SetCursorPosition(41, 1);
                Console.Write($"The highest score - {_highestScore}");

                if (_gameField[_food.X, _food.Y] ==
                    _gameField[_snakePosition.Snake[0].X, _snakePosition.Snake[0].Y])
                {
                    _snakePosition.GrowSnake(new Snake(
                        _snakePosition.Snake[_snakePosition.Snake.Count - 1].X +
                        _snakePosition.Snake[_snakePosition.Snake.Count - 1].Dx,
                        _snakePosition.Snake[_snakePosition.Snake.Count - 1].Y -
                        _snakePosition.Snake[_snakePosition.Snake.Count - 1].Dy,
                        _snakePosition.Snake[_snakePosition.Snake.Count - 1].Skin,
                        _snakePosition.Snake[_snakePosition.Snake.Count - 1].Color,
                        _snakePosition.Snake[_snakePosition.Snake.Count - 1].Dx,
                        _snakePosition.Snake[_snakePosition.Snake.Count - 1].Dy));

                    HandleFood(_gameField, _food, _random);

                    foreach (var snakePart in _snakePosition.Snake)
                        if (_gameField[_food.X, _food.Y] == _gameField[snakePart.X, snakePart.Y])
                            HandleFood(_gameField, _food, _random);

                    score += 100;
                }

                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    _snakePosition = _input.SnakeController(key, _snakePosition);
                }

                if (_gameField[_snakePosition.Snake[0].X - _snakePosition.Snake[0].Dx,
                        _snakePosition.Snake[0].Y + _snakePosition.Snake[0].Dy] != '#')
                    _snake.Move(_gameField, _snakePosition.Snake[0].Color, _snakePosition);
                else
                {
                    RestartGame(ref score, ref isPlaying);
                }

                for (int i = 1; i < _snakePosition.Snake.Count; i++)
                {
                    if (_snakePosition.Snake[0].X == _snakePosition.Snake[i].X &&
                        _snakePosition.Snake[0].Y == _snakePosition.Snake[i].Y)
                    {
                        RestartGame(ref score, ref isPlaying);
                    }
                }

                Thread.Sleep(150);
            }

            Console.ReadKey(true);
        }

        private static void RestartGame(ref int score, ref bool isPlaying)
        {
            Console.Clear();
            Console.CursorVisible = true;
            Console.WriteLine("Play again? Type 'Y' if yes and 'N' if not");
            bool isCorrect = true;
            while (isCorrect)
            {
                var exit = Console.ReadLine();
                switch (exit)
                {
                    case "Y":
                        SetGame();
                        if (score > _highestScore)
                            _highestScore = score;
                        score = 0;
                        isCorrect = false;
                        break;
                    case "N":
                        isPlaying = false;
                        isCorrect = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input, try again");
                        break;
                }
            }

            Console.CursorVisible = false;
        }

        private static void SetGame()
        {
            Console.CursorVisible = false;
            Console.Title = "Snake online";

            _random = new Random();

            _snake = new Snake(_random.Next(1, 19), _random.Next(1, 39), '@', ConsoleColor.Cyan, 0, 0);
            _snakePosition = new SnakePosition(new List<Snake>());
            _snakePosition.GrowSnake(_snake);

            _map = new Map();
            _input = new Input(_snakePosition);
            _food = new Food(_random.Next(1, 19), _random.Next(1, 39));

            _gameField = _map.CreateMap();
            _map.DrawMap(_gameField);

            _food.SpawnFood(_gameField, '*');
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