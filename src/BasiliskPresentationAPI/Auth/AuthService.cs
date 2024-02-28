using Basilisk.Busines.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Basilisk.Presentation.API.Auth
{
    public class AuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IAccountRepository _accountRepository;
        public AuthService(IConfiguration configuration, IAccountRepository accountRepository)
        {
            _configuration = configuration;
            _accountRepository = accountRepository;
        }

        public AuthRespondDTO CreateToken(AuthRequestDTO request)
        {
            var user = _accountRepository.Get(request.Username);

            bool isCorrectPassword =  BCrypt.Net.BCrypt.Verify(request.Password, user.Password);
            if(!isCorrectPassword)
            {
                throw new KeyNotFoundException("Username atau password anda salah");
            }

            var algorithm = SecurityAlgorithms.HmacSha256;

            //memasukan data payload
            var payload = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, request.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var signature = _configuration.GetSection("AppSettings:TokenSignature").Value;
            var encodedSignature = Encoding.UTF8.GetBytes(signature);

            var Token = new JwtSecurityToken
            (
                claims: payload,
                expires: DateTime.Now.AddDays(10),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(encodedSignature), algorithm)
            );
            var serializeToken = new JwtSecurityTokenHandler().WriteToken(Token);
            return new AuthRespondDTO { Token = serializeToken };
        }
    }
}
