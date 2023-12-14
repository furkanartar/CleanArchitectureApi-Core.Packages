using Core.CrossCuttingConcerns.Exceptions.Handlers;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.SeriLog;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Core.CrossCuttingConcerns.Exceptions;

public class ExceptionMiddileware //burada amacımız tüm kodlara ayrı ayrı try-catch yazmadan hataları tek bir noktada yönetmek
{
    private readonly RequestDelegate _next; //_next aslında çalıştırılacak/invoke edilecek metot, kısacası yazdığımız fonksiyonlar buradaki middleware'dan geçecek
    private readonly HttpExceptionHandler _httpExceptionHandler; //http handler'ımızı ekliyoruz
    private readonly IHttpContextAccessor _httpContextAccessor; //httpContext'e erişmek için kullanıyoruz, kullanıcı bilgileri için kullanacağız
    private readonly LoggerServiceBase _loggerServiceBase;

    public ExceptionMiddileware(RequestDelegate next, IHttpContextAccessor httpContextAccessor, LoggerServiceBase loggerServiceBase)
    {
        _next = next;
        _httpExceptionHandler = new HttpExceptionHandler();
        _httpContextAccessor = httpContextAccessor;
        _loggerServiceBase = loggerServiceBase;
    }

    public async Task Invoke(HttpContext context) //klasik middleware, artık tüm kodlar bu middleware içindeki try-catch bloğundan geçecek.
    {
        try
        {
            await _next.Invoke(context); //api'den gelen isteği çalıştır
        }
        catch (Exception exception)
        {
            await logException(context, exception);
            await HandleExceptionAsync(context.Response, exception);
        }
    }

    private Task logException(HttpContext context, Exception exception)
    {
        List<LogParameter> logParameters = new()
        {
            new LogParameter{Type= context.GetType().Name, Value = exception.ToString()}
        };

        LogDetailWithException logDetail = new()
        {
            MethodName = _next.Method.Name,
            Parameters = logParameters,
            User = _httpContextAccessor.HttpContext?.User.Identity?.Name ?? "?",
            ExceptionMessage = exception.Message
        };

        _loggerServiceBase.Error(JsonSerializer.Serialize(logDetail));

        return Task.CompletedTask;
    }

    private Task HandleExceptionAsync(HttpResponse response, Exception exception)
    {
        response.ContentType = "application/json";
        _httpExceptionHandler.Response = response;
        return _httpExceptionHandler.HandleExceptionAsync(exception);
    }
}
