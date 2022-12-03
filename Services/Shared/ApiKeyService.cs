using AutoMapper;
using Microsoft.Extensions.Logging;
using Persistence.Context;

namespace Services.Shared
{
    public class ApiKeyService
    {
        #region Properties

        private readonly PdfApiContext _pdfApiContext;
        private readonly ILogger<ApiKeyService> _logger;
        private readonly IMapper _mapper;

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

        #region Authentication

        #region ApiKey-Validation

        public async Task<bool> ValidateApiKey(string apiKeyValue, CancellationToken cancellationToken)
        {
            try
            {
                //code...

                return false;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Error happened on {nameof(ValidateApiKey)}, whilst attempting to Validate given ApiKey Value for Authentication!");

                throw;
            }
        }

        #endregion ApiKey-Validation

        #endregion Authentication

        #region Helpers

        #endregion Helpers
    }
}
