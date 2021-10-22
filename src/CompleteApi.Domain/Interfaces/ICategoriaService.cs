using CompleteApi.Domain.Models;
using System;
using System.Threading.Tasks;

namespace CompleteApi.Domain.Interfaces
{
    public interface ICategoriaService : IDisposable
    {
        Task Adicionar(Categoria categoria);
        Task Atualizar(Categoria categoria);
        Task Remover(Guid id);
    }
}
