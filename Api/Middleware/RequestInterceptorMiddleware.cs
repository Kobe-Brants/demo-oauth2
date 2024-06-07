namespace Api.Middleware;

public class ContentSecurityPolicyMiddleware
{
    private readonly RequestDelegate _next;
    private const string  CspPolicy = "default-src 'self'; script-src 'self' 'unsafe-inline'; style-src 'self' 'unsafe-inline'; img-src 'self'; font-src 'self';";


    public ContentSecurityPolicyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        httpContext.Response.Headers.Append("Content-Security-Policy", CspPolicy);
        await _next(httpContext);
    }
}

public static class ContentSecurityPolicyMiddlewareExtensions
{
    public static void UseContentSecurityPolicy(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<ContentSecurityPolicyMiddleware>();
    }
}