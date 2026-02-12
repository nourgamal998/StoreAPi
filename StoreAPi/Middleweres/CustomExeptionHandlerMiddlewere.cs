using Azure;
using DomanLayer.Exeptions;
using Microsoft.AspNetCore.Http;
using ServiceStack.Text;
using Shared.ErrorModels;

namespace StoreAPi.CustomMiddleweres
{
    public class CustomExeptionHandlerMiddlewere
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExeptionHandlerMiddlewere> _logger;

        public CustomExeptionHandlerMiddlewere(RequestDelegate Next, ILogger<CustomExeptionHandlerMiddlewere> logger)
        {
            _next = Next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpcontext)
        {
            try
            {
                await _next.Invoke(httpcontext);
                if (httpcontext.Response.StatusCode == StatusCodes.Status404NotFound)
                    await HandleNotFoundEndPointAsync(httpcontext);

            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "something went wrong");
                await HandleExceptionAsync(httpcontext , ex );
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpcontext, Exception ex)
        {
            // create response object
            var response = new ErrorToReturn()
            {
                StatusCode = httpcontext.Response.StatusCode,
                ErrorMessage = ex.Message
            };
           
            // set statuse code for response
            httpcontext.Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                BadRequestException badRequestException=> GetBadRequestErrors(badRequestException, response),
                _ => StatusCodes.Status500InternalServerError

            };
            response.StatusCode = httpcontext.Response.StatusCode;

            // return object as json
            await httpcontext.Response.WriteAsJsonAsync(response);

        }
        private static int GetBadRequestErrors(BadRequestException badRequestException 
                                              , ErrorToReturn response)
        {
            response.Errors = badRequestException.Errors;
            return StatusCodes.Status400BadRequest;

        }

        private static async Task HandleNotFoundEndPointAsync(HttpContext httpcontext)
        {
            var response = new ErrorToReturn()
            {
                StatusCode = StatusCodes.Status404NotFound,
                ErrorMessage = $"End Point {httpcontext.Request.Path} Is Not Found "
            };

            await httpcontext.Response.WriteAsJsonAsync(response);
        }
    }
}
