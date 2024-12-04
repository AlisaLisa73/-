using System;
using System.Collections.Generic;
using System.Diagnostics;

public class Question
{
    public string Text { get; set; }
    public string[] Answers { get; set; }
    public int CorrectAnswerIndex { get; set; }
}

public class Quiz
{
    private List<Question> questions;
    private Stopwatch stopwatch;

    public Quiz(List<Question> questions)
    {
        this.questions = questions;
        stopwatch = new Stopwatch();
    }

    public void Start()
    {
        stopwatch.Start();
        int correctAnswers = 0;
        Console.WriteLine("Начало теста!");

        foreach (var question in questions)
        {
            Console.WriteLine(question.Text);
            for (int i = 0; i < question.Answers.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {question.Answers[i]}");
            }

            Console.Write("Введите номер ответа (1-4): ");
            if (int.TryParse(Console.ReadLine(), out int answer) && answer >= 1 && answer <= 4)
            {
                if (answer - 1 == question.CorrectAnswerIndex)
                {
                    Console.WriteLine("Верно!\n");
                    correctAnswers++;
                }
                else
                {
                    Console.WriteLine($"Неверно. Правильный ответ: {question.Answers[question.CorrectAnswerIndex]}\n");
                }
            }
            else
            {
                Console.WriteLine("Неверный ввод.\n");
            }
        }
        stopwatch.Stop();
        DisplayResults(correctAnswers);

    }

    private void DisplayResults(int correctAnswers)
    {
        Console.WriteLine("Тест завершен!");
        Console.WriteLine($"Количество правильных ответов: {correctAnswers} из {questions.Count}");
        Console.WriteLine($"Время выполнения теста: {stopwatch.Elapsed}");
        if (correctAnswers >= questions.Count * 0.6) //Проходной балл - 60%
        {
            Console.WriteLine("Тест пройден!");
        }
        else
        {
            Console.WriteLine("Тест не пройден.");
        }
    }
}


public class Program
{
    public static void Main(string[] args)
    {
        List<Question> questions = new List<Question>()
        {
            new Question
            {
                Text = "1. Если значения предела функции и самой функции в данной точке равны, то функция в этой точке называется:",
                Answers = new string[] { "возрастающей", "разрывной", "непрерывной", "монотонной" },
                CorrectAnswerIndex = 2
            },
            new Question
            {
                Text = "2. Выберите правильное утверждение:",
                Answers = new string[] { "значение предела функции не единственное", "постоянный множитель нельзя выносить за знак предела", "постоянный множитель можно выносить за знак предела", "предел постоянной величины равен нулю" },
                CorrectAnswerIndex = 2
            },
            new Question
            {
                Text = "3. Действие нахождения производной функции называется:",
                Answers = new string[] { "дифференцирование", "потенцирование", "логарифмирование", "интегрирование" },
                CorrectAnswerIndex = 0
            },
            new Question
            {
                Text = "4. Действие нахождения интеграла от функции называется:",
                Answers = new string[] { "дифференцирование", "потенцирование", "интегрирование", "логарифмирование" },
                CorrectAnswerIndex = 2
            },
            new Question
            {
                Text = "5. В чем сущность физического смысла производной первого порядка?",
                Answers = new string[] { "скорость", "ускорение", "угловой коэффициент", "тангенс угла наклона" },
                CorrectAnswerIndex = 0
            },
            new Question
            {
                Text = "6. Функция может иметь в данной точке:",
                Answers = new string[] { "два предела", "множество пределов", "один предел", "несколько пределов" },
                CorrectAnswerIndex = 2
            },
            new Question
            {
                Text = "7. Продолжите предложение: Предел суммы конечного числа функций равен:",
                Answers = new string[] { "произведению значений пределов каждой функции в отдельности", "сумме пределов каждой функции в отдельности", "сумме значений производных этих функций", "не существует" },
                CorrectAnswerIndex = 1
            },
            new Question
            {
                Text = "8. Определенный интеграл – это:",
                Answers = new string[] { "число", "функция", "множество функций", "другой ответ." },
                CorrectAnswerIndex = 0
            },
            new Question
            {
                Text = "9. Функция, имеющая производную в данной точке, называется:",
                Answers = new string[] { "определенной в этой точке", "интегрируемой в этой точке", "разрывной в этой точке", "дифференцируемой в этой точке" },
                CorrectAnswerIndex = 3
            },
            new Question
            {
                Text = "10. Первообразная – это:",
                Answers = new string[] { "число", "функция", "геометрическая фигура", "другой ответ" },
                CorrectAnswerIndex = 1
            }
        };

        Quiz quiz = new Quiz(questions);
        quiz.Start();
    }
}