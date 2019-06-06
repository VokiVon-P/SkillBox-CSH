using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TweetSharp;

namespace TwitterConsole
{
    static class StringHelper
    {
        private const int tweetLength = 140;
        public static string Cut(string inputText, int maxLength = tweetLength)
        {
            return (!string.IsNullOrWhiteSpace(inputText) && inputText.Length > maxLength) ? inputText.Remove(maxLength) : inputText;
        }

    }
    [Serializable]
    public class TWUser
    {
        public string UserName { get; set; } = "@JIRO";
        public string NickName { get; set; } = "Массажист осьминогов";
        public int FollowersCount { get; set; }
        public int FavouritesCount { get; set; }
        public int FriendsCount { get; set; }
        public string ProfileImageUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }

        public TWUser()
        {

        }

        public TWUser(TwitterUser User)
        {
            UserName = User.Name;
            NickName = User.ScreenName;
            FollowersCount = User.FollowersCount;
            FavouritesCount = User.FavouritesCount;
            FriendsCount = User.FriendsCount;
            ProfileImageUrl = User.ProfileImageUrl;
            CreatedDate = User.CreatedDate;
            Description = User.Description;
        }
    }

    [Serializable]
    public class TwittPost
    {
        public const int TextMaxLength = 10;

        public TwittPost()
        {

        }

        public TwittPost(string text)
        {
            this.Text = StringHelper.Cut(text, TextMaxLength);
        }

        public string AuthorImage { get; set; } = "pack://siteoforigin:,,,/Resources/home icon.png";
        public string AuthorName { get; set; } = "AuthorName";
        public string AuthorNick { get; set; } = "@author";
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public int RetweetCount { get; set; }

        public TWUser User { get; set; }

        private static Regex rgx = new Regex(@"(\#\w+)"); //Выберет тэги вида: #hashtag, #hash_tag, #123, #hash123

        public string GetHashTags()
        {
            var hashTags = rgx.Matches(this.Text);
            if (hashTags != null && hashTags.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var tag in hashTags)
                {
                    if (sb.Length != 0)
                        sb.Append(", ");

                    sb.Append(tag);

                }
                return sb.ToString();
            }
            return string.Empty;
        }
    }
}

