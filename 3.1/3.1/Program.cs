using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите первое число: ");
        int a1 = Convert.ToInt32(Console.ReadLine());

        Console.Write("Введите второе число: ");
        int a2 = Convert.ToInt32(Console.ReadLine());

        string b1 = ToTwosComplement(a1);
        string b2 = ToTwosComplement(a2);

        Console.WriteLine($"Первое число в двоичном представлении: {b1}");
        Console.WriteLine($"Второе число в двоичном представлении: {b2}");

        int sum = a1 + a2;

        string bsum = ToTwosComplement(sum);
        Console.WriteLine($"сумма в двоичном коде: {bsum}");

        Console.WriteLine($"обычная сумма: {sum}");
    }

    static string ToTwosComplement(int num)
    {
        const int bits = 8;
        string bin = Convert.ToString(Math.Abs(num), 2).PadLeft(bits, '0');

        if (num < 0)
        {
            bin = new string(bin.Select(c => c == '0' ? '1' : '0').ToArray());

            int c = 1;
            char[] res = new char[bits];
            for (int i = bits - 1; i >= 0; i--)
            {
                if (bin[i] == '1' && c == 1)
                {
                    res[i] = '0';
                }
                else if (bin[i] == '0' && c == 1)
                {
                    res[i] = '1';
                    c = 0;
                }
                else
                {
                    res[i] = bin[i];
                }
            }

            bin = new string(res);
        }

        return bin;
    }
}