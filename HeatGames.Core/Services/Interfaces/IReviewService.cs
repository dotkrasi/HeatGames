using HeatGames.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeatGames.Core.Services.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewDto>> GetGameReviewsAsync(Guid gameId);
        Task<bool> AddReviewAsync(ReviewDto dto);
    }
}