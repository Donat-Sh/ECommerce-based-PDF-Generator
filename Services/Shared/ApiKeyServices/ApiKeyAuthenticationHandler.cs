using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Services.Interfaces.Shared;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Services.Shared.ApiKeyServices
{
    public class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationOptions>
    {
        #region Properties

        private readonly ICacheService _cacheService;

        #endregion Properties

        #region Ctor

        public ApiKeyAuthenticationHandler(IOptionsMonitor<ApiKeyAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, ICacheService cacheService) : base(options, logger, encoder, clock)
        {
            _cacheService = cacheService;
        }

        #endregion Ctor

        #region HandleAuthenticateAsync

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue(ApiKeyAuthenticationOptions.HeaderName, out var apiKey) || apiKey.Count != 1)
            {
                Logger.LogWarning("Error, someon made an Api request without 'x-api-key' header");
                return AuthenticateResult.Fail("Bad Header Parameters for ApiKey");
            }

            var clientId = _cacheService.GetClientIdFromApiKey(apiKey);

            if (clientId == null)
            {
                Logger.LogWarning($"Error for ApiKey without an invalid API key: {apiKey}");
                return AuthenticateResult.Fail("Bad Header Parameters for ApiKey");
            }

            Logger.BeginScope("{ClientId}", clientId);
            Logger.LogInformation("Client has been Successfully Authenticated!");

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, clientId.ToString())
            };
            
            var identity = new ClaimsIdentity(claims, ApiKeyAuthenticationOptions.DefaultScheme);
            var identities = new List<ClaimsIdentity> { identity };
            var principal = new ClaimsPrincipal(identities);
            var ticket = new AuthenticationTicket(principal, ApiKeyAuthenticationOptions.DefaultScheme);

            return AuthenticateResult.Success(ticket);
        }

        #endregion HandleAuthenticateAsync
    }
}
