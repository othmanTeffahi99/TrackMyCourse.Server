using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace TrackMyCourseApi.Extensions
{
	public static class DistributedCacheExtensions
	{
		private static DistributedCacheEntryOptions desributedCacheEntryOptions = new DistributedCacheEntryOptions
		{
			AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60),
			SlidingExpiration = TimeSpan.FromSeconds(30)
		};

		public static async Task<T?> GetOrCreateAsync<T>(this IDistributedCache distributedCache, string key, Func<Task<T>> Factory, DistributedCacheEntryOptions? distributedCacheEntryOptions = null , CancellationToken cancellationToken = default)
		{
			ArgumentException.ThrowIfNullOrEmpty(key, nameof(key));

			var cachedData = await distributedCache.GetAsync(key, cancellationToken);
			if (cachedData is not null)
			{
				return JsonSerializer.Deserialize<T>(cachedData)!;
			}
			var data = await Factory();


			await distributedCache.SetStringAsync(key, JsonSerializer.Serialize(data), distributedCacheEntryOptions ?? desributedCacheEntryOptions, cancellationToken);
			return data;

		}
	}
}
