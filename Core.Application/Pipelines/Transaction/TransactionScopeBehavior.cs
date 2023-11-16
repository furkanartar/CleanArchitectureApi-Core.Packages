using MediatR;
using System.Transactions;

namespace Core.Application.Pipelines.Transaction;

public class TransactionScopeBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ITransactionalRequest
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        using TransactionScope transactionScope = new(TransactionScopeAsyncFlowOption.Enabled); // TransactionScopeAsyncFlowOption.Enabled async süreci aktif ediyoruz

        TResponse response;

        try
        {
            response = await next(); //next bizim metodumuz oluyor
            transactionScope.Complete();//fonksiyon başarıyla çalıştığı için süreci tamamlıyoruz
        }
        catch (Exception exception)
        {
            transactionScope.Dispose(); //hata olursa süreci iptal ediyoruz.
            throw;//transaction hatası fırlatıyoruz.
        }

        return response;
    }
}
