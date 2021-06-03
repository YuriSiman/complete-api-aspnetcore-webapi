using CompleteApi.Business.Interfaces;
using CompleteApi.Business.Models;
using CompleteApi.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CompleteApi.Data.Repository
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(MvcDbContext context) : base(context) { }

        public async Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId)
        {
            return await Db.Enderecos.AsNoTracking().FirstOrDefaultAsync(e => e.FornecedorId == fornecedorId);
        }
    }
}
