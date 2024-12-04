using System;

    Console.Write("Введите число в диапозоне [-1;1]: ");
    float x = float.Parse(Console.ReadLine());

    if (x < -1 || x > 1)
    {
        Console.WriteLine("диапазон [-1;1]");
        return;
    }

    float e = 1;
    float с = 1;
    int f = 1;
    int i = 1;

    while (Math.Abs(с) > 0.000001)
    {
        с *= x / i;
        f *= i;
        e += с;
        i++;
    }

    Console.WriteLine("e^{0} ≈ {1}", x, e);
