using System.IdentityModel.Tokens.Jwt;
using GorevTakipSistemiAPI.Entities;
using GorevTakipSistemiAPI.Interface.IService;

namespace GorevTakipSistemiAPI.Service
{
    public class TokenService : ITokenService
    {
        public Task<JwtSecurityToken> tokenOlustur(Kullanici kullanici, List<string> roles)
        {
            throw new NotImplementedException();
        }
    }
}
