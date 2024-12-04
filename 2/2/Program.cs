using System;



//int[] num = { -8, 1, -3, 8, -13, 3 };

//int pol = 0;

//foreach (int number in num)
//{
//    if (number > 0)
//    {
//        pol++;
//    }
//}

//Console.WriteLine(pol);

//int[,] array = { { 8, 4, 3 }, { 7, 45, 13 }, { 1, 6, 8 } };

//int sum = 0;


//foreach (int element in array)
//{
//sum += element;
//}

//Console.WriteLine(sum);
int BinarySearch(int[] array, int n)
{
    int start = 0;
    int end = array.Length - 1;

    while (start <= end)
    {
        int middle = (start + end) / 2;

        if (array[middle] == n)
        {
            return middle;
        }
        else if (array[middle] < n)
        {
            start = middle + 1;
        }
        else
        {
            end = middle - 1;
        }
    }

    return -1;
    int[] sortedArray = { 1, 3, 5, 7, 9, 11, 13 };

    Console.Write("Введите число для поиска: ");
    int x = int.Parse(Console.ReadLine());

    int index = BinarySearch(sortedArray, x);

    if (index != -1)
    {
        Console.WriteLine("Число {0} найдено в индексе {1}", x, index);
    }
    else
    {
        Console.WriteLine("Число {0} не найдено в массиве", x);
    }
}