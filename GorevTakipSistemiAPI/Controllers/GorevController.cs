using GorevTakipSistemiAPI.Entities;
using GorevTakipSistemiAPI.Enums;
using GorevTakipSistemiAPI.IRepositories.IGorev;
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
        public  object GorevGetAll()
        {
            return _repository.GetAll();
        }

        [HttpGet("GetWhere")]
        public object GorevGetWhere([FromQuery]int id)
        {
            return _repository.GetWhere(x=>x.Id==id);
        }

        [HttpPost]
        public async Task GorevAdd(Gorev gorev)
        {
            await _repository.Add(gorev);
            await _repository.SaveAsync();
        }

        [HttpPut]
        public async Task GorevUpdate(Gorev gorev)
        {
            _repository.Update(gorev);
            await _repository.SaveAsync();
        }

        [HttpDelete]
        public async Task GorevRemove(int id)
        {
            await _repository.Remove(id);
            await _repository.SaveAsync();
        }

        [HttpDelete("RemoveRange")]
        public async Task GorevRemoveRange(List<int> id)
        {
            _repository.RemoveRange(id);
            await _repository.SaveAsync();
        }

    }
}
