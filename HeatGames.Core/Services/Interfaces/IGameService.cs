using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HeatGames.Core.DTOs;

namespace HeatGamesCore.Services.Interfaces
{
    public interface IGameService
    {
        Task<(IEnumerable<GameDto> Games, int TotalCount)> GetAllGamesAsync(
            string? searchQuery = null,
            string? genre = null,
            Guid? developerId = null, // 🎯 ДОБАВЕНО: Филтър по Developer
            decimal? minPrice = null,
            decimal? maxPrice = null,
            int page = 1,
            int pageSize = 16);

        Task<GameDto?> GetGameByIdAsync(Guid id);
        Task CreateGameAsync(GameDto model);
        Task<bool> UpdateGameAsync(GameDto model);
        Task DeleteGameAsync(Guid id);
    }
}