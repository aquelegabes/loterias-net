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
using System.Collections.Generic;

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
        public async Task GetByDate_WhenCalled_ReturnsNoContent()
        {
            // act
            var result = await _controller.GetByDate("01/01/1900", "pt-BR");
            
            // assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetBetweenDates_WhenCalled_ReturnsOk()
        {
            // act
            var result = await _controller.GetBetweenDates("09/08/2008","16/08/2008", "pt-BR");

            // assert
            Assert.IsType<OkObjectResult>(result);

            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var model = okObjectResult.Value as List<ConcursoSenaVm>;
            Assert.IsType<List<ConcursoSenaVm>>(model);
            Assert.NotNull(model);
        }

        [Fact]
        public async Task GetBetweenDates_WhenCalled_ReturnsNotFound()
        {
            // act
            var result = await _controller.GetBetweenDates("01/01/1900", "01/01/1901", "pt-BR");

            // assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetBetweenDates_WhenCalled_ReturnsBadRequest()
        {
            // act/arrange
            // wrong parameters
            var wrong1 = await _controller.GetBetweenDates("", "01/01/1901", "pt-BR");
            var wrong2 = await _controller.GetBetweenDates("01/01/1901","","pt-BR");
            var wrong3 = await _controller.GetBetweenDates("01/01/1901","01/01/1901","");

            // bad date format
            var wrong4 = await _controller.GetBetweenDates("1901/31/01", "01/01/1901", "pt-BR");

            // wrong culture info
            var wrong5 = await _controller.GetBetweenDates("01/01/1901", "01/01/1901", "ssfg");

            // assert
            Assert.IsType<BadRequestObjectResult>(wrong1);
            Assert.IsType<BadRequestObjectResult>(wrong2);
            Assert.IsType<BadRequestObjectResult>(wrong3);
            Assert.IsType<BadRequestObjectResult>(wrong4);
            Assert.IsType<BadRequestObjectResult>(wrong5);
        }

        [Fact]
        public async Task GetInDates_WhenCalled_ReturnsOk()
        {
            // arrange
            var dates = new string[] { "09/08/2008", "13/08/2008" };

            // act
            var result = await _controller.GetInDates("pt-BR", dates);

            // assert
            Assert.IsType<OkObjectResult>(result);

            var okObjectResult = result as OkObjectResult;

            Assert.NotNull(okObjectResult);
            Assert.IsType<List<ConcursoSenaVm>>(okObjectResult.Value);

            var model = okObjectResult.Value as List<ConcursoSenaVm>;

            Assert.NotNull(model);
            Assert.NotEmpty(model);
            Assert.True(model.Count == 2);
        }

        [Fact]
        public async Task GetInDates_WhenCalled_ReturnsBadRequest()
        {
            // act/arrange
            // wrong/missing parameters
            var wrong1 = await _controller.GetInDates("pt-BR", "01/01/1901", "");
            var wrong2 = await _controller.GetInDates("", "01/01/1901");

            // bad date format
            var wrong3 = await _controller.GetInDates("pt-BR", "1901/30/01", "");

            // wrong culture info
            var wrong4 = await _controller.GetInDates("zulu", "01/01/1901", "");

            // assert
            Assert.IsType<BadRequestObjectResult>(wrong1);
            Assert.IsType<BadRequestObjectResult>(wrong2);
            Assert.IsType<BadRequestObjectResult>(wrong3);
            Assert.IsType<BadRequestObjectResult>(wrong4);
        }

        [Fact]
        public async Task GetInDates_WhenCalled_ReturnsNotFound()
        {
            // act
            var result = await _controller.GetInDates("pt-BR", "01/01/1901");

            // assert
            Assert.IsType<NoContentResult>(result);
        }


        [Fact]
        public async Task GetByNumbers_WhenCalled_ReturnsOkResult()
        {
            // act
            var result = await _controller.GetByNumbers(new int[] { 4, 54, 16 });

            // assert
            Assert.IsType<OkObjectResult>(result);

            var okObjectResult = result as OkObjectResult;

            Assert.NotNull(okObjectResult);
            Assert.IsType<List<ConcursoSenaVm>>(okObjectResult.Value);

            var model = okObjectResult.Value as List<ConcursoSenaVm>;

            Assert.NotNull(model);
            Assert.NotEmpty(model);
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