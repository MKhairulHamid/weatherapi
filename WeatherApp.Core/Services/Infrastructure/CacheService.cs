using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WeatherApp.Core.Interfaces.Infrastructure;

namespace WeatherApp.Core.Services.Infrastructure
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        private readonly ILogger<CacheService> _logger;

        public CacheService(IMemoryCache cache, ILogger<CacheService> logger)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task<T> GetAsync<T>(string key) where T : class
        {
            try
            {
                var value = _cache.Get<T>(key);
                return Task.FromResult(value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving item from cache with key {Key}", key);
                return Task.FromResult<T>(null);
            }
        }

        public Task SetAsync<T>(string key, T value, TimeSpan? expirationTime = null) where T : class
        {
            try
            {
                var options = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = expirationTime ?? TimeSpan.FromHours(1)
                };

                _cache.Set(key, value, options);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error setting item in cache with key {Key}", key);
                return Task.CompletedTask;
            }
        }

        public Task RemoveAsync(string key)
        {
            try
            {
                _cache.Remove(key);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing item from cache with key {Key}", key);
                return Task.CompletedTask;
            }
        }
    }
}