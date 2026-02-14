
using DomanLayer.Models.ProductModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersistenceLayer.Data.Configrations
{
    public class  ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(pt => pt.ProductBrand)
                .WithMany()
                .HasForeignKey(pt => pt.BrandId);

            builder.HasOne(pt => pt.ProductType)
                   .WithMany()
                   .HasForeignKey(pt => pt.TypeId);

            
            builder.Property(p => p.Name).HasColumnType("nvarchar(200)");
            builder.Property(p => p.Description).HasColumnType("nvarchar(500)");
            builder.Property(p => p.Price).HasColumnType("decimal(10,2)");
        }

       
    }
}
