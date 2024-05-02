namespace API.Middleware;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseCustomMiddlewareHandler(this WebApplication app)
    {
        return app.UseMiddleware<ExceptionHandleMiddleware>();
    }
}