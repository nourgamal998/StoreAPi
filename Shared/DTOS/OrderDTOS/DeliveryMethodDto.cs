using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOS.OrderDTOS
{
    public class DeliveryMethodDto
    {
        public int id {  get; set; }
        public string ShortName { get; set; }
        public string description { get; set; }

        public string DeliveryTime { get; set; }
        public decimal Price { get; set; }


    }
}
