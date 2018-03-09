using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Initilization API
using TwitchLib.Models.Client;
using TwitchLib.Events.Client;
using TwitchLib.Models.API.v5.UploadVideo;
using TwitchChatBot;

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
       
            bot.Disconnect();
            Console.ReadLine();
            Console.Clear();
            
        }
    }
}
