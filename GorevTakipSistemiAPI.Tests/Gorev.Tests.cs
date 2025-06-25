using System.Text;
using FakeItEasy;
using FluentAssertions;
using GorevTakipSistemiAPI.Controllers;
using GorevTakipSistemiAPI.DTOs;
using GorevTakipSistemiAPI.Entities;
using GorevTakipSistemiAPI.Interface.IRepositories.IGorev;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace GorevTakipSistemiAPI.Tests
{
    public class GorevTests
    {
        private readonly IRepositoryGorev _repositoryGorev;
        private readonly ISession _session;
        public GorevTests()
        {
            _repositoryGorev = A.Fake<IRepositoryGorev>();
            _session = A.Fake<ISession>();
        }

        [Fact]
        public void Gorevleri_Getirme()
        {
            var gorevler = new List<Gorev>
            {
                new Gorev() {
                    Id = 1,
                    baslik = "deneme1",
                    basTarih = new DateTime(),
                    bitTarih = new DateTime(),
                    konu = "test konu1",
                    durum = Enums.enumDurum.devamEdiyor,
                    kullaniciId=7
                },
                new Gorev() {
                    Id = 2,
                    baslik = "deneme2",
                    basTarih = new DateTime(),
                    bitTarih = new DateTime(),
                    konu = "test konu2",
                    durum = Enums.enumDurum.devamEdiyor,
                    kullaniciId=7
                },
                new Gorev() {
                    Id = 3,
                    baslik = "deneme3",
                    basTarih = new DateTime(),
                    bitTarih = new DateTime(),
                    konu = "test konu3",
                    durum = Enums.enumDurum.devamEdiyor,
                    kullaniciId=7
                },
            }.AsQueryable();

            var kullanici = new Kullanici
            {
                Id = 7,
                UserName = "deneme",
                Email = "den@",
                PasswordHash = "123",
            };


            var fakeSession = A.Fake<ISession>();
            var fakeHttpContext = A.Fake<HttpContext>();
            byte[] sessionBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(kullanici));

            

            var controller = new GorevController(_repositoryGorev)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = fakeHttpContext
                }
            };

            
            A.CallTo(() => fakeSession.TryGetValue("kullanici", out sessionBytes)).Returns(true);
            A.CallTo(() => fakeHttpContext.Session).Returns(fakeSession);
            A.CallTo(() => _repositoryGorev.GetAll(true)).Returns(gorevler.ToList());
            var result = controller.GorevGetAll();

            result.Should().NotBeNull();
        }


    }
}
