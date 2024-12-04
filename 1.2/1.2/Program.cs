using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Делимое (a): ");
        int a = int.Parse(Console.ReadLine());
        Console.Write("Делитель (b): ");
        int b = int.Parse(Console.ReadLine());

        if (b == 0)
        {
            Console.WriteLine("На 0 делить нельзя");
            return;
        }

        int ch = 0; 
        int os= a; 

        if (a < 0)
        {
            a = -a;
            os = -os;
        }

        if (b < 0)
        {
            b = -b;
        }

        while (os >= b)
        {
            os -= b;
            ch++;
        }

        if ((a < 0 && b > 0) || (a > 0 && b < 0))
        {
            ch = -ch;
        }

        Console.WriteLine("Неполное частное: {0}", ch);
    }
}