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
    public class ReviewService : IReviewService
    {
        private readonly HeatGamesDbContext _context;

        public ReviewService(HeatGamesDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ReviewDto>> GetGameReviewsAsync(Guid gameId)
        {
            return await _context.Reviews
                .Include(r => r.User)
                .Where(r => r.GameId == gameId)
                .OrderByDescending(r => r.CreatedOn) // Най-новите първи
                .Select(r => new ReviewDto
                {
                    Id = r.Id,
                    UserId = r.UserId,
                    UserName = r.User.UserName!,
                    GameId = r.GameId,
                    IsPositive = r.IsPositive,
                    Comment = r.Comment,
                    CreatedOn = r.CreatedOn
                })
                .ToListAsync();
        }

        public async Task<bool> AddReviewAsync(ReviewDto dto)
        {
            // Проверка 1: Има ли играта в библиотеката си?
            var ownsGame = await _context.LibraryItems
                .AnyAsync(l => l.UserId == dto.UserId && l.GameId == dto.GameId);

            if (!ownsGame) return false;

            // Проверка 2: Дали вече не е писал ревю за тази игра?
        /*    var alreadyReviewed = await _context.Reviews
                .AnyAsync(r => r.UserId == dto.UserId && r.GameId == dto.GameId);

            if (alreadyReviewed) return false;*/

            var review = new Review
            {
                UserId = dto.UserId,
                GameId = dto.GameId,
                IsPositive = dto.IsPositive,
                Comment = dto.Comment,
                CreatedOn = DateTime.UtcNow
            };

            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}