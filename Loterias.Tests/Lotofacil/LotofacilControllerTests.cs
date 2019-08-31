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
using Loterias.Application.Interfaces;
using Loterias.Application.AutoMapper;
using Loterias.Application.ViewModels;
using Loterias.Domain.Entities.Lotofacil;

#pragma warning disable RCS1090, RCS1205

namespace Loterias.Tests.Lotofacil
{
    public class LotofacilControllerTests
    {
        private readonly IEnumerable<ConcursoLotofacil> _concursos;
        private LotofacilController _controller;

        private IMapper Mapper
        {
            get
            {
                var config = new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperProfile()));
                return config.CreateMapper();
            }
        }

        public LotofacilControllerTests()
        {
            _concursos = new List<ConcursoLotofacil>
            {
                new ConcursoLotofacil
                {
                    Id = 1,
                    Concurso = 1,
                    Acumulado = false,
                    ConcursoEspecial = true,
                    Data = new DateTime(day: 4, month: 9, year: 2018),
                    OnzeAcertos = 2100,
                    DozeAcertos = 126,
                    TrezeAcertos = 51,
                    CatorzeAcertos = 15,
                    Ganhadores = 1,
                    ValorOnze = 4.57m,
                    ValorDoze = 179.54m,
                    ValorTreze = 4_574.44m,
                    ValorCatorze = 24_107.87m,
                    Valor = 454_507.88m,
                    ValorAcumuladoEspecial = 0,
                    Resultado = "1-2-3-4-5-6-7-8-9-10-11-12-13-14-15",
                    GanhadoresModel = new List<GanhadoresLotofacil>
                    {
                        new GanhadoresLotofacil 
                        {
                            Id = 1,
                            EstadoUF = nameof(Common.Enums.Estados.SP),
                            Ganhadores = 1,
                            Localizacao = "Santos"
                        }                        
                    }                    
                },
                new ConcursoLotofacil
                {
                    Id = 2,
                    Concurso = 2,
                    Acumulado = true,
                    ConcursoEspecial = false,
                    Data = new DateTime(day: 7, month: 9, year: 2018),
                    OnzeAcertos = 10745,
                    DozeAcertos = 1457,
                    TrezeAcertos = 347,
                    CatorzeAcertos = 104,
                    Ganhadores = 0,
                    ValorOnze = 2.57m,
                    ValorDoze = 89.54m,
                    ValorTreze = 1_574.44m,
                    ValorCatorze = 10_874.58m,
                    Valor = 0m,
                    ValorAcumuladoEspecial = 0m,
                    Resultado = "2-5-8-11-12-13-18-19-20-21-22-23-24-25",
                }
            };
        }

        #region Get

        [Theory]
        [InlineData(1,3)]
        public async Task GetByNumbers_WhenCalled_ReturnsOkResult(params int[] numbers)
        {
            // arrange
            using (var mock = AutoMock.GetStrict())
            {
                mock.Mock<ILotofacilService>()
                    .Setup(act => act.GetByNumbers(numbers))
                    .ReturnsAsync(_concursos.Where(conc => numbers.All(value => conc.ResultadoOrdenado.Contains(value))));

                var service = mock.Create<ILotofacilService>();
                _controller = new LotofacilController(service, Mapper);

                // act
                var result = await _controller.GetByNumbers(numbers);
                var okObjectResult = result as OkObjectResult;
                var models = okObjectResult.Value as List<ConcursoLotofacilVm>;

                // assert
                mock.Mock<ILotofacilService>()
                    .Verify(func => func.GetByNumbers(numbers), Times.Exactly(1));
                Assert.NotNull(result);
                Assert.IsType<OkObjectResult>(result);
                Assert.NotNull(okObjectResult);
                Assert.IsAssignableFrom<List<ConcursoLotofacilVm>>(okObjectResult.Value);
                Assert.NotNull(models);
                Assert.NotEmpty(models);
            }
        }

        [Fact]
        public async Task GetByNumbers_WhenCalled_ArgNullExc()
        {
            using (var mock = AutoMock.GetStrict())
            {
                // arrange
                mock.Mock<ILotofacilService>()
                    .Setup(act => act.GetByNumbers(null))
                    .ThrowsAsync(new ArgumentNullException());
                
                var service = mock.Create<ILotofacilService>();
                _controller = new LotofacilController(service, Mapper);

                // act
                var result = await _controller.GetByNumbers(null);

                // assert
                mock.Mock<ILotofacilService>()
                    .Verify(func => func.GetByNumbers(null), Times.Exactly(1));
                Assert.IsType<BadRequestObjectResult>(result);
            }
        }

        [Fact]
        public async Task GetByNumbers_WhenCalled_ArgExc()
        {
            using (var mock = AutoMock.GetStrict())
            {
                // arrange
                mock.Mock<ILotofacilService>()
                    .Setup(act => act.GetByNumbers(0))
                    .ThrowsAsync(new ArgumentException());
                
                var service = mock.Create<ILotofacilService>();
                _controller = new LotofacilController(service, Mapper);

                // act
                var result = await _controller.GetByNumbers(0);

                // assert
                mock.Mock<ILotofacilService>()
                    .Verify(func => func.GetByNumbers(0), Times.Exactly(1));
                Assert.IsType<BadRequestObjectResult>(result);
            }
        }

        [Theory]
        [InlineData(1,3)]
        public async Task GetByNumbers_WhenCalled_TimeoutExc(params int[] numbers)
        {
            using (var mock =  AutoMock.GetStrict())
            {
                // arrange
                mock.Mock<ILotofacilService>()
                    .Setup(act => act.GetByNumbers(numbers))
                    .ThrowsAsync(new TimeoutException());
                
                var service = mock.Create<ILotofacilService>();
                _controller = new LotofacilController(service, Mapper);

                // act
                var result = await _controller.GetByNumbers(numbers);
                var objectResult = result as ObjectResult;

                // assert
                mock.Mock<ILotofacilService>()
                    .Verify(func => func.GetByNumbers(numbers), Times.Exactly(1));
                Assert.NotNull(result);
                Assert.IsType<ObjectResult>(result);
                Assert.NotNull(objectResult);
                Assert.True(objectResult.StatusCode == 500);
            }
        }

        [Theory]
        [InlineData(17,16)]
        public async Task GetByNumbers_WhenCalled_ReturnsNoContent(params int[] numbers)
        {
            using (var mock = AutoMock.GetStrict())
            {
                mock.Mock<ILotofacilService>()
                    .Setup(act => act.GetByNumbers(numbers))
                    .ReturnsAsync(_concursos.Where(conc => numbers.All(value => conc.ResultadoOrdenado.Contains(value))));

                var service = mock.Create<ILotofacilService>();
                _controller = new LotofacilController(service, Mapper);

                // act
                var result = await _controller.GetByNumbers(numbers);
                
                // assert
                mock.Mock<ILotofacilService>()
                    .Verify(func => func.GetByNumbers(numbers));
                Assert.IsType<NoContentResult>(result);
            }
        }

        #endregion Get
    }
}