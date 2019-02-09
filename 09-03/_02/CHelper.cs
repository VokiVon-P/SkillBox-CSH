using System;

namespace CHelpers
{

    public static class ConsoleHelper
    {
        public static void KeepConsole()
        {
            /////////////////////////////////////////////
            // Keep the console window open in debug mode.
            Console.WriteLine("\n");
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }

}