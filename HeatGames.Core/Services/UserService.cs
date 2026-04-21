using HeatGames.Core.DTOs;
using HeatGames.Core.Services.Interfaces;
using HeatGames.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace HeatGames.Core.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserProfileDto?> GetProfileAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return null;

            return new UserProfileDto
            {
                Id = user.Id,
                Username = user.UserName!,
                Email = user.Email!,
                ProfilePictureUrl = user.ProfilePictureUrl,
                WalletBalance = user.WalletBalance,
                RegistrationDate = user.RegistrationDate
            };
        }

        public async Task<(bool Success, string Message)> UpdateProfileAsync(UserProfileDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.Id.ToString());
            if (user == null) return (false, "User not found.");

            user.ProfilePictureUrl = dto.ProfilePictureUrl;

            if (!string.IsNullOrEmpty(dto.CurrentPassword) && !string.IsNullOrEmpty(dto.NewPassword))
            {
                var passwordResult = await _userManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);
                if (!passwordResult.Succeeded)
                {
                    return (false, passwordResult.Errors.First().Description);
                }
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) return (false, "Error saving to the database.");

            return (true, "Profile updated successfully!");
        }
    }
}