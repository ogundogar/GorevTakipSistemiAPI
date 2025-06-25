using System.Text;
using FakeItEasy;
using FluentAssertions;
using GorevTakipSistemiAPI.Controllers;
using GorevTakipSistemiAPI.DTOs;
using GorevTakipSistemiAPI.Entities;
using GorevTakipSistemiAPI.Interface.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace GorevTakipSistemiAPI.Tests
{
    public class KullaniciTests
    {
        private readonly UserManager<Kullanici> _UserManager;
        private readonly SignInManager<Kullanici> _SignInManager;
        private readonly ITokenService _TokenService;

        public KullaniciTests()
        {
            _UserManager = A.Fake<UserManager<Kullanici>>();
            _SignInManager = A.Fake<SignInManager<Kullanici>>();
            _TokenService = A.Fake<ITokenService>();
        }

        [Fact]
        public void Kullanici_Kayit_Test()
        {
            var giris = new DTOGiris
            {
                email = "qwe@",
                sifre = "1234"
            };

            var token = new DTOToken
            {
                accessToken = "token123",
                expiration = DateTime.UtcNow.AddSeconds(300),
                RefreshToken = "token123="
            };
            var kullanici = new Kullanici
            {
                Id = 7,
                UserName = "deneme",
                Email = "den@",
                PasswordHash = "123",
            };


            var fakeSession = A.Fake<ISession>();
            byte[] sessionBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(kullanici));


            A.CallTo(() => fakeSession.TryGetValue("kullanici", out sessionBytes)).Returns(true);

            var gorevler = new List<Gorev>();
            A.CallTo(() => _UserManager.FindByEmailAsync(giris.email)).Returns(Task.FromResult(kullanici));
            A.CallTo(() => _SignInManager.CheckPasswordSignInAsync(kullanici, giris.sifre, false)).Returns(Task.FromResult(SignInResult.Success));
            A.CallTo(() => _TokenService.createAccessToken(300, kullanici)).Returns(token);
            var controller = new KullaniciController(_UserManager, _SignInManager, _TokenService);
            var result = controller.Giris(giris);
            result.Should().NotBeNull();

        }
    }
}
