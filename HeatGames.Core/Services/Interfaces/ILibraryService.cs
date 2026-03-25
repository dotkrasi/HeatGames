using HeatGames.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeatGames.Core.Services.Interfaces
{
    public interface ILibraryService
    {
        Task<IEnumerable<LibraryItemDto>> GetUserLibraryAsync(Guid userId);
        Task<bool> UserOwnsGameAsync(Guid userId, Guid gameId); // Проверка дали притежава играта
    }
}