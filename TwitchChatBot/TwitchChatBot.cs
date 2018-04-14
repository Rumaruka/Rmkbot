
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using TwitchChatBot;
using TwitchLib.Api;
using TwitchLib.Api.Models.v5.Users;
using TwitchLib.Api.Services.Events.FollowerService;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Extensions;
using TwitchLib.Client.Models;
using TwitchLib.Client.Services;

namespace TwitchChatBotRMK
{
    internal class TwitchChatBot
    {
        /*
         * credentials - данные, в котором бот входит в свой аккаунт на твиче
        */

        readonly ConnectionCredentials credentials = new ConnectionCredentials(TwitchInfo.BotName, TwitchInfo.BotToken);
       /*
       * Инициализация твич клиента и твич апи
       */
        TwitchClient client;
        TwitchAPI api;
    
      



        internal async void Connect()
        {
            /*
             * Метод подключения бота к каналу
             */
            Console.WriteLine("Connected");




            /*
             * TwitchInfo.ChannelName - ChannelName это канал на твиче, к которому подключается бот
             */
            client = new TwitchClient();
            client.Initialize(credentials, TwitchInfo.ChannelName, autoReListenOnExceptions:false);
            api = new TwitchAPI();
            /*
            * allowedMessage - разрешенное число сообщений
            * time - время бездействия
            */
            int allowedMessage;
            int time ;
            
            Console.WriteLine("Enter allowed messages:");
            allowedMessage = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Add Time:");
            time = Convert.ToInt32(Console.ReadLine());
           

            client.ChatThrottler = new MessageThrottler(client, allowedMessage, TimeSpan.FromSeconds(time));

            await client.ChatThrottler.StartQueue();
           

            await api.InitializeAsync(TwitchInfo.ClientId, TwitchInfo.AccessToken);


            /*
            * Инициализация методов функций бота
            */

            client.OnLog += Client_OnLog;
            client.OnConnectionError += Client_OnConnectionError;
            client.OnMessageReceived += Client_OnMessageReceivedAsync;
            client.OnMessageReceived += TwitchClient_OnMessageReceived;
            client.OnMessageReceived += File_OnMessageReceived;
            client.OnChatCommandReceived += Client_OnChatCommandReceived;
            
            client.AddChatCommandIdentifier('!');
            // TwitchLib.Services.FollowerService follows = new TwitchLib.Services.FollowerService(api, 5, 3);


         


            client.Connect();

            
      

        }

        private void File_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            /*
            * Метод мута пользователя , если тот напишет слово из файла
            */
            string[] Censor = null;
            string for_search = e.ChatMessage.Message;
            string directory_file = @"C:\Users\Rumaruka\Desktop\rmkboter\TwitchChatBot\ModedList\sensor.txt";

            if (File.Exists(directory_file)) Censor = File.ReadAllLines(directory_file);
            if(Censor!=null)
            {
                bool found = false;
                foreach (string word in Censor)
                {
                    if (for_search.ToLower().Contains(word))
                    {

                        found = true;
                    }
                   
                }
                if(found)
                {
                    Console.WriteLine(found);
                    var reply = $"{e.ChatMessage.Username}, уходи с матом.";
                    TimeoutUserExt.TimeoutUser(client, e.ChatMessage.Username, TimeSpan.FromMinutes(10), message: reply);
                    found = false;

                    if (e.ChatMessage.IsBroadcaster)
                    {
                        client.SendMessage(e.ChatMessage.Channel,"Test");
                        found = false;
                    }
                    
                }             

            }
        }
       
