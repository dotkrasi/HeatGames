using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HeatGamesWeb.ViewModels
{
    public class GameViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Title must be between 2 and 150 characters.")]
        [Display(Name = "Game Title")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Description is required.")]
        [Display(Name = "Description")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Please enter a price.")]
        [Range(0.00, 10000.00, ErrorMessage = "Price must be a positive number.")]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Please provide a release date.")]
        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [MaxLength(255)]
        [Display(Name = "Cover URL")]
        public string? CoverImageUrl { get; set; }

        [Required(ErrorMessage = "Please select a developer.")]
        [Display(Name = "Developer")]
        public Guid DeveloperId { get; set; }

        [Display(Name = "Platforms")]
        public List<Guid> SelectedPlatformIds { get; set; } = new();
        public List<Guid> SelectedGenreIds { get; set; } = new List<Guid>();
        public IEnumerable<string> Platforms { get; set; } = new List<string>();
        public IEnumerable<string> Genres { get; set; } = new List<string>();

        // 🎯 НОВИ СВОЙСТВА ЗА ИНДИКАТОРИТЕ:
        public bool IsInCart { get; set; }
        public bool IsInWishlist { get; set; }
    }
}