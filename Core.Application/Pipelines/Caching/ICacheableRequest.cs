namespace Core.Application.Pipelines.Caching;

public interface ICachableRequest
{
    string? CacheKey { get; } //ilgili request'in unique key'i, request'in cache'ine key üzerinden oluşacağız
    bool BypassCache { get; } //development için
    string? CacheGroupKey { get; } //cache gruplama için
    TimeSpan? SlidingExpiration { get; } //cache süresi
}
