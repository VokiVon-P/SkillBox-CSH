using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using TweetSharp;
using TwitterApp.ViewModels;



namespace TwitterApp.Helpers
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

                var loginDialog = new LoginDialog();
                var result = loginDialog.ShowDialog();

                if (result == true)
                {
                    //RF
                    var access = service.GetAccessToken(requestToken, loginDialog.Verifier.Text);
                    service.AuthenticateWith(access.Token, access.TokenSecret);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new TwitterException("Не удалось авторизовать приложение.", ex);
            }
            return false;
        }

        public static TwitterUserViewModel GetUserInfo()
        {
            TwitterUserViewModel userViewModel = null;

            var user = service.GetUserProfile(new GetUserProfileOptions() { IncludeEntities = false, SkipStatus = true });
            if (user != null)
            {
                userViewModel = new TwitterUserViewModel()
                {
                    UserName = user.Name,
                    UserScreenName = user.ScreenName,
                    FavouritesCount = user.FriendsCount,
                    FollowersCount = user.FollowersCount,
                    ProfileImageUrl = user.ProfileImageUrl
                };
            }
            return userViewModel;
        }

        public static List<TwitterPostViewModel> GetTweets()
        {
            try
            {
                var source = service.ListTweetsOnHomeTimeline(new ListTweetsOnHomeTimelineOptions()).ToList();
                var result = new List<TwitterPostViewModel>();
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

        private static TwitterPostViewModel Convert(TwitterStatus src)
        {
            TwitterPostViewModel result = new TwitterPostViewModel(src.Text)
            {
                Text = src.Text,
                CreatedDate = src.CreatedDate,
                RetweetCount = src.RetweetCount,
                User = new TwitterUserViewModel
                {
                    UserName = src.User.Name,
                    UserScreenName = src.User.ScreenName,
                    FollowersCount = src.User.FollowersCount,
                    FavouritesCount = src.User.FavouritesCount,
                    FriendsCount = src.User.FriendsCount,
                    ProfileImageUrl = src.User.ProfileImageUrl,
                    CreatedDate = src.User.CreatedDate,
                    Description = src.User.Description
                }
            };

            return result;
        }

        public static List<TwitterTrendViewModel> GetTrends()
        {
            try
            {
                var source = service.ListLocalTrendsFor(new ListLocalTrendsForOptions() { Id = 1 }); // 1 - весь мир
                var result = new List<TwitterTrendViewModel>();

                if (source != null)
                {
                    foreach (var src in source)
                    {
                        result.Add(Convert(src));
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new TwitterException("Не удалось получить список трендов.", ex);
            }

        }

        private static TwitterTrendViewModel Convert(TwitterTrend src)
        {
            TwitterTrendViewModel result = new TwitterTrendViewModel
            {
                Name = src.Name
            };
            return result;
        }

        public static List<string> GetSortedTrendsList()
        {
            try
            {
                var source = service.ListLocalTrendsFor(new ListLocalTrendsForOptions() { Id = 1 }); // 1 - весь мир
                List<string> result = new List<string>();

                if (source != null)
                {
                    foreach (var src in source)
                    {
                        result.Add(src.Name);
                    }
                    result.Sort();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new TwitterException("Не удалось получить список трендов.", ex);
            }
        }

        public static string GetHashedTrends(IEnumerable<TwitterTrendViewModel> trends)
        {
            var sb = new StringBuilder();

            foreach (var trend in trends)
            {
                if (trend.Name.StartsWith("#"))
                {
                    if (sb.Length != 0)
                        sb.Append(", ");

                    sb.Append(trend.Name);
                }
            }
            return sb.ToString();
        }
    }
}
