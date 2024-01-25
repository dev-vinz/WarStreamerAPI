using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography;
using AspNet.Security.OAuth.Discord;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using WarStreamer.Commons.Extensions;
using WarStreamer.Commons.Tools;
using WarStreamer.Web.API.Authentication;
using WarStreamer.Web.API.Models;

namespace WarStreamer.Web.API.App_Start
{
    public class AuthenticationService
    {
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
                    _configuration.GetSection<DiscordConfig>()
                    ?? throw new ArgumentNullException(nameof(_discordConfig));

                _jwtConfig =
                    _configuration.GetSection<JwtConfig>()
                    ?? throw new ArgumentNullException(nameof(_jwtConfig));

                _aesKey =
                    _configuration.GetValue<string>("AesKey", null!)
                    ?? throw new ArgumentNullException(nameof(_aesKey));
            }
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public string BuildRefreshTokenFromDiscord(
            string discordToken,
            out string initializationVector
        )
        {
            AesEncryption aes = new(_aesKey);
            return aes.Encrypt(discordToken, out initializationVector);
        }

        public async Task<AuthenticationToken> GetAccessToken(string code, string codeVerifier)
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

            return JsonConvert.DeserializeObject<AuthenticationToken>(content)!;
        }

        public string GetDiscordRefreshToken(string cipherToken, string initializationVector)
        {
            AesEncryption aes = new(_aesKey);
            return aes.Decrypt(cipherToken, initializationVector);
        }

        public string GetJwtToken(DiscordUser user, string nonce)
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
                            new Claim("sub", user.Id),
                            new Claim("name", user.GlobalName ?? string.Empty),
                            new Claim("username", user.Username),
                            new Claim("email", user.Email ?? string.Empty),
                            new Claim("avatarUrl", user.AvatarUrl),
                            new Claim("nonce", nonce)
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

        public async Task<DiscordUser> GetUserInformations(string accessToken)
        {
            // Create a new HttpClient
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer",
                accessToken
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

        public async Task<AuthenticationToken> RefreshAccessToken(string refreshToken)
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
                new KeyValuePair<string, string>("refresh_token", refreshToken),
            ];

            // Make the request to Discord
            HttpResponseMessage response = await _httpClient.PostAsync(
                DiscordAuthenticationDefaults.TokenEndpoint,
                new FormUrlEncodedContent(parameters)
            );

            response.EnsureSuccessStatusCode();

            // Get the token from response
            string content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<AuthenticationToken>(content)!;
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
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddHttpClient<AuthenticationService>();
        }
    }
}
