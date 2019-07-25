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

#pragma warning disable RCS1090, RCS1205

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

        [Theory]
        [InlineData(994)]
        [InlineData(995)]
        [InlineData(996)]
        public async Task Get_WhenCalled_ReturnsOkResult(int id)
        {
            // act
            var okResult = await _controller.Get(id);
            var model = await _service.GetById(id);

            // assert

            Assert.IsType<OkObjectResult>(okResult);
            Assert.NotNull(model);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task Get_WhenCalled_ReturnsBadRequest(int id)
        {
            // act
            var badResult = await _controller.Get(id);
            var exception = await Record.ExceptionAsync(async() => await _service.GetById(id));

            // assert
            Assert.IsType<BadRequestObjectResult>(badResult);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentException>(exception);
            if (exception is ArgumentException argEx)
            {
                Assert.Equal(nameof(id), argEx.ParamName);
            }
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsNoResponse()
        {
            // act
            var noResponse = await _controller.Get(3);
            var exception = await Record.ExceptionAsync(async() => await _service.GetById(3));

            // assert
            Assert.IsType<NoContentResult>(noResponse);
            Assert.NotNull(exception);
            Assert.IsType<InvalidOperationException>(exception);
            if (exception is InvalidOperationException ioEx)
            {
                Assert.Contains("no matching", ioEx.Message);
            }
        }

        [Fact]
        public async Task Get_WhenCalled_HasWinners()
        {
            //act
            var hasWinners = await _controller.Get(996);
            var modelReturn = await _service.GetById(996);

            //Assert
            Assert.IsType<OkObjectResult>(hasWinners);

            var okObjectResult = hasWinners as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var model = okObjectResult.Value as ConcursoSenaVm;
            Assert.NotNull(model);
            Assert.NotEmpty(model.GanhadoresModel);

            Assert.NotNull(modelReturn);
            Assert.NotEmpty(modelReturn.GanhadoresModel);
        }

        [Fact]
        public async Task GetBetweenDates_WhenCalled_ReturnsOk()
        {
            // act
            var result = await _controller.GetBetweenDates("pt-BR", "09/08/2008","16/08/2008");
            var modelResult = await _service.GetBetweenDates("pt-BR", "09/08/2008", "16/08/2008");

            // assert
            Assert.IsType<OkObjectResult>(result);

            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var model = okObjectResult.Value as List<ConcursoSenaVm>;
            Assert.IsType<List<ConcursoSenaVm>>(model);
            Assert.NotNull(model);

            Assert.IsAssignableFrom<IEnumerable<ConcursoSena>>(modelResult);
            Assert.IsType<List<ConcursoSena>>(modelResult);
            Assert.NotNull(modelResult);
            Assert.NotEmpty(modelResult);
        }

        [Fact]
        public async Task GetBetweenDates_WhenCalled_ReturnsNoContent()
        {
            // act
            var result = await _controller.GetBetweenDates("pt-BR", "01/01/1900", "01/01/1901");
            var modelResult = await _service.GetBetweenDates("pt-BR", "01/01/1900", "01/01/1901");

            // assert
            Assert.IsType<NoContentResult>(result);
            Assert.Null(modelResult);
        }

        [Theory]
        // wrong parameters
        [InlineData("pt-BR", "", "01/01/1901")]
        [InlineData("pt-BR", "01/01/1901", "")]
        [InlineData("", "01/01/1901", "01/01/1901")]
        // bad date format
        [InlineData("pt-BR", "1901/31/01", "01/01/1901")]
        // wrong culture info
        [InlineData("ssfg", "01/01/1901", "01/01/1901")]
        public async Task GetBetweenDates_WhenCalled_ReturnsBadRequest(string culture, string date1, string date2)
        {
            // act
            var wrong = await _controller.GetBetweenDates(culture, date1, date2);
            var exc = await Record.ExceptionAsync(async() => await _service.GetBetweenDates(culture, date1, date2));

            // assert
            Assert.IsType<BadRequestObjectResult>(wrong);
            Assert.NotNull(exc);
            Assert.IsAssignableFrom<Exception>(exc);

            if (exc is ArgumentNullException argEx)
                Assert.Contains("required", argEx.Message);

            if (exc is System.Globalization.CultureNotFoundException cultEx)
                Assert.Contains("Wrong", cultEx.Message);

            // :shrug:
            if (exc is FormatException fEx)
                Assert.True(exc is FormatException);
        }

        [Theory]
        [InlineData("pt-BR", "09/08/2008", "13/08/2008")]
        public async Task GetInDates_WhenCalled_ReturnsOk(string culture, params string[] dates)
        {
            // act
            var result = await _controller.GetInDates(culture, dates);
            var modelResult = await _service.GetInDates(culture, dates);

            // assert
            Assert.IsType<OkObjectResult>(result);

            var okObjectResult = result as OkObjectResult;

            Assert.NotNull(okObjectResult);
            Assert.IsType<List<ConcursoSenaVm>>(okObjectResult.Value);

            var model = okObjectResult.Value as List<ConcursoSenaVm>;

            Assert.NotNull(model);
            Assert.NotEmpty(model);
            Assert.True(model.Count == 2);

            Assert.NotNull(modelResult);
            Assert.IsAssignableFrom<IEnumerable<ConcursoSena>>(modelResult);
            Assert.IsType<List<ConcursoSena>>(modelResult);
            Assert.NotEmpty(modelResult);
        }

        [Theory]
        // wrong/missing parameters
        [InlineData("pt-BR", "01/01/1901", "")]
        [InlineData("", "01/01/1901")]
        // bad date format
        [InlineData("pt-BR", "1901/30/01", "")]
        // wrong culture info
        [InlineData("zulu", "01/01/1901", "")]
        public async Task GetInDates_WhenCalled_ReturnsBadRequest(string culture, params string[] dates)
        {
            // act
            var wrong = await _controller.GetInDates(culture, dates);
            var exc = await Record.ExceptionAsync(async() => await _service.GetInDates(culture, dates));

            // assert
            Assert.IsType<BadRequestObjectResult>(wrong);
            Assert.NotNull(exc);
            Assert.IsAssignableFrom<Exception>(exc);

            if (exc is ArgumentNullException argEx)
                Assert.Contains("required", argEx.Message);

            if (exc is System.Globalization.CultureNotFoundException cultEx)
                Assert.Contains("Wrong", cultEx.Message);

            // :shrug:
            if (exc is FormatException fEx)
                Assert.True(exc is FormatException);
        }

        [Fact]
        public async Task GetInDates_WhenCalled_ReturnsNoContent()
        {
            // act
            var result = await _controller.GetInDates("pt-BR", "01/01/1901");
            var resultModel = await _service.GetInDates("pt-BR", "01/01/1901");

            // assert
            Assert.IsType<NoContentResult>(result);
            Assert.Null(resultModel);
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

        [Theory]
        [InlineData(0)]
        [InlineData(new int[] { })]
        public async Task GetByNumbers_WhenCalled_ReturnsBadRequest(params int[] numbers)
        {
            // act
            var response = await _controller.GetByNumbers(numbers);

            // assert
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async Task GetByNumbers_WhenCalled_ReturnsNoContent()
        {
            // arrange
            var request = new int[] { 59,42 };

            // act
            var response = await _controller.GetByNumbers(request);

            // assert
            Assert.IsType<NoContentResult>(response);
        }

        [Theory]
        [InlineData("sp")]
        [InlineData("sp", "sc")]
        // if has one empty string but has one state must return the state and ignore empty strings
        [InlineData("sc", "")]
        public async Task GetByStateWinners_WhenCalled_ReturnsOK(params string[] states)
        {
            // act 
            var response = await _controller.GetByStateWinners(states);

            // assert
            Assert.IsType<OkObjectResult>(response);
            var okObjectResult = response as OkObjectResult;
            Assert.IsType<List<ConcursoSenaVm>>(okObjectResult.Value);
            var model = okObjectResult.Value as List<ConcursoSenaVm>;
            Assert.NotNull(model);
            Assert.NotEmpty(model);
        }

        [Fact]
        public async Task GetByStateWinners_WhenCalled_ReturnsNoContent()
        {
            // arrange
            var request = new string[] { "rj" };

            // act
            var response = await _controller.GetByStateWinners(request);

            // assert
            Assert.IsType<NoContentResult>(response);
        }

        [Theory]
        [InlineData("123", "34")]
        [InlineData("")]
        [InlineData("ok")]
        public async Task GetByStateWinners_WhenCalled_ReturnsBadRequest(params string[] states)
        {
            // act
            var response = await _controller.GetByStateWinners(states);

            // assert
            Assert.IsType<BadRequestObjectResult>(response);
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