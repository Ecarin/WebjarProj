using System.Collections.Generic;
using WebjarProj.Models;
using WebjarProj.Models.Requests;

namespace WebjarProj.Services.Interfaces
{
    public interface IAddonService
    {
        Task<Addon> GetAddonByIdAsync(int id);
        Task<List<Addon>> GetAllAddonsAsync();
        Task CreateAddonAsync(Addon addon);
        Task UpdateAddonAsync(Addon addon);
        Task DeleteAddonAsync(int id);
    }
}
