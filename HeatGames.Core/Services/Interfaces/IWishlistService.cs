using HeatGames.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeatGames.Core.Services.Interfaces
{
    public interface IWishlistService
    {
        Task<IEnumerable<WishlistDto>> GetUserWishlistAsync(Guid userId);
        Task<bool> ToggleWishlistAsync(Guid userId, Guid gameId);
    }
}