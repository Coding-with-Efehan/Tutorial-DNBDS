using Discord.Commands;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using Discord;
using Discord.WebSocket;
using System.Linq;

namespace Tutorial.Modules
{
    public class Moderation : ModuleBase
    {
        [Command("purge")]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        public async Task Purge(int amount)
        {
            var messages = await Context.Channel.GetMessagesAsync(amount + 1).FlattenAsync();
            await (Context.Channel as SocketTextChannel).DeleteMessagesAsync(messages);

            var message = await Context.Channel.SendMessageAsync($"{messages.Count()} messages deleted successfully!");
            await Task.Delay(2500);
            await message.DeleteAsync();
        }
    }
}
