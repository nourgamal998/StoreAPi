
using Shared.DTOS.OrderDTOS;

namespace ServiceApstractionLayer
{
    public interface IOrderService
    {
        //Create Order 
        Task<OrderToReturnDto> CreateOrderAsync(OrderDto orderDto, string email);
        //orderDto , email =>

    }
}
