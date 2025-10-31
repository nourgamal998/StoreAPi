using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomanLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersistenceLayer.Data.Configations
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

        private void HasColumnType(string v)
        {
            throw new NotImplementedException();
        }
    }
}
