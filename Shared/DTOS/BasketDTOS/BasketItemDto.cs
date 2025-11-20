using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOS.BasketDTOS
{
    public class BasketItemDto
    {
        public int Id { get; set; } 
        public string ProductName { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;
        [Range (1,1000)] 
        public decimal Price { get; set; }
        public int Quantity { get; set; }

    }
}
