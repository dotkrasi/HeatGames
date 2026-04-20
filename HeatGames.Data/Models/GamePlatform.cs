using System;

namespace HeatGames.Data.Models
{
    public class GamePlatform
    {

        public Guid GameId { get; set; }
        public Game Game { get; set; } = null!;

        public Guid PlatformId { get; set; }
        public Platform Platform { get; set; } = null!;
    }
}