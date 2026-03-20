using HeatGames.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HeatGames.Data.Configuration
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasOne(oi => oi.Order)
                   .WithMany(o => o.OrderItems)
                   .HasForeignKey(oi => oi.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Връзка към играта (няма колекция OrderItems в Game, затова WithMany е празно)
            builder.HasOne(oi => oi.Game)
                   .WithMany()
                   .HasForeignKey(oi => oi.GameId)
                   .OnDelete(DeleteBehavior.Restrict); // Не трием играта, ако е в поръчка

            builder.Property(oi => oi.PriceAtPurchase)
                   .HasColumnType("decimal(18,2)");
        }
    }
}