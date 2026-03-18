
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatGames.Data.Models
{
    public class User : IdentityUser<Guid>
    {
        [MaxLength(255)]
        public string? ProfilePictureUrl { get; set; }

        public decimal WalletBalance { get; set; } = 0.00m;

        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

        // Навигационни свойства    
        public ICollection<Order> Orders { get; set; }
        public ICollection<LibraryItem> LibraryItems { get; set; }
        public ICollection<Wishlist> Wishlists { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}