        private void Client_OnChatCommandReceived(object sender, OnChatCommandReceivedArgs e)
        {
            /*
            * Команды бота
            */

            switch (e.Command.CommandText)
            {
               
                case "время":
                    client.SendMessage(e.Command.ChatMessage.Channel,"Время стрима: " + GetTime()?.ToString(@"hh\:mm\:ss") ?? "Стрим отключен");
                    
                    break;
                case "бот":
                    client.SendMessage(e.Command.ChatMessage.Channel, $"Всем привет, я бот создан Rumaruka. Версия {TwitchInfo.VersionBots}");
               
                    
                    break;
                case "помощь":
                    client.SendMessage(e.Command.ChatMessage.Channel, $"Сейчас работают команды:!бот , !link , !ping и !время");
                 
                    break;
               

                case "gjvjom":

                    client.SendMessage(e.Command.ChatMessage.Channel, $"Этой команды нет! Правильная команда: !помощь");
                  

                    break;
                case "ping":
                    client.SendMessage(e.Command.ChatMessage.Channel, $"Pong");
                  

                    break;
                case "рулетка":


                   
                    Random r = new Random();

                    int rInt = r.Next(1, 3);

                    switch (rInt)
                    {
                        case 1:
                            client.SendMessage(e.Command.ChatMessage.Channel, $"Повезло, {e.Command.ChatMessage.Username}");
                            break;
                        case 2:
                            RuletkaMuted(e.Command.ChatMessage.Username, e.Command.ChatMessage.IsSubscriber, e.Command.ChatMessage.IsModerator);
                            client.SendMessage(e.Command.ChatMessage.Channel, $"Хотяя, {e.Command.ChatMessage.Username}");
                            break;
                        case 3:
                            RuletkaMuted(e.Command.ChatMessage.Username, e.Command.ChatMessage.IsSubscriber, e.Command.ChatMessage.IsModerator);
                            
                            break;
                       
                        default:
                            break;
                    }
                    client.ChatThrottler.StartQueue();

                    break;
                default:
                    // Action taken by all commands
                    break;

                    
            }
            if (e.Command.ChatMessage.IsBroadcaster) { 
            switch (e.Command.CommandText){

                
                case "start":
                    client.SendMessage(e.Command.ChatMessage.Channel, "Начинаем стрим, время через 5 минут начинаем");
                break;
                    case "раздача":
                        /*
                         *Находится в WIP  
                         * */
                        client.SendMessage(e.Command.ChatMessage.Channel, "Прости, создатель, но ты не сделал систему валют на стриме и увеличить количество валют я пока не могу");
                        break;
            default:
                break;
              }
            }
        }

   

        private void Follows_OnNewFollowersDetected(object sender, OnNewFollowersDetectedArgs e)
        {
            client.SendMessage(TwitchInfo.ChannelName, $"Спасибо за подписку {e.NewFollowers}");
        }

       

