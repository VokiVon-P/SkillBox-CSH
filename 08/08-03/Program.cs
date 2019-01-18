using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Linq;
using TweetSharp;
using System.Collections.Generic;

namespace TwitterConsole
{
    class Program
    {
        

        static void Main()
        {
            

            // поиск всех хэш тегов в твитте и вывод их под твиттом
            const string pattern = "#[\\w]+";
            // поиск упоминаний авторов, для проверки, тк хэштегов у меня в ленте часто нет совсем
            //const string pattern = "@[\\w]+";

            TwitterService service;
            List<TwitterStatus> tweets;

            // защищаем вызовы интернет сервисов и от некорректного ввода ключа
            try
            {

                // ключи хранятся в специальном файле для исключения из Git
                service = new TwitterService(TwKeys.consumerKey, TwKeys.consumerSecret);

                var requestToken = service.GetRequestToken();

                var uri = service.GetAuthorizationUri(requestToken);
                Process.Start(uri.ToString());

                var verifier = Console.ReadLine();
                var access = service.GetAccessToken(requestToken, verifier);

                service.AuthenticateWith(access.Token, access.TokenSecret);

                tweets = service.ListTweetsOnHomeTimeline(new ListTweetsOnHomeTimelineOptions()).ToList();
            }
            catch(Exception e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
                throw new TwitterException( "Ошибка инициализации связи с сервисом Твиттер");
            }
            


            // проверка на наличие 15 твиттов в ленте
            int countTweets = tweets.Count <= 15 ? tweets.Count-1 : 14;
            
            for (int i = countTweets; i >= 0; i--)
            {
                Console.WriteLine("-------------------{0:D2}-------------------",i);
                string tweetTxt = tweets[i].Text;
                Console.WriteLine(tweetTxt);

                #region REGEX
                
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
            TwitterTrends trends;
            // защищаем обращение к сервису твиттера
            try
            {
                trends = service.ListLocalTrendsFor(new ListLocalTrendsForOptions() { Id = 1 }); // 1 - весь мир
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
                throw new TwitterException("Ошибка в запросе трендов Твиттера");
            }
            
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
            Console.ReadKey();
        }
    }
}
