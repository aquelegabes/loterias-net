﻿using System;
using System.Threading.Tasks;
using Loterias.Application.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Loterias.Application.ViewModels;
using Microsoft.AspNetCore.Http;
using Loterias.Domain.Entities.Sena;
using System.Globalization;

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
        [ProducesResponseType(typeof(ConcursoSenaVm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var concurso = await _senaService.GetById(id);

                return Ok(_mapper.Map<ConcursoSenaVm>(concurso));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { errorMessage = ex.Message});
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("no matching"))
                    return NoContent();

                return StatusCode(500, new {error = "internal server error", errorMessage = ex.Message});
            }
        }

        /// <summary>
        /// Add a new model to the database
        /// </summary>
        /// <param name="model"></param>
        /// <returns><see cref="ConcursoSenaVm" /></returns>
        [HttpPut]
        [Route("api/[controller]/add")]
        [ProducesResponseType(typeof(ConcursoSenaVm), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add([FromBody] ConcursoSena model)
        {
            try
            {
                var result = await _senaService.Add(model);
                return Ok(_mapper.Map<ConcursoSenaVm>(model));
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { errorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new { errorMessage = ex.Message });
            }
        }

        /// <summary>
        /// Update the specified model.
        /// </summary>
        /// <returns>The updated model</returns>
        /// <param name="model">Model.</param>
        [HttpPost]
        [Route("api/[controller]/update")]
        [ProducesResponseType(typeof(ConcursoSenaVm), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] ConcursoSena model)
        {
            try 
            {
                var result = await _senaService.Update(model);
                return Ok(_mapper.Map<ConcursoSenaVm>(model));
            }
            catch (EntryPointNotFoundException ex)
            {
                return NotFound(new { errorMessage = ex.Message });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { errorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { errorMessage = ex.Message });
            }
        }

        /// <summary>
        /// Gets entity by date.
        /// </summary>
        /// <returns>The by date.</returns>
        /// <param name="date">Date.</param>
        /// <param name="culture">Culture.</param>
        [HttpGet("{date:datetime}/string:culture")]
        [Route("api/[controller]/getbydate")]
        [ProducesResponseType(typeof(ConcursoSenaVm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByDate(string date, string culture)
        {
            try
            {
                var ci = CultureInfo.GetCultureInfo(culture);
                DateTime dateSearch = Convert.ToDateTime(date, ci);
                var result = await _senaService.GetByDate(dateSearch);
                return Ok(_mapper.Map<ConcursoSenaVm>(result));
            }
            catch (ArgumentNullException)
            {
                return BadRequest(new { errorMessage = "Both parameters are required" });
            }
            catch (CultureNotFoundException)
            {
                return BadRequest(new { errorMessage = "Wrong culture info specified, check https://lonewolfonline.net/list-net-culture-country-codes/ for a list containing all culture infos" });
            }
            catch (FormatException)
            {
                return BadRequest(new { errorMessage = "Wrong date format" });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("no matching"))
                    return NoContent();

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
