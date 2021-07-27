namespace Checkout.MVC.Web.Caching.CacheKeys
{
    public interface ICacheKey<TItem>
    {
        string CacheKey { get; }
    }
}
