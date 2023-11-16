using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails;

internal class BusinessProblemDetails : ProblemDetails
{
    //.Net'te hali hazırda olan ProblemDetails'i implemente ediyoruz. Doğrudan ProblemDetails'i de kullanabiliriz ama ileride buraya ihtiyacımız olacak başka şeylerde eklemek isteyebiliriz.
    //Neden direk ProblemDetails'ı dönmediğimizin sebebi ValidationProblemDetails içerisinde yer almakta

    public BusinessProblemDetails(string detail)
    {
        Title = "Rule Violation";
        Detail = detail;
        Status = StatusCodes.Status400BadRequest;
        Type = "https://example.com/probs/business"; // Hataları dökumante etmek için. Bu hatayı alan geliştirici hatanın detaylarını öğrenmek için ziyaret etmesi gereken adresi belirtiyoruz.
    }
}
