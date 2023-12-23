using Microsoft.IdentityModel.Tokens;
using RestfulPetApi.Data;
using RestfulPetApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestfulPetApi.Authentication
{
    public class JwtAuthenticationManager
    {
        private readonly string key;
        private readonly AppDbContext _context;

        public JwtAuthenticationManager(string key, AppDbContext context)
        {
            this.key = key;

            this._context = context;
        }

        public string Authenticate(string username, string password, List<User> users)
        {
            //auth failed - creds incorrect
            if (!users.Any(u => u.UserName == username && u.PasswordHash == password))
            {
                return null;
            }
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                //set duration of token here
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature) //setting sha256 algorithm
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
