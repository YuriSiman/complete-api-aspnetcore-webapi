using CompleteApi.Business.Models;
using System;
using System.Threading.Tasks;

namespace CompleteApi.Business.Interfaces
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        Task<Categoria> ObterCategoriaProdutos(Guid id);
    }
}
