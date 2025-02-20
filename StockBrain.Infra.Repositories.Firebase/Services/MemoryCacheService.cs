
using System.Runtime.Caching;

namespace StockBrain.Infra.Repositories.Firebase.Services;

public static class  MemoryCacheService
{
	private static readonly ObjectCache Cache = MemoryCache.Default;

	public static T GetOrAdd<T>(string key, Func<T> fetchFunction, int expirationInSeconds = 20)
	{
		if (Cache.Contains(key))
			return (T)Cache[key];

		T result = fetchFunction();
		Cache.Set(key, result, DateTimeOffset.Now.AddSeconds(expirationInSeconds));
		return result;
	}

}
