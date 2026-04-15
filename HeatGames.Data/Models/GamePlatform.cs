using System;

namespace HeatGames.Data.Models
{
    public class GamePlatform
    {
        // Не слагаме [Key] тук, защото ще го дефинираме като съставен ключ във Fluent API (по-късно)

        public Guid GameId { get; set; }
        public Game Game { get; set; } = null!;

        public Guid PlatformId { get; set; }
        public Platform Platform { get; set; } = null!;
    }
}