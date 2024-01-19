using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using AspNet.Security.OAuth.Discord;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
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

        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _redirectUri;

        private readonly string _jwtSecretKey;
        private readonly string _jwtDomain;
        private readonly string _jwtAudience;

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
                _clientId =
                    _configuration.GetValue<string>("Discord:ClientId", null!)
                    ?? throw new ArgumentNullException("Discord ClientId is null");

                _clientSecret =
                    _configuration.GetValue<string>("Discord:ClientSecret", null!)
                    ?? throw new ArgumentNullException("Discord ClientSecret is null");

                _redirectUri =
                    _configuration.GetValue<string>("Discord:RedirectUri", null!)
                    ?? throw new ArgumentNullException("Discord RedirectUri is null");

                _jwtSecretKey =
                    _configuration.GetValue<string>("JwtConfig:SecretKey", null!)
                    ?? throw new ArgumentNullException("JWT SecretKey is null");

                _jwtDomain =
                    _configuration.GetValue<string>("JwtConfig:Domain", null!)
                    ?? throw new ArgumentNullException("JWT Domain is null");

                _jwtAudience =
                    _configuration.GetValue<string>("JwtConfig:Audience", null!)
                    ?? throw new ArgumentNullException("JWT Audience is null");
            }
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public async Task<AuthenticationToken> GetAccessToken(string code)
        {
            // Create a new HttpClient
            _httpClient.DefaultRequestHeaders.Clear();

            // Build parameters
            KeyValuePair<string, string>[] parameters =
            [
                new KeyValuePair<string, string>("client_id", _clientId),
                new KeyValuePair<string, string>("client_secret", _clientSecret),
                new KeyValuePair<string, string>(
                    "grant_type",
                    OpenIdConnectGrantTypes.AuthorizationCode
                ),
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("redirect_uri", _redirectUri),
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

        public string GetJwtToken(DiscordUser user)
        {
            // Build the security
            JwtSecurityTokenHandler tokenHandler = new();
            byte[] key = Encoding.UTF8.GetBytes(_jwtSecretKey);

            // Build token
            SecurityTokenDescriptor tokenDescriptor =
                new()
                {
                    Subject = new ClaimsIdentity(
                        new[]
                        {
                            new Claim("id", user.Id),
                            new Claim("username", user.Username),
                            new Claim("globalName", user.GlobalName ?? string.Empty),
                            new Claim("avatarUrl", user.AvatarUrl),
                            new Claim("email", user.Email ?? string.Empty),
                        }
                    ),
                    Expires = DateTime.UtcNow.AddHours(2),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature
                    ),
                    Issuer = _jwtDomain,
                    Audience = _jwtAudience,
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

        /* * * * * * * * * * * * * * * * * *\
        |*              STATIC             *|
        \* * * * * * * * * * * * * * * * * */

        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            // Get configurations
            IConfigurationSection jwtConfiguration = configuration.GetSection("JwtConfig");

            string secretKey =
                jwtConfiguration.GetValue<string>("SecretKey", null!)
                ?? throw new ArgumentNullException("JWT SecretKey is null");

            string domain =
                jwtConfiguration.GetValue<string>("Domain", null!)
                ?? throw new ArgumentNullException("JWT Domain is null");

            string audience =
                jwtConfiguration.GetValue<string>("Audience", null!)
                ?? throw new ArgumentNullException("JWT Audience is null");

            // Build secret key
            byte[] key = Encoding.ASCII.GetBytes(secretKey);

            // Build configurations
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = $"https://{domain}";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidIssuer = domain,
                        ValidAudience = audience,
                    };
                });

            services.AddHttpClient<AuthenticationService>();
        }
    }
}
