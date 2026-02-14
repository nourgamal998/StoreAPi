using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomanLayer.Models.BasketModels
{
    public class CustomerBasket 
    {
        public object? PaymentIntentId;
        public decimal ShippingPrice { get; set; }
        public string BasketId { get; set; } //GUID :created from client "frontend"
        public ICollection<BasketItem> Items { get; set; }
        public int DeliveryMethodId { get; set; }
        public string ClientSecret { get; set; }
    }
}
