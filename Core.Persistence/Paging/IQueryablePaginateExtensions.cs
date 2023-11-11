using Microsoft.EntityFrameworkCore;

namespace Core.Persistence.Paging;

//tüm dataları çekip sonra pagination yapmak istemiyoruz o yüzden query yazıp sayfalama yapacağız db'yi korumak için
public static class QueryablePaginateExtensions
{
    public static async Task<Paginate<T>> ToPaginateAsync<T>(this IQueryable<T> source, int index, int size, CancellationToken cancellationToken = default)
    {
        int count = await source.CountAsync(cancellationToken).ConfigureAwait(false);

        /*
            ConfigureAwait olayı

            Bu, default olarak çalışan main thread ile devam etmek yerine, yeni bir thread ile process execution meydana gelir.

            Yani API projesini ayağı kaldırıyorsun, artık çalışan api projesi için bellekte bir alan açılır (CIL, CLR, JIT ve CLI). İşte biz buna "Main Primary Thread" diyoruz. Bunu oluşturan mekanizmanın başında "CLR" rol alır. Eğer ki, "ConfigureAwait(false)" yazılmasaydı, ThreadPool sınıfı kullanılarak çalışan "Main Primary Thread" (ana iş parçacığı) kullanılır. Ancak, "ConfigureAwait(false)" yazıldığı için "Main Primary Thread" ile devam etmek yerine, yeni bir thread ile işleme devam edilir. Bu, işlemin daha hızlı ve daha etkili bir şekilde çalışmasını sağlar. Aslında bu sayede, "Main Primary Thread" bloke edilmeden diğer işlemleri yapmaya devam edebilir.
         */

        List<T> items = await source.Skip(index * size).Take(size).ToListAsync(cancellationToken).ConfigureAwait(false);

        Paginate<T> list = new()
        {
            Index = index,
            Count = count,
            Items = items,
            Size = size,
            Pages = (int)Math.Ceiling(count / (double)size)
        };

        return list;
    }

    public static Paginate<T> ToPaginate<T>(this IQueryable<T> source, int index, int size, CancellationToken cancellationToken = default)
    {
        int count = source.Count();

        List<T> items = source.Skip(index * size).Take(size).ToList();

        Paginate<T> list = new()
        {
            Index = index,
            Count = count,
            Items = items,
            Size = size,
            Pages = (int)Math.Ceiling(count / (double)size)
        };

        return list;
    }
}
