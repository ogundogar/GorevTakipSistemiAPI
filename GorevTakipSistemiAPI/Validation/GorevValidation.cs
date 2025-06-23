using FluentValidation;
using GorevTakipSistemiAPI.DTOs;
using GorevTakipSistemiAPI.Entities;

namespace GorevTakipSistemiAPI.Validation
{
    public class GorevValidation : AbstractValidator<DTOGorev>
    {
        public GorevValidation()
        {
            RuleFor(x => x.baslik)
                .NotEmpty()
                .WithMessage("Görev başlığı boş olamaz.")
                .Length(3, 100).WithMessage("Görev başlığı 3 ile 100 karakter arasında olmalıdır.");
            RuleFor(x => x.konu)
                .NotEmpty()
                .WithMessage("Görev konusu boş olamaz.")
                .Length(3, 200).WithMessage("Görev konusu 3 ile 200 karakter arasında olmalıdır.");
            RuleFor(x => x.basTarih)
                .NotEmpty()
                .WithMessage("Başlangıç tarihi boş olamaz.");
            RuleFor(x => x.bitTarih)
                .NotEmpty()
                .WithMessage("Bitiş tarihi boş olamaz.")
                .GreaterThanOrEqualTo(x => x.basTarih)
                .WithMessage("Bitiş tarihi başlangıç tarihinden önce olamaz.");
        }
    }
}
