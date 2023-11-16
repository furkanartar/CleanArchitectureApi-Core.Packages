using Core.CrossCuttingConcerns.Exceptions.Types;
using FluentValidation;
using MediatR;
using ValidationException = Core.CrossCuttingConcerns.Exceptions.Types.ValidationException;

namespace Core.Application.Pipelines.Validation;

public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    //TRequest: CreateBrandCommand
    //TResponse: CreateBrandResponse

    //bunu uyguladığımız noktada her request ve response için bir validator varsa bu handle'ı çalıştır

    private readonly IEnumerable<IValidator<TRequest>> _validators; //command'in validator'larına ulaşıyoruz

    public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators) // burada bize fluent validation yardımcı oluyor IoC'de validator'ları veriyor
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        ValidationContext<object> context = new(request);

        IEnumerable<ValidationExceptionModel> errors = _validators
            .Select(validator => validator.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(failure => failure != null)
            .GroupBy(
                keySelector: p => p.PropertyName,
                resultSelector: (propertyName, errors) =>
                    new ValidationExceptionModel { Property = propertyName, Errors = errors.Select(e => e.ErrorMessage) }
            ).ToList();

        if (errors.Any()) //eğer validation'dan geçemediyse yani hata varsa hata fırlatıyoruz
            throw new ValidationException(errors);

        TResponse response = await next(); //hata yoksa çalışmayı devam ettiriyoruz
        return response;
    }
}
