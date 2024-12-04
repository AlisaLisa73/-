using System;


    Console.Write("Введите первое число: ");
    string num1 = Console.ReadLine();
    Console.Write("Введите второе число: ");
    string num2 = Console.ReadLine();

        // Определяем длины строк
    int len1 = num1.Length;
    int len2 = num2.Length;

        // Вычисляем максимальную длину
    int ml = Math.Max(len1, len2);

        // Инициализируем строку для результата и перенос
    string res = "";
    int s = 0;

        // Сложение по цифрам, начиная с конца строки
    for (int i = 0; i < ml; i++) // Изменяем порядок перебора индексов
    {
        int a1 = i < len1 ? int.Parse(num1[len1 - 1 - i].ToString()) : 0;
        int a2 = i < len2 ? int.Parse(num2[len2 - 1 - i].ToString()) : 0;
        int sum = a1 + a2 + s;
        s = sum / 10;
        res = (sum % 10).ToString() + res; 
    }

        // Добавляем перенос, если он есть
    if (s == 1)
    {
        res = "1" + res;
    }

        // Удаляем ведущие нули
    Console.WriteLine("Сумма: {0}", res.TrimStart('0'));

