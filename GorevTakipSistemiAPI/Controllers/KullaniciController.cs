using GorevTakipSistemiAPI.DTOs;
using GorevTakipSistemiAPI.Entities;
using GorevTakipSistemiAPI.Interface.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GorevTakipSistemiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KullaniciController : ControllerBase
    {
        readonly UserManager<Kullanici> _kullaniciManager;
        readonly SignInManager<Kullanici> _signInManager;
        readonly ITokenService _tokenService;

        public KullaniciController(UserManager<Kullanici> kullaniciManager, 
            SignInManager<Kullanici> signInManager, 
            ITokenService tokenService)
        {
            _kullaniciManager = kullaniciManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<IActionResult> KullaniciOlustur(DTOKullanici kullanici)
        {
           IdentityResult result= await _kullaniciManager.CreateAsync(new()
            {
                UserName=kullanici.adi,
                Email = kullanici.email,
            },kullanici.sifre);

            return result.Succeeded
                ? Ok("Kullanıcı Oluşturuldu")
                : BadRequest(result.Errors.Select(x => x.Description).ToList());
        }

        [HttpPost("[action]")]
        public async Task<DTOToken> Giris(DTOGiris kullanici)
        {
            Kullanici girisKullanici = await _kullaniciManager.FindByEmailAsync(kullanici.email);
            if (girisKullanici == null)
            {
                throw new Exception("E-posta adresi bulunamadı!");
            }
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.CheckPasswordSignInAsync(girisKullanici, kullanici.sifre, false);
            if (result.Succeeded)
            {
               DTOToken token= _tokenService.createAccessToken(20,girisKullanici);
                return token;
            }
            throw new Exception("Şifrenizi kontrol ediniz!");
        }


        [HttpGet]
        [Authorize(AuthenticationSchemes ="Admin")]
        public object Kullanicilar()
        {
            var girisKullanici = _kullaniciManager.Users.Select(x=>new { x.UserName,x.Email}). ToList();
            return girisKullanici;
        }
    }
}
