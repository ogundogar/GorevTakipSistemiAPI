using System;
using FluentValidation;
using GorevTakipSistemiAPI.DTOs;
using GorevTakipSistemiAPI.Entities;

namespace GorevTakipSistemiAPI.Validation
{
    public class KullaniciValidation : AbstractValidator<DTOKullanici>
    {
        public KullaniciValidation()
        {
            RuleFor(x => x.adi)
                .NotEmpty()
                .WithMessage("Kullanıcı adı boş olamaz.")
                .Length(3, 50).WithMessage("Kullanıcı adı 3 ile 50 karakter arasında olmalıdır.");
            RuleFor(x => x.email)
                .NotEmpty()
                .WithMessage("E-posta boş olamaz.")
                .EmailAddress()
                .WithMessage("Geçerli bir e-posta adresi giriniz.");
            RuleFor(x => x.sifre)
                .NotEmpty()
                .WithMessage("Şifre boş olamaz.")
                .MinimumLength(3).WithMessage("Şifre en az 3 karakter olmalıdır.");
            RuleFor(x => x.sifreTekrar)
                .Equal(x => x.sifre)
                .WithMessage("Şifre ve şifre doğrulama aynı değil.");

        }
    }
}
