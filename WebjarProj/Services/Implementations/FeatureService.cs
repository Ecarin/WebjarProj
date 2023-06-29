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
    public class FeatureService : IFeatureService
    {
        private readonly WebjarDbContext _context;

        public FeatureService(WebjarDbContext context)
        {
            _context = context;
        }

        public async Task<Feature> GetFeatureByIdAsync(int id)
        {
            return await _context.Features.FindAsync(id);
        }

        public async Task<List<Feature>> GetAllFeaturesAsync()
        {
            return await _context.Features.ToListAsync();
        }

        public async Task CreateFeatureAsync(Feature feature)
        {
            if (feature is null)
                throw new ArgumentNullException(nameof(feature));

            _context.Features.Add(feature);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFeatureAsync(Feature feature)
        {
            if (feature is null)
                throw new ArgumentNullException(nameof(feature));

            _context.Features.Update(feature);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFeatureAsync(int id)
        {
            var feature = await _context.Features.FindAsync(id);
            if (feature is null)
                throw new ArgumentException("Feature not found.");

            _context.Features.Remove(feature);
            await _context.SaveChangesAsync();
        }
    }
}
