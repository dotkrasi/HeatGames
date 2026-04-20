using HeatGames.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeatGames.Core.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetUserOrdersAsync(Guid userId);

        Task<(bool Success, string Message)> PurchaseGameAsync(Guid userId, Guid gameId);

        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
    }
}