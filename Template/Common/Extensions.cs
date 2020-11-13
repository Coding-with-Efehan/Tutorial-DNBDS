using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Template.Common
{
    public static class Extensions
    {
        public static async Task<IMessage> SendSuccessAsync(this ISocketMessageChannel channel, string title, string description)
        {
            var embed = new EmbedBuilder()
                .WithColor(new Color(43, 182, 115))
                .WithDescription(description)
                .WithAuthor(author =>
                {
                    author
                    .WithIconUrl("https://icons-for-free.com/iconfiles/png/512/complete+done+green+success+valid+icon-1320183462969251652.png")
                    .WithName(title);
                })
                .Build();

            var message = await channel.SendMessageAsync(embed: embed);
            return message;
        }

        public static async Task<IMessage> SendErrorAsync(this ISocketMessageChannel channel, string title, string description)
        {
            var embed = new EmbedBuilder()
                .WithColor(new Color(231, 76, 60))
                .WithDescription(description)
                .WithAuthor(author =>
                {
                    author
                    .WithIconUrl("https://icons.iconarchive.com/icons/paomedia/small-n-flat/1024/sign-error-icon.png")
                    .WithName(title);
                })
                .Build();

            var message = await channel.SendMessageAsync(embed: embed);
            return message;
        }
    }
}
