using FluentValidation;
using System.Net;
using System.Text.Json;

namespace HospitalCheckupSystem.API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            await WriteResponse(
                context,
                HttpStatusCode.BadRequest,
                new
                {
                    message = "Validation failed",
                    errors = ex.Errors.Select(e => new
                    {
                        field = e.PropertyName,
                        error = e.ErrorMessage
                    })
                });
        }
        catch (InvalidOperationException ex)
        {
            await WriteResponse(
                context,
                HttpStatusCode.Conflict,
                new
                {
                    message = ex.Message
                });
        }
        catch (KeyNotFoundException ex)
        {
            await WriteResponse(
                context,
                HttpStatusCode.NotFound,
                new
                {
                    message = ex.Message
                });
        }
        catch (Exception)
        {
            await WriteResponse(
                context,
                HttpStatusCode.InternalServerError,
                new
                {
                    message = "An unexpected error occurred"
                });
        }
    }

    private static async Task WriteResponse(
        HttpContext context,
        HttpStatusCode status,
        object body)
    {
        context.Response.StatusCode = (int)status;
        context.Response.ContentType = "application/json";

        var json = JsonSerializer.Serialize(body);
        await context.Response.WriteAsync(json);
    }
}