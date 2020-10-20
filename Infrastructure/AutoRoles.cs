using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class AutoRoles
    {
        private readonly TutorialContext _context;

        public AutoRoles(TutorialContext context)
        {
            _context = context;
        }

        public async Task<List<AutoRole>> GetAutoRolesAsync(ulong id)
        {
            var autoRoles = await _context.AutoRoles
                .Where(x => x.ServerId == id)
                .ToListAsync();

            return await Task.FromResult(autoRoles);
        }

        public async Task AddAutoRoleAsync(ulong id, ulong roleId)
        {
            var server = await _context.Servers
                .FindAsync(id);

            if (server == null)
                _context.Add(new Server { Id = id });

            _context.Add(new AutoRole { RoleId = roleId, ServerId = id });
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAutoRoleAsync(ulong id, ulong roleId)
        {
            var autoRole = await _context.AutoRoles
                .Where(x => x.RoleId == roleId)
                .FirstOrDefaultAsync();

            _context.Remove(autoRole);
            await _context.SaveChangesAsync();
        }

        public async Task ClearAutoRolesAsync(List<AutoRole> autoRoles)
        {
            _context.RemoveRange(autoRoles);
            await _context.SaveChangesAsync();
        }
    }
}
