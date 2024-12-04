using System;

class BullsAndCows
{
    static void Main(string[] args)
    {

        string secretNumber = GenerateSecretNumber();

        Console.WriteLine("Игра \"Быки и коровы\"!");
        Console.WriteLine("Попробуйте угадать четырехзначное число, все цифры которого различны.");

        int attempts = 0;
        while (true)
        {
            attempts++;
            string guess = Console.ReadLine();

            if (guess.Length != 4 || !IsUniqueDigits(guess))
            {
                Console.WriteLine("Неверный ввод. Пожалуйста, введите четырехзначное число с различными цифрами.");
                continue;
            }

            int bulls = CountBulls(secretNumber, guess);
            int cows = CountCows(secretNumber, guess);

            Console.WriteLine($"{bulls} быков и {cows} коров.");

            if (bulls == 4)
            {
                Console.WriteLine($"Поздравляю! Вы угадали за {attempts} попыток.");
                break;
            }
        }
    }

    static string GenerateSecretNumber()
    {
        Random random = new Random();
        string number = "";
        while (number.Length < 4)
        {
            int digit = random.Next(0, 10);
            if (!number.Contains(digit.ToString()))
            {
                number += digit;
            }
        }
        return number;
    }
    static bool IsUniqueDigits(string str)
    {
        for (int i = 0; i < str.Length; i++)
        {
            for (int j = i + 1; j < str.Length; j++)
            {
                if (str[i] == str[j])
                {
                    return false;
                }
            }
        }
        return true;
    }

    static int CountBulls(string secretNumber, string guess)
    {
        int bulls = 0;
        for (int i = 0; i < 4; i++)
        {
            if (secretNumber[i] == guess[i])
            {
                bulls++;
            }
        }
        return bulls;
    }

    static int CountCows(string secretNumber, string guess)
    {
        int cows = 0;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (i != j && secretNumber[i] == guess[j])
                {
                    cows++;
                }
            }
        }
        return cows;
    }
}