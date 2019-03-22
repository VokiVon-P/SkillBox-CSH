using System;
using System.Collections.Generic;
using CHelpers;
/*
 * Реализовать модель стоянки самолетов в виде стэка.
 * Размер стоянки - 5 мест.
 * Вводим в консоль команды “въехать на стоянку” + № борта и “покинуть стоянку”; 
 * если стоянка заполнена - очередному борту въезжать нельзя(выводим соответствующее сообщение). 
 * При выезде со стоянки печатаем номер выезжающего борта.
*/

namespace StackApp
{
    class Program
    {
        static void Main(string[] args)
        {
            const string IN_CMD = "1";//"въехать на стоянку";
            const string OUT_CMD = "2";//"покинуть стоянку";
            const string EXIT_CMD = "exit";
            const int MAX_COUNT = 5;

            // наша стоянка
            Stack<int> parking = new Stack<int>();

            // напишем инструкцию по командам!
            Console.WriteLine("============================================");
            Console.WriteLine("   Добро пожаловать на стоянку самолетов!");
            Console.WriteLine(" Вы можете использовать следующие команды: ");
            Console.WriteLine($" {IN_CMD} \t- команда запрашивающая въезд на стоянку ");
            Console.WriteLine($" {OUT_CMD} \t- команда выезда со стоянки ");
            Console.WriteLine($" {EXIT_CMD} \t\t\t- команда завершения работы ");
            Console.WriteLine("============================================");
            Console.WriteLine("");


            bool flagExit = false;
            while (!flagExit)
            {
                Console.Write("Введите команду: ");
                string ans = Console.ReadLine();
                switch (ans)
                {
                    case OUT_CMD:
                        {
                            if (parking.Count > 0)
                                Console.WriteLine($"Борт №{parking.Pop()} покинул стоянку!");
                            else
                                Console.WriteLine($"Стоянка пуста!");
                            break;
                        }
                    case IN_CMD:
                        {
                            if (parking.Count >= MAX_COUNT)
                            {
                                Console.WriteLine($"На стоянке {parking.Count} самолетов. Стоянка заполнена!");
                                break;
                            }

                            Console.Write("Введите номер борта: ");
                            // разбор ввода и проверка на корректность
                            if (int.TryParse(Console.ReadLine(), out int order_num))
                            {
                                parking.Push(order_num);
                                Console.WriteLine($"Борт {order_num} припаркован на стоянку!");
                            }
                            else
                                Console.WriteLine("Вы ввели неправильный номер бота!");

                            break;
                        }
                    case EXIT_CMD:
                        {
                            Console.WriteLine("Выход из программы");
                            flagExit = true;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Введена неверная команда!");
                            break;
                        }
                }
            }

            Console.WriteLine();
            Console.WriteLine($"На стоянке {parking.Count} самолетов");

            ConsoleHelper.KeepConsole();
        }
    }
}
