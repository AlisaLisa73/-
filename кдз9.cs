using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

public class Card
{
    public char Symbol { get; set; }
    public bool IsRevealed { get; set; } = false;
    public int Row { get; set; }
    public int Col { get; set; }
}

public class MemoryGame
{
    private int size;
    private Card[,] grid;
    private List<char> symbols;
    private Stopwatch stopwatch;
    private Card firstRevealedCard = null; // Changed to Card object
    private int moves = 0;

    public MemoryGame(int size)
    {
        this.size = size;
        this.grid = new Card[size, size];
        this.symbols = Enumerable.Range(0, (size * size) / 2).Select(i => (char)('A' + i)).ToList();

        InitializeGrid();
        stopwatch = new Stopwatch();
    }

    private void InitializeGrid()
    {
        List<Card> cards = new List<Card>();
        foreach (char symbol in symbols)
        {
            cards.Add(new Card { Symbol = symbol });
            cards.Add(new Card { Symbol = symbol });
        }

        Random random = new Random();
        cards = cards.OrderBy(x => random.Next()).ToList();

        int k = 0;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                grid[i, j] = cards[k];
                grid[i, j].Row = i;
                grid[i, j].Col = j;
                k++;
            }
        }
    }

    public void Start()
    {
        stopwatch.Start();
        while (!CheckWin())
        {
            Console.Clear();
            PrintGrid();
            SelectCard();
        }
        stopwatch.Stop();
        Console.WriteLine($"Поздравляем! Вы выиграли за {stopwatch.Elapsed}");
        Console.WriteLine($"Количество ходов: {moves}");
    }

    private void PrintGrid()
    {
        Console.WriteLine("  " + string.Join(" ", Enumerable.Range(0, size).Select(x => x.ToString().PadLeft(2))));
        for (int i = 0; i < size; i++)
        {
            Console.Write(i.ToString().PadLeft(2) + " ");
            for (int j = 0; j < size; j++)
            {
                Console.Write((grid[i, j].IsRevealed ? grid[i, j].Symbol.ToString() : "#").PadLeft(3));
            }
            Console.WriteLine();
        }
    }

    private void SelectCard()
    {
        Console.Write("Введите координаты карты (строка столбец) через пробел: ");
        string input = Console.ReadLine();

        if (int.TryParse(input.Split(' ')[0], out int row) && int.TryParse(input.Split(' ')[1], out int col) &&
            row >= 0 && row < size && col >= 0 && col < size && !grid[row, col].IsRevealed)
        {
            moves++;
            grid[row, col].IsRevealed = true;
            if (firstRevealedCard == null)
            {
                firstRevealedCard = grid[row, col];
            }
            else
            {
                HandleSecondCard(grid[row, col]);
                firstRevealedCard = null; // Reset for the next turn
            }
            PrintGrid();
        }
        else
        {
            Console.WriteLine("Некорректный ввод координат!");
        }
    }

    private void HandleSecondCard(Card secondCard)
    {
        if (firstRevealedCard.Symbol == secondCard.Symbol)
        {
            Console.WriteLine("Совпадение!");
        }
        else
        {
            Console.WriteLine("Не совпадение!");
            Thread.Sleep(1000);
            firstRevealedCard.IsRevealed = false;
            secondCard.IsRevealed = false;
        }
    }

    private bool CheckWin()
    {
        return grid.Cast<Card>().All(card => card.IsRevealed);
    }

    public static void Main(string[] args)
    {
        Console.Write("Введите размер сетки (2n): ");
        if (int.TryParse(Console.ReadLine(), out int size) && size % 2 == 0 && size >= 2)
        {
            MemoryGame game = new MemoryGame(size);
            game.Start();
        }
        else
        {
            Console.WriteLine("Некорректный размер сетки!");
        }
    }
}