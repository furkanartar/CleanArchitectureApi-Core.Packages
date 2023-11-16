using Core.CrossCuttingConcerns.Exceptions.Types;

namespace Core.CrossCuttingConcerns.Exceptions.Handlers;

public abstract class ExceptionHandler
{
    //gelen hataları bu class'ı implemente eden yerde işleyeceğiz. Ex: HttpExceptionHandler ileride desktop app için harici handler yazabilmeyi sağlıyoruz
    public Task HandleExceptionAsync(Exception exception) =>
        exception switch /* bu switch kullanımında tür üzerinden ayrım yapıyoruz.*/
        {
            BusinessException businessException => HandleException(businessException),//validation exception, authorization exception vs olabilir
            _ => HandleException(exception)
        };

    protected abstract Task HandleException(BusinessException businessException); // gelen exception BusinessException ise burası çalışacak
    protected abstract Task HandleException(Exception exception); // gelen exception switch içerisinde hiç birisine uymaz ise burası çalışacak
}
