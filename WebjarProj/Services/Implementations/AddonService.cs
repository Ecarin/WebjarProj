using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebjarProj.Data;
using WebjarProj.Models;
using WebjarProj.Services.Interfaces;

namespace WebjarProj.Services.Implementations
{
    public class AddonService : IAddonService
    {
        private readonly WebjarDbContext _context;

        public AddonService(WebjarDbContext context)
        {
            _context = context;
        }

        public async Task<Addon> GetAddonByIdAsync(int id)
        {
            return await _context.Addons.FindAsync(id);
        }

        public async Task<List<Addon>> GetAllAddonsAsync()
        {
            return await _context.Addons.ToListAsync();
        }

        public async Task CreateAddonAsync(Addon addon)
        {
            if (addon == null)
                throw new ArgumentNullException(nameof(addon));

            _context.Addons.Add(addon);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAddonAsync(Addon addon)
        {
            if (addon == null)
                throw new ArgumentNullException(nameof(addon));

            _context.Addons.Update(addon);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAddonAsync(int id)
        {
            var addon = await _context.Addons.FindAsync(id);
            if (addon == null)
                throw new ArgumentException("Addon not found.");

            _context.Addons.Remove(addon);
            await _context.SaveChangesAsync();
        }
    }
}
