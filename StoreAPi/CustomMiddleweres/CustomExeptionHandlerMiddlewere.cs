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
                await HandleExceptionAsync(httpcontext, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpcontext, Exception ex)
        {

            // ste statuse code for response
            httpcontext.Response.StatusCode = ex switch
            {
                NotFoundExeption => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError

            };
            // create response object
            var response = new ErrorToReturn()
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                ErrorMessage = ex.Message
            };
            // return object as json
            await httpcontext.Response.WriteAsJsonAsync(response);
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
