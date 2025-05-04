using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtTokenService
{
    public class JwtToken
    {
        private readonly string _llaveJwt;

        public JwtToken(IConfiguration configuration)
        {
            _llaveJwt = configuration["LLaveJwt"];
        }

        /// <summary>
        /// Genera un token
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="role"></param>
        /// <param name="clienteId"></param>
        /// <param name="correo"></param>
        /// <param name="fechaDeExpiracion"></param>
        /// <returns></returns>
        public string ObtenerToken(string nombre, string role, string clienteId, string correo, DateTime fechaDeExpiracion) 
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_llaveJwt));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);            

            var claims = new List<Claim>
        {
            new Claim("Nombre", nombre),
            new Claim("Role", role),
            new Claim("ClienteId",clienteId),
            new Claim("email", correo)
            //new Claim("scope", string.Join(" ", user.Scopes))
        };

            var tokenOptions = new JwtSecurityToken(
                //issuer: "https://localhost:5002",            
                claims: claims,
                expires: fechaDeExpiracion,
                signingCredentials: signingCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return tokenString;
        }
    }
}
