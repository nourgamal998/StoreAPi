using Shared.DTOS.OrderDTOS;

namespace ServiceApstractionLayer
{
    public interface IOrderService
    {
        //Create Order 
        Task<OrderToReturnDto> CreateOrderAsync(OrderDto orderDto, string email);
        
        //GetDeliveryMethod
        Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync();

        //getAllOrders of spesific customer 
        Task<IEnumerable<OrderToReturnDto>> GetAllOrdersAsync(string email);
        Task<OrderToReturnDto> GetOrderByIdAsync(Guid id);

    }
}
