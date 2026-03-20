using HeatGames.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HeatGames.Data.Configuration
{
    public class GameGenreConfiguration : IEntityTypeConfiguration<GameGenre>
    {
        public void Configure(EntityTypeBuilder<GameGenre> builder)
        {
            // 1. Дефинираме съставен първичен ключ (Composite Primary Key)
            // Тъй като таблицата е свързваща, нейният ключ е комбинация от GameId и GenreId
            builder.HasKey(gg => new { gg.GameId, gg.GenreId });

            // 2. Връзка: Една GameGenre запис сочи към ЕДНА Game, 
            // която има МНОГО GameGenres
            builder.HasOne(gg => gg.Game)
                   .WithMany(g => g.GameGenres)
                   .HasForeignKey(gg => gg.GameId)
                   .OnDelete(DeleteBehavior.Cascade); // Ако изтрием игра, трием и жанровете й

            // 3. Връзка: Един GameGenre запис сочи към ЕДИН Genre, 
            // който има МНОГО GameGenres
            builder.HasOne(gg => gg.Genre)
                   .WithMany(g => g.GameGenres)
                   .HasForeignKey(gg => gg.GenreId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}