using CompleteApi.Domain.Models;
using System;
using System.Threading.Tasks;

namespace CompleteApi.Domain.Interfaces
{
    public interface IProdutoService : IDisposable
    {
        Task Adicionar(Produto produto);
        Task Atualizar(Produto produto);
        Task Remover(Guid id);
    }
}
