using HeatGames.Data;
using HeatGames.Core.DTOs;
using HeatGames.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeatGames.Core.Services
{
    public class DeveloperService : IDeveloperService
    {
        private readonly HeatGamesDbContext _context;

        public DeveloperService(HeatGamesDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DeveloperDto>> GetAllDevelopersAsync()
        {
            return await _context.Developers
                .Select(d => new DeveloperDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Website = d.Website
                })
                .ToListAsync();
        }

        public async Task CreateDeveloperAsync(DeveloperDto dto)
        {
            var developer = new HeatGames.Data.Models.Developer
            {
                Id = dto.Id,
                Name = dto.Name,
                Website = dto.Website
            };

            await _context.Developers.AddAsync(developer);
            await _context.SaveChangesAsync();
        }
        public async Task<DeveloperDto?> GetDeveloperByIdAsync(Guid id)
        {
            var developer = await _context.Developers.FindAsync(id);
            if (developer == null) return null;

            return new DeveloperDto
            {
                Id = developer.Id,
                Name = developer.Name,
                Website = developer.Website
            };
        }

        public async Task<bool> UpdateDeveloperAsync(DeveloperDto dto)
        {
            var developer = await _context.Developers.FindAsync(dto.Id);
            if (developer == null) return false;

            developer.Name = dto.Name;
            developer.Website = dto.Website;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task DeleteDeveloperAsync(Guid id)
        {
            var developer = await _context.Developers.FindAsync(id);
            if (developer != null)
            {
                _context.Developers.Remove(developer);
                await _context.SaveChangesAsync();
            }
        }
    }
}