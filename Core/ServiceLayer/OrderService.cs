using DomanLayer.Models.ProductModels;
using AutoMapper;
using DomainLayer.Models.OrderModels;
using DomanLayer.Contracts;
using DomanLayer.Exeptions;
using ServiceApstractionLayer;
using Shared.DTOS.OrderDTOS;


namespace ServiceLayer
{
    public class OrderService(IMapper _mapper, IBasketReposatory _basketRepository,
        IUnitOfWork _unitOfWork) : IOrderService
    {

        public async Task<OrderToReturnDto> CreateOrderAsync(OrderDto orderDto, string email)
        {
            //Map Address

            var orderAddress = _mapper.Map<OrderAddress>(orderDto.Address);

            var basket = await _basketRepository.GetBasketAsync(orderDto.BasketId)
                       ?? throw new BasketNotFoundException(orderDto.BasketId);


            //Create order items
            List<OrderItem> orderItems = [];
            var productRepo = _unitOfWork.GetRepository<Product, int>();
            foreach (var basketItem in basket.Items)
            {
                var OriginalProduct = await productRepo.GetByIdAsync(basketItem.Id)
                    ?? throw new ProductNotFoundException(basketItem.Id);

                var orderItem = new OrderItem()
                {
                    Product = new ProductItemOrdered()
                    {
                        ProductId = OriginalProduct.Id,
                        ProductName = OriginalProduct.Name,
                        PictureUrl = OriginalProduct.PictureUrl,
                    },
                    Price = OriginalProduct.Price,
                    Quantity = basketItem.Quantity

                };
                orderItems.Add(orderItem);
            }

            //Get delivery method 
           var deliveryMethod =await _unitOfWork.GetRepository<DeliveryMethod, int>()
                                               .GetByIdAsync(orderDto.DeliveryMethodId)
                ?? throw new DeliveryMethodNotFoundException(orderDto.DeliveryMethodId);

            //Subtotal
            var subtotal = orderItems.Sum(i => i.Price * i.Quantity);

            //Create Order Object
            var order = new Order(email, orderAddress, deliveryMethod, orderItems, subtotal);
            
            await _unitOfWork.GetRepository<Order,Guid>().AddAsync(order);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<OrderToReturnDto>(order);
        }
    }
}




