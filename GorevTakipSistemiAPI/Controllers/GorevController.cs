using GorevTakipSistemiAPI.DTOs;
using GorevTakipSistemiAPI.Entities;
using GorevTakipSistemiAPI.Enums;
using GorevTakipSistemiAPI.Interface.IRepositories.IGorev;
using GorevTakipSistemiAPI.Repositories.Gorev;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GorevTakipSistemiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GorevController : ControllerBase
    {
        readonly IRepositoryGorev _repository;
        public GorevController(IRepositoryGorev repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public object GorevGetAll()
        {
            var gorevler=_repository.GetAll().Select(x => new
            {
                x.Id,
                x.baslik,
                baslangicTarihi= x.basTarih,
                bitisTarihi=x.bitTarih,
                x.kullanici.UserName,
                x.konu,
                x.durum
            }).ToList();

            return Ok(gorevler);
        }

        public class gorevFiltrele
        {
            public int? Id { get; set; }
            public string? baslik { get; set; }
            public DateTime? basTarih { get; set; }
            public DateTime? bitTarih { get; set; }
            public string? konu { get; set; }
            public enumDurum? durum { get; set; }
        }


        [HttpGet("GetWhere")]
        public object GorevGetWhere([FromQuery] gorevFiltrele gorevFiltrele)
        {
            var gorevler = _repository;
            IQueryable query=null;
            if (gorevFiltrele.Id != null)
                query = gorevler.GetWhere(x => x.Id == gorevFiltrele.Id);
            if (gorevFiltrele.baslik != null)
                query = gorevler.GetWhere(x => x.baslik.Contains(gorevFiltrele.baslik));
            if (gorevFiltrele.basTarih != null)
                query = gorevler.GetWhere(x => x.basTarih == gorevFiltrele.basTarih);
            if (gorevFiltrele.bitTarih != null)
                query = gorevler.GetWhere(x => x.bitTarih == gorevFiltrele.bitTarih);
            if (gorevFiltrele.durum != null)
                query = gorevler.GetWhere(x => x.durum == gorevFiltrele.durum);
            return query;
        }

        [HttpPost]
        public async Task<IActionResult> GorevAdd(DTOGorev grv)
        {
            try
            {
                Gorev gorev = new()
                {
                    baslik = grv.baslik,
                    basTarih = grv.basTarih,
                    bitTarih = grv.bitTarih,
                    konu = grv.konu,
                    durum = grv.durum,
                    kullaniciId = grv.kullaniciId
                };

                await _repository.Add(gorev);
                await _repository.SaveAsync();
                return Ok("Başarılı bir şekilde işlem gerçekleşmiştir.");
            }
            catch (Exception ex)
            {
                return Ok("Hata oluştu: " + ex.Message);
            }

        }

        [HttpPut]
        public bool GorevUpdate(DTOGorev grv)
        {
            try
            {
                Gorev gorev = new()
                {
                    Id = (int)grv.Id,
                    baslik = grv.baslik,
                    basTarih = grv.basTarih,
                    bitTarih = grv.bitTarih,
                    konu = grv.konu,
                    durum = grv.durum,
                    kullaniciId = grv.kullaniciId
                };

                _repository.Update(gorev);
                _repository.SaveAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpDelete]
        public async Task GorevRemove(int id)
        {
            await _repository.Remove(id);
            await _repository.SaveAsync();
        }

        [HttpDelete("RemoveRange")]
        public async Task GorevRemoveRange([FromBody] List<int> idList)
        {
            _repository.RemoveRange(idList);
            await _repository.SaveAsync();
        }

    }

}
