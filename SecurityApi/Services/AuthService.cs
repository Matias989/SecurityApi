using Microsoft.IdentityModel.Tokens;
using SecurityApi.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SecurityApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly ILogger<AuthService> _logger;
        public AuthService(IConfiguration config, ILogger<AuthService> logger)
        {
            _config = config;
            _logger = logger;
        }

        public string CreateToken(string emailUser)
        {
            _logger.LogInformation("------Start AuthService - CreateToken ------");
            try
            {
                //Token Creation

                var secretKey = _config.GetValue<string>("Jwt:Key");
                var key = Encoding.ASCII.GetBytes(secretKey);

                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, emailUser));

                var tokenDescription = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.Now.AddHours(4),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var createdToken = tokenHandler.CreateToken(tokenDescription);

                var barerToken = tokenHandler.WriteToken(createdToken);

                return barerToken;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return String.Empty;
            }
            finally
            {
                _logger.LogInformation("------Finish AuthService - CreateToken ------");
            }
        }
    }
}
