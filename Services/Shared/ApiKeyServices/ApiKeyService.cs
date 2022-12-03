using AutoMapper;
using Microsoft.Extensions.Logging;
using Persistence.Context;
using Services.Interfaces.Shared;
using System.Security.Cryptography;

namespace Services.Shared.ApiKeyServices
{
    public class ApiKeyService : IApiKeyService
    {
        #region Properties

        private readonly PdfApiContext _pdfApiContext;
        private readonly ILogger<ApiKeyService> _logger;
        private readonly IMapper _mapper;
        private const int _numberOfSecureBytesToGenerate = 32;

        #endregion Properties

        #region Ctor

        public ApiKeyService(
                                       PdfApiContext pdfApiContext,
                                       ILogger<ApiKeyService> logger,
                                       IMapper mapper
                                  )
        {
            _pdfApiContext = pdfApiContext;
            _logger = logger;
            _mapper = mapper;
        }

        #endregion Ctor

        #region Helpers

        #region GenerateApiKey

        public string GenerateApiKey()
        {
            const string _prefix = "CT-";
            const int _lengthOfKey = 36;

            var bytes = RandomNumberGenerator.GetBytes(_numberOfSecureBytesToGenerate);

            return string.Concat(_prefix, Convert.ToBase64String(bytes) //Base64 Encoding.
                         .Replace("/", "")
                         .Replace("+", "")
                         .Replace("=", "")
                         .AsSpan(0, _lengthOfKey - _prefix.Length));
        }

        #endregion GenerateApiKey

        #endregion Helpers
    }
}
