using DomainLayer.Exceptions;
using Shared.ErrorModels;

namespace E_Commerce.Web.CustomMiddleWares
{
    public class CustomExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddleWare> _logger;

        public CustomExceptionHandlerMiddleWare(RequestDelegate Next , ILogger<CustomExceptionHandlerMiddleWare> logger)
        {
            _next = Next;
            this._logger = logger;
        }
        public async Task InvokeAsync(HttpContext context) 
        {
            try
            {
                await _next.Invoke(context);
                await HandleNotFoundEndPointAsync(context);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went Wrong");
                await HandleExpcetionAsync(context, ex);
            }
        }

        private static async Task HandleExpcetionAsync(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = ex switch
            {
                NotFoundExpceptions => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };
            var Response = new ErrorToReturn()
            {
                StatusCode = context.Response.StatusCode,
                ErrorMessage = ex.Message
            };
            await context.Response.WriteAsJsonAsync(Response);
        }

        private static async Task HandleNotFoundEndPointAsync(HttpContext context)
        {
            if (context.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var Response = new ErrorToReturn()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = $"End point you call = {context.Request.Path} is Not Found! "
                };
                await context.Response.WriteAsJsonAsync(Response);
            }
        }
    }
}
