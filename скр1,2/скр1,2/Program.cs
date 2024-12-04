string a = Console.ReadLine();
string b = Console.ReadLine();
string resultat = "";
int ostatok = 0;
int sumSimv;
int maxLen = Math.Max(a.Length, b.Length);
a = a.PadLeft(maxLen, '0');
b = b.PadLeft(maxLen, '0');
for (int i = maxLen - 1; i >= 0; i--)
{
    sumSimv = (a[i] - '0') + (b[i] - '0') + ostatok;
    ostatok = sumSimv / 10;
    resultat = (sumSimv % 10).ToString() + resultat;
}
if (ostatok > 0)
{
    resultat = ostatok.ToString() + resultat;
}
Console.WriteLine($"Результат: {resultat}");
