using HeatGames.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeatGames.Core.Services.Interfaces
{
    public interface IDeveloperService
    {
        Task<IEnumerable<DeveloperDto>> GetAllDevelopersAsync();
        Task CreateDeveloperAsync(DeveloperDto dto);
        Task<DeveloperDto?> GetDeveloperByIdAsync(Guid id);
        Task<bool> UpdateDeveloperAsync(DeveloperDto dto);
        Task DeleteDeveloperAsync(Guid id);
    }
}