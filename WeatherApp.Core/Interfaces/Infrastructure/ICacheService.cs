using System;
using System.Threading.Tasks;

namespace WeatherApp.Core.Interfaces.Infrastructure
{
    public interface ICacheService
    {
        /// <summary>
        /// Gets item from cache
        /// </summary>
        Task<T> GetAsync<T>(string key) where T : class;

        /// <summary>
        /// Sets item in cache with expiration
        /// </summary>
        Task SetAsync<T>(string key, T value, TimeSpan? expirationTime = null) where T : class;

        /// <summary>
        /// Removes item from cache
        /// </summary>
        Task RemoveAsync(string key);
    }
}