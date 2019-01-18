using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        public const string twtext = "0123456789 qwertyuiop[]\asdfghjkl;'zxcvbnm,./";

        static void Main(string[] args)
        {
           

            TweeterAccount twaccount = new TweeterAccount();

            System.Console.WriteLine($"В аккаунт с ником: {twaccount.Nickname}");
            System.Console.WriteLine($"и полным именем: {twaccount.PublicName}");
            
            twaccount.MakeTweet(twtext);


            //string[] words = { "cherry", "apple", "blueberry" };

            //// Use method syntax to apply a lambda expression to each element  
            //// of the words array.   
            //int shortestWordLength = words.Min(w => w.Length);
            //Console.WriteLine(shortestWordLength);


            /////////////////////////////////////////////
            // Keep the console window open in debug mode.
            System.Console.WriteLine("\n");
            System.Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }
    
    }
}
