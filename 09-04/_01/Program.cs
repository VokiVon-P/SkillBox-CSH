using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        // определяем перечисление статусов
        enum OrderStatus { Новый, Подтвержден, Обрабатывается, Отгружен, Доставлен };

        static void Main(string[] args)
        {
            // наша очередь заказов
            Queue<OrderStatus> orders = new Queue<OrderStatus>();

            Console.WriteLine($"В нашем магазине сейчас {orders.Count} заказов");

            // заполняем массив случайными значениями из нашего перечисления1
            Random rnd = new Random();
            // запоминаем кол-во элементов в enum
            int lengthEnum = Enum.GetValues(typeof(OrderStatus)).Length;
            while (orders.Count < 10)
            {
                OrderStatus item = (OrderStatus)rnd.Next(lengthEnum);
                orders.Enqueue(item);
                Console.WriteLine($"{item}");
            }
            //for(int i =0; i< orders.Length; i++)
            //{
            //    orders[i] = (OrderStatus)rnd.Next(lengthEnum);
            //    //проверка содержимого массива - индекс увеличиваем для человеческого восприятия


            //}
            Console.WriteLine();
            Console.WriteLine($"В нашем магазине сейчас {orders.Count} заказов");
            Console.WriteLine("Введите номер заказа чтобы узнать его статус:");

            // разбор ввода и проверка на корректность
            //string  inputString = Console.ReadLine();
            //var success = int.TryParse(inputString, out int idx);
            //bool result = success && idx > 0 && idx <= orders.Length;

            //if (result)
            //{
            //    // вывод значения с поправкой на подсчет заказов с 1
            //    Console.WriteLine("Статус вашего заказа - {0}", orders[idx-1]);
            //}
            //else
            //    Console.WriteLine("Вы ввели не правильный номер заказа!");


            ConsoleHelper.KeepConsole();
        }
    }
}
