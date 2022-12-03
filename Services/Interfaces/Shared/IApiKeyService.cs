using System;

namespace Services.Interfaces.Shared
{
    public interface IApiKeyService
    {
        #region Generation

        #region GenerateApiKey

        string GenerateApiKey();

        #endregion GenerateApiKey

        #endregion Generation
    }
}
