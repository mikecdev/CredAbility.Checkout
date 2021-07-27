using Checkout.MVC.Web.Caching;
using Checkout.MVC.Web.Models.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Checkout.MVC.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static void ConfigureCaching(this IServiceCollection services, IConfiguration configuration)
        {
            var cachingoptions = new CachingOptions();
            configuration.GetSection("CachingOptions").Bind(cachingoptions);

            services.AddMemoryCache(x => { x.SizeLimit = cachingoptions.CacheSizeLimit; });

            services.AddSingleton<ICacheStore, MemoryCacheStore>();
        }
    }
}
