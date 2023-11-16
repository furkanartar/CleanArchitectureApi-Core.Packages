using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails;

public class BusinessProblemDetails : ProblemDetails
{
    //.Net'te hali hazırda olan ProblemDetails'i implemente ediyoruz. Doğrudan ProblemDetails'i de kullanabiliriz ama ileride buraya ihtiyacımız olacak başka şeylerde eklemek isteyebiliriz.

    public BusinessProblemDetails(string detail)
    {
        Title = "Rule Violation";
        Detail = detail;
        Status = StatusCodes.Status400BadRequest;
        Type = "https://example.com/probs/business"; // Hataları dökumante etmek için. Bu hatayı alan geliştirici hatanın detaylarını öğrenmek için ziyaret etmesi gereken adresi belirtiyoruz.
    }
}
