using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Число 1: ");
        int n1 = int.Parse(Console.ReadLine());
        Console.Write("Число 2: ");
        int n2 = int.Parse(Console.ReadLine());
        Console.Write("Число 3: ");
        int n3 = int.Parse(Console.ReadLine());

        int rez = 0;
        int cnt = 0; 

        for (int i = 0; i < 32; i++)
        {
            if (((n1 >> i) & 1) == 1)
            {
                cnt++;
            }
            if (((n2 >> i) & 1) == 1)
            {
                cnt++;
            }
            if (((n3 >> i) & 1) == 1)
            {
                cnt++;
            }
            if (cnt > 1)
            {
                rez |= (1 << i);
            }

            cnt = 0; 
        }

        Console.WriteLine("Новое число: {0}", rez);
    }
}