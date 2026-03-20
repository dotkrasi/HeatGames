using HeatGames.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HeatGames.Data.Configuration
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            // Връзка с Developer (Един разработчик -> Много игри)
            builder.HasOne(g => g.Developer)
                   .WithMany(d => d.Games)
                   .HasForeignKey(g => g.DeveloperId)
                   .OnDelete(DeleteBehavior.Restrict); // Не даваме да се трие разработчик, ако има игри

            // Гарантираме правилен тип за цената в SQL
            builder.Property(g => g.Price)
                   .HasColumnType("decimal(18,2)");
        }
    }
}