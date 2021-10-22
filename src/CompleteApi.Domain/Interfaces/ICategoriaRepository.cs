using CompleteApi.Domain.Models;
using System;
using System.Threading.Tasks;

namespace CompleteApi.Domain.Interfaces
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        Task<Categoria> ObterCategoriaProdutos(Guid id);
    }
}
