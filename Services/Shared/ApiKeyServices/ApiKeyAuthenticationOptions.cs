using Microsoft.AspNetCore.Authentication;
using System;

namespace Services.Shared.ApiKeyServices
{
    public class ApiKeyAuthenticationOptions : AuthenticationSchemeOptions
    {
        public const string DefaultScheme = "ClientKey";
        public const string HeaderName = "x-api-key";
    }
}
