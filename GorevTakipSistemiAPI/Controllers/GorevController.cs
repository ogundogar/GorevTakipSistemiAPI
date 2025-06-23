using GorevTakipSistemiAPI.DTOs;
using GorevTakipSistemiAPI.Entities;
using GorevTakipSistemiAPI.Enums;
using GorevTakipSistemiAPI.Interface.IRepositories.IGorev;
using GorevTakipSistemiAPI.Repositories.Gorev;
using GorevTakipSistemiAPI.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
            Kullanici? sessionKullanici;
            var session = HttpContext.Session.GetString("kullanici");
            if (!string.IsNullOrEmpty(session))
            {
                sessionKullanici = JsonConvert.DeserializeObject<Kullanici>(session);
                var gorevler = _repository.GetAll().Where(x => x.kullaniciId == sessionKullanici.Id).Select(x => new
                {
                    x.Id,
                    x.baslik,
                    baslangicTarihi = x.basTarih,
                    bitisTarihi = x.bitTarih,
                    x.kullanici.UserName,
                    x.konu,
                    x.durum
                }).ToList();
                return Ok(gorevler);
            }

            return Ok();

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

            Kullanici? sessionKullanici;
            var session = HttpContext.Session.GetString("kullanici");
            if (!string.IsNullOrEmpty(session))
            {
                sessionKullanici = JsonConvert.DeserializeObject<Kullanici>(session);
                var gorevler = _repository.GetAll().Where(x => x.kullaniciId == sessionKullanici.Id);

                if (gorevFiltrele.Id != null)
                    gorevler = gorevler.Where(x => x.Id == gorevFiltrele.Id);

                if (gorevFiltrele.baslik != null)
                    gorevler = gorevler.Where(x => x.baslik.Contains(gorevFiltrele.baslik));

                if (gorevFiltrele.basTarih != null)
                    gorevler = gorevler.Where(x => x.basTarih >= gorevFiltrele.basTarih);

                if (gorevFiltrele.bitTarih != null)
                    gorevler = gorevler.Where(x => x.bitTarih <= gorevFiltrele.bitTarih);

                if (gorevFiltrele.durum != null)
                    gorevler = gorevler.Where(x => x.durum == gorevFiltrele.durum);

                gorevler.Select(x => new
                {
                    x.Id,
                    x.baslik,
                    baslangicTarihi = x.basTarih,
                    bitisTarihi = x.bitTarih,
                    x.kullanici.UserName,
                    x.konu,
                    x.durum
                }).ToList();

                return Ok(gorevler);
            }

            
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> GorevAdd(DTOGorev grv)
        {
            try
            {
                var validator = new GorevValidation();

                var validationResult = validator.Validate(grv);

                if (!validationResult.IsValid)
                {
                    var hatalar = string.Join("\n", validationResult.Errors.Select(e => $"- {e.ErrorMessage}"));
                    return Ok("Hata oluştu!\n" + hatalar);
                }


                Kullanici? sessionKullanici;
                var session = HttpContext.Session.GetString("kullanici");
                if (string.IsNullOrEmpty(session))
                {
                    return Ok("Hata oluştu!");
                }
                sessionKullanici = JsonConvert.DeserializeObject<Kullanici>(session);

                Gorev gorev = new()
                {
                    baslik = grv.baslik,
                    basTarih = grv.basTarih,
                    bitTarih = grv.bitTarih,
                    konu = grv.konu,
                    durum = grv.durum,
                    kullaniciId = sessionKullanici.Id
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
        public IActionResult GorevUpdate(DTOGorev grv)
        {
            try
            {
                var validator = new GorevValidation();

                var validationResult = validator.Validate(grv);

                if (!validationResult.IsValid)
                {
                    var hatalar = string.Join("\n", validationResult.Errors.Select(e => $"- {e.ErrorMessage}"));
                    return Ok("Hata oluştu!\n" + hatalar);
                }


                Kullanici? sessionKullanici;
                var session = HttpContext.Session.GetString("kullanici");
                if (string.IsNullOrEmpty(session))
                {
                    return Ok("Hata oluştu!");
                }
                sessionKullanici = JsonConvert.DeserializeObject<Kullanici>(session);


                Gorev gorev = new()
                {
                    Id = (int)grv.Id,
                    baslik = grv.baslik,
                    basTarih = grv.basTarih,
                    bitTarih = grv.bitTarih,
                    konu = grv.konu,
                    durum = grv.durum,
                    kullaniciId = sessionKullanici.Id
                    
                };

                _repository.Update(gorev);
                _repository.SaveAsync();
                return Ok("Başarılı bir şekilde işlem gerçekleşmiştir.");
            }
            catch (Exception ex)
            {
                return Ok("Hata oluştu: " + ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> GorevRemove(int id)
        {
            Kullanici? sessionKullanici;
            var session = HttpContext.Session.GetString("kullanici");
            if (string.IsNullOrEmpty(session))
            {
                return Ok("Silme işlemi sırasında hata oluştu");
            }
            sessionKullanici = JsonConvert.DeserializeObject<Kullanici>(session);

            var gorev = _repository.GetAll().Where(x=>x.Id==id).Select(x=>x.kullaniciId).FirstOrDefault();
            if (gorev == sessionKullanici.Id)
            {
                if (id>0) { 
                await _repository.Remove(id);
                await _repository.SaveAsync();
                return Ok("Başarılı bir şekilde işlem gerçekleşmiştir.");
                }
            }
            return Ok("Silme işlemi sırasında hata oluştu");
        }

        [HttpDelete("RemoveRange")]
        public async Task GorevRemoveRange([FromBody] List<int> idList)
        {
            _repository.RemoveRange(idList);
            await _repository.SaveAsync();
        }

    }

}
