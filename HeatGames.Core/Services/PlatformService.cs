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
    public class PlatformService : IPlatformService
    {
        private readonly HeatGamesDbContext _context;

        public PlatformService(HeatGamesDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PlatformDto>> GetAllPlatformsAsync()
        {
            return await _context.Platforms
                .Select(p => new PlatformDto
                {
                    Id = p.Id,
                    Name = p.Name
                })
                .ToListAsync();
        }

        public async Task<PlatformDto?> GetPlatformByIdAsync(Guid id)
        {
            var platform = await _context.Platforms.FindAsync(id);
            if (platform == null) return null;

            return new PlatformDto
            {
                Id = platform.Id,
                Name = platform.Name
            };
        }

        public async Task CreatePlatformAsync(PlatformDto dto)
        {
            var platform = new Platform
            {
                Id = dto.Id,
                Name = dto.Name
            };
            await _context.Platforms.AddAsync(platform);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePlatformAsync(PlatformDto dto)
        {
            var platform = await _context.Platforms.FindAsync(dto.Id);
            if (platform == null) return false;

            platform.Name = dto.Name;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task DeletePlatformAsync(Guid id)
        {
            var platform = await _context.Platforms.FindAsync(id);
            if (platform != null)
            {
                _context.Platforms.Remove(platform);
                await _context.SaveChangesAsync();
            }
        }
    }
}