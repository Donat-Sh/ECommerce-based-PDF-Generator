using Microsoft.Extensions.Caching.Memory;
using Persistence.Context;
using Services.Interfaces.Shared;

namespace Services.Shared.CachingService
{
    public class CacheService : ICacheService
    {
        #region Properties

        private readonly PdfApiContext _pdfApiContext;
        private readonly IMemoryCache _memoryCache;

        #endregion Properties

        #region Ctor

        public CacheService(
                                PdfApiContext pdfApiContext,
                                IMemoryCache memoryCache
                            )
        {
            _pdfApiContext = pdfApiContext;
            _memoryCache = memoryCache;
        }

        #endregion Ctor

        #region GetClientIdFromApiKey

        public Guid GetClientIdFromApiKey(string apiKey)
        {
            if (!_memoryCache.TryGetValue<Dictionary<string, Guid>>($"Authentication_ApiKeys", out var internalKeys))
            {
                internalKeys = GetCurrentExistingClients();

                _memoryCache.Set($"Authentication_ApiKeys", internalKeys);
            }

            if (!internalKeys.TryGetValue(apiKey, out var clientId))
                return Guid.Empty;

            return clientId;
        }

        #endregion GetClientIdFromApiKey

        #region InvalidateApiKey

        public void InvalidateApiKey(string apiKey)
        {
            if (_memoryCache.TryGetValue<Dictionary<string, Guid>>("Authentication_ApiKeys", out var internalKeys))
            {
                if (internalKeys.ContainsKey(apiKey))
                {
                    internalKeys.Remove(apiKey);
                    _memoryCache.Set("Authentication_ApiKeys", internalKeys);
                }
            }
        }

        #region InvalidateApiKey

        #region Helpers

        public Dictionary<string, Guid> GetCurrentExistingClients() => new Dictionary<string, Guid>()
        {
            { "X-API-KEY", new Guid("7a8a7cd837b042b58b56617114f4d3d7") }
        };

        #endregion Helpers
    }
}
