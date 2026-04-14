using HeatGames.Core.DTOs;

namespace HeatGames.Core.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserProfileDto?> GetProfileAsync(Guid userId);
        Task<(bool Success, string Message)> UpdateProfileAsync(UserProfileDto dto);
    }
}