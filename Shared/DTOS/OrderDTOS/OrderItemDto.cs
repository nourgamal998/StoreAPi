using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOS.OrderDTOS
{
    public class OrderItemDto
    {
        public string productName { get; set; } = null!;
        public  string PictureUrl { get; set; } = null!;
        public decimal price { get; set; }
        public int Quantity { get; set; }

    }
}
