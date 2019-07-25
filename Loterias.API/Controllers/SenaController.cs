﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Loterias.Application.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Loterias.Application.ViewModels;
using Microsoft.AspNetCore.Http;
using Loterias.Domain.Entities.Sena;
using System.Globalization;
using System.Collections.Generic;
using Loterias.Common.Utils;

#pragma warning disable RCS1090
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
        /// <param name="id">Id (integer)</param>
        /// <returns><see cref="ConcursoSenaVm"/></returns>
        /// <response code="200">Returns the entity</response>
        /// <response code="204">No entity found on id</response>
        /// <response code="400">Id cannot be zero or lower</response>
        /// <response code="500">Unexpected error</response>
        [HttpGet("id/{id}")]
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
        /// Gets all entities between the specified dates.
        /// </summary>
        /// <returns>Entities</returns>
        /// <param name="culture">Culture.</param>
        /// <param name="date1">Date 1.</param>
        /// <param name="date2">Date 2.</param>
        /// <response code="200">Returns the entities</response>
        /// <response code="204">No entity found on date</response>
        /// <response code="400">Bad date format, invalid culture, null parameters</response>
        /// <response code="500">Unexpected error</response>
        [HttpGet("betweendates")]
        [ProducesResponseType(typeof(IEnumerable<ConcursoSenaVm>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBetweenDates(string culture, string date1, string date2)
        {
            try
            {
                var result = await _senaService.GetBetweenDates(culture, date1, date2);

                if (result?.Any() != true)
                    return NoContent();

                return Ok(_mapper.Map<List<ConcursoSenaVm>>(result));
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { errorMessage = ex.Message });
            }
            catch (CultureNotFoundException ex)
            {
                return BadRequest(new { errorMessage = ex.Message });
            }
            catch (FormatException ex)
            {
                string[] parameters = new string[] {date1,date2};
                return BadRequest(new { errorMessage = "Wrong date format", @params = parameters, exceptionMessage = ex.Message });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("no matching"))
                    return NoContent();

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gets all entities in the specified dates.
        /// </summary>
        /// <returns>Entities</returns>
        /// <param name="culture">Culture.</param>
        /// <param name="dates">Dates.</param>
        /// <response code="200">Returns the entity</response>
        /// <response code="204">No entity found on dates</response>
        /// <response code="400">Bad date format, invalid culture, null parameters</response>
        /// <response code="500">Unexpected error</response>
        [HttpPost("indates")]
        [ProducesResponseType(typeof(IEnumerable<ConcursoSenaVm>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetInDates(string culture, [FromBody]params string[] dates)
        {
            try
            {
                var result = await _senaService.GetInDates(culture, dates);

                if (result?.Any() != true)
                    return NoContent();

                return Ok(_mapper.Map<List<ConcursoSenaVm>>(result));
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { errorMessage = ex.Message });
            }
            catch (CultureNotFoundException ex)
            {
                return BadRequest(new { errorMessage = ex.Message });
            }
            catch (FormatException ex)
            {
                return BadRequest(new { errorMessage = "Wrong date format", @params = ex.Data["dates"], exceptionMessage = ex.Message });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("no matching"))
                    return NoContent();

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gets all entities that contains the specified sorted numbers
        /// </summary>
        /// <param name="numbers">Numbers</param>
        /// <returns>Entities</returns>
        /// <response code="200">Returns the entities</response>
        /// <response code="204">No entity found in specified numbers</response>
        /// <response code="400">Bad date format, invalid culture, null parameters</response>
        /// <response code="500">Unexpected error</response>
        [HttpPost("bynumbers")]
        [ProducesResponseType(typeof(IEnumerable<ConcursoSenaVm>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByNumbers([FromBody]int [] numbers)
        {
            try
            {
                var result = await _senaService.GetByNumbers(numbers);

                if (result?.Any() != true)
                    return NoContent();

                return Ok(_mapper.Map<List<ConcursoSenaVm>>(result));
            }
            catch  (ArgumentNullException ex)
            {
                return BadRequest(new { errorMessage = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { errorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { error = "internal server error", errorMessage = ex.Message });
            }
        }

        /// <summary>
        /// Gets all entities that contains winners with the specified states.
        /// </summary>
        /// <param name="states">States as two character.</param>
        /// <returns>Entities</returns>
        /// <response code="200">Returns the entities</response>
        /// <response code="204">No entity found in specified states</response>
        /// <response code="400">Invalid states, null parameters.</response>
        /// <response code="500">Unexpected error</response>
        [HttpPost("bystatewinners")]
        [ProducesResponseType(typeof(IEnumerable<ConcursoSenaVm>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByStateWinners([FromBody]params string[] states)
        {
            try
            {
                var result = await _senaService.GetByStateWinners(states);

                if (result?.Any() != true)
                    return NoContent();

                return Ok(_mapper.Map<List<ConcursoSenaVm>>(result));
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { errorMessage = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { errorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { error = "internal server error", errorMessage = ex.Message });
            }
        }

        /// <summary>
        /// Add a new model to the database
        /// </summary>
        /// <param name="model"></param>
        /// <returns><see cref="ConcursoSenaVm" /></returns>
        /// <response code="201">Returns the entities</response>
        /// <response code="400">Invalid model</response>
        /// <response code="500">Unexpected error</response>
        [HttpPut]
        [Route("add")]
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
        /// <response code="201">Returns the entities</response>
        /// <response code="400">Invalid model</response>
        /// <response code="500">Unexpected error</response>
        [HttpPost]
        [Route("update")]
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
    }
}
