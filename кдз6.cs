using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

public enum FoodType
{
    Regular,
    Fast,
    Slow
}

public class Point
{
    public int X { get; set; }
    public int Y { get; set; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public class Food
{
    public Point Position { get; set; }
    public FoodType Type { get; set; }
    public ConsoleColor Color { get; set; }

    public Food(int maxX, int maxY)
    {
        GenerateNewPosition(maxX, maxY);
        Type = FoodType.Regular;
        Color = ConsoleColor.Green;
    }

    public void GenerateNewPosition(int maxX, int maxY)
    {
        Random random = new Random();
        Position = new Point(random.Next(1, maxX - 1), random.Next(1, maxY - 1));
    }

    public void Draw()
    {
        Console.SetCursorPosition(Position.X, Position.Y);
        Console.ForegroundColor = Color;
        Console.Write("@");
    }
}

public class Snake
{
    public List<Point> Body { get; set; } = new List<Point>();
    public Direction Direction { get; set; } = Direction.Right;
    public ConsoleColor Color { get; set; } = ConsoleColor.White;

    public Snake(int startX, int startY)
    {
        Body.Add(new Point(startX, startY));
    }

    public void Move(Food food, List<Point> obstacles)
    {
        Point head = new Point(Body[0].X, Body[0].Y);

        switch (Direction)
        {
            case Direction.Up: head.Y--; break;
            case Direction.Down: head.Y++; break;
            case Direction.Left: head.X--; break;
            case Direction.Right: head.X++; break;
        }

       
        if (head.X < 1 || head.X >= Console.WindowWidth - 1 || head.Y < 1 || head.Y >= Console.WindowHeight - 1 ||
            Body.Skip(1).Any(p => p.X == head.X && p.Y == head.Y) || obstacles.Any(p => p.X == head.X && p.Y == head.Y))
        {
            GameOver();
        }

        Body.Insert(0, head);

        
        if (head.X == food.Position.X && head.Y == food.Position.Y)
        {
            food.GenerateNewPosition(Console.WindowWidth - 2, Console.WindowHeight - 1);
            
        }
        else
        {
            Body.RemoveAt(Body.Count - 1); 
        }
    }

    public void GameOver()
    {
        Console.Clear();
        Console.WriteLine("Game Over!");
        Console.ReadKey();
        Environment.Exit(0);
    }

    public void Draw()
    {
        foreach (Point point in Body)
        {
            Console.SetCursorPosition(point.X, point.Y);
            Console.ForegroundColor = Color;
            Console.Write("s");
        }
    }
}

public class Game
{
    private Snake snake;
    private Food food;
    private List<Point> obstacles;
    private Stopwatch stopwatch;
    private int moveDelay;

    public Game(int width, int height)
    {
        Console.WindowWidth = width;
        Console.WindowHeight = height;
        Console.CursorVisible = false;
        snake = new Snake(width / 2, height / 2);
        food = new Food(width, height);
        obstacles = GenerateObstacles(width, height, 10);
        moveDelay = 150;
        stopwatch = new Stopwatch();
    }

    private List<Point> GenerateObstacles(int width, int height, int numObstacles)
    {
        List<Point> obstacles = new List<Point>();
        Random random = new Random();
        for (int i = 0; i < numObstacles; i++)
        {
            Point obstacle;
            do
            {
                obstacle = new Point(random.Next(1, width - 1), random.Next(1, height - 1));
            } while (snake.Body.Contains(obstacle) || obstacles.Contains(obstacle) || obstacle.Equals(food.Position));
            obstacles.Add(obstacle);
        }
        return obstacles;
    }

    public void Run()
    {
        while (true)
        {
            DrawBorder();
            food.Draw();
            snake.Draw();
            DrawObstacles();

            stopwatch.Restart();
            while (stopwatch.ElapsedMilliseconds < moveDelay)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    switch (key.Key)
                    {
                        case ConsoleKey.UpArrow: if (snake.Direction != Direction.Down) snake.Direction = Direction.Up; break;
                        case ConsoleKey.DownArrow: if (snake.Direction != Direction.Up) snake.Direction = Direction.Down; break;
                        case ConsoleKey.LeftArrow: if (snake.Direction != Direction.Right) snake.Direction = Direction.Left; break;
                        case ConsoleKey.RightArrow: if (snake.Direction != Direction.Left) snake.Direction = Direction.Right; break;
                    }
                }
                Thread.Sleep(1);
            }

            snake.Move(food, obstacles);
        }
    }

    private void DrawBorder()
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        for (int x = 0; x < Console.WindowWidth; x++)
        {
            Console.SetCursorPosition(x, 0);
            Console.Write("#");
            Console.SetCursorPosition(x, Console.WindowHeight - 1);
            Console.Write("#");
        }

        for (int y = 0; y < Console.WindowHeight; y++)
        {
            Console.SetCursorPosition(0, y);
            Console.Write("#");
            Console.SetCursorPosition(Console.WindowWidth - 1, y);
            Console.Write("#");
        }
    }

    private void DrawObstacles()
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        foreach (var obstacle in obstacles)
        {
            Console.SetCursorPosition(obstacle.X, obstacle.Y);
            Console.Write("X");
        }
    }

    public static void Main(string[] args)
    {
        Game game = new Game(60, 20);
        game.Run();
    }
}