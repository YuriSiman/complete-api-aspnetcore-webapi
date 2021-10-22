using CompleteApi.Domain.Interfaces;
using CompleteApi.Domain.Models;
using CompleteApi.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CompleteApi.Data.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(ApiDbContext options) : base(options) { }

        public Task<Categoria> ObterCategoriaProdutos(Guid id)
        {
            return Db.Categorias.AsNoTracking().Include(c => c.Produtos).FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
