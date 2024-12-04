using System;

public class Paint
{
    public static void Main(string[] args)
    {
        // Выбор игрока
        int playerChoice;
        do
        {
            Console.WriteLine("Выберите дверь: 1 , 2 ");
        } while (!int.TryParse(Console.ReadLine(), out playerChoice) || (playerChoice != 1 && playerChoice != 2));


        // Случайный выбор врага
        Random random = new Random();
        int enemyAction = random.Next(1, 3); // 1 - битва, 2 - разговор

        if (playerChoice == enemyAction)
        {
            if (playerChoice == 1)
            {
                Console.WriteLine("Я тебя убью!");

            }
            else
            {
                Console.WriteLine("Поговорим?");
                // Здесь можно добавить разговора
            }
        }

        Console.ReadKey();
    }
}
