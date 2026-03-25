using HeatGames.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeatGames.Core.Services.Interfaces
{
    public interface IDeveloperService
    {
        // Трябва ни само да вземем всички разработчици за падащото меню
        Task<IEnumerable<DeveloperDto>> GetAllDevelopersAsync();
    }
}