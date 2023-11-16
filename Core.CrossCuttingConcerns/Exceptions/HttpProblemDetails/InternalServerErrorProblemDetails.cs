using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails;

internal class InternalServerErrorProblemDetails : ProblemDetails
{
    //.Net'te hali hazırda olan ProblemDetails'i implemente ediyoruz. Doğrudan ProblemDetails'i de kullanabiliriz ama ileride buraya ihtiyacımız olacak başka şeylerde eklemek isteyebiliriz. O yüzden Problem Details'i implemente ediyorum.

    public InternalServerErrorProblemDetails(string detail)
    {
        Title = "Internal Server Error";
        Detail = "Internal Server Error";
        Status = StatusCodes.Status500InternalServerError;
        Type = "https://example.com/probs/internal"; // Hataları dökumante etmek için. Bu hatayı alan geliştirici hatanın detaylarını öğrenmek için ziyaret etmesi gereken adresi belirtiyoruz.
    }
}