using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomanLayer.Models;

namespace DomainLayer.Models.OrderModels
{
    public class DeliveryMethod : BaseEntity<int>
    {
        public string DeliveryMethodId { get; set; } = default!;
        public string ShortName { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string DeliveryTime { get; set; } = default!;
        public decimal Price { get; set; } 
    }
}
