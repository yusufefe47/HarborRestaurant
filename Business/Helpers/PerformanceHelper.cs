namespace HarborRestaurant.Business.Concrete
{
    public static class PerformanceHelper
    {
        /// <summary>
        /// Generate cache key based on action, language and parameters
        /// </summary>
        public static string GenerateCacheKey(string controller, string action, string language = "tr", params object[] parameters)
        {
            var key = $"{controller}_{action}_{language}";
            
            if (parameters?.Length > 0)
            {
                var paramString = string.Join("_", parameters.Where(p => p != null).Select(p => p.ToString()));
                if (!string.IsNullOrEmpty(paramString))
                {
                    key += $"_{paramString}";
                }
            }
            
            return key.ToLowerInvariant();
        }

        /// <summary>
        /// Get cache duration based on data type
        /// </summary>
        public static TimeSpan GetCacheDuration(string dataType)
        {
            return dataType.ToLowerInvariant() switch
            {
                "static" => TimeSpan.FromHours(24), // Static content like about, contact info
                "menu" => TimeSpan.FromHours(4), // Menu items can change during the day
                "rooms" => TimeSpan.FromHours(2), // Room availability changes frequently
                "blog" => TimeSpan.FromHours(1), // Blog posts and comments update regularly
                "home" => TimeSpan.FromMinutes(30), // Homepage content updates frequently
                "reservation" => TimeSpan.FromMinutes(5), // Reservation data changes very frequently
                _ => TimeSpan.FromMinutes(15) // Default short cache
            };
        }

        /// <summary>
        /// Determine if request should be cached based on various factors
        /// </summary>
        public static bool ShouldCache(string controller, string action, bool isAuthenticated = false)
        {
            // Don't cache for authenticated users (personalized content)
            if (isAuthenticated)
                return false;

            // Don't cache POST requests or form submissions
            var nonCacheableActions = new[] { "create", "update", "delete", "submit", "send", "book", "reserve" };
            if (nonCacheableActions.Any(a => action.ToLowerInvariant().Contains(a)))
                return false;

            // Don't cache admin actions
            if (controller.ToLowerInvariant().Contains("admin"))
                return false;

            return true;
        }

        /// <summary>
        /// Get cache tags for cache invalidation
        /// </summary>
        public static string[] GetCacheTags(string controller, string action)
        {
            var tags = new List<string> { controller.ToLowerInvariant() };

            // Add specific tags based on controller
            switch (controller.ToLowerInvariant())
            {
                case "home":
                    tags.AddRange(new[] { "homepage", "featured", "highlights" });
                    break;
                case "menu":
                    tags.AddRange(new[] { "menu", "categories", "items" });
                    break;
                case "rooms":
                    tags.AddRange(new[] { "rooms", "availability" });
                    break;
                case "blog":
                    tags.AddRange(new[] { "blog", "posts", "categories" });
                    break;
                case "about":
                    tags.AddRange(new[] { "about", "static" });
                    break;
                case "contact":
                    tags.AddRange(new[] { "contact", "static" });
                    break;
            }

            return tags.ToArray();
        }
    }
}
