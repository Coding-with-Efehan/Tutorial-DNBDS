using Discord;
using Discord.WebSocket;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Utilities
{
    public class RanksHelper
    {
        private readonly Ranks _ranks;

        public RanksHelper(Ranks ranks)
        {
            _ranks = ranks;
        }

        public async Task<List<IRole>> GetRanksAsync(IGuild guild)
        {
            var roles = new List<IRole>();
            var invalidRanks = new List<Rank>();

            var ranks = await _ranks.GetRanksAsync(guild.Id);

            foreach (var rank in ranks)
            {
                var role = guild.Roles.FirstOrDefault(x => x.Id == rank.RoleId);
                if (role == null)
                {
                    invalidRanks.Add(rank);
                }
                else
                {
                    var currentUser = await guild.GetCurrentUserAsync();
                    var hierarchy = (currentUser as SocketGuildUser).Hierarchy;

                    if (role.Position > hierarchy)
                        invalidRanks.Add(rank);
                    else
                        roles.Add(role);
                }
            }

            if (invalidRanks.Count > 0)
                await _ranks.ClearRanksAsync(invalidRanks);

            return roles;
        }
    }
}