        private void Client_OnMessageReceivedAsync(object sender, OnMessageReceivedArgs e)
        {
            /*
            * Бот здоровается, если пользователь здоровается
            */
            var message = e.ChatMessage.Message;
            var condition = StringComparison.InvariantCultureIgnoreCase;
            if (message.StartsWith("здравствуйте", condition) || 
				message.StartsWith("привет", condition) || 
				message.StartsWith("хой", condition) || 
				message.StartsWith("дарова", condition) || 
				message.StartsWith("qq", condition) || 
				message.StartsWith("здрям", condition) || 
				message.StartsWith("ку", condition))
            {
                Random r = new Random();

                int rInt = r.Next(1, 10);

                switch (rInt)
                {
                    case 1:
                        client.SendMessage(e.ChatMessage.Channel, $"Привет тебе, {e.ChatMessage.DisplayName}");
                        client.ChatThrottler.StartQueue();
                        break;
                    case 2:
                        client.SendMessage(e.ChatMessage.Channel, $"Я по тебе соскучился, давно не виделись {e.ChatMessage.DisplayName}");
                        client.ChatThrottler.StartQueue();
                        break;
                    case 3:
                        client.SendMessage(e.ChatMessage.Channel, $"* Обнимаю {e.ChatMessage.DisplayName} * привеееет ^^");
                        client.ChatThrottler.StartQueue();
                        break;
                    case 4:
                        client.SendMessage(e.ChatMessage.Channel, $"Я рад привествовать тебя {e.ChatMessage.DisplayName}");
                        client.ChatThrottler.StartQueue();
                        break;
                    case 5:
                        client.SendMessage(e.ChatMessage.Channel, $"Выпускаем всех котят в чате {e.ChatMessage.DisplayName}");
                        client.ChatThrottler.StartQueue();
                        break;
                    case 6:
                        client.SendMessage(e.ChatMessage.Channel, $"Здорова {e.ChatMessage.DisplayName}");
                        client.ChatThrottler.StartQueue();
                        break;
                    case 7:
                        client.SendMessage(e.ChatMessage.Channel, $"Здравия желаю {e.ChatMessage.DisplayName}");
                        client.ChatThrottler.StartQueue();
                        break;
                    case 8:
                        client.SendMessage(e.ChatMessage.Channel, $"Ты скучал? Я скучал, {e.ChatMessage.DisplayName}");
                        client.ChatThrottler.StartQueue();
                        break;
                    case 9:
                        client.SendMessage(e.ChatMessage.Channel, $"Хой, {e.ChatMessage.DisplayName}");
                        client.ChatThrottler.StartQueue();
                        break;
                    case 10:
                        client.SendMessage(e.ChatMessage.Channel, $"Обнимашки, {e.ChatMessage.DisplayName} ?");
                        client.ChatThrottler.StartQueue();
                        break;
                    default:
                        break;
                }
            }
            
        }
        private void Client_OnConnectionError(object sender, OnConnectionErrorArgs e)
        {
            Console.WriteLine($"Error!!{e.Error} ");
        }

        private void Client_OnLog(object sender, OnLogArgs e)
        {
            Console.WriteLine(e.Data);
        }
        /*
            * Подготовка команды !время - пишет сколько по времени идет прямая трансляция
            */
        private TimeSpan? GetTime()
        {
            string userId = GetUserID(TwitchInfo.ChannelName);
            if (userId == null)
            {
                return null;
            }
            return api.Streams.v5.GetUptimeAsync(userId).Result;
        }

        string GetUserID(string username)
        {
            
            User[] userList = api.Users.v5.GetUserByNameAsync(username).Result.Matches;
            if (userList == null || userList.Length == 0)
            {
                return null;
            }
            return userList[0].Id;
        }





        //Защита от капса
        const int _messageLength = 5;
        const int _capsPercentage = 80;
       
        private void TwitchClient_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            ViolatesProtections(e.ChatMessage.Username, e.ChatMessage.IsSubscriber, e.ChatMessage.IsModerator, e.ChatMessage);
        }

        private void ViolatesProtections(string username, bool sub, bool mod, ChatMessage message)
        {
            try
            {
                if (sub) return;
                if (mod) return;
                if (ViolateCapsProtection(message.Message))
                {
                    var reply = $"@{username}, пожалуйста не капси.";
                    TimeoutUserExt.TimeoutUser(client, username, TimeSpan.FromMinutes(10), message: reply);
                   
                }
             
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

     
        private bool ViolateCapsProtection(string message)
        {
            if (message.Length >= _messageLength)
            {
                var totalChars = 0;
                var capsCount = 0;
                foreach (var character in message.ToCharArray())
                {
                    if (character == ' ') continue;

                    totalChars++;
                    if (char.IsUpper(character))
                        capsCount++;
                }
                if ((((double)capsCount / totalChars) * 100) > 80)
                    return true;
            }
            return false;
        }



        //Caps Protection Ending

        //Рулетка
        private void RuletkaMuted(string username, bool sub, bool mod)
        {
                var reply = $"@{username}, я попал.";
                TimeoutUserExt.TimeoutUser(client, username, TimeSpan.FromSeconds(10), message: reply);

          
        }



        internal void Disconnect()
        {
            /*
            * Отключение бота
            */

            client.ChatThrottler.StopQueue();
            Console.WriteLine("Disconnected");
        }

    }




}

 

  


