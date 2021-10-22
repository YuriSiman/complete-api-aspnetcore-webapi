using CompleteApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompleteApi.Domain.Interfaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(Guid fornecedorId);
        Task<IEnumerable<Produto>> ObterProdutosFornecedoresCategorias();
        Task<Produto> ObterProdutoFornecedorCategoria(Guid id);
    }
}
