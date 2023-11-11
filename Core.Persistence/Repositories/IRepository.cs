using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Core.Persistence.Repositories;

//Senkron repository
public interface IRepository<TEntity, TEntityId> : IQuery<TEntity>
    where TEntity : Entity<TEntityId>
{
    //TEntity -> hangi entity ile çalışacağız? (Ex: brand)
    //TEntityId -> brand'in id alanının tipini temsil ediyor. (Ex: Brand için Guid kullanıyoruz)
    //where -> TEntityId olarak aldığımız tipin gerçekten Entity'nin id tipi mi olduğunu kontrol ediyoruz. (Ex: Brand için Guid kullanıyoruz)


    TEntity? Get(
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
        );

    /*
        predicate -> Expression Linq kütüphanesinden gelen lambda ile where koşulu olacak. Bu sayede kullanıcıyı id sınırlamayacağız.
        include -> Join desteği sağlıyor. IIncludableQueryable Linq üzerinden değil, entity framework üzerinden geliyor. Ayrıca join zorunlu olmadığı için defaultta null olarak tanımladım.
        withDeleted -> Silinen verileri getireim mi? Soft delete ile çalıştığımız için soft delete ile silinen verilerin getirilip getirilmeyeceğini soruyoruz. defaultta false
        enableTracking -> entity framework'ün tracking özelliğinin kullanılıp kullanılmayacağını soruyorum
        cancellationToken -> asenkron işlem olduğu için iptal etme durumu için alıyoruz. default değer olarak default tanımlı
    */

    Paginate<TEntity> GetList(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
        );
    /*
        Get fonksiyonundan farkları:
        predicate artık zorunlu değil, where şartı koşmayabiliriz.
        orderBy kullanarak datayı sıralayabiliriz.
        index ve size kullanarak sayfalama yapacağız
        ayrıca return yaptığımız datada IPaginate'den gelen sayfalama yapısı olacak
    */

    Paginate<TEntity> GetListByDynamic(
        DynamicQuery dynamic,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
        );
    /*
        GetList fonksiyonunda farkları;
        dinamik olarak filtreleyebiliriz. Mesela araç listesinde sol tarafta motor gücü, marka, renk, model gibi filtreleri dinamik olarka gönderip filtrelemeyi sağlar.
        orderBy ile sıralama özelliği yok.
     */


    //veritabanında ilgili şartlara uyan en az 1 veri var mı?
    bool Any(
        Expression<Func<TEntity, bool>>? predicate = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
        );

    //ilgili entity'i db'ye ekleme
    TEntity Add(TEntity entity);

    //toplu ekleme
    ICollection<TEntity> AddRange(ICollection<TEntity> entities);

    //update
    TEntity Update(TEntity entity);

    //toplu update
    ICollection<TEntity> UpdateRange(ICollection<TEntity> entities);

    //silme işlemi, parmanent kalıcı olarak silme durumunu soruyor. değer false ise soft delete yapıyoruz.
    TEntity Delete(TEntity entity, bool permanent = false);

    ICollection<TEntity> DeleteRange(ICollection<TEntity> entities, bool permanent = false);
}
