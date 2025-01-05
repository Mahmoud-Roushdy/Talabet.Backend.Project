using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services;

namespace Talabat.Service
{
    public class TokenService : ITokenService
    {
        private IConfiguration _Configuration { get; }
        public TokenService(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

   

        public async Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager)
        {
            var AuthClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.GivenName, user.DisplayName),
                new Claim(ClaimTypes.Email, user.Email),

            };
            var Roles = await userManager.GetRolesAsync(user);
            foreach (var role in Roles)
                AuthClaims.Add(new Claim(ClaimTypes.Role, role));
            var AuthKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Configuration["JWT:Key"]));

            var token = new JwtSecurityToken(

                issuer: _Configuration["JWT:ValidIssuer"],
                audience: _Configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(double.Parse(_Configuration["JWT:DurationInDays"])),
                claims: AuthClaims,
                signingCredentials: new SigningCredentials(AuthKey, SecurityAlgorithms.HmacSha256Signature)

                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
