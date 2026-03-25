using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HeatGames.Core.DTOs;
namespace HeatGamesCore.Services.Interfaces
{
    public interface IGameService
    {
        Task<IEnumerable<GameDto>> GetAllGamesAsync();
        Task<GameDto?> GetGameByIdAsync(Guid id);
        Task CreateGameAsync(GameDto model);
        Task<bool> UpdateGameAsync(GameDto model);
        Task DeleteGameAsync(Guid id);
    }
}