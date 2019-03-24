using System;
using System.Collections.Generic;
using CHelpers;


/*
 * Урок 5. Смоделировать регистрацию в некотором сервисе с помощью Dictionary – консольное приложение,
 * которое предлагает ввести учетную запись (логин).
 * Если логин найден – выводит на экран “Здравствуйте, &lt;имя&gt;”,
 * если нет – предлагает зарегистрироваться (логин и имя).
 * Отдельная команда выводит на экран имена всех пользователей.
 *
 */

namespace _09_05
{
    class Program
    {
        static void Main(string[] args)
        {
            const string ALL_CMD = "all";//"все пользователи";
            const string EXIT_CMD = "exit";

            // наш словарь
            Dictionary<string, string> logins = new Dictionary<string, string>();
           

            // напишем инструкцию по командам!
            Console.WriteLine("============================================");
            Console.WriteLine("   Добро пожаловать в сервис!");
            Console.WriteLine(" Вы можете использовать следующие команды: ");
            Console.WriteLine($" {ALL_CMD} \t- вывести список всех пользователей сервиса ");
            Console.WriteLine($" {EXIT_CMD} \t- команда завершения работы ");
            Console.WriteLine(" остальные введенные слова будут использоваться как логин!");
            Console.WriteLine("============================================");
            Console.WriteLine("");


            bool flagExit = false;
            while (!flagExit)
            {
                Console.Write("Введите логин: ");
                string ans = Console.ReadLine();
                if (ans is null)
                {
                    Console.WriteLine("Введен неправильный логин!");
                }

                switch (ans)
                {
                    case EXIT_CMD:
                        {
                            Console.WriteLine("Выход из программы");
                            flagExit = true;
                            break;
                        }
                    case ALL_CMD:
                        {
                            Console.WriteLine("Список пользователей сервиса: ");
                            foreach (var item in logins)
                            {
                                Console.WriteLine($"login: {item.Key}  \t- имя: {item.Value}");
                            }
                            break;
                        }
                    default:
                        {
                            if (logins.ContainsKey(ans ?? throw new InvalidOperationException()))
                            {
                                Console.WriteLine($"Здравствуйте {logins[ans]}");
                            }
                            else
                            {
                                Console.WriteLine("Необходима регистация!");
                                Console.Write("Введите имя: ");
                                string usrName = Console.ReadLine();
                                logins[ans] = usrName;
                                Console.WriteLine($"Зарегистрирован пользователь {ans} - имя: {usrName}");
                            }

                            break;
                        }
                }
            }

            ConsoleHelper.KeepConsole();
        }
    }
}

