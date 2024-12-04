using System;

    Console.Write("Введите значение x: ");
    int x = int.Parse(Console.ReadLine());
    int x2 = x * x;
    int res = (x2+1)*(x2+x)+ 1;

    Console.WriteLine("Результат: {0}", res);