using GestionareFederatieTriatlon.Entitati;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GestionareFederatieTriatlon.Manageri
{
    public class TokenManager : ITokenManager
    {
        //ne trebuie cheia 
        //ne ajuta sa luam date din apssetings -> IConfiguration
        private readonly IConfiguration configurare;
        private readonly UserManager<Utilizator> utilizatorManager;

        public TokenManager(IConfiguration configurare,
            UserManager<Utilizator> utilizatorManager)
        {
            this.configurare = configurare;
            this.utilizatorManager = utilizatorManager;
        }
        public bool IsTokenExpired(string token)
        {
            var tokenManipulare = new JwtSecurityTokenHandler();
            var cheieSecreta = configurare.GetSection("Jwt").GetSection("SecretKey").Get<string>();

            try
            {
                tokenManipulare.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(cheieSecreta)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero 
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var expirationDate = jwtToken.ValidTo;

                return expirationDate <= DateTime.UtcNow;
            }
            catch (Exception)
            {
                // Token validation failed
                return true;
            }
        }
        public async Task<string> CreareToken(Utilizator utilizator)
        {
            var cheieSecreta = configurare.GetSection("Jwt").GetSection("SecretKey").Get<string>();
            
            var cheie = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(cheieSecreta));
            //specific cheia si ce alg de criptare se foloseste
            
            var credentiale = new SigningCredentials(cheie, SecurityAlgorithms.HmacSha256Signature);

            var claimuri = new List<Claim>();
            var roluri = await utilizatorManager.GetRolesAsync(utilizator);
            foreach(var rol in roluri)
            {
                claimuri.Add(new Claim(ClaimTypes.Role, rol));
            }
            //descriere a tokenului
            var tokenDecriere = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claimuri),
                Expires = DateTime.Now.AddDays(1),//DateTime.Now.AddMinutes(1),
                SigningCredentials = credentiale
            };

            var tokenManipulare = new JwtSecurityTokenHandler();
            var token = tokenManipulare.CreateToken(tokenDecriere); //un token securizat

            return tokenManipulare.WriteToken(token);//token citibil
        }
    }
}
