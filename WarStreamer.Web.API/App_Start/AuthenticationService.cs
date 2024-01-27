using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography;
using AspNet.Security.OAuth.Discord;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using WarStreamer.Commons.Extensions;
using WarStreamer.Commons.Tools;
using WarStreamer.Interfaces.Maps;
using WarStreamer.ViewModels;
using WarStreamer.Web.API.Authentication;
using WarStreamer.Web.API.Models;

namespace WarStreamer.Web.API.App_Start
{
    public class AuthenticationService
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             CONSTANTS                             *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private static readonly string CONFIG_AES = "AesKey";

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        private readonly DiscordConfig _discordConfig;
        private readonly JwtConfig _jwtConfig;

        private readonly string _aesKey;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public AuthenticationService(HttpClient httpClient, IConfiguration configuration)
        {
            // Inputs
            {
                _httpClient = httpClient;
                _configuration = configuration;
            }

            // Tools
            {
                _discordConfig =
                    configuration.GetSection<DiscordConfig>()
                    ?? throw new ArgumentNullException(
                        nameof(configuration),
                        "Invalid Discord Key"
                    );

                _jwtConfig =
                    configuration.GetSection<JwtConfig>()
                    ?? throw new ArgumentNullException(nameof(configuration), "Invalid JWT key");

                _aesKey =
                    configuration.GetValue<string>(CONFIG_AES, null!)
                    ?? throw new ArgumentNullException(nameof(configuration), "Invalid AES key");
            }
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public string BuildJwtToken(DiscordUser user, string nonce)
        {
            // Build the security
            JwtSecurityTokenHandler tokenHandler = new();

            // Create RSA instance and import private key
            using RSA rsa = RSA.Create();
            rsa.ImportFromPem(_jwtConfig.PrivateKey);

            // Export parameters and create security key
            RSAParameters parameters = rsa.ExportParameters(true);
            RsaSecurityKey securityKey = new(parameters) { KeyId = _jwtConfig.KeyId };

            // Build token
            SecurityTokenDescriptor tokenDescriptor =
                new()
                {
                    Subject = new ClaimsIdentity(
                        new[]
                        {
                            new Claim(JwtClaimTypes.Subject, user.Id),
                            new Claim(JwtClaimTypes.Name, user.Username),
                            new Claim(JwtClaimTypes.Nonce, nonce),
                            new Claim(JwtClaimTypes.Role, "User")
                        }
                    ),
                    Expires = DateTime.UtcNow.AddHours(2),
                    SigningCredentials = new SigningCredentials(
                        securityKey,
                        SecurityAlgorithms.RsaSha256
                    ),
                    Issuer = _jwtConfig.Domain,
                    Audience = _jwtConfig.Audience,
                };

            // Create whole token
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string DecryptToken(string cipherToken, string initVector)
        {
            AesEncryption aes = new(_aesKey);
            return aes.Decrypt(cipherToken, initVector);
        }

        public string EncryptToken(string plainToken, out string initVector)
        {
            AesEncryption aes = new(_aesKey);
            return aes.Encrypt(plainToken, out initVector);
        }

        public async Task<DiscordAuthTokens> GetDiscordTokens(string code, string codeVerifier)
        {
            // Create a new HttpClient
            _httpClient.DefaultRequestHeaders.Clear();

            // Build parameters
            KeyValuePair<string, string>[] parameters =
            [
                new KeyValuePair<string, string>("client_id", _discordConfig.ClientId),
                new KeyValuePair<string, string>("client_secret", _discordConfig.ClientSecret),
                new KeyValuePair<string, string>(
                    "grant_type",
                    OpenIdConnectGrantTypes.AuthorizationCode
                ),
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("code_verifier", codeVerifier),
                new KeyValuePair<string, string>("redirect_uri", _discordConfig.RedirectUri),
                new KeyValuePair<string, string>("scope", "identify email"),
            ];

            // Make the request to Discord
            HttpResponseMessage response = await _httpClient.PostAsync(
                DiscordAuthenticationDefaults.TokenEndpoint,
                new FormUrlEncodedContent(parameters)
            );

            response.EnsureSuccessStatusCode();

            // Get the token from response
            string content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<DiscordAuthTokens>(content)!;
        }

        public async Task<DiscordAuthTokens> GetDiscordTokens(string discordRefreshToken)
        {
            // Create a new HttpClient
            _httpClient.DefaultRequestHeaders.Clear();

            // Build parameters
            KeyValuePair<string, string>[] parameters =
            [
                new KeyValuePair<string, string>("client_id", _discordConfig.ClientId),
                new KeyValuePair<string, string>("client_secret", _discordConfig.ClientSecret),
                new KeyValuePair<string, string>(
                    "grant_type",
                    OpenIdConnectGrantTypes.RefreshToken
                ),
                new KeyValuePair<string, string>("refresh_token", discordRefreshToken),
            ];

            // Make the request to Discord
            HttpResponseMessage response = await _httpClient.PostAsync(
                DiscordAuthenticationDefaults.TokenEndpoint,
                new FormUrlEncodedContent(parameters)
            );

            response.EnsureSuccessStatusCode();

            // Get the token from response
            string content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<DiscordAuthTokens>(content)!;
        }

        public async Task<DiscordUser> GetUserInformations(string discordAccessToken)
        {
            // Create a new HttpClient
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer",
                discordAccessToken
            );

            // Make the request to discord
            HttpResponseMessage response = await _httpClient.GetAsync(
                DiscordAuthenticationDefaults.UserInformationEndpoint
            );

            response.EnsureSuccessStatusCode();

            // Get the user from response
            string content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<DiscordUser>(content)!;
        }

        /* * * * * * * * * * * * * * * * * *\
        |*              STATIC             *|
        \* * * * * * * * * * * * * * * * * */

        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            // Get configurations
            JwtConfig jwtConfiguration = configuration.GetSection<JwtConfig>();

            // Create RSA instance and import private key
            using RSA rsa = RSA.Create();
            rsa.ImportFromPem(jwtConfiguration.PrivateKey);

            // Export parameters and create security key
            RSAParameters parameters = rsa.ExportParameters(true);
            RsaSecurityKey securityKey = new(parameters) { KeyId = jwtConfiguration.KeyId };

            // Build configurations
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = $"https://{jwtConfiguration.Domain}";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = securityKey,
                        ValidIssuer = jwtConfiguration.Domain,
                        ValidAudience = jwtConfiguration.Audience,
                        ValidAlgorithms = new[] { SecurityAlgorithms.RsaSha256 },
                        NameClaimType = JwtClaimTypes.Name,
                        RoleClaimType = JwtClaimTypes.Role,
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context => AuthenticationFailed(context),
                        OnTokenValidated = context => TokenValidated(context),
                    };
                });

            services.AddHttpClient<AuthenticationService>();
        }

        private static Task AuthenticationFailed(AuthenticationFailedContext context)
        {
            // Get the refresh token map
            IAuthTokenMap? authTokenMap =
                context.HttpContext.RequestServices.GetService<IAuthTokenMap>();

            // Verify it exists
            if (authTokenMap == null)
            {
                return Task.CompletedTask;
            }

            // Fetch the expired token
            string? expiredToken = context
                .Request.Headers.Authorization.FirstOrDefault()
                ?.Split(" ")
                .Last();

            // Verify it exists
            if (expiredToken == null)
            {
                return Task.CompletedTask;
            }

            // If the exception is SecurityTokenExpired and the request is refresh
            if (
                context.Exception is SecurityTokenExpiredException
                && context.Request.Path == "/auth/refresh"
            )
            {
                JwtSecurityTokenHandler handler = new();

                // If token can't be parsed
                if (handler.ReadToken(expiredToken) is not JwtSecurityToken jsonToken)
                {
                    return Task.CompletedTask;
                }

                // Get the authentication token from the JWT token
                AuthTokenViewModel? authToken = authTokenMap.GetByUserId(jsonToken.Subject);

                // Verify it exists
                if (authToken == null)
                {
                    return Task.CompletedTask;
                }

                // Get AES private key
                string aesKey =
                    context
                        .HttpContext.RequestServices.GetService<IConfiguration>()
                        ?.GetValue<string>(CONFIG_AES, null!)
                    ?? throw new ArgumentNullException(nameof(context), "Invalid AES key");

                // Decrypt registered access key
                AesEncryption aes = new(aesKey);
                string oldAccessToken = aes.Decrypt(authToken.AccessToken, authToken.AccessIV);

                // If the tokens aren't the same, deny the access
                if (oldAccessToken != expiredToken)
                {
                    return Task.CompletedTask;
                }

                // Create temporary a claims identity with only the user id
                ClaimsIdentity claims =
                    new(
                        new Claim[] { new(JwtClaimTypes.Subject, jsonToken.Subject), },
                        "temp_refresh_authorization"
                    );

                context.Principal = new ClaimsPrincipal(claims);
                context.Success();
            }

            return Task.CompletedTask;
        }

        private static Task TokenValidated(TokenValidatedContext context)
        {
            // Get the refresh token map
            IAuthTokenMap? authTokenMap =
                context.HttpContext.RequestServices.GetService<IAuthTokenMap>();

            // Verify it exists
            if (authTokenMap == null)
            {
                return Task.CompletedTask;
            }

            // Ensure it's a jwt token
            if (context.SecurityToken is not JsonWebToken jwtToken)
            {
                return Task.CompletedTask;
            }

            // Get the authentication token from the JWT token
            AuthTokenViewModel? authToken = authTokenMap.GetByUserId(jwtToken.Subject);

            // Verify it exists
            if (authToken != null)
            {
                return Task.CompletedTask;
            }

            // Otherwise, throw an unauthorize exception
            context.Fail(new UnauthorizedAccessException());

            return Task.CompletedTask;
        }
    }
}
