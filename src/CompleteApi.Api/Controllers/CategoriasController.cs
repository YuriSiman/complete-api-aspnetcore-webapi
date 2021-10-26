using AutoMapper;
using CompleteApi.Api.ViewModels;
using CompleteApi.Domain.Interfaces;
using CompleteApi.Domain.Models;
using CompleteApi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompleteApi.Api.Controllers
{
    [Route("api/[controller]")]
    public class CategoriasController : MainController
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly ICategoriaService _categoriaService;
        private readonly IMapper _mapper;

        public CategoriasController(ICategoriaRepository categoriaRepository, ICategoriaService categoriaService, IMapper mapper, INotificador notificador) : base (notificador)
        {
            _categoriaRepository = categoriaRepository;
            _categoriaService = categoriaService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CategoriaViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<CategoriaViewModel>>(await _categoriaRepository.ObterTodos());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CategoriaViewModel>> ObterPorId(Guid id)
        {
            var categoriaViewModel = await ObterCategoriaProdutos(id);

            if (categoriaViewModel == null) NotFound();

            return Ok(categoriaViewModel);
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaViewModel>> Adicionar(CategoriaViewModel categoriaViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _categoriaService.Adicionar(_mapper.Map<Categoria>(categoriaViewModel));

            return CustomResponse(categoriaViewModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<CategoriaViewModel>> Atualizar(Guid id, CategoriaViewModel categoriaViewModel)
        {
            if(id != categoriaViewModel.Id)
            {
                NotificarErro("O ID informado não é o mesmo que foi passado na query");
                return CustomResponse(categoriaViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _categoriaService.Atualizar(_mapper.Map<Categoria>(categoriaViewModel));

            return CustomResponse(categoriaViewModel);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<CategoriaViewModel>> Excluir(Guid id)
        {
            var categoriaViewModel = await ObterCategoriaProdutos(id);

            if (categoriaViewModel == null) return NotFound();

            await _categoriaService.Remover(id);

            return CustomResponse(categoriaViewModel);
        }

        private async Task<CategoriaViewModel> ObterCategoriaProdutos(Guid id)
        {
            return _mapper.Map<CategoriaViewModel>(await _categoriaRepository.ObterCategoriaProdutos(id));
        }
    }
}
