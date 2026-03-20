using HeatGamesWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeatGamesWeb.Services.Interfaces
{
    public interface IGameService
    {
        Task<IEnumerable<GameViewModel>> GetAllGamesAsync();
        Task<GameViewModel?> GetGameByIdAsync(Guid id);
        Task CreateGameAsync(GameViewModel model);
        Task<bool> UpdateGameAsync(GameViewModel model);
        Task DeleteGameAsync(Guid id);
    }
}