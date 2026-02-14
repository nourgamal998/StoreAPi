using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.OrderModels;
using DomanLayer.Models;

//namespace DomainLayer.Models.OrderModels
//{
//    public class Order: BaseEntity<Guid>
//    {

//        public Order(string userEmail,OrderAddress address, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal, object paymentIntentId)
//        {
//            UserEmail = userEmail;
//            Address = address;
//            DeliveryMethod = deliveryMethod;

//            Items = items;
//            SubTotal = subTotal;
//        }

//        public string UserEmail { get; set; } = null!;
//        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
//        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
//        public OrderAddress Address { get; set; } = null!;
//        public DeliveryMethod DeliveryMethod { get; set; } = null!; 

//        public int DeliveryMethodId { get; set; } //FK

//        public ICollection<OrderItem> Items { get; set; } = [];

//        public decimal SubTotal { get; set; }
//        [NotMapped]
//        public decimal GetTotal { get => SubTotal + DeliveryMethod.Price; }

//    }
//}
public class Order : BaseEntity<Guid>
{
    public Order(
        string userEmail,
        OrderAddress address,
        DeliveryMethod deliveryMethod,
        ICollection<OrderItem> items,
        decimal subTotal,
        string? paymentIntentId)
    {
        UserEmail = userEmail;
        Address = address;
        DeliveryMethod = deliveryMethod;
        Items = items;
        SubTotal = subTotal;
        PaymentIntentId = paymentIntentId;
    }

    public string UserEmail { get; set; } = null!;
    public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
    public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
    public OrderAddress Address { get; set; } = null!;
    public DeliveryMethod DeliveryMethod { get; set; } = null!;
    public int DeliveryMethodId { get; set; }

    public ICollection<OrderItem> Items { get; set; } = [];

    public decimal SubTotal { get; set; }

    public string? PaymentIntentId { get; set; }  

    [NotMapped]
    public decimal GetTotal => SubTotal + DeliveryMethod.Price;
}
