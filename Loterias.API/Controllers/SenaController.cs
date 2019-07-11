﻿using System;
using System.Threading.Tasks;
using Loterias.Application.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Loterias.Application.ViewModels;

namespace Loterias.API.Controllers
{
    /// <summary>
    /// Api responsável por todos os métodos relacionados ao concurso mega-sena
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SenaController : ControllerBase
    {
        private readonly ISenaService _senaService;
        private readonly IMapper _mapper;

        /// <summary>
        /// controller
        /// </summary>
        /// <param name="senaService"></param>
        public SenaController(ISenaService senaService, IMapper mapper)
        {
            _senaService = senaService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the result from an Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="ConcursoSenaVm"/></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, StatusCode = 200,Type = typeof(ConcursoSenaVm))]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var concurso = await _senaService.GetById(id);
                if (concurso == null)
                    return NoContent();
                    
                return Ok(_mapper.Map<ConcursoSenaVm>(concurso));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { errorMessage = ex.Message});
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {error = "internal server error", errorMessage = ex.Message});
            }
        }
    }
}
