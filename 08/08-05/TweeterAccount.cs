using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    // класс для аккаунта пользователя в системе Твиттер
    // основное неизменное поле NickName вида: @Pupkin
    
    class TweeterAccount
    {
        // заглушки для теста
        private const string nick = "@JIRO";
        private const string fname = "Массажист осьминогов";

        // внутренние поля
        private string username;
        private string password;

        private string email;
        private string language;
        private AccountProfile profile;

        private readonly string nickname;

        // отсортированный по времени список твиттов
        // private TweetList tweets;
                
        public string PublicName => profile.FullName;
        public string Nickname => nickname;

        public TweeterAccount()
        {
            nickname = nick;
            profile = new AccountProfile()
            {
                FullName = fname
            };
        }

        public void MakeTweet( string txtTweet)
        {
            var tweet = new Tweet(txtTweet, this);
            // дальше нужно добавить твитт в список
            // tweets.Insert(tweet);

            Console.WriteLine($"Добавлен твитт:\n{tweet.Text}");
        }

    }

    // класс сопутствующей информации аккаунта 
    class AccountProfile
    {
        // private string fullname;
        private string website;
        private DateTime birthdate;
        private string location;

        public string FullName { get; set; }

        public AccountProfile()
        {
            

        }
    }
}
