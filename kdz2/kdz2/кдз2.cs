using System;

public class Game
{
    private char[,] board;
    private char currentPlayer;
    private bool isGameOver;

    public Game()
    {
        board = new char[3, 3];
        currentPlayer = 'X';
        isGameOver = false;
        InitializeBoard();
    }

    private void InitializeBoard()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                board[i, j] = ' ';
            }
        }
    }

    private void PrintBoard()
    {
        Console.WriteLine("-------------");
        for (int i = 0; i < 3; i++)
        {
            Console.Write("| ");
            for (int j = 0; j < 3; j++)
            {
                Console.Write(board[i, j] == ' ' ? "  | " : $"{board[i, j]} | ");
            }
            Console.WriteLine("\n-------------");
        }
    }

    private bool IsValidMove(int cellNumber)
    {
        if (cellNumber < 1 || cellNumber > 9) return false;
        int row = (cellNumber - 1) / 3;
        int col = (cellNumber - 1) % 3;
        return board[row, col] == ' ';
    }

    private bool CheckWin()
    {
        // Check rows, columns, and diagonals (same as before)
        for (int i = 0; i < 3; i++)
        {
            if (board[i, 0] != ' ' && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2]) return true;
            if (board[0, i] != ' ' && board[0, i] == board[1, i] && board[1, i] == board[2, i]) return true;
        }
        if (board[0, 0] != ' ' && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2]) return true;
        if (board[0, 2] != ' ' && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0]) return true;
        return false;
    }

    private bool IsBoardFull()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] == ' ') return false;
            }
        }
        return true;
    }

    private void MakeMove(int cellNumber)
    {
        int row = (cellNumber - 1) / 3;
        int col = (cellNumber - 1) % 3;
        board[row, col] = currentPlayer;
    }

    private void SwitchPlayer()
    {
        currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
    }

    //Simple AI (random move)
    private void ComputerMove()
    {
        Random random = new Random();
        int cellNumber;
        do
        {
            cellNumber = random.Next(1, 10);
        } while (!IsValidMove(cellNumber));
        MakeMove(cellNumber);
    }

    public void PlayGame(bool vsComputer)
    {
        while (!isGameOver)
        {
            PrintBoard();
            Console.WriteLine($"Ход игрока {currentPlayer}");

            int move;
            if (currentPlayer == 'O' && vsComputer)
            {
                ComputerMove();
            }
            else
            {
                do
                {
                    Console.Write("Введите номер клетки (1-9): ");
                    if (int.TryParse(Console.ReadLine(), out move) && IsValidMove(move))
                    {
                        MakeMove(move);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Неверный ход. Попробуйте ещё раз.");
                    }
                } while (true);

            }


            if (CheckWin())
            {
                PrintBoard();
                Console.WriteLine($"Игрок {currentPlayer} победил!");
                isGameOver = true;
            }
            else if (IsBoardFull())
            {
                PrintBoard();
                Console.WriteLine("Ничья!");
                isGameOver = true;
            }
            else
            {
                SwitchPlayer();
            }
        }
    }

    public static void Main(string[] args)
    {
        Game game = new Game();
        Console.WriteLine("Выберите режим игры:");
        Console.WriteLine("1. Игрок против игрока");
        Console.WriteLine("2. Игрок против компьютера");
        int choice = int.Parse(Console.ReadLine());
        game.PlayGame(choice == 2);
    }
}