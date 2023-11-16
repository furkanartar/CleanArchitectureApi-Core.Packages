namespace Core.CrossCuttingConcerns.Exceptions.Types;

public class ValidationException : Exception
{
    public IEnumerable<ValidationExceptionModel> Errors { get; }
    public ValidationException()
           : base()
    {
        Errors = Array.Empty<ValidationExceptionModel>();
    }

    public ValidationException(string? message)
        : base(message)
    {
        Errors = Array.Empty<ValidationExceptionModel>();
    }

    public ValidationException(string? message, Exception? innerException)
        : base(message, innerException)
    {
        Errors = Array.Empty<ValidationExceptionModel>();
    }

    public ValidationException(IEnumerable<ValidationExceptionModel> errors)
        : base(BuildErrorMessage(errors))
    {
        Errors = errors;
    }

    private static string BuildErrorMessage(IEnumerable<ValidationExceptionModel> errors)
    {
        IEnumerable<string> arr = errors.Select(
            x => $"{Environment.NewLine} -- {x.Property}: {string.Join(Environment.NewLine, values: x.Errors ?? Array.Empty<string>())}"
        );
        return $"Validation failed: {string.Join(string.Empty, arr)}";
    }
}

public class ValidationExceptionModel // burada business exception gibi sadece mesaj döndürmek istemiyoruz. Hangi property'de hangi hatalar olduğunu da döndürmek istiyoruz.
{
    public string? Property { get; set; } //hangi field'da hata var?
    public IEnumerable<string>? Errors { get; set; } //hangi hatalar var?
}