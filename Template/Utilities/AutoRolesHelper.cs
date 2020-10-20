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
    public class AutoRolesHelper
    {
        private readonly AutoRoles _autoRoles;

        public AutoRolesHelper(AutoRoles autoRoles)
        {
            _autoRoles = autoRoles;
        }

        public async Task<List<IRole>> GetAutoRolesAsync(IGuild guild)
        {
            var roles = new List<IRole>();
            var invalidAutoRoles = new List<AutoRole>();

            var autoRoles = await _autoRoles.GetAutoRolesAsync(guild.Id);

            foreach (var autoRole in autoRoles)
            {
                var role = guild.Roles.FirstOrDefault(x => x.Id == autoRole.RoleId);
                if (role == null)
                {
                    invalidAutoRoles.Add(autoRole);
                }
                else
                {
                    var currentUser = await guild.GetCurrentUserAsync();
                    var hierarchy = (currentUser as SocketGuildUser).Hierarchy;

                    if (role.Position > hierarchy)
                        invalidAutoRoles.Add(autoRole);
                    else
                        roles.Add(role);
                }
            }

            if (invalidAutoRoles.Count > 0)
                await _autoRoles.ClearAutoRolesAsync(invalidAutoRoles);

            return roles;
        }
    }
}
