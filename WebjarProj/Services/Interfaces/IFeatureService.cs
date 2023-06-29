using WebjarProj.Models;

namespace WebjarProj.Services.Interfaces
{
    public interface IFeatureService
    {
        Task<Feature> GetFeatureByIdAsync(int id);
        Task<List<Feature>> GetAllFeaturesAsync();
        Task CreateFeatureAsync(Feature feature);
        Task UpdateFeatureAsync(Feature feature);
        Task DeleteFeatureAsync(int id);
    }
}