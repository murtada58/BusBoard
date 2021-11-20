using System;

namespace BusBoard
{
    public static class Global
    {
        public static string GetUserInput(string message)
        {
            Console.Write(message);
            string response = Console.ReadLine();
            return response;
        }
    }
}