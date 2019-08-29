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
        private readonly IEnumerable<ConcursoSena> _senas;
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
                },
                new ConcursoSena
                {
                    Id = 997,
                    Concurso = 997,
                    Data = new DateTime(day: 19, month: 08, year: 2008),
                    Acumulado = false,
                    Valor = 11_018_725.67m,
                    Resultado = "04-07-41-04-29-31",
                    Ganhadores = 1,
                    GanhadoresQuadra = 6_721,
                    GanhadoresQuina = 462,
                    ValorAcumulado = 0m,
                    ValorQuadra = 1004.39m,
                    ValorQuina = 21_179.92m,
                    GanhadoresModel = new List<GanhadoresSena>
                    {
                        new GanhadoresSena
                        {
                            ConcursoId = 997,
                            Id = 238,
                            Ganhadores = 1,
                            EstadoUF = nameof(Common.Enums.Estados.SP),
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
                    .ReturnsAsync(_senas.First(f => f.Id.Equals(id)));

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
        public async Task Get_WhenCalled_ThrowsArgExc(int id)
        {
            using (var mock = AutoMock.GetStrict())
            {
                // arrange
                mock.Mock<ISenaService>()
                    .Setup(act => act.GetById(id))
                    .ThrowsAsync(new ArgumentException("Id cannot be zero or lower.", nameof(id)));

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
                    .ThrowsAsync(new InvalidOperationException("Found no matching objects."));

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
        public async Task Get_WhenCalled_ThrowsTimeoutExc()
        {
            using (var mock = AutoMock.GetStrict())
            {
                // arrange
                mock.Mock<ISenaService>()
                    .Setup(act => act.GetById(3))
                    .ThrowsAsync(new TimeoutException("Waiting response time expired."));

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
        public async Task Get_WhenCalled_ReturnsOkAndHasWinners()
        {
            using (var mock = AutoMock.GetStrict())
            {
                // arrange
                mock.Mock<ISenaService>()
                    .Setup(act => act.GetById(996))
                    .ReturnsAsync(_senas.First(f => f.Id.Equals(996)));

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

        #endregion Get

        #region GetBetweenDates

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
                    .ReturnsAsync(_senas.Where(w => w.Data.Date >= dateSearch1 && w.Data.Date <= dateSearch2));

                    //_sena.Where(w => w.Data.Date >= dateSearch1 && w.Data.Date <= dateSearch2)
                var service = mock.Create<ISenaService>();

                _controller = new SenaController(service, Mapper);

                // act
                var result = await _controller.GetBetweenDates(culture, date1, date2);
                var okObjectResult = result as OkObjectResult;
                var model = okObjectResult.Value as List<ConcursoSenaVm>;

                // assert
                mock.Mock<ISenaService>()
                    .Verify(func => func.GetBetweenDates(culture,date1,date2), Times.Exactly(1));
                Assert.NotNull(result);
                Assert.IsType<OkObjectResult>(result);
                Assert.NotNull(okObjectResult);
                Assert.IsType<List<ConcursoSenaVm>>(model);
                Assert.NotNull(model);
            }
        }

        [Theory]
        [InlineData("pt-BR", "01/01/1900", "01/01/1901")]
        public async Task GetBetweenDates_WhenCalled_ReturnsNoContent(string culture, string date1, string date2)
        {
            using (var mock = AutoMock.GetStrict())
            {
                // arrange
                mock.Mock<ISenaService>()
                    .Setup(act => act.GetBetweenDates(culture,date1,date2))
                    .ReturnsAsync(new List<ConcursoSena>() as IEnumerable<ConcursoSena>);

                var service = mock.Create<ISenaService>();
                _controller = new SenaController(service, Mapper);

                // act
                var result = await _controller.GetBetweenDates(culture, date1 ,date2);

                // assert
                mock.Mock<ISenaService>()
                    .Verify(func => func.GetBetweenDates(culture, date1 ,date2), Times.Exactly(1));
                Assert.IsType<NoContentResult>(result);
            }
        }

        [Theory]
        [InlineData("pt-BR", "", "01/01/1901")]
        public async Task GetBetweenDates_WhenCalled_ThrowsArgNullExc(string culture, string date1, string date2)
        {
            using (var mock = AutoMock.GetStrict())
            {
                // arrange
                mock.Mock<ISenaService>()
                    .Setup(act => act.GetBetweenDates(culture, date1, date2))
                    .ThrowsAsync(new ArgumentNullException());
                
                var service = mock.Create<ISenaService>();
                _controller = new SenaController(service, Mapper);

                // act
                var result = await _controller.GetBetweenDates(culture, date1, date2);

                // assert
                mock.Mock<ISenaService>()
                    .Verify(func => func.GetBetweenDates(culture, date1, date2), Times.Exactly(1));
                Assert.IsType<BadRequestObjectResult>(result);
            }
        }

        [Theory]
        [InlineData("pt-BR", "12/2008/13", "13/08/2008")]
        public async Task GetBetweenDates_WhenCalled_ThrowsFormatExc(string culture, string date1, string date2)
        {
            using (var mock = AutoMock.GetStrict())
            {
                // arrange
                mock.Mock<ISenaService>()
                    .Setup(act => act.GetBetweenDates(culture,date1,date2))
                    .ThrowsAsync(new FormatException());
                
                var service = mock.Create<ISenaService>();
                _controller = new SenaController(service, Mapper);

                // act
                var result = await _controller.GetBetweenDates(culture, date1, date2);

                // assert
                mock.Mock<ISenaService>()
                    .Verify(func => func.GetBetweenDates(culture, date1, date2), Times.Exactly(1));
                Assert.IsType<BadRequestObjectResult>(result);
            }
        }

        [Theory]
        [InlineData("ssg", "09/08/2008", "13/08/2008")]
        public async Task GetBetweenDates_WhenCalled_ThrowsCultureNotFoundExc (string culture, string date1, string date2)
        {
            using (var mock = AutoMock.GetStrict())
            {
                // arrange
                mock.Mock<ISenaService>()
                    .Setup(act => act.GetBetweenDates(culture, date1, date2))
                    .ThrowsAsync(new CultureNotFoundException());

                var service = mock.Create<ISenaService>();
                _controller = new SenaController(service, Mapper);

                // act
                var result = await _controller.GetBetweenDates(culture, date1, date2);
                
                // assert
                mock.Mock<ISenaService>()
                    .Verify(func => func.GetBetweenDates(culture, date1, date2), Times.Exactly(1));
                Assert.IsType<BadRequestObjectResult>(result);
            }
        }

        [Theory]
        [InlineData("pt-BR", "09/08/2008", "13/08/2008")]
        public async Task GetBetweenDates_WhenCalled_ThrowsTimeoutExc (string culture, string date1, string date2)
        {
            using (var mock = AutoMock.GetStrict())
            {
                // arrange
                mock.Mock<ISenaService>()
                    .Setup(act => act.GetBetweenDates(culture, date1, date2))
                    .ThrowsAsync(new TimeoutException());
                
                var service = mock.Create<ISenaService>();
                _controller = new SenaController(service, Mapper);

                // act
                var result = await _controller.GetBetweenDates(culture, date1, date2);
                var objResult = result as ObjectResult;

                // assert
                mock.Mock<ISenaService>()
                    .Verify(func => func.GetBetweenDates(culture, date1, date2), Times.Exactly(1));
                Assert.NotNull(result);
                Assert.IsType<ObjectResult>(result);
                Assert.True(objResult.StatusCode == 500);
            }
        }

        #endregion GetBetweenDates

        #region GetInDates

        [Theory]
        [InlineData("pt-BR", "09/08/2008", "13/08/2008")]
        public async Task GetInDates_WhenCalled_ReturnsOk(string culture, params string[] dates)
        {
            using (var mock = AutoMock.GetStrict())
            {
                var ci = CultureInfo.GetCultureInfo(culture);
                var listDates = dates.Select(value => Convert.ToDateTime(value, ci)).ToArray();

                //arrange
                mock.Mock<ISenaService>()
                    .Setup(act => act.GetInDates(culture, dates))
                    .ReturnsAsync(_senas.Where(w => listDates.Any(a => a.Date.Equals(w.Data))));
                
                var service = mock.Create<ISenaService>();
                _controller = new SenaController(service, Mapper);

                // act
                var result = await _controller.GetInDates(culture, dates);
                var okObjectResult = result as OkObjectResult;
                var model = okObjectResult.Value as List<ConcursoSenaVm>;

                // assert
                mock.Mock<ISenaService>()
                    .Verify(func => func.GetInDates(culture, dates), Times.Exactly(1));
                Assert.IsType<OkObjectResult>(result);
                Assert.IsType<OkObjectResult>(result);
                Assert.NotNull(okObjectResult);
                Assert.IsType<List<ConcursoSenaVm>>(okObjectResult.Value);
                Assert.NotNull(model);
                Assert.NotEmpty(model);
                Assert.True(model.Count == 2);
            }
        }

        [Theory]
        [InlineData("pt-BR", "01/01/1901")]
        public async Task GetInDates_WhenCalled_ReturnsNoContent(string culture, params string[] dates)
        {
            using (var mock = AutoMock.GetStrict())
            {
                // arrange
                var ci = CultureInfo.GetCultureInfo(culture);
                var listDates = dates.Select(value => Convert.ToDateTime(value, ci)).ToArray();

                mock.Mock<ISenaService>()
                    .Setup(act => act.GetInDates(culture, dates))
                    .ReturnsAsync(_senas.Where(w => listDates.Any(a => a.Date.Equals(w.Data))));

                var service = mock.Create<ISenaService>();
                _controller = new SenaController(service, Mapper);

                // act
                var result = await _controller.GetInDates(culture, dates);

                // assert
                mock.Mock<ISenaService>()
                    .Verify(func => func.GetInDates(culture, dates), Times.Exactly(1));
                Assert.IsType<NoContentResult>(result);
            }
        }

        [Theory]
        // argNullEx
        [InlineData("pt-BR", "")]
        public async Task GetInDates_WhenCalled_ThrowsArgNullExc(string culture, params string[] dates)
        {
            using (var mock = AutoMock.GetStrict())
            {
                // arrange
                mock.Mock<ISenaService>()
                    .Setup(act => act.GetInDates(culture, dates))
                    .ThrowsAsync(new ArgumentNullException());
                
                var service = mock.Create<ISenaService>();
                _controller = new SenaController(service, Mapper);

                // act
                var result = await _controller.GetInDates(culture, dates);

                // assert
                mock.Mock<ISenaService>()
                    .Verify(func => func.GetInDates(culture, dates), Times.Exactly(1));
                Assert.IsType<BadRequestObjectResult>(result);
            }
        }

        [Theory]
        // formatExc
        [InlineData("pt-BR", "1901/30/01")]
        public async Task GetInDates_WhenCalled_ThrowsFormatExc(string culture, params string[] dates)
        {
            using (var mock = AutoMock.GetStrict())
            {
                // arrange
                mock.Mock<ISenaService>()
                    .Setup(act => act.GetInDates(culture, dates))
                    .ThrowsAsync(new FormatException());
                
                var service = mock.Create<ISenaService>();
                _controller = new SenaController(service, Mapper);

                // act
                var result = await _controller.GetInDates(culture, dates);

                // assert
                mock.Mock<ISenaService>()
                    .Verify(func => func.GetInDates(culture, dates), Times.Exactly(1));
                Assert.IsType<BadRequestObjectResult>(result);
            }
        }

        [Theory]
        // cultureExc
        [InlineData("zulu", "01/01/1901")]
        public async Task GetInDates_WhenCalled_ThrowsCultureExc(string culture, params string[] dates)
        {
            using (var mock = AutoMock.GetStrict())
            {
                // arrange
                mock.Mock<ISenaService>()
                    .Setup(act => act.GetInDates(culture, dates))
                    .ThrowsAsync(new CultureNotFoundException());
                
                var service = mock.Create<ISenaService>();
                _controller = new SenaController(service, Mapper);

                // act
                var result = await _controller.GetInDates(culture, dates);

                // assert
                mock.Mock<ISenaService>()
                    .Verify(func => func.GetInDates(culture, dates), Times.Exactly(1));
                Assert.IsType<BadRequestObjectResult>(result);
            }
        }

        [Theory]
        [InlineData("pt-BR", "01/01/1901")]
        public async Task GetInDates_WhenCalled_ThrowsTimeoutExc(string culture, params string[] dates)
        {
            using (var mock = AutoMock.GetStrict())
            {
                // arrange
                mock.Mock<ISenaService>()
                    .Setup(act => act.GetInDates(culture, dates))
                    .ThrowsAsync(new TimeoutException());
                
                var service = mock.Create<ISenaService>();
                _controller = new SenaController(service, Mapper);

                // act
                var result = await _controller.GetInDates(culture, dates);
                var objResult = result as ObjectResult;

                // assert
                mock.Mock<ISenaService>()
                    .Verify(func => func.GetInDates(culture, dates), Times.Exactly(1));
                Assert.NotNull(result);
                Assert.IsType<ObjectResult>(result);
                Assert.True(objResult.StatusCode == 500);
            }
        }

        #endregion GetInDates

        #region GetByNumbers

        [Theory]
        [InlineData(4, 54, 16)]
        public async Task GetByNumbers_WhenCalled_ReturnsOkResult(params int[] numbers)
        {
            using (var mock = AutoMock.GetStrict())
            {
                // arrange
                mock.Mock<ISenaService>()
                    .Setup(act => act.GetByNumbers(numbers))
                    .ReturnsAsync(_senas.Where(where => numbers.All(value => where.ResultadoOrdenado.Contains(value))));
                
                var service = mock.Create<ISenaService>();
                _controller = new SenaController(service, Mapper);

                // act
                var result = await _controller.GetByNumbers(numbers);
                var okObjectResult = result as OkObjectResult;
                var models = okObjectResult.Value as List<ConcursoSenaVm>;

                // assert
                mock.Mock<ISenaService>()
                    .Verify(func => func.GetByNumbers(numbers), Times.Exactly(1));
                Assert.NotNull(result);
                Assert.IsType<OkObjectResult>(result);
                Assert.NotNull(okObjectResult);
                Assert.IsAssignableFrom<List<ConcursoSenaVm>>(okObjectResult.Value);
                Assert.NotNull(models);
                Assert.NotEmpty(models);
            }
        }
        
        [Theory]
        [InlineData(60)]
        public async Task GetByNumbers_WhenCalled_ReturnsNoContent(params int[] numbers)
        {
            using (var mock = AutoMock.GetStrict())
            {
                // arrange
                mock.Mock<ISenaService>()
                    .Setup(act => act.GetByNumbers(numbers))
                    .ReturnsAsync(_senas.Where(where => numbers.All(value => where.ResultadoOrdenado.Contains(value))));
                
                var service = mock.Create<ISenaService>();
                _controller = new SenaController(service, Mapper);

                // act
                var result = await _controller.GetByNumbers(numbers);

                // assert
                mock.Mock<ISenaService>()
                    .Verify(func => func.GetByNumbers(numbers), Times.Exactly(1));
                Assert.IsType<NoContentResult>(result);
            }
        }

        [Theory]
        [InlineData(0)]
        public async Task GetByNumbers_WhenCalled_ThrowsArgExc(params int[] numbers)
        {
            using (var mock = AutoMock.GetStrict())
            {
                // arrange
                mock.Mock<ISenaService>()
                    .Setup(act => act.GetByNumbers(numbers))
                    .ThrowsAsync(new ArgumentException());
                
                var service = mock.Create<ISenaService>();
                _controller = new SenaController(service, Mapper);

                // act
                var result = await _controller.GetByNumbers(numbers);

                // assert
                mock.Mock<ISenaService>()
                    .Verify(func => func.GetByNumbers(numbers), Times.Exactly(1));
                
                Assert.IsType<BadRequestObjectResult>(result);
            }
        }

        [Theory]
        [InlineData(new int[] { })]
        public async Task GetByNumbers_WhenCalled_ThrowsArgNullExc(params int[] numbers)
        {
            using (var mock = AutoMock.GetStrict())
            {
                // arrange
                mock.Mock<ISenaService>()
                    .Setup(act => act.GetByNumbers(numbers))
                    .ThrowsAsync(new ArgumentNullException());
                
                var service = mock.Create<ISenaService>();
                _controller = new SenaController(service, Mapper);

                // act
                var result = await _controller.GetByNumbers(numbers);

                // assert
                mock.Mock<ISenaService>()
                    .Verify(func => func.GetByNumbers(numbers), Times.Exactly(1));
                Assert.IsType<BadRequestObjectResult>(result);
            }
        }

        [Theory]
        [InlineData(4, 54, 16)]
        public async Task GetByNumbers_WhenCalled_ThrowsTimeoutExc(params int[] numbers)
        {
            using (var mock = AutoMock.GetStrict())
            {
                // arrange
                mock.Mock<ISenaService>()
                    .Setup(act => act.GetByNumbers(numbers))
                    .ThrowsAsync(new TimeoutException());
                
                var service = mock.Create<ISenaService>();
                _controller = new SenaController(service, Mapper);

                // act
                var result = await _controller.GetByNumbers(numbers);
                var objResult = result as ObjectResult;

                // assert
                mock.Mock<ISenaService>()
                    .Verify(func => func.GetByNumbers(numbers), Times.Exactly(1));
                Assert.NotNull(result);
                Assert.IsType<ObjectResult>(result);
                Assert.True(objResult.StatusCode == 500);
            }
        }

        #endregion GetByNumbers

        #region GetByStateWinners

        [Theory]
        [InlineData("sp", "sc")]
        public async Task GetByStateWinners_WhenCalled_ReturnsOkResult(params string[] states)
        {
            using (var mock = AutoMock.GetStrict())
            {
                var statesList = states.ToList();

                // arrange
                mock.Mock<ISenaService>()
                    .Setup(act => act.GetByStateWinners(states))
                    .ReturnsAsync(_senas.Where(conc => 
                        conc.GanhadoresModel != null
                        && conc.GanhadoresModel.All(winn =>
                            statesList.Any(state =>
                            winn.EstadoUF.Equals(state, StringComparison.OrdinalIgnoreCase)))));
                
                var service = mock.Create<ISenaService>();
                _controller = new SenaController(service, Mapper);

                // act
                var result = await _controller.GetByStateWinners(states);
                var okObjectResult = result as OkObjectResult;
                var model = okObjectResult.Value as List<ConcursoSenaVm>;

                // assert
                mock.Mock<ISenaService>()
                    .Verify(func => func.GetByStateWinners(states), Times.Exactly(1));
                Assert.IsType<OkObjectResult>(result);
                Assert.NotNull(okObjectResult);
                Assert.IsType<List<ConcursoSenaVm>>(okObjectResult.Value);
                Assert.NotNull(model);
                Assert.NotEmpty(model);
            }
        }

        [Theory]
        [InlineData("pa")]
        public async Task GetByStateWinners_WhenCalled_ReturnsNoContent(params string[] states)
        {
            using (var mock = AutoMock.GetStrict())
            {
                var statesList = states.ToList();

                // arrange
                mock.Mock<ISenaService>()
                    .Setup(act => act.GetByStateWinners(states))
                    .ReturnsAsync(_senas.Where(conc => 
                        conc.GanhadoresModel != null
                        && conc.GanhadoresModel.All(winn =>
                            statesList.Any(state =>
                            winn.EstadoUF.Equals(state, StringComparison.OrdinalIgnoreCase)))));
                
                var service = mock.Create<ISenaService>();
                _controller = new SenaController(service, Mapper);

                // act
                var result = await _controller.GetByStateWinners(states);

                // assert
                mock.Mock<ISenaService>()
                    .Verify(func => func.GetByStateWinners(states), Times.Exactly(1));
                Assert.IsType<NoContentResult>(result);
            }
        }

        #endregion GetByStateWinners
    }
}