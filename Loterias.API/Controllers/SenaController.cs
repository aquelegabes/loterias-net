using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Loterias.Application.Interfaces;
using Loterias.Domain.Entities.Sena;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Loterias.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SenaController : ControllerBase
    {
        private readonly ISenaService _senaService;
        
        public SenaController(ISenaService _senaService)
        {
            this._senaService = _senaService;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/sena/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ConcursoSena),StatusCodes.Status200OK)]
        public async Task<ActionResult<ConcursoSena>> Get(int id)
        {
            if (id <= 0)
                return BadRequest(id);
            
            return await _senaService.GetById(id);
        }

        // POST sena/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT sena/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE sena/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
