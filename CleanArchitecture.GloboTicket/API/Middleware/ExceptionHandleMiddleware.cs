using System.Net;
using System.Text.Json;
using Application.Exceptions;
using HttpContext = Microsoft.AspNetCore.Http.HttpContext;

namespace API.Middleware;

public class ExceptionHandleMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandleMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            await ConvertExpression(context, e);
        }
    }

    private async Task ConvertExpression(HttpContext context, Exception exception)
    {
        HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

        context.Response.ContentType = "application/json";

        var result = string.Empty;

        switch (exception)
        {
            case BadHttpRequestException badRequestException:
                httpStatusCode = HttpStatusCode.BadRequest;
                result = badRequestException.Message;
                break;

            case NotFoundException:
                httpStatusCode = HttpStatusCode.NotFound;
                break;
            
            case FluentValidation.ValidationException validationException:
                httpStatusCode = HttpStatusCode.UnprocessableContent;
                var validationObject = new
                {
                    Message = validationException.Message,
                    Errors = new List<string>()
                };
                foreach (var failure in validationException.Errors)
                {
                    validationObject.Errors.Add(failure.ErrorMessage);
                }
                result = JsonSerializer.Serialize(validationObject);
                break;

            case not null:
                httpStatusCode = HttpStatusCode.BadRequest;
                break;
        }

        context.Response.StatusCode = (int)httpStatusCode;

        if (string.IsNullOrEmpty(result))
        {
            result = JsonSerializer.Serialize(new { error = exception?.Message });
        }

        await context.Response.WriteAsync(result);
    }
}