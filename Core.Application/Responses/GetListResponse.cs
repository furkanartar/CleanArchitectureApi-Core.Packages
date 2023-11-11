using Core.Persistence.Paging;

namespace Core.Application.Responses;

public class GetListResponse<T> : BasePageableModel //hangi eleman için çalışacağız
{
    private IList<T> _items;

    public IList<T> Items
    {
        get => _items ??= new List<T>(); // _items varsa onu dön yoksa boş liste oluştur.
        set => _items = value;
    }
}