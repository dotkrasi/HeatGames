using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HeatGames.Core.DTOs
{
    public class GameDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The 'Title' field is required.")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "The title must be between 2 and 150 characters.")]
        [Display(Name = "Title")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "The 'Description' field is required.")]
        [MinLength(10, ErrorMessage = "The description must be at least 10 characters long.")]
        [Display(Name = "Description")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Please enter a price.")]
        [Range(0.00, 10000.00, ErrorMessage = "The price must be a positive number.")]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Please specify a release date.")]
        [DataType(DataType.Date)]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        [Url(ErrorMessage = "Please enter a valid URL for the image.")]
        [MaxLength(255)]
        [Display(Name = "Cover URL")]
        public string? CoverImageUrl { get; set; }

        [Required(ErrorMessage = "Developer selection is required.")]
        [Display(Name = "Developer")]
        public Guid DeveloperId { get; set; }

        public string? DeveloperName { get; set; }
        public IEnumerable<string> Genres { get; set; } = new List<string>();
        public IEnumerable<string> Platforms { get; set; } = new List<string>();

        public List<Guid> SelectedPlatformIds { get; set; } = new();
        public List<Guid> SelectedGenreIds { get; set; } = new List<Guid>();
    }
}