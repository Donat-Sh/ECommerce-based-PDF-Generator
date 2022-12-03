using System;

namespace Services.Interfaces.Shared
{
    public interface ICacheService
    {
        #region GetClientIdFromApiKey

        Guid GetClientIdFromApiKey(string apiKey);
        
        #endregion GetClientIdFromApiKey
    }
}
