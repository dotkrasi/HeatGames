using HeatGames.Data;
using HeatGames.Data.Models;
using HeatGames.Core.DTOs;
using HeatGames.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeatGames.Core.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly HeatGamesDbContext _context;

        public WishlistService(HeatGamesDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WishlistDto>> GetUserWishlistAsync(Guid userId)
        {
            return await _context.Wishlists
                .Include(w => w.Game)
                .Where(w => w.UserId == userId)
                .Select(w => new WishlistDto
                {
                    Id = w.Id,
                    UserId = w.UserId,
                    GameId = w.GameId,
                    GameTitle = w.Game.Title,
                    CoverImageUrl = w.Game.CoverImageUrl,
                    CurrentPrice = w.Game.Price,
                    AddedOn = w.AddedOn
                })
                .ToListAsync();
        }

        public async Task<bool> ToggleWishlistAsync(Guid userId, Guid gameId)
        {
            var existingItem = await _context.Wishlists
                .FirstOrDefaultAsync(w => w.UserId == userId && w.GameId == gameId);

            if (existingItem != null)
            {
                _context.Wishlists.Remove(existingItem);
            }
            else
            {
                await _context.Wishlists.AddAsync(new Wishlist
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    GameId = gameId,
                    AddedOn = DateTime.UtcNow
                });
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}