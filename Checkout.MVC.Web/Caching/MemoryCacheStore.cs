using Checkout.MVC.Web.Caching.CacheKeys;
using Checkout.MVC.Web.Models.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace Checkout.MVC.Web.Caching
{
    public class MemoryCacheStore : ICacheStore
    {
        private readonly IMemoryCache _memorycache;
        private readonly Dictionary<string, TimeSpan> _experationConfiguration;

        private readonly Dictionary<string, int> _sizeConfiguration;

        public MemoryCacheStore(IMemoryCache memorycache, IOptions<CachingOptions> cachingOptions)
        {
            _memorycache = memorycache;
            _experationConfiguration = cachingOptions.Value.Expiry;
            _sizeConfiguration = cachingOptions.Value.Size;
        }

        public void Add<TItem>(TItem item, ICacheKey<TItem> key)
        {
            var cacheObjectName = item.GetType().Name;
            var timespan = _experationConfiguration[cacheObjectName];
            var size = _sizeConfiguration[cacheObjectName];
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSize(size)
                .SetSlidingExpiration(timespan);

            this._memorycache.Set(key.CacheKey, item, cacheEntryOptions);
        }

        public void Add<TItem>(TItem item, string key)
        {
            var timespan = _experationConfiguration[key];
            var size = _sizeConfiguration[key];
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSize(size)
                .SetSlidingExpiration(timespan);

            this._memorycache.Set(key, item, cacheEntryOptions);
        }

        public TItem Get<TItem>(ICacheKey<TItem> key) where TItem : class
        {
            if (this._memorycache.TryGetValue(key.CacheKey, out TItem value))
                return value;

            return null;
        }
    }
}
