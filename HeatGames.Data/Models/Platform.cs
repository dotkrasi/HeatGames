using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HeatGames.Data.Models
{
    public class Platform
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        // Навигационно свойство за много-към-много връзката
        public ICollection<GamePlatform> GamePlatforms { get; set; } = new List<GamePlatform>();
    }
}