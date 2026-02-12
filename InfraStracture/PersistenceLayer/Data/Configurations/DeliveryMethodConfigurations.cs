using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.OrderModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersistenceLayer.Data.Confegations
{
    public class DeliveryMethodConfigurations: 
        IEntityTypeConfiguration<DeliveryMethod>
    {
       
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(x => x.Price).HasPrecision(18,2);

            builder.Property(x => x.ShortName).HasMaxLength(50);

            builder.Property(x => x.DeliveryTime).HasMaxLength(50);
            builder.Property(x => x.Description).HasMaxLength(100);
        }
    }
}
