using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Loterias.Application.Interfaces;
using Loterias.Common.Enums;
using Loterias.Common.Extensions;
using Loterias.Domain.Entities.Sena;

namespace Loterias.Tests.Sena
{
    public class SenaServiceFake : ISenaService
    {
        private readonly List<ConcursoSena> _senas;

        private readonly List<GanhadoresSena> _senasWinners;

        public SenaServiceFake()
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
                    ValorQuina = 24_991.48m,
                },
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
                },
            };

            _senasWinners = new List<GanhadoresSena>
            {
                new GanhadoresSena
                {
                    ConcursoId = 996,
                    Id = 236,
                    Ganhadores = 1,
                    EstadoUF = nameof(Estados.SP),
                    Localizacao = null,
                },
                new GanhadoresSena
                {
                    ConcursoId = 996,
                    Id = 237,
                    Ganhadores = 1,
                    EstadoUF = nameof(Estados.SC),
                    Localizacao = null,
                }
            };
        }

        public async Task<ConcursoSena> GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id parameter cannot be zero or negative.", nameof(id));

            var result = _senas.First(f => f.Id.Equals(id));
            if (result != null)
            {
                result.GanhadoresModel = _senasWinners.Where(w => w.ConcursoId.Equals(result.Id)).ToList();
                return await Task.FromResult(result);
            }

            return await Task.FromResult<ConcursoSena>(null);
        }

        public async Task<ConcursoSena> GetByDate(DateTime date) 
        {
            var model = _senas.FirstOrDefault(w => w.Data.Date.Equals(date.Date));
            if (model == null)
                throw new Exception("Predicate found no matching objects");
            return await Task.FromResult(model);
        }

        public async Task<IEnumerable<ConcursoSena>> GetBetweenDates(DateTime date1, DateTime date2)
        {
            if (date1 == null || date2 == null)
                throw new ArgumentNullException($"{nameof(date1)} or {nameof(date2)}", "Dates cannot be null.");
            
            var findList = _senas.Where(w => w.Data.Date >= date1 && w.Data.Date <= date2).ToList();
            IEnumerable<ConcursoSena> result;
            if (findList != null && findList.Count > 0)
            {
                result = new List<ConcursoSena>();
                foreach (var model in findList)
                {
                    var add = model;
                    add.GanhadoresModel = _senasWinners.Where(w => w.ConcursoId.Equals(model.Id)).ToList();
                    result.Append(add);
                }
                return await Task.FromResult(result);
            }
            return await Task.FromResult<List<ConcursoSena>>(null);
        }

        public async Task<IEnumerable<ConcursoSena>> GetInDates(params DateTime[] dates)
        {
            if (dates.Any(a => a==null))
                throw new ArgumentNullException("Specified dates cannot be null");
            
            var findList = _senas.Where(w => dates.Any(a => a.Date.Equals(w.Data))).ToList();
            List<ConcursoSena> result;
            if (findList != null && findList.Count > 0)
            {
                result = new List<ConcursoSena>();
                foreach (var model in findList)
                {
                    var add = model;
                    add.GanhadoresModel = _senasWinners.Where(w => w.ConcursoId.Equals(model.Id)).ToList();
                    result.Add(add);
                }
                return await Task.FromResult(result);
            }
            return await Task.FromResult<List<ConcursoSena>>(null);
        }

        public async Task<IEnumerable<ConcursoSena>> GetByNumbers(int[] numbers)
        {
            var findList = _senas.Where(where => numbers.All(value => where.ResultadoOrdenado.Contains(value))).ToList();
            List<ConcursoSena> result;
            if (findList != null && findList.Count > 0)
            {
                result = new List<ConcursoSena>();
                foreach (var model in findList)
                {
                    var add = model;
                    add.GanhadoresModel = _senasWinners.Where(w => w.ConcursoId.Equals(model.Id)).ToList();
                    result.Add(add);
                }
                return await Task.FromResult(result);
            }
            return await Task.FromResult<List<ConcursoSena>>(null);
        }

        public async Task<ConcursoSena> Add(ConcursoSena model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model), "Cannot add a null reference");

            try 
            {
                _senas.Add(model);
                return await Task.FromResult(model);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ConcursoSena> Update(ConcursoSena model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model), "Cannot update a null reference");

            var find = _senas.Find(f => f.Id.Equals(model.Id));
            if (find == null)
                throw new EntryPointNotFoundException("Could not find an object on specified index.");

            try 
            {
                _senas.Remove(find);
                _senas.Add(model);
                return await Task.FromResult(model);
            }
            catch (Exception)
            {
                throw;
            }
        }

        
    }
}