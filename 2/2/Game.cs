using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2
{
    internal class Game
    {
        string[,] _field = new string[10,10];
        int playerX = 5;
        int playerY = 5;
        public Game()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    _field[i, j] = "#";
                }
            }
            _field[5, 5] = "C";
        }
        public void Swap(int i1, int i2, int j1, int j2)
        {
            string temp = _field[i1, j1];
            _field[i1, j1] = _field[i2, j2];
            _field[i2, j2] = temp;
        }
        public void Print()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.Write(_field[i, j]+" ");
                }
                Console.WriteLine();
        }
    }

    public void Run()
    { 
        while(true)
            {
            var key = Console.ReadKey();

            switch(key.Key)
            {
                case ConsoleKey.LeftArrow:
                    Console.WriteLine("ИДУ НАЛЕВО!");
                    break;
                case ConsoleKey.RightArrow:
                    Console.WriteLine("ИДУ НАПРАВО!");
                    break;
                case ConsoleKey.UpArrow:
                    Console.WriteLine("ИДУ ВВЕРХ!");
                    break;
                case ConsoleKey.DownArrow:
                    Console.WriteLine("ИДУ ВНИЗ!");
                    break;
            }
        }
