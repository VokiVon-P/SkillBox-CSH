using System;

namespace TwitterConsole
{


    internal class TwitterException : Exception
    {
        public TwitterException(string message) : base(message)
        {

        }

        public TwitterException(string message, Exception innerException)
           : base(message + "\n\t" + innerException.Message, innerException)
        {

        }


    }
}
