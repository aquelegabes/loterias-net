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
        /// <summary>
        /// Serviço responsável pelos métodos relacionados a pesquisa dos concursos.
        /// </summary>
        private readonly ISenaService _service;

        /// <summary>
        /// Mapeador responsável para converter entidades de modelo em view-models.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// controller
        /// </summary>
        /// <param name="mapper" />
        /// <param name="service"></param>
        public SenaController(ISenaService service, IMapper mapper)
        {
            _service = service;
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
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ConcursoSenaVm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var concurso = await _service.GetById(id);
                return Ok(_mapper.Map<ConcursoSenaVm>(concurso));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { 
                    errorMessage = ex.Message,
                    parameters = ex.Data["params"]
                });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("no matching"))
                    return NoContent();

                return StatusCode(StatusCodes.Status500InternalServerError, new {
                    errorMessage = ex.Message,
                    parameters = ex.Data["params"]
                });
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
                var result = await _service.GetBetweenDates(culture, date1, date2);

                if (result?.Any() != true)
                    return NoContent();

                return Ok(_mapper.Map<List<ConcursoSenaVm>>(result));
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new {
                    errorMessage = ex.Message,
                    parameters = ex.Data["params"]
                });
            }
            catch (CultureNotFoundException ex)
            {
                return BadRequest(new {
                    errorMessage = ex.Message,
                    parameters = ex.Data["params"]
                });
            }
            catch (FormatException ex)
            {
                return BadRequest(new {
                    errorMessage = ex.Message,
                    parameters = ex.Data["params"]
                });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("no matching"))
                    return NoContent();

                return StatusCode(StatusCodes.Status500InternalServerError, new {
                    errorMessage = ex.Message,
                    parameters = ex.Data["params"]
                });
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
        [HttpGet("indates")]
        [ProducesResponseType(typeof(IEnumerable<ConcursoSenaVm>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetInDates(string culture, [FromHeader]params string[] dates)
        {
            try
            {
                var result = await _service.GetInDates(culture, dates);

                if (result?.Any() != true)
                    return NoContent();

                return Ok(_mapper.Map<List<ConcursoSenaVm>>(result));
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new {
                    errorMessage = ex.Message,
                    parameters = ex.Data["params"]
                });
            }
            catch (CultureNotFoundException ex)
            {
                return BadRequest(new {
                    errorMessage = ex.Message,
                    parameters = ex.Data["params"]
                });
            }
            catch (FormatException ex)
            {
                return BadRequest(new {
                    errorMessage = ex.Message,
                    parameters = ex.Data["params"]
                });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("no matching"))
                    return NoContent();

                return StatusCode(StatusCodes.Status500InternalServerError, new {
                    errorMessage = ex.Message,
                    parameters = ex.Data["params"]
                });
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
        [HttpGet("bynumbers")]
        [ProducesResponseType(typeof(IEnumerable<ConcursoSenaVm>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByNumbers([FromHeader]int [] numbers)
        {
            try
            {
                var result = await _service.GetByNumbers(numbers);

                if (result?.Any() != true)
                    return NoContent();

                return Ok(_mapper.Map<List<ConcursoSenaVm>>(result));
            }
            catch  (ArgumentNullException ex)
            {
                return BadRequest(new {
                    errorMessage = ex.Message,
                    parameters = ex.Data["params"]
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new {
                    errorMessage = ex.Message,
                    parameters = ex.Data["params"]
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new {
                    errorMessage = ex.Message,
                    parameters = ex.Data["params"]
                });
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
        [HttpGet("bystatewinners")]
        [ProducesResponseType(typeof(IEnumerable<ConcursoSenaVm>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByStateWinners([FromHeader]params string[] states)
        {
            try
            {
                var result = await _service.GetByStateWinners(states);

                if (result?.Any() != true)
                    return NoContent();

                return Ok(_mapper.Map<List<ConcursoSenaVm>>(result));
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new {
                    errorMessage = ex.Message,
                    parameters = ex.Data["params"]
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new {
                    errorMessage = ex.Message,
                    parameters = ex.Data["params"]
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new {
                    errorMessage = ex.Message,
                    parameters = ex.Data["params"]
                });
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
        [HttpPost]
        [Route("")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [ProducesResponseType(typeof(ConcursoSenaVm), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add([FromBody] ConcursoSenaVm model)
        {
            try
            {
                var inputModel = _mapper.Map<ConcursoSena>(model);
                var result = await _service.Add(inputModel);
                // "localhost" only apply on test cases
                var uri = (Request?.Host.Value ?? "localhost") + $"/api/sena/{result.Id}";
                return Created(uri, _mapper.Map<ConcursoSenaVm>(result));
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new {
                    errorMessage = ex.Message,
                    parameters = ex.Data["params"]
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new {
                    errorMessage = ex.Message,
                    parameters = ex.Data["params"]
                });
            }
        }

        /// <summary>
        /// Update the specified model.
        /// </summary>
        /// <returns>The updated model</returns>
        /// <param name="concurso"><see cref="Int32" />A valid Id.</param>
        /// <param name="model">Model.</param>
        /// <response code="202">Returns the entity</response>
        /// <response code="400">Invalid model</response>
        /// <response code="500">Unexpected error</response>
        [HttpPut]
        [Route("{concurso}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [ProducesResponseType(typeof(ConcursoSenaVm), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int concurso, [FromBody] ConcursoSenaVm model)
        {
            try
            {
                var inputModel = _mapper.Map<ConcursoSena>(model);
                var result = await _service.Update(concurso, inputModel);
                return Accepted(_mapper.Map<ConcursoSenaVm>(result));
            }
            catch (EntryPointNotFoundException ex)
            {
                return BadRequest(new {
                    errorMessage = ex.Message,
                    parameters = ex.Data["params"]
                });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new {
                    errorMessage = ex.Message,
                    parameters = ex.Data["params"]
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new {
                    errorMessage = ex.Message,
                    parameters = ex.Data["params"]
                });
            }
        }
    }
}
