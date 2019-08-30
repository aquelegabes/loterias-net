using System;
using System.Linq;
using System.Threading.Tasks;
using Loterias.Application.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Loterias.Application.ViewModels;
using Microsoft.AspNetCore.Http;
using Loterias.Domain.Entities.Lotofacil;
using System.Globalization;
using System.Collections.Generic;

#pragma warning disable RCS1090

namespace Loterias.API.Controllers
{
    /// <summary>
    /// Api responsável por todos os métodos relacionados ao concurso lotofacil
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LotofacilController : ControllerBase
    {
        /// <summary>
        /// Serviço responsável pelos métodos relacionados a pesquisa dos concursos.
        /// </summary>
        private readonly ILotofacilService _service;

        /// <summary>
        /// Mapeador responsável para converter entidades de modelo em view-models.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Construtor base para instanciar serviço e mapeador.
        /// </summary>
        /// <param name="service">Serviço.</param>
        /// <param name="mapper">Mapeador.</param>
        public LotofacilController(ILotofacilService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
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
        [ProducesResponseType(typeof(IEnumerable<ConcursoLotofacilVm>), StatusCodes.Status200OK)]
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

                return Ok(_mapper.Map<List<ConcursoLotofacilVm>>(result));
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
    }
}