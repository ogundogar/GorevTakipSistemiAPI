using System.ComponentModel.DataAnnotations.Schema;
using GorevTakipSistemiAPI.Entities.Common;
using GorevTakipSistemiAPI.Enums;

namespace GorevTakipSistemiAPI.Entities
{
    public class Gorev:BaseEntity
    {
        public string baslik { get; set; }
        public DateTime basTarih { get; set; }
        public DateTime bitTarih { get; set; }
        public string konu { get; set; }
        public enumDurum durum{ get; set; }
        public Kullanici kullanici { get; set; }

    }
}
