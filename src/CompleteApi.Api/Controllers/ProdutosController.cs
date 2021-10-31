using AutoMapper;
using CompleteApi.Api.Extensions;
using CompleteApi.Api.ViewModels;
using CompleteApi.Domain.Interfaces;
using CompleteApi.Domain.Models;
using CompleteApi.Identity.Extensions;
using CompleteApi.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompleteApi.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ProdutosController : MainController
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;
        private readonly UploadFile _uploadFile;

        public ProdutosController(IProdutoRepository produtoRepository, IProdutoService produtoService, IMapper mapper, UploadFile uploadFile, INotificador notificador, IUser user) : base(notificador, user)
        {
            _produtoRepository = produtoRepository;
            _produtoService = produtoService;
            _mapper = mapper;
            _uploadFile = uploadFile;
        }

        [HttpGet]
        public async Task<IEnumerable<ProdutoViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterProdutosFornecedoresCategorias());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProdutoViewModel>> ObterPorId(Guid id)
        {
            var produtoViewModel = await ObterProduto(id);

            if (produtoViewModel == null) return NotFound();

            return produtoViewModel;
        }

        [ClaimsAuthorize("Produto", "Adicionar")]
        [HttpPost]
        public async Task<ActionResult<ProdutoViewModel>> Adicionar([ModelBinder(BinderType = typeof(JsonWithFilesFormDataModelBinder))] ProdutoViewModel produtoViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            // Upload da Imagem
            if (!await UploadImagem(produtoViewModel)) return CustomResponse(produtoViewModel);

            await _produtoService.Adicionar(_mapper.Map<Produto>(produtoViewModel));

            return CustomResponse(produtoViewModel);
        }

        [RequestSizeLimit(40000000)] //[DisableRequestSizeLimit]
        [HttpPost("imagem")]
        public ActionResult AdicionarImagem(IFormFile file)
        {
            return Ok(file);
        }

        [ClaimsAuthorize("Produto", "Atualizar")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Atualizar(Guid id, ProdutoViewModel produtoViewModel)
        {
            if (id != produtoViewModel.Id)
            {
                NotificarErro("Os ids informados não são iguais!");
                return CustomResponse();
            }

            var produtoAtualizado = await ObterProduto(id);

            // Update da Imagem
            if (!await UpdateImagem(produtoViewModel, produtoAtualizado)) return CustomResponse(produtoViewModel);

            await _produtoService.Atualizar(_mapper.Map<Produto>(produtoAtualizado));

            return CustomResponse(produtoViewModel);
        }

        [ClaimsAuthorize("Produto", "Excluir")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ProdutoViewModel>> Excluir(Guid id)
        {
            var produtoViewModel = await ObterProduto(id);

            if (produtoViewModel == null) return NotFound();

            await _produtoService.Remover(id);

            return CustomResponse(produtoViewModel);
        }

        private async Task<ProdutoViewModel> ObterProduto(Guid id)
        {
            return _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterProdutoFornecedorCategoria(id));
        }

        private async Task<bool> UploadImagem(ProdutoViewModel produtoViewModel)
        {
            var imagemNome = Guid.NewGuid() + "_";

            if(!await _uploadFile.UploadArquivo(produtoViewModel.ImagemUpload, imagemNome))
            {
                NotificarErro("Arquivo Inválido!");
                return false;
            }

            produtoViewModel.Imagem = imagemNome + produtoViewModel.ImagemUpload.FileName;
            return true;
        }

        private async Task<bool> UpdateImagem(ProdutoViewModel produtoViewModel, ProdutoViewModel produtoAtualizado)
        {
            if (string.IsNullOrEmpty(produtoViewModel.Imagem)) produtoViewModel.Imagem = produtoAtualizado.Imagem;

            if (!ModelState.IsValid) return false;

            if (produtoViewModel.ImagemUpload != null)
            {
                var imagemNome = Guid.NewGuid() + "_" + produtoViewModel.Imagem;
                if (!await _uploadFile.UploadArquivo(produtoViewModel.ImagemUpload, imagemNome))
                {
                    NotificarErro("Arquivo Inválido!");
                    return false;
                }

                produtoAtualizado.Imagem = imagemNome;
            }

            produtoAtualizado.FornecedorId = produtoViewModel.FornecedorId;
            produtoAtualizado.Nome = produtoViewModel.Nome;
            produtoAtualizado.Descricao = produtoViewModel.Descricao;
            produtoAtualizado.Valor = produtoViewModel.Valor;
            produtoAtualizado.Ativo = produtoViewModel.Ativo;

            return true;
        }
    }
}
