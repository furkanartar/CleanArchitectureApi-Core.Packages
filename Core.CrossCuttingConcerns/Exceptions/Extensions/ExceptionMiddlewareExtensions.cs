using Microsoft.AspNetCore.Builder;

namespace Core.CrossCuttingConcerns.Exceptions.Extensions;

public static class ExceptionMiddlewareExtensions // extension olduğu için static
{
    public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app) //yazdığımız middleware'i sisteme entegre ediyoruz
        => app.UseMiddleware<ExceptionMiddileware>();
}
