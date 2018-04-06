using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Initilization API


namespace TwitchChatBotRMK
{
    class Program
    {
        static void Main(string[] args)
        {

            /*
             * Запуск Twitch bot
            */
            TwitchChatBot bot = new TwitchChatBot();
            bot.Connect();
            Console.ReadLine();
            /*
             * Отключение Twitch бота 
             */
            bot.Disconnect();
            Console.ReadLine();
            Console.Clear();
            
        }
    }
}
