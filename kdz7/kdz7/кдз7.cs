using System;
using System.Collections.Generic;
using System.Diagnostics;

public class Character
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int AttackPower { get; set; }
    public List<Item> Inventory { get; set; } = new List<Item>();
    public int X { get; set; }
    public int Y { get; set; }

    public Character(string name, int health, int attackPower)
    {
        Name = name;
        Health = health;
        AttackPower = attackPower;
    }

    public void Attack(Character target)
    {
        int damage = AttackPower;
        target.Health -= damage;
        Console.WriteLine($"{Name} атаковал {target.Name}, нанеся {damage} урона!");
        if (target.Health <= 0)
        {
            Console.WriteLine($"{target.Name} повержен!");
            target.DropItem(new Item("Зелье здоровья", 20));
        }
    }

    public void UseItem(Item item)
    {
        if (Inventory.Contains(item))
        {
            Health += item.HealthRestore;
            Console.WriteLine($"{Name} использовал {item.Name}, восстановив {item.HealthRestore} здоровья!");
            Inventory.Remove(item);
        }
    }

    public void DropItem(Item item)
    {
        Console.WriteLine($"{Name} оставил {item.Name}.");
        Inventory.Add(item);
    }

    public virtual void Interact(Character player) { }
}

public class Item
{
    public string Name { get; set; }
    public int HealthRestore { get; set; }

    public Item(string name, int healthRestore)
    {
        Name = name;
        HealthRestore = healthRestore;
    }
}

public class NPC : Character
{
    public List<string> Dialogue { get; set; }
    public List<Item> ItemsToGive { get; set; } = new List<Item>();

    public NPC(string name, int health, int attackPower, List<string> dialogue, List<Item> itemsToGive) : base(name, health, attackPower)
    {
        Dialogue = dialogue;
        ItemsToGive = itemsToGive;
    }

    public override void Interact(Character player)
    {
        Console.WriteLine($"Вы встретили {Name}.");
        foreach (string line in Dialogue)
        {
            Console.WriteLine(line);
        }
        Console.WriteLine("Что вы хотите сделать?");
        Console.WriteLine("1. Поговорить");
        Console.WriteLine("2. Атаковать");

        if (int.TryParse(Console.ReadLine(), out int choice))
        {
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Привет! Как дела?");
                    Console.WriteLine("Ваш ответ:"); // Предлагаем игроку ответить
                    string playerResponse = Console.ReadLine(); // Считываем ответ игрока
                    Console.WriteLine($"Вы сказали: {playerResponse}"); // Выводим ответ игрока (можно убрать, если не нужно)


                    if (ItemsToGive.Count > 0)
                    {
                        Console.WriteLine($"Я могу подарить вам {ItemsToGive[0].Name}");
                        player.Inventory.Add(ItemsToGive[0]);
                        ItemsToGive.RemoveAt(0);
                    }
                    else
                    {
                        Console.WriteLine("Больше ничего нет.");
                    }
                    break;
                case 2:
                    player.Attack(this);
                    break;
                default:
                    Console.WriteLine("Неверный выбор.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Неверный ввод.");
        }

        Console.WriteLine("Пока!");
    }
}


public class Maze
{
    public char[,] Map { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public int PlayerX { get; set; }
    public int PlayerY { get; set; }
    public int ExitX { get; set; }
    public int ExitY { get; set; }
    private Random random = new Random();

    public Maze(int width, int height)
    {
        Width = width;
        Height = height;
        Map = new char[height, width];
        GenerateMaze();
        PlacePlayer();
        PlaceExit();
    }

    private void GenerateMaze()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                Map[y, x] = '#';
            }
        }

        int currentX = 1;
        int currentY = 1;
        Map[currentY, currentX] = ' ';

        List<(int x, int y)> stack = new List<(int x, int y)>();
        stack.Add((currentX, currentY));
        HashSet<(int x, int y)> visited = new HashSet<(int x, int y)>();
        visited.Add((currentX, currentY));

        while (stack.Count > 0)
        {
            (int x, int y) currentCell = stack[^1];
            List<(int x, int y)> neighbors = GetUnvisitedNeighbors(currentCell.x, currentCell.y, visited);

            if (neighbors.Count > 0)
            {
                (int x, int y) nextCell = neighbors[random.Next(neighbors.Count)];
                int dx = (nextCell.x - currentCell.x) / 2;
                int dy = (nextCell.y - currentCell.y) / 2;
                Map[currentCell.y + dy, currentCell.x + dx] = ' ';
                Map[nextCell.y, nextCell.x] = ' ';
                stack.Add(nextCell);
                visited.Add(nextCell);
            }
            else
            {
                stack.RemoveAt(stack.Count - 1);
            }
        }

        PlacePlayer();
        PlaceExit();
    }

    private List<(int x, int y)> GetUnvisitedNeighbors(int x, int y, HashSet<(int x, int y)> visited)
    {
        List<(int x, int y)> neighbors = new List<(int x, int y)>();
        int[] dx = { 0, 0, 2, -2 };
        int[] dy = { 2, -2, 0, 0 };

        for (int i = 0; i < 4; i++)
        {
            int nx = x + dx[i];
            int ny = y + dy[i];
            if (nx >= 1 && nx < Width - 1 && ny >= 1 && ny < Height - 1 && !visited.Contains((nx, ny)))
            {
                neighbors.Add((nx, ny));
            }
        }
        return neighbors;
    }

    private void PlacePlayer()
    {
        PlayerX = 1;
        PlayerY = 1;
        Map[PlayerY, PlayerX] = 'C';
    }

    private void PlaceExit()
    {
        do
        {
            ExitX = random.Next(Width - 2) + 1;
            ExitY = random.Next(Height - 2) + 1;
        } while (Map[ExitY, ExitX] == '#' || (ExitX == PlayerX && ExitY == PlayerY));
        Map[ExitY, ExitX] = 'E';
    }

    public bool IsValidMove(int newX, int newY)
    {
        return newX >= 0 && newX < Width && newY >= 0 && newY < Height && Map[newY, newX] != '#';
    }

    public void MovePlayer(int dx, int dy)
    {
        int newX = PlayerX + dx;
        int newY = PlayerY + dy;

        if (IsValidMove(newX, newY))
        {
            Map[PlayerY, PlayerX] = ' ';
            PlayerX = newX;
            PlayerY = newY;
            Map[PlayerY, PlayerX] = 'C';
        }
    }

    public bool CheckWin()
    {
        return PlayerX == ExitX && PlayerY == ExitY;
    }
}

