using DDDSample.Domain.OrderAggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDSample.InfraStructure.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.CustomerName).HasMaxLength(200).IsRequired();
            builder.Property(o => o.Status).HasConversion<int>();
            builder.Property(o => o.CreatedAtUtc);


            builder.HasMany(typeof(OrderItem), nameof(Order.Items))
                .WithOne()
                .HasForeignKey(nameof(OrderItem.OrderId));
        }
    }
}
