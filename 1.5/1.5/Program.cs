using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Длины сторон 1 треугольника:");
        Console.Write("a1: ");
        double a1 = double.Parse(Console.ReadLine());
        Console.Write("b1: ");
        double b1 = double.Parse(Console.ReadLine());
        Console.Write("c1: ");
        double c1 = double.Parse(Console.ReadLine());

        Console.WriteLine("Длины сторон 2 треугольника:");
        Console.Write("a2: ");
        double a2 = double.Parse(Console.ReadLine());
        Console.Write("b2: ");
        double b2 = double.Parse(Console.ReadLine());
        Console.Write("c2: ");
        double c2 = double.Parse(Console.ReadLine());

        bool isSimilar = (a1 / a2 == b1 / b2 && a1 / a2 == c1 / c2) ||
                        (a1 / b2 == b1 / a2 && a1 / b2 == c1 / c2) ||
                        (a1 / c2 == b1 / a2 && a1 / c2 == c1 / b2);

        Console.WriteLine(isSimilar ? "да" : "нет");
    }
}
