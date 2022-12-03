using System;

namespace Services.Interfaces
{
    public interface IApiKeyService
    {
        #region Authentication

        #region ApiKey-Validation

        Task<bool> ValidateApiKey(string apiKeyValue, CancellationToken cancellationToken);

        #endregion ApiKey-Validation

        #endregion Authentication

        #region Generation

        #region GenerateApiKey

        string GenerateApiKey();

        #endregion GenerateApiKey

        #endregion Generation
    }
}
