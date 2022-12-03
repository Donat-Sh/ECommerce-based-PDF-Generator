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

        #region Helpers

        public Dictionary<string, Guid> GetCurrentExistingClients() => new Dictionary<string, Guid>()
        {
            { "asdasd", 1231312312 }
        };

        #endregion Helpers
    }
}
