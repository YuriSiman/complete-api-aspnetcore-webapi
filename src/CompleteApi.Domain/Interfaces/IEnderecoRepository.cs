using CompleteApi.Domain.Models;
using System;
using System.Threading.Tasks;

namespace CompleteApi.Domain.Interfaces
{
    public interface IEnderecoRepository : IRepository<Endereco>
    {
        Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId);
    }
}
