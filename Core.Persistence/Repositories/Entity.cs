namespace Core.Persistence.Repositories;

public class Entity<TId>
{
    public TId Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }//nullable bu alanlar zorunlu olmadığı için null geçilebileceğini belirtiyoruz
    public DateTime? DeletedDate { get; set; }//nullable

    public Entity()
    {
        Id = default; //! id için değer verilmemişse mesela TId'nin tipi int ise 0 değerini vermesini istiyoruz
    }

    public Entity(TId id)
    {
        Id = id;
    }
}
