using HeatGames.Data;
using HeatGames.Core.DTOs;
using HeatGames.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeatGames.Core.Services
{
    public class GenreService : IGenreService
    {
        private readonly HeatGamesDbContext _context;

        public GenreService(HeatGamesDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GenreDto>> GetAllGenresAsync()
        {
            return await _context.Genres
                .Select(g => new GenreDto
                {
                    Id = g.Id,
                    Name = g.Name
                })
                .ToListAsync();
        }
        public async Task<GenreDto?> GetGenreByIdAsync(Guid id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null) return null;

            return new GenreDto
            {
                Id = genre.Id,
                Name = genre.Name
            };
        }

        public async Task CreateGenreAsync(GenreDto dto)
        {
            var genre = new HeatGames.Data.Models.Genre
            {
                Id = dto.Id,
                Name = dto.Name
            };
            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateGenreAsync(GenreDto dto)
        {
            var genre = await _context.Genres.FindAsync(dto.Id);
            if (genre == null) return false;

            genre.Name = dto.Name;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task DeleteGenreAsync(Guid id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre != null)
            {
                _context.Genres.Remove(genre);
                await _context.SaveChangesAsync();
            }
        }
    }
}