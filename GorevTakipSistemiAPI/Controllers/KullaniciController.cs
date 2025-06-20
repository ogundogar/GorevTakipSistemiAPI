using GorevTakipSistemiAPI.DTOs;
using GorevTakipSistemiAPI.Entities;
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

        public KullaniciController(UserManager<Kullanici> kullaniciManager, SignInManager<Kullanici> signInManager)
        {
            _kullaniciManager = kullaniciManager;
            _signInManager = signInManager;
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
        public async Task<IActionResult> Giris(DTOGiris kullanici)
        {
            Kullanici girisKullanici = await _kullaniciManager.FindByEmailAsync(kullanici.email);
            if (girisKullanici == null)
            {
                throw new Exception("E-posta adresi bulunamadı!");
            }
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.CheckPasswordSignInAsync(girisKullanici, kullanici.sifre, false);
            if (result.Succeeded)
            {

            }
            throw new Exception("Şifrenizi kontrol ediniz!");
        }


        [HttpGet]
        public object Kullanicilar()
        {
            var girisKullanici = _kullaniciManager.Users.Select(x=>new { x.UserName,x.Email}). ToList();
            return girisKullanici;
        }
    }
}
