using System.Net;
using BugTicketingSystem.BL.Dtos.Common;
using BugTicketingSystem.BL.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace BugTicketingSystem.API.MiddleWare
{
    //public class ExceptionHandling
    //{
    //    private readonly RequestDelegate _next;
    //    private readonly ILogger<ExceptionHandling> _logger;
    //    public ExceptionHandling(
    //        RequestDelegate next,
    //        ILogger<ExceptionHandling> logger)
    //    {
    //        _next = next;
    //        _logger = logger;
    //    }
    //    public async Task InvokeAsync(HttpContext context)
    //    {
    //        try
    //        {
    //            await _next(context);
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError(ex, "An unhandled exception occurred.");
    //            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    //            await context.Response.WriteAsJsonAsync(new { error = "An error occurred while processing your request." });
    //        }
    //    }
    //    private static Task HandleExceptionAsync(HttpContext context)
    //    {
    //        context.Response.ContentType = "application/json";
    //        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    //        var response = new
    //        {
    //            StatusCode = 500,
    //            Message = "Internal Server Error from the custom middleware."
    //        };
    //        return context.Response.WriteAsJsonAsync(response);
    //    }
    //}
    public class BuiltInExceptionHandling : IExceptionHandler
    {
        private readonly ILogger<BuiltInExceptionHandling> _logger;

        public BuiltInExceptionHandling(ILogger<BuiltInExceptionHandling> logger)
        {
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            if (exception is BusinessValidationExceptions ex)
            {
                httpContext.Response.StatusCode =
                    (int)HttpStatusCode.BadRequest;
                httpContext.Response.ContentType = "application/json";

                await httpContext.Response.WriteAsJsonAsync(
                    ex.Errors.Select(e => new ResultError
                    {
                        Code = e.ErrorCode,
                        Message = e.ErrorMessage,
                    }), cancellationToken);
                return true;

            }
            else if (exception is FluentValidation.ValidationException validationEx)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                httpContext.Response.ContentType = "application/json";

                var errors = validationEx.Errors.Select(e => new ResultError
                {
                    Code = e.ErrorCode ?? "ERR-VALIDATION",
                    Message = e.ErrorMessage,
                });

                await httpContext.Response.WriteAsJsonAsync(errors, cancellationToken);
                return true;
            }

            else
            {
                _logger.LogError(exception, exception.Message);
                httpContext.Response.StatusCode =
                    (int)HttpStatusCode.InternalServerError;
            }
            return true;
        }
    }
}
