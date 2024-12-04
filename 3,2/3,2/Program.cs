using System;

    Console.Write("Введите радиус окружности: ");
    int r = int.Parse(Console.ReadLine());

    for (int y = -r; y <= r; y++)
    {
        for (int x = -r; x <= r; x++)
        {
           double d = Math.Sqrt(x * x + y * y);

            if (Math.Abs(d - r) < 1)
            {
                Console.Write("*");
            }
            else
            {
                Console.Write(" ");
            }
        }
        Console.WriteLine(); 
    }
