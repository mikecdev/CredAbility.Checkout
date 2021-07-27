using Checkout.MVC.Web.Caching.CacheKeys;

namespace Checkout.MVC.Web.Caching
{
    public interface ICacheStore
    {
        void Add<TItem>(TItem item, ICacheKey<TItem> key);

        void Add<TItem>(TItem item, string key);

        TItem Get<TItem>(ICacheKey<TItem> key) where TItem : class;
    }
}
