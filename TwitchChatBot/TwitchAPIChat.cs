using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib;
using TwitchLib.Api.Sections;

namespace TwitchChatBot
{
    class TwitchAPIChat
    {
        public Undocumented Undocumented { get; }
        public Users Users { get; }
        public Videos Videos { get; }
        public Debugging Debugging { get; }
        public Teams Teams { get; }
        public Subscriptions Subscriptions { get; }
        public Streams Streams { get; }
        public Search Search { get; }
        public Root Root { get; }
        public Ingests Ingests { get; }
        public Games Games { get; }
        public Follows Follows { get; }
        public Communities Communities { get; }
        public Collections Collections { get; }
        public Clips Clips { get; }
        public Chat Chat { get; }
        public Channels Channels { get; }
        public ChannelFeeds ChannelFeeds { get; }
        public Bits Bits { get; }
        public Badges Badges { get; }
        public Blocks Blocks { get; }
        public Auth Auth { get; }
        public IApiSettings Settings { get; }
        public ThirdParty ThirdParty { get; }
        public Webhooks Webhooks { get; }
    }
}
