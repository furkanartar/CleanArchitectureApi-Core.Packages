namespace Core.Persistence.Repositories;

public class Entity<TId> : IEntityTimestamps
{
    public TId Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }

    public Entity()
    {
        Id = default; //! id için değer verilmemişse mesela TId'nin tipi örneğin int ise 0 değerini vermesini istiyoruz
    }

    public Entity(TId id)
    {
        Id = id;
    }
}
