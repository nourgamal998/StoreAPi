using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.OrderModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace PersistenceLayer.Data.Configurations
{
    public class OrderConfigurations :
        IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            //builder.ToTable("Orders");

            //builder.Property(o => o.SubTotal).HasColumnType("decimal(8,2)");

            //builder.HasMany(o => o.Items).WithOne();

            //builder.HasOne(o => o.DeliveryMethod)
            //       .WithMany().HasForeignKey(o => o.DeliveryMethodId);

            //builder.OwnsOne(o => o.Address);
            builder.Property(x => x.SubTotal).HasPrecision(8, 2);
            builder.OwnsOne(x => x.Address, OEntity => 
            {
                OEntity.Property(x => x.FirstName).HasMaxLength(100);
                OEntity.Property(x => x.LastName).HasMaxLength(100);
                OEntity.Property(x => x.City).HasMaxLength(100);
                OEntity.Property(x => x.Country).HasMaxLength(100);
                OEntity.Property(x => x.Street).HasMaxLength(200);

            });

        }
    }
}



