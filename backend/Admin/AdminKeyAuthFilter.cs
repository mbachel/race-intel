namespace RaceIntel.Api.Admin;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class AdminKeyAuthFilter : IAsyncActionFilter
{
    private readonly IConfiguration _config;

    public AdminKeyAuthFilter(IConfiguration config)
    {
        _config = config;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var configuredKey = _config["Admin:Key"];

        if (string.IsNullOrWhiteSpace(configuredKey))
        {
            context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
            return;
        }

        if (!context.HttpContext.Request.Headers.TryGetValue("X-Admin-Key", out var provided) ||
            provided.Count != 1 ||
            !string.Equals(provided[0], configuredKey, StringComparison.Ordinal))
        {
            context.Result = new UnauthorizedObjectResult(new { error = "Unauthorized" });
            return;
        }

        await next();
    }
}