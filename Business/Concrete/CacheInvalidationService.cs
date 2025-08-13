using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.Distributed;

namespace HarborRestaurant.Business.Concrete
{
    public class CacheInvalidationService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IDistributedCache? _distributedCache;

        public CacheInvalidationService(IMemoryCache memoryCache, IDistributedCache? distributedCache = null)
        {
            _memoryCache = memoryCache;
            _distributedCache = distributedCache;
        }

        /// <summary>
        /// Invalidate specific cache entries when data is updated
        /// </summary>
        public async Task InvalidateCacheAsync(string? pattern = null)
        {
            // Clear memory cache entries
            if (!string.IsNullOrEmpty(pattern))
            {
                // For specific pattern-based cache invalidation
                var cacheKeys = GetCacheKeys(pattern);
                foreach (var key in cacheKeys)
                {
                    _memoryCache.Remove(key);
                }
            }

            // Clear distributed cache if available
            if (_distributedCache != null && !string.IsNullOrEmpty(pattern))
            {
                try
                {
                    await _distributedCache.RemoveAsync(pattern);
                }
                catch
                {
                    // Ignore Redis connection errors
                }
            }
        }

        /// <summary>
        /// Clear all cache when major data changes occur
        /// </summary>
        public async Task ClearAllCacheAsync()
        {
            // Clear memory cache
            if (_memoryCache is MemoryCache mc)
            {
                mc.Clear();
            }

            // Clear distributed cache if available
            if (_distributedCache != null)
            {
                try
                {
                    // Note: Redis doesn't have a built-in clear all command for specific patterns
                    // This would need to be implemented based on your specific Redis setup
                    // For now, we'll just clear common cache keys
                    var commonKeys = new[] { "home_data", "menu_data", "rooms_data", "about_data", "blog_data" };
                    foreach (var key in commonKeys)
                    {
                        await _distributedCache.RemoveAsync(key);
                        await _distributedCache.RemoveAsync($"{key}_tr");
                        await _distributedCache.RemoveAsync($"{key}_en");
                    }
                }
                catch
                {
                    // Ignore Redis connection errors
                }
            }
        }

        private List<string> GetCacheKeys(string pattern)
        {
            // Common cache key patterns used in the application
            var keys = new List<string>();
            
            switch (pattern.ToLower())
            {
                case "home":
                    keys.AddRange(new[] { "home_data", "home_data_tr", "home_data_en" });
                    break;
                case "menu":
                    keys.AddRange(new[] { "menu_data", "menu_data_tr", "menu_data_en", "menu_categories" });
                    break;
                case "rooms":
                    keys.AddRange(new[] { "rooms_data", "rooms_data_tr", "rooms_data_en" });
                    break;
                case "about":
                    keys.AddRange(new[] { "about_data", "about_data_tr", "about_data_en" });
                    break;
                case "blog":
                    keys.AddRange(new[] { "blog_data", "blog_data_tr", "blog_data_en", "blog_posts", "blog_categories" });
                    break;
                default:
                    keys.Add(pattern);
                    break;
            }

            return keys;
        }
    }
}
