namespace Footballito.Api.Middleware;

public static class ExceptionMiddlewareExtensions
{
    public static WebApplication UseExceptionMiddleware(this WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        return app;
    }
}
