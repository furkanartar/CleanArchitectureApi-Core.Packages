using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Core.CrossCuttingConcerns.Exceptions.Extensions;

public static class ProblemDetailsExtensions // extension olduğu için static
{
    //TProblemDetail tipindeki detail'i serialize et diyoruz. Amacımız json'a çeviren fonksiyonu yazmak.
    public static string AsJson<TProblemDetail>(this TProblemDetail details) where TProblemDetail : ProblemDetails =>
        JsonSerializer.Serialize(details);
}
