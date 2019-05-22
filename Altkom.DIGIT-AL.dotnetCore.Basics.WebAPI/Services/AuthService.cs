using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Altkom.DIGIT_AL.dotnetCore.Basics.IServices;
using Altkom.DIGIT_AL.dotnetCore.Basics.Models;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Altkom.DIGIT_AL.dotnetCore.Basics.WebAPI
{
    public class AuthService : IAuthService
    {
        private static readonly string Secret = Guid.NewGuid().ToString();
        public static byte[] Key => Encoding.ASCII.GetBytes(Secret);

         private List<User> _users = new List<User>() {new User{Username = "test", Password = "test"}, new User{Username = "test1", Password = "test2"}};

        private ILogger _logger;
        public AuthService(ILogger<AuthService> logger) {
            _logger = logger;
        }

        public string Authenticate(User user)
        {
            if(user == null)
                return null;
            user = _users.Where(x => x.Username == user.Username).SingleOrDefault(x => x.Password == user.Password);
            if(user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new System.Security.Claims.ClaimsIdentity (new Claim[]{
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token); 
        }
    }
}