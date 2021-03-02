using Microsoft.IdentityModel.Tokens;
using PosApiJwt.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace PosApiJwt.Helpers
{
    public class TokenHelper
    {
        public const string Issuer = "DAM";
        public const string Audience = "PSEP";

        public const string Secret = "Egi7kAO3Py/kuaFCop9GMXbTZRiZuOVzSOpVrxfIQTr9wVDZ+l+xcGq4G3yhtMaPY6erAE898kdC0EH6TX/Ajw==";

        //Important note***************
        //The secret is a base64-encoded string, always make sure to use a secure long string so no one can guess it. ever!.
        //a very recommended approach to use is through the HMACSHA256() class, to generate such a secure secret, you can refer to the below function
        // you can run a small test by calling the GenerateSecureSecret() function to generate a random secure secret once, grab it, and use it as the secret above 
        // or you can save it into appsettings.json file and then load it from them, the choice is yours

        // public static string GenerateSecureSecret()
        // {
        //     var hmac = new HMACSHA256();
        //     return Convert.ToBase64String(hmac.Key);
        // }

        public static string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Convert.FromBase64String(Secret);

            var claimsIdentity = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.NameIdentifier, user.Username),//.Id.ToString()),
                new Claim("IsBlocked", user.Blocked.ToString())
            });
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Issuer = Issuer,
                Audience = Audience,
                Expires = DateTime.Now.AddMinutes(60),
                SigningCredentials = signingCredentials,

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
