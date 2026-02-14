using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceApstractionLayer;
using Shared.DTOS.OrderDTOS;

namespace PresentationLayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController (IServiceManager _serviceManager) : ApiBaseControllers
    {
        [Authorize]
        [HttpPost] //post baseurl ,api,order
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {
           var orderToReturn= await _serviceManager.OrderService.
                CreateOrderAsync(orderDto, GetEmailFromToken());
            return Ok(orderToReturn);
        }
    }
}
