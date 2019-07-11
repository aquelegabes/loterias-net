using Loterias.Domain.Entities.Sena;
using Loterias.Application.Interfaces;
using Xunit;
using Loterias.API.Controllers;
using Loterias.Application.AutoMapper;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            var okResult = _controller.Get(994);

            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsBadRequest()
        {
            var badResult = _controller.Get(0);

            Assert.IsType<BadRequestObjectResult>(badResult.Result);
        }
    }
}