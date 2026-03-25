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
    public class OrderService : IOrderService
    {
        private readonly HeatGamesDbContext _context;

        public OrderService(HeatGamesDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderDto>> GetUserOrdersAsync(Guid userId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Game)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .Select(o => new OrderDto
                {
                    Id = o.Id,
                    UserId = o.UserId,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    OrderItems = o.OrderItems.Select(oi => new OrderItemDto
                    {
                        Id = oi.Id,
                        OrderId = oi.OrderId,
                        GameId = oi.GameId,
                        GameTitle = oi.Game.Title,
                        PriceAtPurchase = oi.PriceAtPurchase
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<(bool Success, string Message)> PurchaseGameAsync(Guid userId, Guid gameId)
        {
            // 1. Взимаме потребителя и играта
            var user = await _context.Users.FindAsync(userId);
            var game = await _context.Games.FindAsync(gameId);

            if (user == null || game == null)
                return (false, "Потребителят или играта не бяха намерени.");

            // 2. Проверка дали вече притежава играта
            var ownsGame = await _context.LibraryItems.AnyAsync(l => l.UserId == userId && l.GameId == gameId);
            if (ownsGame)
                return (false, "Вече притежавате тази игра във вашата библиотека.");

            // 3. Проверка за наличност на средства
            if (user.WalletBalance < game.Price)
                return (false, "Нямате достатъчно средства в портфейла.");

            // === ТРАНЗАКЦИЯ: Започваме същинската покупка ===

            // Стъпка А: Взимаме му парите
            user.WalletBalance -= game.Price;

            // Стъпка Б: Създаваме Поръчка
            var order = new Order
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                TotalAmount = game.Price
            };
            await _context.Orders.AddAsync(order);

            // Стъпка В: Създаваме OrderItem
            var orderItem = new OrderItem
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                GameId = gameId,
                PriceAtPurchase = game.Price
            };
            await _context.OrderItems.AddAsync(orderItem);

            // Стъпка Г: Добавяме играта в Библиотеката му
            var libraryItem = new LibraryItem
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                GameId = gameId,
                PurchaseDate = DateTime.UtcNow,
                PlayTimeMinutes = 0
            };
            await _context.LibraryItems.AddAsync(libraryItem);

            // Стъпка Д: Ако играта е била в Wishlist-а, я премахваме автоматично
            var wishlistItem = await _context.Wishlists.FirstOrDefaultAsync(w => w.UserId == userId && w.GameId == gameId);
            if (wishlistItem != null)
            {
                _context.Wishlists.Remove(wishlistItem);
            }

            // Запазваме всичко наведнъж. Ако нещо гръмне, нищо няма да се запише (EF Core го прави автоматично!)
            await _context.SaveChangesAsync();

            return (true, "Успешна покупка! Играта е добавена във вашата библиотека.");
        }
    }
}