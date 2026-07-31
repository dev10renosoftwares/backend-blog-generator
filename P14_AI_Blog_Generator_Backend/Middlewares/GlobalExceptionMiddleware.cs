using System.Net;
using System.Text.Json;
using P14_AI_Blog_Generator_Backend.Exceptions;
using P14_AI_Blog_Generator_Backend.Models.DomainModels;
using P14_AI_Blog_Generator_Backend.ApiResponse;

namespace P14_AI_Blog_Generator_Backend.Middlewares;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;

    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    private readonly IWebHostEnvironment _environment;

    public GlobalExceptionMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionMiddleware> logger,
        IWebHostEnvironment environment)
    {
        _next = next;
        _logger = logger;
        _environment = environment;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Unhandled exception. TraceId: {TraceId}",
                context.TraceIdentifier);

            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        HttpStatusCode statusCode;
        string message;

        switch (exception)
        {
            case BadRequestException:
                statusCode = HttpStatusCode.BadRequest;
                message = exception.Message;
                break;

            case UnauthorizedException:
                statusCode = HttpStatusCode.Unauthorized;
                message = exception.Message;
                break;

            case ForbiddenException:
                statusCode = HttpStatusCode.Forbidden;
                message = exception.Message;
                break;

            case NotFoundException:
                statusCode = HttpStatusCode.NotFound;
                message = exception.Message;
                break;

            case ConflictException:
                statusCode = HttpStatusCode.Conflict;
                message = exception.Message;
                break;

            default:
                statusCode = HttpStatusCode.InternalServerError;
                message = _environment.IsDevelopment()
                    ? exception.Message
                    : "An unexpected error occurred.";
                break;
        }

        context.Response.StatusCode = (int)statusCode;

        var response = new ApiResponse<object>
        {
            Success = false,
            Message = message,
            Errors = _environment.IsDevelopment()
    ? new List<string> { exception.ToString() }
    : null
        };

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(response));
    }
}