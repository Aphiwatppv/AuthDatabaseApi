using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthDataAccess.SecurityLogic
{
    public class  TokenService
    {

        private string _SecretKey = string.Empty;

        private readonly SymmetricSecurityKey _signingKey; 

        public TokenService(string Secretekey)
        {
            _SecretKey = Secretekey;
            _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_SecretKey));
        }


        public string GenerateTokenForUser(string userId, string userEmail )
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, userEmail),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), 
                new Claim("userId", userId) 
            }),
                Expires = DateTime.UtcNow.AddHours(2), 
                SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
