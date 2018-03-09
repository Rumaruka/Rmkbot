
using System;
using TwitchLib;
using TwitchLib.Models.Client;
using TwitchLib.Events.Client;
using TwitchLib.Models.API.v5.Users;
using TwitchChatBot;
using TwitchLib.Events.Services.FollowerService;
using TwitchLib.Models.API.Undocumented.ChatProperties;
using TwitchLib.Extensions.Client;

namespace TwitchChatBotRMK
{
    internal class TwitchChatBot
    {

        readonly ConnectionCredentials credentials = new ConnectionCredentials(TwitchInfo.BotName, TwitchInfo.BotToken);

        TwitchClient client;
        TwitchAPI api;
        
        

        public ChatProperties ChatProperties { get => ChatProperties; set => ChatProperties = value; }

        internal void Disconnect()
        {
            Console.WriteLine("Disconnected");
        }
        internal void Connect()
        {
            Console.WriteLine("Connected");





            client = new TwitchClient(credentials, TwitchInfo.ChannelName, logging: false);
          
            api = new TwitchAPI(TwitchInfo.ClientId,TwitchInfo.AccessToken);
           





            client.OnLog += Client_OnLog;
            client.OnConnectionError += Client_OnConnectionError;
            client.OnMessageReceived += Client_OnMessageReceivedAsync;
            client.OnMessageReceived += TwitchClient_OnMessageReceived;
            client.OnChatCommandReceived += Client_OnChatCommandReceived;
            
            client.AddChatCommandIdentifier('!');
            TwitchLib.Services.FollowerService follows = new TwitchLib.Services.FollowerService(api, 5, 3);
            

            follows.StartService();
            follows.OnNewFollowersDetected += Follows_OnNewFollowersDetected;
            client.Connect();

            api.Settings.ClientId = TwitchInfo.ClientId;
            

        }

     
        private void Client_OnChatCommandReceived(object sender, OnChatCommandReceivedArgs e)
        {


            switch (e.Command.CommandText)
            {
               
                case "время":
                    client.SendMessage("Время стрима: " + GetTime()?.ToString(@"hh\:mm\:ss") ?? "Стрим отключен");
                    break;
                case "бот":
                    client.SendMessage($"Всем привет, я бот создан Rumaruka. Версия {TwitchInfo.VersionBots}");
                    System.Threading.Thread.Sleep(10000);
                    break;
                case "помощь":
                    client.SendMessage($"Сейчас работают команды:!бот , !link , !ping и !время");
                    System.Threading.Thread.Sleep(10000);
                    break;
               

                case "gjvjom":

                    client.SendMessage($"Этой команды нет! Правильная команда: !помощь");
                
                    break;
                case "ping":
                    client.SendMessage($"Pong");
                  

                    break;
                case "рулетка":


                   
                    Random r = new Random();

                    int rInt = r.Next(1, 3);

                    switch (rInt)
                    {
                        case 1:
                            client.SendMessage($"Повезло, {e.Command.ChatMessage.Username}");
                            break;
                        case 2:
                            RuletkaMuted(e.Command.ChatMessage.Username, e.Command.ChatMessage.IsSubscriber, e.Command.ChatMessage.IsModerator);
                            client.SendMessage($"Хотяя, {e.Command.ChatMessage.Username}");
                            break;
                        case 3:
                            RuletkaMuted(e.Command.ChatMessage.Username, e.Command.ChatMessage.IsSubscriber, e.Command.ChatMessage.IsModerator);
                            
                            break;
                       
                        default:
                            break;
                    }


                    break;
                default:
                    // Action taken by all commands
                    break;

                    
            }
            if (e.Command.ChatMessage.IsBroadcaster) { 
            switch (e.Command.CommandText){

                
                case "start":
                    client.SendMessage("Начинаем стрим, время через 5 минут начинаем");
                break;
                    case "раздача":
                        /*
                         *Находится в WIP  
                         * */
                        client.SendMessage("Прости, создатель, но ты не сделал систему валют на стриме и увеличить количество валют я пока не могу");
                        break;
            default:
                break;
              }
            }
        }

   

        private void Follows_OnNewFollowersDetected(object sender, OnNewFollowersDetectedArgs e)
        {
            client.SendMessage($"Спасибо за подписку {e.NewFollowers}");
        }

       

