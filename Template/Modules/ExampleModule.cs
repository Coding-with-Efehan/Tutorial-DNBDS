using System.Data;
using System.Threading.Tasks;
using Discord.Commands;
using Infrastructure;
using Microsoft.Extensions.Logging;

namespace Template.Modules
{
    public class ExampleModule : ModuleBase<SocketCommandContext>
    {
        private readonly ILogger<ExampleModule> _logger;
        private readonly Servers _servers;

        public ExampleModule(ILogger<ExampleModule> logger, Servers servers)
        {
            _logger = logger;
            _servers = servers;
        }

        [Command("ping")]
        public async Task PingAsync()
        {
            await ReplyAsync("Pong!");
        }

        [Command("echo")]
        public async Task EchoAsync([Remainder] string text)
        {
            await ReplyAsync(text);
        }

        [Command("math")]
        public async Task MathAsync([Remainder] string math)
        {
            var dt = new DataTable();
            var result = dt.Compute(math, null);

            await ReplyAsync($"Result: {result}");
        }

        [Command("prefix")]
        [RequireUserPermission(Discord.GuildPermission.Administrator)]
        public async Task Prefix(string prefix = null)
        {
            if(prefix == null)
            {
                var guildPrefix = await _servers.GetGuildPrefix(Context.Guild.Id) ?? "!";
                await ReplyAsync($"The current prefix of this bot is `{guildPrefix}`.");
                return;
            }

            if(prefix.Length > 8)
            {
                await ReplyAsync("The length of the new prefix is too long!");
                return;
            }

            await _servers.ModifyGuildPrefix(Context.Guild.Id, prefix);
            await ReplyAsync($"The prefix has been adjusted to `{prefix}`.");
        }
    }
}