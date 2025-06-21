using GorevTakipSistemiAPI.Enums;

namespace GorevTakipSistemiAPI.DTOs
{
    public class DTOGorev
    {
        public string baslik { get; set; }
        public DateTime basTarih { get; set; }
        public DateTime bitTarih { get; set; }
        public string konu { get; set; }
        public enumDurum durum { get; set; }
        public int kullaniciId { get; set; }
    }
}
