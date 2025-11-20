using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomanLayer.Models.BasketModels
{
    public class CustomerBasket 
    {
        public string Id { get; set; } //GUID :created from client "frontend"
        public ICollection<BasketItem>? Items { get; set; } 
    }
}
