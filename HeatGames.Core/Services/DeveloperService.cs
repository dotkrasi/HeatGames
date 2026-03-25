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
    }
}