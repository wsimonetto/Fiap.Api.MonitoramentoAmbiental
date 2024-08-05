using Fiap.Api.MonitoramentoAmbiental.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Fiap.Api.MonitoramentoAmbiental.Services
{
    public class AuthService : IAuthService
    {
        //Utilizado para fazer o teste
        private readonly string _secret = "f+ujXAKHk00L5jlMXo2XhAWawsOoihNP1OiAM25lLSO57+X7uBMQgwPju6yzyePi";

        private List<UserModel> _users = new List<UserModel>
                {
                    new UserModel { UserId = 1, Username = "operador01", Password = "pass123", Role = "operador" },
                    new UserModel { UserId = 2, Username = "analista01", Password = "pass123", Role = "analista" },
                    new UserModel { UserId = 3, Username = "gerente01", Password = "pass123", Role = "gerente" },
                    new UserModel { UserId = 4, Username = "operador02", Password = "pass123", Role = "operador" },
                    new UserModel { UserId = 5, Username = "analista02", Password = "pass123", Role = "analista" },
                    new UserModel { UserId = 6, Username = "gerente02", Password = "pass123", Role = "gerente" },
                    new UserModel { UserId = 7, Username = "operador03", Password = "pass123", Role = "operador" }
                };
        public UserModel Authenticate(string username, string password)
        {
            return _users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }

        //utilizado para fazer o teste
        public string GenerateJwtToken(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Hash, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: "fiap",
                audience: "fiap",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(5),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }

} //FIM
