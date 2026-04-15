using HeatGames.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeatGames.Core.Services.Interfaces
{
    public interface IPlatformService
    {
        Task<IEnumerable<PlatformDto>> GetAllPlatformsAsync();
        Task<PlatformDto?> GetPlatformByIdAsync(Guid id);
        Task CreatePlatformAsync(PlatformDto dto);
        Task<bool> UpdatePlatformAsync(PlatformDto dto);
        Task DeletePlatformAsync(Guid id);
    }
}