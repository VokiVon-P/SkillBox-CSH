using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Linq;
using TweetSharp;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace TwitterConsole
{
    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="Exception"></exception>
        static void Main()
        {
            #region OLD_init
            /*
                        TwitterService InitTwitter()
                        {
                            string testtxt = "OK";
                            try
                            {
                                // ключи хранятся в специальном файле для исключения из Git
                                var serv = new TwitterService(TwKeys.ConsumerKey, TwKeys.ConsumerSecret);

                                var requestToken = serv.GetRequestToken();
                                if (serv.Response.StatusCode != HttpStatusCode.OK)
                                {
                                    testtxt = $"Ошибка при запросе токена безопасности: GetRequestToken() - {serv.Response.StatusDescription}";
                                    throw new Exception(testtxt);
                                }

                                try
                                {
                                    var uri = serv.GetAuthorizationUri(requestToken);
                                    Process.Start(uri.ToString());

                                    Console.Write("Введите код авторизации: ");
                                    var verifier = Console.ReadLine();

                                    var access = serv.GetAccessToken(requestToken, verifier);
                                    if (access.UserId == 0) throw new Exception($"{access} : {access.UserId}");

                                    serv.AuthenticateWith(access.Token, access.TokenSecret);
                                }
                                catch (Exception e)
                                {

                                    testtxt = $"Ошибка при аутентификации: Возможно введен неверный ответный ключ - {e}";
                                    throw new Exception(testtxt);
                                }


                                return serv;
                            }
                            finally
                            {
                                // вывод тестовой информации о  инициализации
                                // вне зависимости от результата 
                                Console.WriteLine($"Инициализация сервиса Twitter: {testtxt}");
                            }

                        }
            */
            #endregion
            // защищаем вызовы интернет сервисов и от некорректного ввода ключа
            try
            {
                
                List<TwittPost> tweets;
               
                try
                {
                    if (TwitterHelper.Init())
                    {
                        tweets = TwitterHelper.GetTweets();
                        foreach(var post in tweets)
                        {
                            Console.WriteLine($"\n--- {post.AuthorNick} \n {post.AuthorName} ");
                            Console.WriteLine($"--- {post.Text} ");
                        }

                        /// <remarks>
                        /// Сериализция XML
                        /// </remarks>
                        
                        var serializer = new XmlSerializer(typeof(List<TwittPost>));
                        using (var fs = new FileStream("Posts.xml", FileMode.OpenOrCreate))
                        {
                            serializer.Serialize(fs, tweets);
                        }

                        /// <remarks>
                        /// Сериализция JSON
                        /// </remarks>
                        var jsonSerializer = JsonSerializer.CreateDefault();
                        using (var fs = File.CreateText("Posts.json"))
                        
                        {
                            jsonSerializer.Serialize(fs, tweets);
                        }


                    }
                }
                catch (Exception e)
                {
                    
                    throw new TwitterException($"Ошибка инициализации связи с сервисом Твиттер : {e}");
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
            }
            finally
            {

                Console.WriteLine("\nНажмите любую клавишу для выхода из приложения");
                Console.ReadKey();
            }
            
            
        }

        



    }
}
