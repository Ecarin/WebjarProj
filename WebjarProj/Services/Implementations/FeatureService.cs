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
        private readonly WebjarDbContext _dbContext;

        public FeatureService(WebjarDbContext context)
        {
            _dbContext = context;
        }

        public async Task<Feature> GetFeatureByIdAsync(int id)
        {
            return await _dbContext.Features.FindAsync(id);
        }

        public async Task<List<Feature>> GetAllFeaturesAsync()
        {
            return await _dbContext.Features.ToListAsync();
        }

        public async Task CreateFeatureAsync(Feature feature)
        {
            // Using transaction for database safety
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                if (feature is null)
                    throw new ArgumentNullException(nameof(feature));

                _dbContext.Features.Add(feature);
                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                // Rollback any changes
                transaction.Rollback();
                throw;
            }
        }

        public async Task UpdateFeatureAsync(Feature feature)
        {
            // Using transaction for database safety
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                if (feature is null)
                    throw new ArgumentNullException(nameof(feature));

                _dbContext.Features.Update(feature);
                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                // Rollback any changes
                transaction.Rollback();
                throw;
            }
        }

        public async Task DeleteFeatureAsync(int id)
        {
            // Using transaction for database safety
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                var feature = await _dbContext.Features.FindAsync(id);
                if (feature is null)
                    throw new ArgumentException("Feature not found.");

                _dbContext.Features.Remove(feature);
                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                // Rollback any changes
                transaction.Rollback();
                throw;
            }
        }
    }
}
