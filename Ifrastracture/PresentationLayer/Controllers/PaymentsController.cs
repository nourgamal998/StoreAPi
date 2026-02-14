using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Controllers;
using ServiceAbstractionlayer;
using ServiceApstractionLayer;
using Shared.DTOS.BasketDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using ServiceLayer;

namespace PresentationLayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : BasketController
    {
        private readonly IServiceManager _serviceManager;

        public PaymentsController(IServiceManager serviceManager) : base(serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [Authorize]
        [HttpPost("{basketId}")]
        public async Task<ActionResult<BasketDto>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket =  _serviceManager.PaymentService;
            return Ok(basket);
        }

        #region Copied
        //stripe listen --forward-to http://localhost:7040/api/Payments/webhook

        [HttpPost("webhook")]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            //Logic
            await _serviceManager.PaymentService.UpdatePaymentStatus(json,
                Request.Headers["Stripe-Signature"]);

            return new EmptyResult();
        }

        #endregion
    }
}
