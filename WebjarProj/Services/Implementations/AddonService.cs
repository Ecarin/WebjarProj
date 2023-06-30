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
        private readonly WebjarDbContext _dbContext;

        public AddonService(WebjarDbContext context)
        {
            _dbContext = context;
        }

        public async Task<Addon> GetAddonByIdAsync(int id)
        {
            return await _dbContext.Addons.FindAsync(id);
        }

        public async Task<List<Addon>> GetAddonByIdsAsync(
            List<int> addonIds)
        {
            return await _dbContext.Addons
                .Where(a => addonIds.Contains(a.Id))
                .ToListAsync();
        }

        public async Task<List<Addon>> GetAllAddonsAsync()
        {
            return await _dbContext.Addons.ToListAsync();
        }

        public async Task CreateAddonAsync(Addon addon)
        {
            if (addon is null)
                throw new ArgumentNullException(nameof(addon));

            _dbContext.Addons.Add(addon);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAddonAsync(Addon addon)
        {
            if (addon is null)
                throw new ArgumentNullException(nameof(addon));

            _dbContext.Addons.Update(addon);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAddonAsync(int id)
        {
            var addon = await _dbContext.Addons.FindAsync(id);
            if (addon is null)
                throw new ArgumentException("Addon not found.");

            _dbContext.Addons.Remove(addon);
            await _dbContext.SaveChangesAsync();
        }
    }
}
