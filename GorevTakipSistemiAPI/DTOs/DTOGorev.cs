using GorevTakipSistemiAPI.Enums;

namespace GorevTakipSistemiAPI.DTOs
{
    public class DTOGorev
    {
        public int Id { get; set; }
        public string baslik { get; set; }
        public DateTime basTarih { get; set; }
        public DateTime bitTarih { get; set; }
        public string konu { get; set; }
        public enumDurum durum { get; set; }
        public int kullaniciId { get; set; }
    }
}
