using Core.CrossCuttingConcerns.Exceptions.Handlers;
using Microsoft.AspNetCore.Http;

namespace Core.CrossCuttingConcerns.Exceptions;

public class ExceptionMiddileware //burada amacımız tüm kodlara ayrı ayrı try-catch yazmadan hataları tek bir noktada yönetmek
{
    private readonly RequestDelegate _next; //.Net bu isimlendirme standartını kullandığı için _next kullanımını yaptım
    //_next aslında çalıştırılacak/invoke edilecek metot, kısacası yazdığımız fonksiyonlar buradaki middleware'dan geçecek

    private readonly HttpExceptionHandler _httpExceptionHandler; //http handler'ımızı ekliyoruz

    public ExceptionMiddileware(RequestDelegate next)
    {
        _next = next;
        _httpExceptionHandler = new HttpExceptionHandler(); // 1 tane olduğu için burada varsayılanda instance'ını oluşturuyorum, eğer masaüstü app'de olsaydı parametre olarak alırdık
    }

    public async Task Invoke(HttpContext context) //klasik middleware, artık tüm kodlar bu middleware içindeki try-catch bloğundan geçecek.
    {
        try
        {
            await _next.Invoke(context); //api'den gelen isteği çalıştır
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context.Response, exception);
        }
    }

    private Task HandleExceptionAsync(HttpResponse response, Exception exception)
    {
        response.ContentType = "application/json";
        _httpExceptionHandler.Response = response;
        return _httpExceptionHandler.HandleExceptionAsync(exception);
    }
}
