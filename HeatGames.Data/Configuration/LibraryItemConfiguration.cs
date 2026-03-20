using HeatGames.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HeatGames.Data.Configuration
{
    public class LibraryItemConfiguration : IEntityTypeConfiguration<LibraryItem>
    {
        public void Configure(EntityTypeBuilder<LibraryItem> builder)
        {
            builder.HasOne(li => li.User)
                   .WithMany(u => u.LibraryItems)
                   .HasForeignKey(li => li.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(li => li.Game)
                   .WithMany()
                   .HasForeignKey(li => li.GameId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}