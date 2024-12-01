using System;
using System.Collections.Generic;

// Helper classes
class Cell
{
    public int X { get; set; }
    public int Y { get; set; }
    public char Value { get; set; }
    public bool IsMovable { get; set; } = false;
    public bool IsCrossable { get; set; } = true;
}

class Grass : Cell
{
    public Grass(int x, int y)
    {
        X = x;
        Y = y;
        Value = '#';
        IsMovable = true;
    }
}

class Tree : Cell
{
    public Tree(int x, int y)
    {
        X = x;
        Y = y;
        Value = 'T';
        IsCrossable = false;
        IsMovable = false;
    }
}

class Character : Cell
{
    public int Health { get; set; } = 100;
    public Character(int x, int y)
    {
        X = x;
        Y = y;
        Value = 'C';
        IsMovable = true;
        IsCrossable = false;
    }
}

class Rock : Cell
{
    public Rock(int x, int y)
    {
        X = x;
        Y = y;
        Value = 'R';
        IsMovable = true;
        IsCrossable = false;
    }
}

class Plate : Cell
{
    public bool IsActivated { get; set; } = false;
    public Plate(int x, int y)
    {
        X = x;
        Y = y;
        Value = 'O';
        IsMovable = false;
        IsCrossable = true;
    }
}


public class Game
{
    private char[,] board;
    private int playerX, playerY;
    private List<Rock> rocks;
    private List<Plate> plates;
    private char currentPlayer = 'X';

    public Game()
    {
        board = new char[10, 10];
        playerX = 5;
        playerY = 5;
        rocks = new List<Rock>();
        plates = new List<Plate>();
        InitializeBoard();
    }

    private void InitializeBoard()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                board[i, j] = '#';
            }
        }

        // Trees
        board[0, 0] = 'T'; board[0, 9] = 'T'; board[9, 0] = 'T'; board[9, 9] = 'T';
        board[3, 3] = 'T'; board[6, 6] = 'T';

        // Rocks
        rocks.Add(new Rock(2, 2));
        rocks.Add(new Rock(7, 3));
        board[2, 2] = 'R';
        board[7, 3] = 'R';

        // Plates
        plates.Add(new Plate(1, 1));
        plates.Add(new Plate(1, 8));
        plates.Add(new Plate(8, 1));
        plates.Add(new Plate(8, 8));
        board[1, 1] = 'O';
        board[1, 8] = 'O';
        board[8, 1] = 'O';
        board[8, 8] = 'O';
    }

    private void PrintBoard()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("----------------------------------------");
        for (int i = 0; i < 10; i++)
        {
            Console.Write("| ");
            for (int j = 0; j < 10; j++)
            {
                char c = board[i, j];
                if (c == 'R') Console.ForegroundColor = ConsoleColor.DarkGray;
                else if (c == 'T') Console.ForegroundColor = ConsoleColor.DarkGreen;
                else if (c == 'O' || c == 'Ⓡ' || c == 'Ⓒ') Console.ForegroundColor = ConsoleColor.Yellow;
                else if (c == 'C') Console.ForegroundColor = ConsoleColor.Blue;
                else Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(c == ' ' ? "  " : $"{c} ");
                Console.ResetColor();
                Console.Write("| ");
            }
            Console.WriteLine("\n----------------------------------------");
        }
    }

    private bool IsValidMove(int newX, int newY)
    {
        return newX >= 0 && newX < 10 && newY >= 0 && newY < 10 && board[newX, newY] != 'T';
    }

    private bool IsPlate(int x, int y)
    {
        foreach (var p in plates)
        {
            if (p.X == x && p.Y == y) return true;
        }
        return false;
    }

    private void ActivatePlate(int x, int y)
    {
        foreach (var p in plates)
        {
            if (p.X == x && p.Y == y && !p.IsActivated)
            {
                p.IsActivated = true;
                board[x, y] = (currentPlayer == 'X') ? 'Ⓡ' : 'Ⓒ';
            }
        }
    }

    private bool AreAllPlatesActivated()
    {
        foreach (var p in plates)
        {
            if (!p.IsActivated) return false;
        }
        return true;
    }

    private bool MoveRock(int rockX, int rockY, int dx, int dy)
    {
        int newRockX = rockX + dx;
        int newRockY = rockY + dy;
        if (!IsValidMove(newRockX, newRockY) || board[newRockX, newRockY] == 'R') return false;
        board[newRockX, newRockY] = 'R';
        board[rockX, rockY] = '#';
        foreach (var r in rocks)
        {
            if (r.X == rockX && r.Y == rockY)
            {
                r.X = newRockX;
                r.Y = newRockY;
                return true;
            }
        }
        return false;
    }

    public void PlayGame()
    {
        board[playerX, playerY] = 'C';

        while (true)
        {
            PrintBoard();
            Console.WriteLine($"Ход игрока {currentPlayer}: (w-a-s-d)");

            ConsoleKeyInfo key = Console.ReadKey(true);
            int dx = 0, dy = 0;

            switch (key.Key)
            {
                case ConsoleKey.W: dy = -1; break;
                case ConsoleKey.S: dy = 1; break;
                case ConsoleKey.A: dx = -1; break;
                case ConsoleKey.D: dx = 1; break;
                default: continue;
            }

            int newPlayerX = playerX + dx;
            int newPlayerY = playerY + dy;

            if (IsValidMove(newPlayerX, newPlayerY))
            {
                bool movedRock = false;
                foreach (var rock in rocks)
                {
                    if (rock.X == newPlayerX && rock.Y == newPlayerY)
                    {
                        if (MoveRock(rock.X, rock.Y, dx, dy))
                        {
                            movedRock = true;
                        }
                        else
                        {
                            Console.WriteLine("Камень не может двигаться в этом направлении!");
                        }
                    }
                }
                if (!movedRock)
                {
                    board[playerX, playerY] = '#';
                    playerX = newPlayerX;
                    playerY = newPlayerY;
                    board[playerX, playerY] = 'C';
                }
                ActivatePlate(playerX, playerY);

                if (AreAllPlatesActivated())
                {
                    Console.WriteLine("Победа!");
                    break;
                }
                currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
            }
            else
            {
                Console.WriteLine("Неверный ход!");
            }
        }
    }

    public static void Main(string[] args)
    {
        Game game = new Game();
        game.PlayGame();
    }
}