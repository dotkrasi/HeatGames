using HeatGames.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeatGames.Core.Services.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreDto>> GetAllGenresAsync();
        Task<GenreDto?> GetGenreByIdAsync(Guid id);
        Task CreateGenreAsync(GenreDto dto);
        Task<bool> UpdateGenreAsync(GenreDto dto);
        Task DeleteGenreAsync(Guid id);
    }
}