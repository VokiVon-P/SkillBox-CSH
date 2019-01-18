using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public static class StringHelper
    {
        public static string Cut(string inputText, int maxLength)
        {
            return (!string.IsNullOrWhiteSpace(inputText) && inputText.Length > maxLength) ? inputText.Remove(maxLength) : inputText;
        }

    }

    class Tweet
    {
        private const int MaxLength = 20;

        private readonly DateTime moment;
        private readonly TweeterAccount owner;
        private readonly string tweetText;

        private int likes;

        

        public int Likes => likes; 
        public string Text => tweetText;

        public Tweet(string text, TweeterAccount tweetOwner)
        {
            this.moment = DateTime.Now;
            this.owner = tweetOwner;
            this.tweetText = StringHelper.Cut(text, MaxLength);
            this.likes = 0;
        }

        public void AddLike() => likes++;
        public void RemoveLike() { if (likes > 0) likes--; }

    }

    class Replies
    {

    }

    class Retweets
    {

    }

}

 