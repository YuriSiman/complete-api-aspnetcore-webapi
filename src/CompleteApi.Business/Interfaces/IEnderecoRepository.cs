using CompleteApi.Business.Models;
using System;
using System.Threading.Tasks;

namespace CompleteApi.Business.Interfaces
{
    public interface IEnderecoRepository : IRepository<Endereco>
    {
        Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId);
    }
}