        private void Client_OnMessageReceivedAsync(object sender, OnMessageReceivedArgs e)
        {
            if (e.ChatMessage.Message.StartsWith("здравствуйте", StringComparison.InvariantCultureIgnoreCase))
            {
                Random r = new Random();

                int rInt = r.Next(1, 10);

                switch (rInt)
                {
                    case 1:
                        client.SendMessage($"Привет тебе, {e.ChatMessage.DisplayName}");
                        break;
                    case 2:
                        client.SendMessage($"Я по тебе соскучился, давно не виделись {e.ChatMessage.DisplayName}");
                        break;
                    case 3:
                        client.SendMessage($"* Обнимаю {e.ChatMessage.DisplayName} * привеееет ^^");
                        break;
                    case 4:
                        client.SendMessage($"Я рад привествовать тебя {e.ChatMessage.DisplayName}");
                        break;
                    case 5:
                        client.SendMessage($"Выпускаем всех котят в чате {e.ChatMessage.DisplayName}");
                        break;
                    case 6:
                        client.SendMessage($"Здорова {e.ChatMessage.DisplayName}");
                        break;
                    case 7:
                        client.SendMessage($"Здравия желаю {e.ChatMessage.DisplayName}");
                        break;
                    case 8:
                        client.SendMessage($"Ты скучал? Я скучал, {e.ChatMessage.DisplayName}");
                        break;
                    case 9:
                        client.SendMessage($"Хой, {e.ChatMessage.DisplayName}");
                        break;
                    case 10:
                        client.SendMessage($"Обнимашки, {e.ChatMessage.DisplayName} ?");
                        break;
                    default:
                        break;
                }
            }
                if (e.ChatMessage.Message.StartsWith("привет"))
            {
                Random r = new Random();

                int rInt = r.Next(1, 10);

                switch (rInt)
                {
                    case 1:
                        client.SendMessage($"Привет тебе, {e.ChatMessage.DisplayName}");
                        break;
                    case 2:
                        client.SendMessage($"Я по тебе соскучился, давно не виделись {e.ChatMessage.DisplayName}");
                        break;
                    case 3:
                        client.SendMessage($"* Обнимаю {e.ChatMessage.DisplayName} * привеееет ^^");
                        break;
                    case 4:
                        client.SendMessage($"Я рад привествовать тебя {e.ChatMessage.DisplayName}");
                        break;
                    case 5:
                        client.SendMessage($"Выпускаем всех котят в чате {e.ChatMessage.DisplayName}");
                        break;
                    case 6:
                        client.SendMessage($"Здорова {e.ChatMessage.DisplayName}");
                        break;
                    case 7:
                        client.SendMessage($"Здравия желаю {e.ChatMessage.DisplayName}");
                        break;
                    case 8:
                        client.SendMessage($"Ты скучал? Я скучал, {e.ChatMessage.DisplayName}");
                        break;
                    case 9:
                        client.SendMessage($"Хой, {e.ChatMessage.DisplayName}");
                        break;
                    case 10:
                        client.SendMessage($"Обнимашки, {e.ChatMessage.DisplayName} ?");
                        break;
                    default:
                        break;
                }

               
     }
           

                if (e.ChatMessage.Message.StartsWith("хой"))
            {
                Random r = new Random();

                int rInt = r.Next(1, 10);

                switch (rInt)
                {
                    case 1:
                        client.SendMessage($"Привет тебе, {e.ChatMessage.DisplayName}");
                        break;
                    case 2:
                        client.SendMessage($"Я по тебе соскучился, давно не виделись {e.ChatMessage.DisplayName}");
                        break;
                    case 3:
                        client.SendMessage($"* Обнимаю {e.ChatMessage.DisplayName} * привеееет ^^");
                        break;
                    case 4:
                        client.SendMessage($"Я рад привествовать тебя {e.ChatMessage.DisplayName}");
                        break;
                    case 5:
                        client.SendMessage($"Выпускаем всех котят в чате {e.ChatMessage.DisplayName}");
                        break;
                    case 6:
                        client.SendMessage($"Здорова {e.ChatMessage.DisplayName}");
                        break;
                    case 7:
                        client.SendMessage($"Здравия желаю {e.ChatMessage.DisplayName}");
                        break;
                    case 8:
                        client.SendMessage($"Ты скучал? Я скучал, {e.ChatMessage.DisplayName}");
                        break;
                    case 9:
                        client.SendMessage($"Хой, {e.ChatMessage.DisplayName}");
                        break;
                    case 10:
                        client.SendMessage($"Обнимашки, {e.ChatMessage.DisplayName} ?");
                        break;
                    default:
                        break;
                }
            }

            if (e.ChatMessage.Message.StartsWith("дарова"))
            {
                Random r = new Random();

                int rInt = r.Next(1, 10);

                switch (rInt)
                {
                    case 1:
                        client.SendMessage($"Привет тебе, {e.ChatMessage.DisplayName}");
                        break;
                    case 2:
                        client.SendMessage($"Я по тебе соскучился, давно не виделись {e.ChatMessage.DisplayName}");
                        break;
                    case 3:
                        client.SendMessage($"* Обнимаю {e.ChatMessage.DisplayName} * привеееет ^^");
                        break;
                    case 4:
                        client.SendMessage($"Я рад привествовать тебя {e.ChatMessage.DisplayName}");
                        break;
                    case 5:
                        client.SendMessage($"Выпускаем всех котят в чате {e.ChatMessage.DisplayName}");
                        break;
                    case 6:
                        client.SendMessage($"Здорова {e.ChatMessage.DisplayName}");
                        break;
                    case 7:
                        client.SendMessage($"Здравия желаю {e.ChatMessage.DisplayName}");
                        break;
                    case 8:
                        client.SendMessage($"Ты скучал? Я скучал, {e.ChatMessage.DisplayName}");
                        break;
                    case 9:
                        client.SendMessage($"Хой, {e.ChatMessage.DisplayName}");
                        break;
                    case 10:
                        client.SendMessage($"Обнимашки, {e.ChatMessage.DisplayName} ?");
                        break;
                    default:
                        break;
                }

            }


            if (e.ChatMessage.Message.StartsWith("qq"))
            {
                Random r = new Random();

                int rInt = r.Next(1, 10);

                switch (rInt)
                {
                    case 1:
                        client.SendMessage($"Привет тебе, {e.ChatMessage.DisplayName}");
                        break;
                    case 2:
                        client.SendMessage($"Я по тебе соскучился, давно не виделись {e.ChatMessage.DisplayName}");
                        break;
                    case 3:
                        client.SendMessage($"* Обнимаю {e.ChatMessage.DisplayName} * привеееет ^^");
                        break;
                    case 4:
                        client.SendMessage($"Я рад привествовать тебя {e.ChatMessage.DisplayName}");
                        break;
                    case 5:
                        client.SendMessage($"Выпускаем всех котят в чате {e.ChatMessage.DisplayName}");
                        break;
                    case 6:
                        client.SendMessage($"Здорова {e.ChatMessage.DisplayName}");
                        break;
                    case 7:
                        client.SendMessage($"Здравия желаю {e.ChatMessage.DisplayName}");
                        break;
                    case 8:
                        client.SendMessage($"Ты скучал? Я скучал, {e.ChatMessage.DisplayName}");
                        break;
                    case 9:
                        client.SendMessage($"Хой, {e.ChatMessage.DisplayName}");
                        break;
                    case 10:
                        client.SendMessage($"Обнимашки, {e.ChatMessage.DisplayName} ?");
                        break;
                    default:
                        break;
                }
            }

            if (e.ChatMessage.Message.StartsWith("здрям"))
            {
                Random r = new Random();

                int rInt = r.Next(1, 10);

                switch (rInt)
                {
                    case 1:
                        client.SendMessage($"Привет тебе, {e.ChatMessage.DisplayName}");
                        break;
                    case 2:
                        client.SendMessage($"Я по тебе соскучился, давно не виделись {e.ChatMessage.DisplayName}");
                        break;
                    case 3:
                        client.SendMessage($"* Обнимаю {e.ChatMessage.DisplayName} * привеееет ^^");
                        break;
                    case 4:
                        client.SendMessage($"Я рад привествовать тебя {e.ChatMessage.DisplayName}");
                        break;
                    case 5:
                        client.SendMessage($"Выпускаем всех котят в чате {e.ChatMessage.DisplayName}");
                        break;
                    case 6:
                        client.SendMessage($"Здорова {e.ChatMessage.DisplayName}");
                        break;
                    case 7:
                        client.SendMessage($"Здравия желаю {e.ChatMessage.DisplayName}");
                        break;
                    case 8:
                        client.SendMessage($"Ты скучал? Я скучал, {e.ChatMessage.DisplayName}");
                        break;
                    case 9:
                        client.SendMessage($"Хой, {e.ChatMessage.DisplayName}");
                        break;
                    case 10:
                        client.SendMessage($"Обнимашки, {e.ChatMessage.DisplayName} ?");
                        break;
                    default:
                        break;
                }
            }
            if (e.ChatMessage.Message.StartsWith("ку"))
            {
                Random r = new Random();

                int rInt = r.Next(1, 10);

                switch (rInt)
                {
                    case 1:
                        client.SendMessage($"Привет тебе, {e.ChatMessage.DisplayName}");
                        break;
                    case 2:
                        client.SendMessage($"Я по тебе соскучился, давно не виделись {e.ChatMessage.DisplayName}");
                        break;
                    case 3:
                        client.SendMessage($"* Обнимаю {e.ChatMessage.DisplayName} * привеееет ^^");
                        break;
                    case 4:
                        client.SendMessage($"Я рад привествовать тебя {e.ChatMessage.DisplayName}");
                        break;
                    case 5:
                        client.SendMessage($"Выпускаем всех котят в чате {e.ChatMessage.DisplayName}");
                        break;
                    case 6:
                        client.SendMessage($"Здорова {e.ChatMessage.DisplayName}");
                        break;
                    case 7:
                        client.SendMessage($"Здравия желаю {e.ChatMessage.DisplayName}");
                        break;
                    case 8:
                        client.SendMessage($"Ты скучал? Я скучал, {e.ChatMessage.DisplayName}");
                        break;
                    case 9:
                        client.SendMessage($"Хой, {e.ChatMessage.DisplayName}");
                        break;
                    case 10:
                        client.SendMessage($"Обнимашки, {e.ChatMessage.DisplayName} ?");
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





        //Caps Protection Start
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

        //SystemRulets
        private void RuletkaMuted(string username, bool sub, bool mod)
        {
                var reply = $"@{username}, я попал.";
                TimeoutUserExt.TimeoutUser(client, username, TimeSpan.FromSeconds(10), message: reply);

          
        }
      



    }




}

 

  