public class Game
{
    private Maze maze;
    private Character player;
    private List<NPC> npcs;
    private TimeSpan timeLimit;
    private Stopwatch stopwatch;

    public Game(int mazeWidth, int mazeHeight, TimeSpan timeLimit)
    {
        maze = new Maze(mazeWidth, mazeHeight);
        player = new Character("Игрок", 100, 10);
        player.X = maze.PlayerX;
        player.Y = maze.PlayerY;
        npcs = new List<NPC>();
        this.timeLimit = timeLimit;
        stopwatch = new Stopwatch();
    }

    public void AddNPC(NPC npc)
    {
        npcs.Add(npc);
        Random random = new Random();
        do
        {
            npc.X = random.Next(maze.Width);
            npc.Y = random.Next(maze.Height);
        } while (maze.Map[npc.Y, npc.X] == '#' || (npc.X == player.X && npc.Y == player.Y));
        maze.Map[npc.Y, npc.X] = 'N';
    }

    public void Run()
    {
        Console.WriteLine("Добро пожаловать в игру!");
        stopwatch.Start();
        while (true)
        {
            if (stopwatch.Elapsed >= timeLimit)
            {
                Console.WriteLine($"Время вышло! Вы проиграли. (Время: {stopwatch.Elapsed})");
                break;
            }
            maze.Map[player.Y, player.X] = 'C'; 
            PrintMap();
            HandlePlayerInput();
            CheckForNPCs();
            if (maze.CheckWin())
            {
                Console.WriteLine("Вы победили!");
                break;
            }
        }
        stopwatch.Stop();
        Console.WriteLine("Игра окончена.");
    }

    private void PrintMap()
    {
        Console.WriteLine($"Здоровье: {player.Health}");
        Console.WriteLine($"Время: {stopwatch.Elapsed}");
        for (int y = 0; y < maze.Height; y++)
        {
            for (int x = 0; x < maze.Width; x++)
            {
                Console.Write(maze.Map[y, x]);
            }
            Console.WriteLine();
        }
    }

    private void HandlePlayerInput()
    {
        ConsoleKeyInfo key = Console.ReadKey(true);
        int dx = 0, dy = 0;

        switch (key.Key)
        {
            case ConsoleKey.W: dy = -1; break;
            case ConsoleKey.S: dy = 1; break;
            case ConsoleKey.A: dx = -1; break;
            case ConsoleKey.D: dx = 1; break;
            default: return;
        }

        maze.MovePlayer(dx, dy);
        player.X = maze.PlayerX;
        player.Y = maze.PlayerY;
    }

    private void CheckForNPCs()
    {
        foreach (var npc in npcs)
        {
            if (npc.X == player.X && npc.Y == player.Y)
            {
                npc.Interact(player);
            }
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Game game = new Game(15, 15, TimeSpan.FromSeconds(60));

        List<Item> itemsForMerchant = new List<Item> { new Item("Зелье", 20) };
        game.AddNPC(new NPC("Торговец", 50, 5, new List<string> { "Привет! Хочешь купить зелье?", "Зелье стоит 10 монет." }, itemsForMerchant));
        game.AddNPC(new NPC("Стражник", 100, 10, new List<string> { "Стой! Куда путь?", "Пропускаю только тех, у кого есть ключ." }, new List<Item>()));

        game.Run();
    }
}