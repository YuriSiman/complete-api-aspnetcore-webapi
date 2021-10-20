using AutoMapper;
using CompleteApi.Api.ViewModels;
using CompleteApi.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompleteApi.Api.Controllers
{
    [Route("api/[controller]")]
    public class FornecedoresController : MainController
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;

        public FornecedoresController(IFornecedorRepository fornecedorRepository, IMapper mapper)
        {
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<FornecedorViewModel>> ObterTodos()
        {
            var fornecedorViewModel = _mapper.Map<IEnumerable<FornecedorViewModel>> (await _fornecedorRepository.ObterTodos());

            return fornecedorViewModel;
        }
    }
}
