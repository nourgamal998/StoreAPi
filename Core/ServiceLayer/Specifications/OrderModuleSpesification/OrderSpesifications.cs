using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.OrderModels;
using DomanLayer.Specifications;

namespace ServiceLayer.Specifications.OrderModuleSpesification
{
    public class OrderSpesifications : BaseSpecifications<Order,Guid>
    {
        public OrderSpesifications(string Email): base (O=>O.UserEmail==Email) 
        {
            AddInclude(O=>O.DeliveryMethod);
            AddInclude(O=>O.Items);
            AddOrderByDescending(O => O.OrderDate);
        }

    }
}
