namespace Core.Persistence.Paging;
// gelişen ihtiyaçlar doğrultusunda ezilebilmesi için abstract yapıyoruz
public abstract class BasePageableModel // Application katmanında Response'larda kullanacağımız model
{
    public int Size { get; set; }
    public int Index { get; set; }
    public int Count { get; set; } // Burası verinin büyüklüğüne göre long vs olabilir.
    public int Pages { get; set; }
    public bool HasPrevious { get; set; }
    public bool HasNext { get; set; }
}
