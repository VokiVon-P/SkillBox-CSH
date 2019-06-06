using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using TweetSharp;


namespace TwitterConsole
{
    static class TwitterHelper
    {

        static TwitterService service;

        public static bool Init()
        {
            OAuthRequestToken requestToken;
            try
            {
                service = new TwitterService(TwKeys.ConsumerKey, TwKeys.ConsumerSecret);

                requestToken = service.GetRequestToken();
                if (requestToken == null)
                    throw new TwitterException("Не удалось выполнить запрос.");
            }
            catch (Exception ex)
            {
                throw new TwitterException("Не удалось выполнить запрос.", ex);
            }

            Uri uri;
            try
            {
                uri = service.GetAuthorizationUri(requestToken);
                Process.Start(uri.ToString());

                Console.Write("Введите код авторизации: ");
                var verifier = Console.ReadLine();

                var access = service.GetAccessToken(requestToken, verifier);
                service.AuthenticateWith(access.Token, access.TokenSecret);
                return true;
            }
            catch (Exception ex)
            {
                throw new TwitterException("Не удалось авторизовать приложение.", ex);
            }
        }

        public static TWUser GetUserInfo()
        {
            TWUser userProfile = null;

            var user = service.GetUserProfile(new GetUserProfileOptions() { IncludeEntities = false, SkipStatus = true });
            if (user != null)
            {
                userProfile = new TWUser(user);
                
            }
            return userProfile;
        }

        public static List<TwittPost> GetTweets()
        {
            try
            {
                var source = service.ListTweetsOnHomeTimeline(new ListTweetsOnHomeTimelineOptions()).ToList();
                var result = new List<TwittPost>();
                if (source != null)
                {
                    foreach (var src in source)
                    {
                        result.Add(Convert(src));
                    }

                    //result.Sort((a, b) =>
                    //{
                    //    return -DateTime.Compare(a.CreatedDate, b.CreatedDate);
                    //});
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new TwitterException("Не удалось получить список твитов.", ex);
            }
        }

        private static TwittPost Convert(TwitterStatus src)
        {
            TwittPost result = new TwittPost(src.Text)
            {
                Text = src.Text,
                CreatedDate = src.CreatedDate,
                RetweetCount = src.RetweetCount,
                User = new TWUser(src.User),
                AuthorName = src.User.Name,
                AuthorNick = src.User.ScreenName,
                AuthorImage = src.User.ProfileImageUrl
            };

            return result;
        }

    }
}
