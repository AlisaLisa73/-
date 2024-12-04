using System;
    Console.Write("Введите строку S: ");
    string S = Console.ReadLine();
    Console.Write("Введите подстроку S1: ");
    string S1 = Console.ReadLine();
    int cnt = CountOccurrences(S, S1);
    Console.WriteLine("Количество вхождений S1 в S: {0}", cnt);

    static int CountOccurrences(string text, string a)
{
    int cnt = 0;
    int i = 0;

    while ((i = text.IndexOf(a, i)) != -1)
    {
        cnt++;
        i += a.Length; 
    }

    return cnt;
}