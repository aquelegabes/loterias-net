using Moq;
using Xunit;
using System;
using AutoMapper;
using System.Linq;
using Autofac.Extras.Moq;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Loterias.API.Controllers;
using System.Collections.Generic;
using Loterias.Domain.Entities.Sena;
using Loterias.Application.Interfaces;
using Loterias.Application.AutoMapper;
using Loterias.Application.ViewModels;

#pragma warning disable RCS1090, RCS1205

namespace Loterias.Tests.Sena
{
    public class SenaControllerTests
    {
        private readonly List<ConcursoSena> _senas;
        private SenaController _controller;

        private IMapper Mapper
        {
            get
            {
                var config = new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperProfile()));
                return config.CreateMapper();
            }
        }

        public SenaControllerTests()
        {
            _senas = new List<ConcursoSena>
            {
                new ConcursoSena
                {
                    Id = 994,
                    Concurso = 994,
                    Data = new DateTime(day: 09, month: 08, year: 2008),
                    Acumulado = true,
                    Valor = 0m,
                    Resultado = "44-40-58-29-03-20",
                    Ganhadores = 0,
                    GanhadoresQuadra = 3838,
                    GanhadoresQuina = 47,
                    ValorAcumulado = 21_402_602.8m,
                    ValorQuadra = 389.25m,
                    ValorQuina = 31_786.28m,
                },
                new ConcursoSena
                {
                    Id = 995,
                    Concurso = 995,
                    Data = new DateTime(day: 13, month: 08, year: 2008),
                    Acumulado = true,
                    Valor = 0m,
                    Resultado = "04-54-55-36-16-31",
                    Ganhadores = 0,
                    GanhadoresQuadra = 6790,
                    GanhadoresQuina = 76,
                    ValorAcumulado = 24_901_410.33m,
                    ValorQuadra = 279.73m,
                    ValorQuina = 24_991.48m
                },
                // have winners
                new ConcursoSena
                {
                    Id = 996,
                    Concurso = 996,
                    Data = new DateTime(day: 16, month: 08, year: 2008),
                    Acumulado = false,
                    Valor = 14_458_257.67m,
                    Resultado = "21-23-20-07-29-15",
                    Ganhadores = 2,
                    GanhadoresQuadra = 11901,
                    GanhadoresQuina = 165,
                    ValorAcumulado = 0m,
                    ValorQuadra = 183.15m,
                    ValorQuina = 13_209.87m,
                    GanhadoresModel = new List<GanhadoresSena>
                    {
                        new GanhadoresSena
                        {
                            ConcursoId = 996,
                            Id = 236,
                            Ganhadores = 1,
                            EstadoUF = nameof(Common.Enums.Estados.SP),
                            Localizacao = null
                        },
                        new GanhadoresSena
                        {
                            ConcursoId = 996,
                            Id = 237,
                            Ganhadores = 1,
                            EstadoUF = nameof(Common.Enums.Estados.SC),
                            Localizacao = null
                        }
                    }
                }
            };
        }

        #region Get

        [Theory]
        [InlineData(994)]
        [InlineData(995)]
        [InlineData(996)]
        public async Task Get_WhenCalled_ReturnsOkResult(int id)
        {
            using (var mock = AutoMock.GetStrict())
            {
                // arrange
                mock.Mock<ISenaService>()
                    .Setup(act => act.GetById(id))
                    .Returns(Task.FromResult(_senas.Find(f => f.Id.Equals(id))));

                var service = mock.Create<ISenaService>();

                _controller = new SenaController(service, Mapper);

                // act
                var okResult = await _controller.Get(id);
                var okObjectResult = okResult as OkObjectResult;
                var okResultModel = okObjectResult.Value as ConcursoSenaVm;

                // assert
                mock.Mock<ISenaService>()
                   .Verify(func => func.GetById(id), Times.Exactly(1));
                Assert.NotNull(okResult);
                Assert.IsType<OkObjectResult>(okResult);
                Assert.NotNull(okObjectResult);
                Assert.NotNull(okResultModel);
            }
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task Get_WhenCalled_ReturnsBadRequest(int id)
        {
            using (var mock = AutoMock.GetStrict())
            {
                // arrange
                mock.Mock<ISenaService>()
                    .Setup(act => act.GetById(id))
                    .Returns(Task.FromException<ConcursoSena>(
                            new ArgumentException("Id cannot be zero or lower.", nameof(id))));

                var service = mock.Create<ISenaService>();

                _controller = new SenaController(service, Mapper);

                // act
                var badResult = await _controller.Get(id);

                // assert
                mock.Mock<ISenaService>()
                    .Verify(func => func.GetById(id), Times.Exactly(1));
                Assert.NotNull(badResult);
                Assert.IsType<BadRequestObjectResult>(badResult);
            }
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsNoResponse()
        {
            using (var mock = AutoMock.GetStrict())
            {
                // arrange
                mock.Mock<ISenaService>()
                    .Setup(act => act.GetById(3))
                    .Returns(Task.FromException<ConcursoSena>(new InvalidOperationException("Found no matching objects.")));

                var service = mock.Create<ISenaService>();

                _controller = new SenaController(service, Mapper);

                // act
                var noResponse = await _controller.Get(3);

                // assert
                mock.Mock<ISenaService>()
                    .Verify(func => func.GetById(3), Times.Exactly(1));
                Assert.NotNull(noResponse);
                Assert.IsType<NoContentResult>(noResponse);
            }
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsTimeout()
        {
            using (var mock = AutoMock.GetStrict())
            {
                // arrange
                mock.Mock<ISenaService>()
                    .Setup(act => act.GetById(3))
                    .Returns(Task.FromException<ConcursoSena>(new TimeoutException("Waiting response time expired.")));

                var service = mock.Create<ISenaService>();

                _controller = new SenaController(service, Mapper);

                // act
                var status500 = await _controller.Get(3);
                var objResult = status500 as ObjectResult;

                // assert
                mock.Mock<ISenaService>()
                    .Verify(func => func.GetById(3), Times.Exactly(1));
                Assert.NotNull(status500);
                Assert.IsType<ObjectResult>(status500);
                Assert.True(objResult.StatusCode == 500);
            }
        }

        [Fact]
        public async Task Get_WhenCalled_HasWinners()
        {
            using (var mock = AutoMock.GetStrict())
            {
                // arrange
                mock.Mock<ISenaService>()
                    .Setup(act => act.GetById(996))
                    .Returns(Task.FromResult(_senas.Find(f => f.Id.Equals(996))));

                var service = mock.Create<ISenaService>();

                _controller = new SenaController(service, Mapper);

                //act
                var hasWinners = await _controller.Get(996);
                var okObjectResult = hasWinners as OkObjectResult;
                var model = okObjectResult.Value as ConcursoSenaVm;

                //Assert
                mock.Mock<ISenaService>()
                    .Verify(func => func.GetById(996), Times.Exactly(1));

                Assert.NotNull(hasWinners);
                Assert.IsType<OkObjectResult>(hasWinners);
                Assert.NotNull(okObjectResult);
                Assert.NotNull(model);
                Assert.NotNull(model.GanhadoresModel);
                Assert.NotEmpty(model.GanhadoresModel);
            }
        }

        [Theory]
        [InlineData("pt-BR", "09/08/2008", "16/08/2008")]
        public async Task GetBetweenDates_WhenCalled_ReturnsOk(string culture, string date1, string date2)
        {
            using (var mock = AutoMock.GetStrict())
            {
                // arrange
                var dateSearch1 = Convert.ToDateTime(date1, new CultureInfo(culture));
                var dateSearch2 = Convert.ToDateTime(date2, new CultureInfo(culture));

                mock.Mock<ISenaService>()
                    .Setup(act => act.GetBetweenDates(culture, date1, date2))
                    .Returns(Task.FromResult(_senas.Where(w => w.Data.Date >= dateSearch1 && w.Data.Date <= dateSearch2)));

                    //_sena.Where(w => w.Data.Date >= dateSearch1 && w.Data.Date <= dateSearch2)
                var service = mock.Create<ISenaService>();

                _controller = new SenaController(service, Mapper);

                // act
                var result = await _controller.GetBetweenDates("pt-BR", "09/08/2008", "16/08/2008");
                var okObjectResult = result as OkObjectResult;
                var model = okObjectResult.Value as List<ConcursoSenaVm>;

                // assert
                Assert.NotNull(result);
                Assert.IsType<OkObjectResult>(result);
                Assert.NotNull(okObjectResult);
                Assert.IsType<List<ConcursoSenaVm>>(model);
                Assert.NotNull(model);
            }
        }

        // [Theory]
        // [InlineData("pt-BR", "01/01/1900", "01/01/1901")]
        // public async Task GetBetweenDates_WhenCalled_ReturnsNoContent()
        // {
        //     using (var mock = AutoMock.GetStrict())
        //     {
        //         // arrange
        //         mock.Mock<ISenaService>()
        //             .Setup(act => act.GetById(id))
        //             .Returns(Task.FromException<ConcursoSena>(
        //                     new ArgumentException("Id cannot be zero or lower.", nameof(id))));

        //         // act
        //         var result = await _controller.GetBetweenDates();

        //         // assert
        //         Assert.IsType<NoContentResult>(result);
        //         Assert.Null(modelResult);
        //     }
        // }

        //[Theory]
        //// wrong parameters
        //[InlineData("pt-BR", "", "01/01/1901")]
        //[InlineData("pt-BR", "01/01/1901", "")]
        //[InlineData("", "01/01/1901", "01/01/1901")]
        //// bad date format
        //[InlineData("pt-BR", "1901/31/01", "01/01/1901")]
        //// wrong culture info
        //[InlineData("ssfg", "01/01/1901", "01/01/1901")]
        //public async Task GetBetweenDates_WhenCalled_ReturnsBadRequest(string culture, string date1, string date2)
        //{
        //    // act
        //    var wrong = await _controller.GetBetweenDates(culture, date1, date2);
        //    var exc = await Record.ExceptionAsync(async () => await _service.GetBetweenDates(culture, date1, date2));

        //    // assert
        //    Assert.IsType<BadRequestObjectResult>(wrong);
        //    Assert.NotNull(exc);
        //    Assert.IsAssignableFrom<Exception>(exc);

        //    if (exc is ArgumentNullException argEx)
        //        Assert.Contains("required", argEx.Message, StringComparison.OrdinalIgnoreCase);

        //    if (exc is System.Globalization.CultureNotFoundException cultEx)
        //        Assert.Contains("Wrong", cultEx.Message, StringComparison.OrdinalIgnoreCase);

        //    // :shrug:
        //    if (exc is FormatException)
        //        Assert.True(exc is FormatException);

        //    if (exc is Exception)
        //        Assert.True(exc is Exception);
        //}

        //[Theory]
        //[InlineData("pt-BR", "09/08/2008", "13/08/2008")]
        //public async Task GetInDates_WhenCalled_ReturnsOk(string culture, params string[] dates)
        //{
        //    // act
        //    var result = await _controller.GetInDates(culture, dates);
        //    var modelResult = await _service.GetInDates(culture, dates);

        //    // assert
        //    Assert.IsType<OkObjectResult>(result);

        //    var okObjectResult = result as OkObjectResult;

        //    Assert.NotNull(okObjectResult);
        //    Assert.IsType<List<ConcursoSenaVm>>(okObjectResult.Value);

        //    var model = okObjectResult.Value as List<ConcursoSenaVm>;

        //    Assert.NotNull(model);
        //    Assert.NotEmpty(model);
        //    Assert.True(model.Count == 2);

        //    Assert.NotNull(modelResult);
        //    Assert.IsAssignableFrom<IEnumerable<ConcursoSena>>(modelResult);
        //    Assert.IsType<List<ConcursoSena>>(modelResult);
        //    Assert.NotEmpty(modelResult);
        //}

        //[Theory]
        //// wrong/missing parameters
        //[InlineData("pt-BR", "")]
        //[InlineData("", "01/01/1901")]
        //// bad date format
        //[InlineData("pt-BR", "1901/30/01")]
        //// wrong culture info
        //[InlineData("zulu", "01/01/1901")]
        //public async Task GetInDates_WhenCalled_ReturnsBadRequest(string culture, params string[] dates)
        //{
        //    // act
        //    var wrong = await _controller.GetInDates(culture, dates);
        //    var exc = await Record.ExceptionAsync(async () => await _service.GetInDates(culture, dates));

        //    // assert
        //    Assert.IsType<BadRequestObjectResult>(wrong);
        //    Assert.NotNull(exc);
        //    Assert.IsAssignableFrom<Exception>(exc);

        //    if (exc is ArgumentNullException argEx)
        //        Assert.Contains("required", argEx.Message, StringComparison.OrdinalIgnoreCase);

        //    if (exc is System.Globalization.CultureNotFoundException cultEx)
        //        Assert.Contains("Wrong", cultEx.Message, StringComparison.OrdinalIgnoreCase);

        //    // :shrug:
        //    if (exc is FormatException fEx)
        //        Assert.True(exc is FormatException);
        //}

        //[Fact]
        //public async Task GetInDates_WhenCalled_ReturnsNoContent()
        //{
        //    // act
        //    var result = await _controller.GetInDates("pt-BR", "01/01/1901");
        //    var resultModel = await _service.GetInDates("pt-BR", "01/01/1901");

        //    // assert
        //    Assert.IsType<NoContentResult>(result);
        //    Assert.Null(resultModel);
        //}

        //[Fact]
        //public async Task GetByNumbers_WhenCalled_ReturnsOkResult()
        //{
        //    // act
        //    var result = await _controller.GetByNumbers(new int[] { 4, 54, 16 });
        //    var resultModel = await _service.GetByNumbers(new int[] { 4, 54, 16 });

        //    // assert
        //    Assert.IsType<OkObjectResult>(result);

        //    var okObjectResult = result as OkObjectResult;

        //    Assert.NotNull(okObjectResult);
        //    Assert.IsType<List<ConcursoSenaVm>>(okObjectResult.Value);

        //    var model = okObjectResult.Value as List<ConcursoSenaVm>;

        //    Assert.NotNull(model);
        //    Assert.NotEmpty(model);

        //    Assert.NotNull(resultModel);
        //    Assert.NotEmpty(resultModel);
        //}

        //[Theory]
        //[InlineData(0)]
        //[InlineData(new int[] { })]
        //public async Task GetByNumbers_WhenCalled_ReturnsBadRequest(params int[] numbers)
        //{
        //    // act
        //    var response = await _controller.GetByNumbers(numbers);
        //    var exc = await Record.ExceptionAsync(async () => await _service.GetByNumbers(numbers));

        //    // assert
        //    Assert.IsType<BadRequestObjectResult>(response);

        //    Assert.NotNull(exc);
        //    Assert.IsAssignableFrom<Exception>(exc);

        //    if (exc is ArgumentNullException argNulEx)
        //        Assert.Contains("specified", argNulEx.Message, StringComparison.OrdinalIgnoreCase);

        //    if (exc is ArgumentException argEx)
        //        Assert.Contains("must be", argEx.Message, StringComparison.OrdinalIgnoreCase);

        //    if (exc is Exception ex)
        //        Assert.True(exc is Exception);
        //}

        //[Theory]
        //[InlineData(59, 42)]
        //[InlineData(39, 14)]
        //public async Task GetByNumbers_WhenCalled_ReturnsNoContent(params int[] request)
        //{
        //    // act
        //    var response = await _controller.GetByNumbers(request);
        //    var responseModel = await _service.GetByNumbers(request);

        //    // assert
        //    Assert.IsType<NoContentResult>(response);
        //    Assert.Null(responseModel);
        //}

        //[Theory]
        //[InlineData("sp")]
        //[InlineData("sp", "sc")]
        //// if has one empty string but has one state must return the state and ignore empty strings
        //[InlineData("sc", "")]
        //public async Task GetByStateWinners_WhenCalled_ReturnsOK(params string[] states)
        //{
        //    // act 
        //    var response = await _controller.GetByStateWinners(states);
        //    var resultModel = await _service.GetByStateWinners(states);

        //    // assert
        //    Assert.IsType<OkObjectResult>(response);
        //    var okObjectResult = response as OkObjectResult;
        //    Assert.IsType<List<ConcursoSenaVm>>(okObjectResult.Value);
        //    var model = okObjectResult.Value as List<ConcursoSenaVm>;
        //    Assert.NotNull(model);
        //    Assert.NotEmpty(model);

        //    Assert.NotNull(resultModel);
        //    Assert.IsAssignableFrom<IEnumerable<ConcursoSena>>(resultModel);
        //    Assert.IsType<List<ConcursoSena>>(resultModel);
        //    Assert.NotEmpty(resultModel);
        //}

        //[Fact]
        //public async Task GetByStateWinners_WhenCalled_ReturnsNoContent()
        //{
        //    // arrange
        //    var request = new string[] { "rj" };

        //    // act
        //    var response = await _controller.GetByStateWinners(request);
        //    var resultModel = await _service.GetByStateWinners(request);

        //    // assert
        //    Assert.IsType<NoContentResult>(response);
        //    Assert.Null(resultModel);
        //}

        //[Theory]
        //[InlineData("123", "34")]
        //[InlineData("")]
        //[InlineData("ok")]
        //public async Task GetByStateWinners_WhenCalled_ReturnsBadRequest(params string[] states)
        //{
        //    // act
        //    var response = await _controller.GetByStateWinners(states);
        //    var exc = await Record.ExceptionAsync(async () => await _service.GetByStateWinners(states));

        //    // assert
        //    Assert.IsType<BadRequestObjectResult>(response);

        //    Assert.NotNull(exc);
        //    Assert.IsAssignableFrom<Exception>(exc);

        //    if (exc is ArgumentNullException argNullExc)
        //        Assert.Contains("specified", argNullExc.Message, StringComparison.OrdinalIgnoreCase);

        //    if (exc is ArgumentException argExc)
        //        Assert.Contains("states", argExc.Message, StringComparison.OrdinalIgnoreCase);

        //    if (exc is Exception)
        //        Assert.True(exc is Exception);
        //}

        //#endregion Get

        //#region Add/Update/Remove

        //[Fact]
        //public async Task Add_WhenPost_Success()
        //{
        //    // arrange
        //    var modelAdd = new ConcursoSena
        //    {
        //        Concurso = 1,
        //        Data = new DateTime(day: 11, month: 03, year: 1996),
        //        Resultado = "41-05-04-52-30-33",
        //        GanhadoresQuadra = 2016,
        //        GanhadoresQuina = 17,
        //        ValorAcumulado = 1_714_650.23m,
        //        ValorQuadra = 330.21m,
        //        ValorQuina = 39_158.92m
        //    };

        //    // act
        //    var result = await _controller.Add(modelAdd);

        //    // assert
        //    Assert.IsType<OkObjectResult>(result);

        //    var okObjectResult = result as OkObjectResult;
        //    Assert.NotNull(okObjectResult);

        //    var modelVm = okObjectResult.Value as ConcursoSenaVm;
        //    Assert.IsType<ConcursoSenaVm>(modelVm);
        //    Assert.NotNull(modelVm);
        //}

        //[Fact]
        //public async Task Add_WhenPost_ReturnsBadRequest()
        //{
        //    // arrange 
        //    ConcursoSena model = null;

        //    // act
        //    var result = await _controller.Add(model);

        //    // assert
        //    Assert.IsType<BadRequestObjectResult>(result);
        //}

        //[Fact]
        //public async Task Update_WhenPost_ReturnsOk()
        //{
        //    // arrange
        //    var model = await _service.GetById(996);

        //    model.Resultado = "01-02-03-04-05-06";

        //    // act
        //    var result = await _controller.Update(model);

        //    // assert
        //    Assert.IsType<OkObjectResult>(result);

        //    var okObjectResult = result as OkObjectResult;
        //    Assert.NotNull(okObjectResult);

        //    var modelResult = okObjectResult.Value as ConcursoSenaVm;
        //    Assert.IsType<ConcursoSenaVm>(modelResult);
        //    Assert.NotNull(modelResult);
        //}

        //[Fact]
        //public async Task Update_WhenPost_ReturnsBadRequest()
        //{
        //    // arrange
        //    ConcursoSena model = null;

        //    // act
        //    var result = await _controller.Update(model);

        //    // assert
        //    Assert.IsType<BadRequestObjectResult>(result);
        //}

        //[Fact]
        //public async Task Update_WhenPost_ReturnsNotFound()
        //{
        //    // arrange
        //    var model = await _service.GetById(996);
        //    // need to instantiate a new ConcursoModel otherwise it will alter the object in the list
        //    var modelUpdate = new ConcursoSena
        //    {
        //        Acumulado = model.Acumulado,
        //        Concurso = -1,
        //        Id = -1,
        //        Data = model.Data,
        //        Ganhadores = model.Ganhadores,
        //        GanhadoresQuadra = model.GanhadoresQuadra,
        //        GanhadoresQuina = model.GanhadoresQuina,
        //        Resultado = model.Resultado,
        //        Valor = model.Valor,
        //        ValorAcumulado = model.ValorAcumulado,
        //        ValorQuadra = model.ValorQuadra,
        //        ValorQuina = model.ValorQuina
        //    };

        //    // act
        //    var result = await _controller.Update(modelUpdate);

        //    // assert
        //    Assert.IsType<NotFoundObjectResult>(result);
        //    var notFoundObject = result as NotFoundObjectResult;
        //    var json = JsonConvert.SerializeObject(notFoundObject.Value);
        //    Assert.Contains("Could not find an object on specified index.", json);
        //}
        #endregion Add/Update/Remove
    }
}