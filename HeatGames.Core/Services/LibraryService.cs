using HeatGames.Data;
using HeatGames.Core.DTOs;
using HeatGames.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeatGames.Core.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly HeatGamesDbContext _context;

        public LibraryService(HeatGamesDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LibraryItemDto>> GetUserLibraryAsync(Guid userId)
        {
            return await _context.LibraryItems
                .Include(l => l.Game)
                .Where(l => l.UserId == userId)
                .Select(l => new LibraryItemDto
                {
                    Id = l.Id,
                    UserId = l.UserId,
                    GameId = l.GameId,
                    GameTitle = l.Game.Title,
                    CoverImageUrl = l.Game.CoverImageUrl,
                    PurchaseDate = l.PurchaseDate,
                    PlayTimeMinutes = l.PlayTimeMinutes
                })
                .ToListAsync();
        }

        public async Task<bool> UserOwnsGameAsync(Guid userId, Guid gameId)
        {
            return await _context.LibraryItems
                .AnyAsync(l => l.UserId == userId && l.GameId == gameId);
        }

        public async Task UpdatePlayTimeAsync(Guid libraryItemId, int minutesToAdd)
        {
            var libraryItem = await _context.LibraryItems.FindAsync(libraryItemId);

            if (libraryItem != null)
            {
                libraryItem.PlayTimeMinutes += minutesToAdd;
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<Guid>> GetOwnedGameIdsAsync(Guid userId)
        {
            return await _context.LibraryItems
                .Where(l => l.UserId == userId)
                .Select(l => l.GameId)
                .ToListAsync();
        }
    }
}