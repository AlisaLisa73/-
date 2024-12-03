using System;

public class Game
{
    private const int Width = 10;
    private const int Height = 10;

    private enum TileType { Grass, Stone, Tree, PlateInactive, PlateActive, Player }

    private TileType[,] gameBoard;
    private int playerX, playerY;
    private int platesToActivate;

    public Game(char[,] level)
    {
        gameBoard = new TileType[Height, Width];
        platesToActivate = 0;

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                switch (level[y, x])
                {
                    case '#': gameBoard[y, x] = TileType.Grass; break;
                    case 'R': gameBoard[y, x] = TileType.Stone; break;
                    case 'T': gameBoard[y, x] = TileType.Tree; break;
                    case 'O': gameBoard[y, x] = TileType.PlateInactive; platesToActivate++; break;
                    case 'C': gameBoard[y, x] = TileType.Player; playerX = x; playerY = y; break;
                }
            }
        }
    }

    public bool Play()
    {
        while (true)
        {
            DrawBoard();

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            int newX = playerX;
            int newY = playerY;

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow: newY--; break;
                case ConsoleKey.DownArrow: newY++; break;
                case ConsoleKey.LeftArrow: newX--; break;
                case ConsoleKey.RightArrow: newX++; break;
            }

            if (TryMove(newX, newY))
            {
                if (CheckWinCondition()) return true;
            }
        }
    }

    private bool TryMove(int newX, int newY)
    {
        if (newX < 0 || newX >= Width || newY < 0 || newY >= Height) return false;

        if (gameBoard[newY, newX] == TileType.Stone)
        {
            return TryPushStone(newX, newY);
        }
        else if (gameBoard[newY, newX] != TileType.Tree)
        {
            MovePlayer(newX, newY);
            return true;
        }
        return false;
    }

    private bool TryPushStone(int newX, int newY)
    {
        int pushX = newX + (newX - playerX);
        int pushY = newY + (newY - playerY);

        if (pushX < 0 || pushX >= Width || pushY < 0 || pushY >= Height || gameBoard[pushY, pushX] == TileType.Stone || gameBoard[pushY, pushX] == TileType.Tree)
        {
            return false;
        }

        gameBoard[newY, newX] = TileType.Player;
        gameBoard[playerY, playerX] = TileType.Grass;
        gameBoard[pushY, pushX] = TileType.Stone;
        playerX = newX;
        playerY = newY;
        return true;
    }

    private void MovePlayer(int newX, int newY)
    {
        gameBoard[newY, newX] = TileType.Player;
        gameBoard[playerY, playerX] = TileType.Grass;
        playerX = newX;
        playerY = newY;
        ActivatePlateIfNeeded(newX, newY);
    }


    private void ActivatePlateIfNeeded(int x, int y)
    {
        if (gameBoard[y, x] == TileType.PlateInactive)
        {
            gameBoard[y, x] = TileType.PlateActive;
        }
    }

    private bool CheckWinCondition()
    {
        int activatedPlates = 0;
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                if (gameBoard[y, x] == TileType.PlateActive)
                    activatedPlates++;
            }
        }
        return activatedPlates == platesToActivate;
    }


    private void DrawBoard()
    {
        Console.Clear();
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                Console.ForegroundColor = GetTileColor(gameBoard[y, x]);
                Console.Write(GetTileSymbol(gameBoard[y, x]));
                Console.ResetColor();
            }
            Console.WriteLine();
        }
    }


    private ConsoleColor GetTileColor(TileType type)
    {
        switch (type)
        {
            case TileType.Grass: return ConsoleColor.Green;
            case TileType.Stone: return ConsoleColor.DarkGray;
            case TileType.Tree: return ConsoleColor.DarkGreen;
            case TileType.PlateInactive: return ConsoleColor.DarkYellow;
            case TileType.PlateActive: return ConsoleColor.Yellow;
            case TileType.Player: return ConsoleColor.White;
            default: return ConsoleColor.White;
        }
    }

    private char GetTileSymbol(TileType type)
    {
        switch (type)
        {
            case TileType.Grass: return '#';
            case TileType.Stone: return 'R';
            case TileType.Tree: return 'T';
            case TileType.PlateInactive: return 'O';
            case TileType.PlateActive: return 'Ⓡ';
            case TileType.Player: return 'C';
            default: return ' ';
        }
    }



    public static void Main(string[] args)
    {
        //Example Level
        char[,] level1 = {
            {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
            {'#', 'R', '#', '#', '#', 'O', '#', '#', '#', '#'},
            {'#', '#', '#', '#', '#', '#', '#', '#', 'O', '#'},
            {'#', '#', '#', '#', '#', 'T', '#', '#', '#', '#'},
            {'#', '#', '#', 'O', '#', '#', '#', '#', '#', '#'},
            {'#', '#', '#', '#', '#', '#', '#', 'R', '#', '#'},
            {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
            {'#', 'O', '#', '#', '#', '#', '#', '#', '#', '#'},
            {'#', '#', '#', '#', '#', '#', '#', '#', 'C', '#'},
            {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#'}
        };

        Game game = new Game(level1);
        if (game.Play()) Console.WriteLine("Вы выиграли!");
    }
}
