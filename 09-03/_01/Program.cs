using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Linq;
using TweetSharp;
using System.Collections.Generic;
using System.Globalization;
using System.Net;

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

            // защищаем вызовы интернет сервисов и от некорректного ввода ключа
            try
            {
                TwitterService service;
                List<TwitterStatus> tweets;
                List<string> trendsList = new List<string>();

                #region InitTwitter
                try
                {
                    service = InitTwitter();

                    tweets = service.ListTweetsOnHomeTimeline(new ListTweetsOnHomeTimelineOptions()).ToList();
                }
                catch (Exception e)
                {
                    
                    throw new TwitterException($"Ошибка инициализации связи с сервисом Твиттер : {e}");
                }

                #endregion

                #region Tweets
                /* Блок твиттов пока не нужен
                
                // проверка на наличие 15 твиттов в ленте
                int countTweets = tweets.Count <= 15 ? tweets.Count - 1 : 14;

                for (int i = countTweets; i >= 0; i--)
                {
                    Console.WriteLine("-------------------{0:D2}-------------------", i);
                    string tweetTxt = tweets[i].Text;
                    Console.WriteLine(tweetTxt);

                    #region REGEX

                    // поиск всех хэш тегов в твитте и вывод их под твиттом
                    const string pattern = "#[\\w]+";
                    // поиск упоминаний авторов, для проверки, тк хэштегов у меня в ленте часто нет совсем
                    //const string pattern = "@[\\w]+";


                    var matches = Regex.Matches(tweetTxt, pattern);
                    foreach (var match in matches)
                    {
                        Console.WriteLine(match);
                    }

                    #endregion

                    // вычисляем возраст поста
                    TimeSpan ageTw = DateTime.Now.Subtract(tweets[i].CreatedDate);
                    // две версии вывода возраста поста
                    //Console.WriteLine("С момента публикации прошло {0} дней {1} часов {2} минут и {3} секунд", ageTW.Days, ageTW.Hours, ageTW.Minutes, ageTW.Seconds);
                    Console.WriteLine("С момента публикации прошло {0:dd\\d\\-hh\\:mm\\:ss}", ageTw);

                    Console.WriteLine("----------------------------------------\n");
                }
                */
                #endregion
                // TwitterTrends trends;
                // защищаем обращение к сервису твиттера и заполнении трендов
                try
                {
                    TwitterTrends trends = service.ListLocalTrendsFor(new ListLocalTrendsForOptions() {Id = 1}) 
                                 ?? throw new ArgumentNullException("service.ListLocalTrendsFor(new ListLocalTrendsForOptions() {Id = 1})"); // 1 - весь мир
                    foreach (var twitterTrend in trends) trendsList.Add($"{twitterTrend.Name}");
                    trendsList.Sort();
                }
                catch (Exception e)
                {
                    throw new TwitterException("Ошибка в запросе трендов Твиттера :" + e.Message);
                }

                #region oldTrensOut

                /* предыдущая версия вывода
                var builder = new StringBuilder();
                foreach (var trend in trends)
                {
                    string trendName = trend.Name;
                    if (trendName.StartsWith("#"))
                    {
                        if (builder.Length > 0) builder.Append(", ");
                        builder.Append(trendName);
                    }

                    Console.WriteLine(trend.Name);
                    Console.WriteLine();
                }
                Console.WriteLine("Строка с трендами на #:");
                Console.WriteLine(builder);
                */

                #endregion


                Console.WriteLine($"--- {trendsList.Count} отсортированных трендов моего Твиттера:");
                foreach (var trand in trendsList)
                {
                    Console.WriteLine(trand);
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
