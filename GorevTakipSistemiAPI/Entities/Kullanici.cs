using Microsoft.AspNetCore.Identity;

namespace GorevTakipSistemiAPI.Entities
{
    public class Kullanici:IdentityUser<int>
    {
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }
        public ICollection<Gorev> Gorevler { get; set; }
    }
}
