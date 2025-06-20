using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using GorevTakipSistemiAPI.DTOs;
using GorevTakipSistemiAPI.Entities;
using GorevTakipSistemiAPI.Interface.IService;
using Microsoft.IdentityModel.Tokens;
using static System.Net.Mime.MediaTypeNames;

namespace GorevTakipSistemiAPI.Service
{
    public class TokenService : ITokenService
    {
        readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DTOToken createAccessToken(int second, Kullanici kullanici)
        {
            DTOToken token = new();

            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:TokenKey"]));


            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            token.expiration = DateTime.UtcNow.AddSeconds(second);


            JwtSecurityToken securityToken = new(
                //audience: _configuration["Token:Audience"],
                //issuer: _configuration["Token:Issuer"],
                expires: token.expiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials,
                claims: new List<Claim> { new(ClaimTypes.Name, kullanici.UserName) }
                );


            JwtSecurityTokenHandler tokenHandler = new();
            token.accessToken = tokenHandler.WriteToken(securityToken);


            token.RefreshToken = CreateRefreshToken();
            return token;
        }

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }
}
