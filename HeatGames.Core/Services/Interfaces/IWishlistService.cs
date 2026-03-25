using HeatGames.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeatGames.Core.Services.Interfaces
{
    public interface IWishlistService
    {
        Task<IEnumerable<WishlistDto>> GetUserWishlistAsync(Guid userId);
        // Метод, който добавя играта, ако я няма, или я маха, ако вече е там (Toggle)
        Task<bool> ToggleWishlistAsync(Guid userId, Guid gameId);
    }
}