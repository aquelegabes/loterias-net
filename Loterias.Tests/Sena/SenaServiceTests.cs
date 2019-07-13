using Loterias.Application.Interfaces;
using Xunit;
using Loterias.API.Controllers;
using Loterias.Application.AutoMapper;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Loterias.Application.ViewModels;
using Loterias.Domain.Entities.Sena;
using System;

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

        #region Get

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
        #endregion Get

        #region Add/Update/Remove

        [Fact]
        public async Task Add_WhenPost_Success()
        {
            // arrange
            var modelAdd = new ConcursoSena
            {
                Concurso = 1,
                Data = new DateTime(day: 11, month: 03, year: 1996),
                Resultado = "41-05-04-52-30-33",
                GanhadoresQuadra = 2016,
                GanhadoresQuina = 17,
                ValorAcumulado = 1_714_650.23m,
                ValorQuadra = 330.21m,
                ValorQuina = 39_158.92m,
            };

           // act
            var result = await _controller.Add(modelAdd);

            // assert
            Assert.IsType<OkObjectResult>(result);

            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var modelVm = okObjectResult.Value as ConcursoSenaVm;
            Assert.IsType<ConcursoSenaVm>(modelVm);
        }

        [Fact]
        public async Task Add_WhenPost_ReturnsBadRequest()
        {
            // arrange 
            ConcursoSena model = null;

            // act
            var result = await _controller.Add(model);

            // assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion Add/Update/Remove
    }
}