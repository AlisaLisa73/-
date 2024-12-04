using System;

class Program
{
    static void Main(string[] args)
    {

        Console.Write("Введите размерность матрицы (нечетное число): ");
        int N = int.Parse(Console.ReadLine());


        if (N % 2 == 0)
        {
            Console.WriteLine("Размерность матрицы должна быть нечетным числом.");
            return;
        }


        int[,] matrix = new int[N, N];

        Random random = new Random();
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                matrix[i, j] = random.Next(1, 100);
            }
        }

        Console.WriteLine("\nЭлементы по спирали, начиная с центра:");
        PrintSpiral(matrix);
    }

    static void PrintSpiral(int[,] matrix)
    {
        int N = matrix.GetLength(0);
        int center = N / 2;
        int row = center;
        int col = center;
        int direction = 0; 
        bool[,] visited = new bool[N, N];
        visited[row, col] = true;
        Console.Write(matrix[row, col] + " ");

        int stepsMade = 0;
        int layer = 1;

        while (true)
        {
            while (true)
            {
                bool canMove = false;
                switch (direction)
                {
                    case 0:
                        if (col < N - layer && !visited[row, col + 1])
                        {
                            canMove = true;
                            col++;
                        }
                        break;
                    case 1:
                        if (row > layer - 1 && !visited[row - 1, col])
                        {
                            canMove = true;
                            row--;
                        }
                        break;
                    case 2:
                        if (col > layer - 1 && !visited[row, col - 1])
                        {
                            canMove = true;
                            col--;
                        }
                        break;
                    case 3:
                        if (row < N - layer && !visited[row + 1, col])
                        {
                            canMove = true;
                            row++;
                        }
                        break;
                }

                if (canMove)
                {
                    visited[row, col] = true;
                    Console.Write(matrix[row, col] + " ");
                    stepsMade++;
                }
                else
                {
                    break;
                }
            }

            direction = (direction + 1) % 4;

            if (stepsMade == 2 * (N - layer * 2))
            {
                stepsMade = 0;
                layer++;
            }

            bool allVisited = true;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (!visited[i, j])
                    {
                        allVisited = false;
                        break;
                    }
                }
                if (!allVisited) break;
            }

            if (allVisited) break;
        }
    }
}