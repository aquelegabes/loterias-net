using Loterias.Application.Interfaces;
using Xunit;
using Loterias.API.Controllers;
using Loterias.Application.AutoMapper;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Loterias.Application.ViewModels;

namespace Loterias.Tests.Sena
{
    public class SenaTest
    {
        private readonly ISenaService _service;
        private readonly SenaController _controller;

        public SenaTest()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperProfile()));
            var mapper = config.CreateMapper();

            _service = new SenaServiceFake();
            _controller = new SenaController(_service, mapper);
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsOkResult()
        {
            var okResult = await _controller.Get(994);

            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsBadRequest()
        {
            var badResult = await _controller.Get(0);

            Assert.IsType<BadRequestObjectResult>(badResult);
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsNoResponse()
        {
            var noResponse = await _controller.Get(3);

            Assert.IsType<NoContentResult>(noResponse);
        }

        [Fact]
        public async Task Get_WhenCalled_HasWinners()
        {
            //act
            var hasWinners = await _controller.Get(996);

            //Assert
            Assert.IsType<OkObjectResult>(hasWinners);

            var okObjectResult = hasWinners as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var model = okObjectResult.Value as ConcursoSenaVm;
            Assert.NotNull(model);
            Assert.NotEmpty(model.GanhadoresModel);
        }
    }
}