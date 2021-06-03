using CompleteApi.Business.Interfaces;
using CompleteApi.Business.Models;
using CompleteApi.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CompleteApi.Data.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(MvcDbContext options) : base(options) { }

        public Task<Categoria> ObterCategoriaProdutos(Guid id)
        {
            return Db.Categorias.AsNoTracking().Include(c => c.Produtos).FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
