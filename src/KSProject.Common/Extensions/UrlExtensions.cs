using Microsoft.AspNetCore.Http;

namespace KSProject.Common.Extensions;

public static class UrlExtensions
{
    public static string GetOnlyDomain(this HttpContext context)
    {
        return context.Request.Host.Host;
    }
}
