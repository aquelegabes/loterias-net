﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Loterias.Domain.Entities.Sena;
using Loterias.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Loterias.API.Controllers
{
    /// <summary>
    /// Api responsável por todos os métodos relacionados ao concurso mega-sena
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SenaController : ControllerBase
    {
        private readonly IRepositoryConcursoSena _concursoSena;

        /// <summary>
        /// controller
        /// </summary>
        /// <param name="concursoSena"></param>
        public SenaController(IRepositoryConcursoSena concursoSena)
        {
            _concursoSena = concursoSena;
        }

        /// <summary>
        /// Gets the result from an Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, StatusCode = 200,Type = typeof(ConcursoSena))]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var concurso = await _concursoSena.GetById(id);
                return Ok(concurso);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
