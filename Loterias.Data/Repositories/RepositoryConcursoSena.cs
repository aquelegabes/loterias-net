using Loterias.Domain.Interfaces.Repositories;
using Loterias.Domain.Entities.Sena;
using Loterias.Data.Context;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Loterias.Data.Repositories
{
    public class RepositoryConcursoSena : RepositoryBase<ConcursoSena>, IRepositoryConcursoSena
    {
        public RepositoryConcursoSena(LoteriasContext context) : base(context) { }
        
    }
}