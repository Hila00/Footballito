using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Footballito.Application.Exceptions;

namespace Footballito.Api.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var status = StatusCodes.Status500InternalServerError;
        var title = "An unexpected error occurred.";

        var problem = new ProblemDetails();

        switch (exception)
        {
            case NotFoundException nf:
                status = StatusCodes.Status404NotFound;
                title = "Not Found";
                problem.Detail = nf.Message;
                break;
            case ValidationException ve:
                status = StatusCodes.Status400BadRequest;
                title = "Validation Failed";
                problem.Detail = ve.Message;
                problem.Extensions["errors"] = ve.Errors;
                break;
            case ConflictException ce:
                status = StatusCodes.Status409Conflict;
                title = "Conflict";
                problem.Detail = ce.Message;
                break;
            case UnauthorizedException ue:
                status = StatusCodes.Status401Unauthorized;
                title = "Unauthorized";
                problem.Detail = ue.Message;
                break;
            case ArgumentException ae:
                status = StatusCodes.Status400BadRequest;
                title = "Bad Request";
                problem.Detail = ae.Message;
                break;
            default:
                problem.Detail = exception.Message;
                break;
        }

        problem.Title = title;
        problem.Status = status;
        problem.Instance = context.TraceIdentifier;

        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        var result = JsonSerializer.Serialize(problem, options);

        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = status;

        _logger.LogError(exception, "Request {TraceId} failed with {Status}: {Message}", context.TraceIdentifier, status, exception.Message);

        return context.Response.WriteAsync(result);
    }
}
