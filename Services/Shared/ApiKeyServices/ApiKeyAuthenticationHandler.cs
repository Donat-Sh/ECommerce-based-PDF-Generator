using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Services.Shared.ApiKeyServices
{
    public class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationOptions>
    {
        #region Ctor

        public ApiKeyAuthenticationHandler(IOptionsMonitor<ApiKeyAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
            
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

            var clientId = await GetClientIdFromApiKey(apiKey);

            if (clientId == null)
            {
                Logger.LogWarning($"Error for ApiKey without an invalid API key: {apiKey}");
                return AuthenticateResult.Fail("Bad Header Parameters for ApiKey");
            }

            Logger.BeginScope("{ClientId}", clientId);
            Logger.LogInformation("Client has been Successfully Authenticated!");

            var claims = new[]
            {
                //new Claim(ClaimTypes.Name, clientId.ToString())
                new Claim(clientId.ToString())
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
