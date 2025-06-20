using System.IdentityModel.Tokens.Jwt;
using GorevTakipSistemiAPI.DTOs;
using GorevTakipSistemiAPI.Entities;

namespace GorevTakipSistemiAPI.Interface.IService
{
    public interface ITokenService
    {
        DTOToken createAccessToken(int second, Kullanici kullanici);
        string CreateRefreshToken();
    }
}
