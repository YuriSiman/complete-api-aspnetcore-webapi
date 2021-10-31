using CompleteApi.Domain.Interfaces;
using CompleteApi.Domain.Models;
using CompleteApi.Service.Interfaces;
using CompleteApi.Service.Validations.Produtos;
using System;
using System.Threading.Tasks;

namespace CompleteApi.Service.Services
{
    public class ProdutoService : MainService, IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        //private readonly IUser _user;

        public ProdutoService(IProdutoRepository produtoRepository, INotificador notificador/*, IUser user*/) : base(notificador)
        {
            _produtoRepository = produtoRepository;
            //_user = user;
        }

        public async Task Adicionar(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;

            //var user = _user.GetUserId();

            await _produtoRepository.Adicionar(produto);
        }

        public async Task Atualizar(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;

            await _produtoRepository.Atualizar(produto);
        }

        public async Task Remover(Guid id)
        {
            await _produtoRepository.Remover(id);
        }

        public void Dispose()
        {
            _produtoRepository?.Dispose();
        }
    }
}
