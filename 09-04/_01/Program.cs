using System;
using System.Collections.Generic;
using CHelpers;

/* ===========================================================================================
 * В ДЗ про заказы интернет-магазина заменить массив на очередь.
 * Вводим в консоль команды “разместить заказ” + № заказа и “выполнить заказ”.
 * Если в очереди скопилось 10 заказов, запрещаем размещать новые заказы и выводим сообщение.
 * При выполнении выводим номер выполненного заказа.
 * ===========================================================================================
*/

namespace QueueApp
{
    class Program
    {
        
        static void Main(string[] args)
        {
            const string IN_CMD = "разместить заказ";
            const string OUT_CMD = "выполнить заказ";
            const string EXIT_CMD = "exit";
            const int MAX_COUNT = 10;

            // наша очередь заказов
            Queue<int> orders = new Queue<int>();

            // напишем инструкцию по командам!
            Console.WriteLine("============================================");
            Console.WriteLine("       Добро пожаловать в магазин!");
            Console.WriteLine(" Вы можете использовать следующие команды: ");
            Console.WriteLine($" {IN_CMD} \t- команда размещения заказа ");
            Console.WriteLine($" {OUT_CMD} \t- команда выполнения следующего заказа ");
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
                            if(orders.Count > 0)
                                Console.WriteLine($"Заказ №{orders.Dequeue()} выполнен!");
                            else
                                Console.WriteLine($"Очередь заказов пуста!");
                            break;
                        }
                    case IN_CMD:
                        {
                            if(orders.Count >= MAX_COUNT)
                            {
                                Console.WriteLine($"В обработке {orders.Count} заказов. Очередь перегружена!");
                                break;
                            }

                            Console.Write("Введите номер заказа: ");
                            // разбор ввода и проверка на корректность
                            if (int.TryParse(Console.ReadLine(), out int order_num))
                            {
                                orders.Enqueue(order_num);
                                Console.WriteLine($"Заказ {order_num} добавлен!");
                            }
                            else
                                Console.WriteLine("Вы ввели неправильный номер заказа!");

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
            Console.WriteLine($"В нашем магазине сейчас {orders.Count} заказов");

            ConsoleHelper.KeepConsole();
        }
    }
}
