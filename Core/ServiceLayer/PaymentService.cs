using AutoMapper;
using DomainLayer.Models.OrderModels;
using DomainLayer.Models;
using DomanLayer.Contracts;
using DomanLayer.Exeptions;
using Microsoft.Extensions.Configuration;
using ServiceAbstractionlayer;
using ServiceLayer.Specifications.OrderModuleSpecifications;
using Shared.DTOS.BasketDTOS;
using Stripe;


namespace ServiceLayer.Services
{
    public class PaymentService(IConfiguration _configuration, 
                                IBasketReposatory _basketRepository,
                                IUnitOfWork _unitOfWork,
                                IMapper _mapper) : IPaymentService
    {
        public async Task<BasketDto> CreateOrUpdatePaymentIntent(string basketId)
        {
            //Configure Stripe : Install Package Stripe.Net

            StripeConfiguration.ApiKey=_configuration["StripeSettings:SecretKey"];

            //Get Basket By Id
            var basket = await _basketRepository.GetBasketAsync(basketId)               
            ??throw new BasketNotFoundException(basketId);


            //Get Ammount (products Qty * price)+Delivery Method Cost


            var productRepo = _unitOfWork.GetRepository<DomanLayer.Models.ProductModels.Product, int>();
          foreach (var item in basket.Items)
            {
                var originalProduct = await productRepo.GetByIdAsync(item.Id)
                    ?? throw new ProductNotFoundException(item.Id);
                item.Price=originalProduct.Price;
            }
            ArgumentNullException.ThrowIfNull(basket.DeliveryMethodId);
            var deliveryMehtod = await _unitOfWork.GetRepository<DeliveryMethod, int>()
                                .GetByIdAsync(basket.DeliveryMethodId!) ??
               throw new DeliveryMethodNotFoundException(basket.DeliveryMethodId!);

            basket.ShippingPrice=deliveryMehtod.Price;
            var basketAmount = (long)(basket.Items.Sum(item => item.Quantity * item.Price)+deliveryMehtod.Price)*100;
            //Create Payment Intent Id
            var paymentServie = new PaymentIntentService();
            if(basket.PaymentIntentId is null)//Create
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount=basketAmount,
                    Currency="USD",
                    PaymentMethodTypes= ["card"]
                };
                var paymentIntent = await paymentServie.CreateAsync(options);
                basket.PaymentIntentId=paymentIntent.Id;
                basket.ClientSecret=paymentIntent.ClientSecret;

            }
            else //Update
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount=basketAmount,
                };
                object value = await paymentServie.UpdateAsync(basket.PaymentIntentId, options);
                
            }
            await _basketRepository.CreatOrUpdateBasketAsync(basket);
            return _mapper.Map<BasketDto>(basket);
        }



        public async Task UpdatePaymentStatus(string jsonRequest, string stripeHeader)
        { 
            var stripeEvent = EventUtility.ConstructEvent(jsonRequest,
                      stripeHeader, _configuration["StripeSettings:EndPointSecret"]);


            var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
            if (stripeEvent.Type == EventTypes.PaymentIntentPaymentFailed)
            {
                await UpdatePaymentFailedAsync(paymentIntent.Id);
            }
            else if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded)
            {
                await UpdatePaymentReceivedAsync(paymentIntent.Id);

            }
            // ... handle other event types
            else
            {
                Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
            }
        }

        private async Task UpdatePaymentReceivedAsync(string paymentIntentId)
        { 
            var order = await _unitOfWork.GetRepository<Order, Guid>()
                .GetByIdAsync(new OrderWithPaymentIntentIdSpecifications(paymentIntentId));

            order.OrderStatus = OrderStatus.PaymentReceived;

            _unitOfWork.GetRepository<Order, Guid>().Update(order);
            await _unitOfWork.SaveChangesAsync();
        }

        private async Task UpdatePaymentFailedAsync(string paymentIntentId)
        {
            var order = await _unitOfWork.GetRepository<Order, Guid>()
                .GetByIdAsync(new OrderWithPaymentIntentIdSpecifications(paymentIntentId));

            order.OrderStatus = OrderStatus.PaymentFailed;

            _unitOfWork.GetRepository<Order, Guid>().Update(order);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
