using System.IdentityModel.Tokens.Jwt;
using GorevTakipSistemiAPI.Entities;

namespace GorevTakipSistemiAPI.Interface.IService
{
    public interface ITokenService
    {
        Task<JwtSecurityToken> tokenOlustur(Kullanici kullanici, List<string> roles);
    }
}
