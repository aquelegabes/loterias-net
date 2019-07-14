using System;
using System.Threading.Tasks;
using Xunit;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using Loterias.Application.Interfaces;
using Loterias.API.Controllers;
using Loterias.Application.AutoMapper;
using Loterias.Application.ViewModels;
using Loterias.Domain.Entities.Sena;



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

        [Fact]
        public async Task GetByDate_WhenCalled_ReturnsOk()
        {
            // act
            var result = await _controller.GetByDate("16/08/2008","pt-BR");

            // assert
            Assert.IsType<OkObjectResult>(result);
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var modelVm = okObjectResult.Value as ConcursoSenaVm;
            Assert.NotNull(modelVm);
            Assert.IsType<ConcursoSenaVm>(modelVm);
        }

        [Fact]
        public async Task GetByDate_WhenCalled_ReturnsBadRequest()
        {
            // act and asserts
            var resultEmpty = await _controller.GetByDate("","");
            Assert.IsType<BadRequestObjectResult>(resultEmpty);

            var resultOneEmpty = await _controller.GetByDate("16/08/2008","");
            Assert.IsType<BadRequestObjectResult>(resultOneEmpty);

            resultOneEmpty = await _controller.GetByDate("", "pt-BR");
            Assert.IsType<BadRequestObjectResult>(resultOneEmpty);
        }

        [Fact]
        public async Task GetByDate_WhernCalled_ReturnsNoContent()
        {
            // act
            var result = await _controller.GetByDate("01/01/1900", "pt-BR");
            
            // assert
            Assert.IsType<NoContentResult>(result);
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
            Assert.NotNull(modelVm);
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

        [Fact]
        public async Task Update_WhenPost_ReturnsOk()
        {
            // arrange
            var model = await _service.GetById(996);
            
            model.Resultado = "01-02-03-04-05-06";

            // act
            var result = await _controller.Update(model);

            // assert
            Assert.IsType<OkObjectResult>(result);

            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var modelResult = okObjectResult.Value as ConcursoSenaVm;
            Assert.IsType<ConcursoSenaVm>(modelResult);
            Assert.NotNull(modelResult);
        }

        [Fact]
        public async Task Update_WhenPost_ReturnsBadRequest()
        {
            // arrange
            ConcursoSena model = null;

            // act
            var result = await _controller.Update(model);

            // assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Update_WhenPost_ReturnsNotFound()
        {
            // arrange
            var model = await _service.GetById(996);
            // need to instantiate a new ConcursoModel otherwise it will alter the object in the list
            var modelUpdate = new ConcursoSena
            {
                Acumulado = model.Acumulado,
                Concurso = -1,
                Id = -1,
                Data = model.Data,
                Ganhadores = model.Ganhadores,
                GanhadoresQuadra = model.GanhadoresQuadra,
                GanhadoresQuina = model.GanhadoresQuina,
                Resultado = model.Resultado,
                Valor = model.Valor,
                ValorAcumulado = model.ValorAcumulado,
                ValorQuadra = model.ValorQuadra,
                ValorQuina = model.ValorQuina
            };

            // act
            var result = await _controller.Update(modelUpdate);

            // assert
            Assert.IsType<NotFoundObjectResult>(result);
            var notFoundObject = result as NotFoundObjectResult;
            var json = JsonConvert.SerializeObject(notFoundObject.Value);
            Assert.Contains("Could not find an object on specified index.", json);
        }
        #endregion Add/Update/Remove
    }
}