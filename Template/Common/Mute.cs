using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Common
{
    public class Mute
    {
        public SocketGuild Guild;
        public SocketGuildUser User;
        public IRole Role;
        public DateTime End;
    }
}
