using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Коэффициент a: ");
        double a = double.Parse(Console.ReadLine());
        Console.Write("Коэффициент b: ");
        double b = double.Parse(Console.ReadLine());
        Console.Write("Коэффициент c: ");
        double c = double.Parse(Console.ReadLine());
        if (a == 0)
        {
            if (b == 0)
            {
                Console.WriteLine("Бесконечно много корней");
            }
            else
            {
                Console.WriteLine("1 корень: x = {0}", -c / b);
            }
            return;
        }

        double d = b * b - 4 * a * c;

        if (d > 0)
        {
            Console.WriteLine("2 корня:");
            Console.WriteLine("x1 = {0}", (-b + Math.Sqrt(d)) / (2 * a));
            Console.WriteLine("x2 = {0}", (-b - Math.Sqrt(d)) / (2 * a));
        }
        else if (d == 0)
        {
            Console.WriteLine("1 корень:");
            Console.WriteLine("x = {0}", -b / (2 * a));
        }
        else
        {
            Console.WriteLine("Нет корней");
        }
    }
}